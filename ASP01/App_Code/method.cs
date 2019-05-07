using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// method 的摘要描述
/// </summary>
public class Method
{
    //透過 DataFromSql 向 MSSQL 撈資料
    private DataFromSql fromSql;

    // 必要參數
    private string stock;
    private double alpha;
    private string sigma_method;

    // K_day回算天數, Test_day運算筆數
    private int K_day;
    private int Test_day;
    
    // 屬性 透過JSON.asp序列化傳給前端
    public string[] Date { get; set; }
    public double[] Rt { get; set; }
    public double[] Rv { get; set; }
    public double[] RCv { get; set; }

    public Method(
        string stock_name="1201", 
        double alpha=0.05,
        // 讓需要 sigma 的函數 Cm(),Mote() 判斷
        string sigma_method ="簡單(Sample)",  
        string method_fun= "Cm"
        )
    {
        this.stock = stock_name;
        this.fromSql = new DataFromSql(this.stock);

        this.alpha = alpha;
        this.sigma_method = sigma_method;

        this.K_day = 500;
        this.Test_day = this.fromSql.Return_list.Count() - this.K_day;
        

        // 預測目標 K_day開始(500~737) 共Test_day(238)筆
        this.Date = fromSql.Date_list.GetRange(K_day, Test_day).ToArray();
        this.Rt = fromSql.Return_list.GetRange(K_day, Test_day).ToArray();

        switch (method_fun)
        {
            case "歷史模擬法":
                this.Hist();
                break;
            case "變異數共變異數法":
                this.Cm();
                break;
            case "蒙地卡羅模擬法":
                this.Mote();
                break;
        }
    }

    private void Hist()
    {

        int Risk_order = Convert.ToInt16(this.alpha * this.K_day);

        List<double> Risk_DayValue = new List<Double>();
        // 預測 (499~736) 共Test_day(238)筆
        //for (int i = 0; i < this.Test_day; i++)
        //{
        //    var num = this.fromSql.Return_list.GetRange(i, this.K_day).OrderBy(it => it);
        //    Risk_DayValue.Add(num.Skip(Risk_order).Take(1).ToList()[0]);
        //}

        List<double> ConditionRisk_DayValue = new List<Double>();

        for (int i = 0; i < this.Test_day; i++)
        {
            var num = this.fromSql.Return_list.GetRange(i, this.K_day).OrderBy(it => it);
            Risk_DayValue.Add(num.Skip(Risk_order).Take(1).ToList()[0]);
            ConditionRisk_DayValue.Add(num.Take(Risk_order).Average());

        }

        this.Rv = Risk_DayValue.ToArray();
        this.RCv = ConditionRisk_DayValue.ToArray();
    }

    private List<double> Sigma(int t = 0)
    {
        // t = 0 OR 1
        // t = 1 Weight_Sigma 需要前一筆 Sigma
        List<double> sigma = new List<double>();
        double mean;
        double temp;
        for (int i = 0; i < this.Test_day; i++)
        {
            var num = this.fromSql.Return_list.GetRange(i, this.K_day - t);
            mean = num.Average();
            temp = 0;

            foreach (var item in num)
            {
                temp += Math.Pow((item - mean), 2);
            }
            
            sigma.Add(
                Math.Sqrt((temp / (num.Count() - 1)))
            );
        }
        return sigma;
    }

    private List<double> Weight_Sigma()
    {
        List<double> weight_sigma = new List<double>();

        // Sigma(1) 498~735 共Test_day(238)筆
        List<double> sigma = Sigma(1);

        // 499~736 共Test_day(238)筆
        var num = this.fromSql.Return_list.GetRange(this.K_day - 1, this.Test_day);

        for (int i = 0; i < this.Test_day; i++)
        {
            weight_sigma.Add(
                Math.Sqrt(
                    0.94 * Math.Pow(sigma[i],2) + (1-0.94) * Math.Pow(num[i], 2)
                )
            );
        }

        return weight_sigma;
    }

    private void Cm()
    {
        double z;

        switch (this.alpha)
        {
            case 0.05:
                z = 1.645;
                break;
            case 0.01:
                z = 2.33;
                break;
            default:
                z = 1.645;
                break;
        }
        // 不同simga
        List<double> sigma;
        switch (this.sigma_method)
        {
            case "簡單(Sample)":
                sigma = this.Sigma();
                break;
            case "加權(Weight)":
                sigma = this.Weight_Sigma();
                break;
            default:
                sigma = this.Sigma();
                break;
        }

        List<double> Risk_DayValue = new List<Double>();

        //for (int i = 0; i < this.Test_day; i++)
        //{
        //    Risk_DayValue.Add(
        //        -z* sigma[i]
        //    );
        //}

        List<double> ConditionRisk_DayValue = new List<Double>();

        for (int i = 0; i < this.Test_day; i++)
        {
            Risk_DayValue.Add(
                -z * sigma[i]
            );

            ConditionRisk_DayValue.Add(
                (1/this.alpha) * (
                    (-sigma[i]/(2*Math.PI)) * Math.Exp(
                        -Math.Pow(Risk_DayValue[i],2)/(2*Math.Pow(sigma[i],2))
                    )
                )
            );
        }

        this.Rv = Risk_DayValue.ToArray();
        this.RCv = ConditionRisk_DayValue.ToArray();
    }

    private void Mote()
    {
        double t = 10; // 分成t期
        double N = 1000; // N條不同路徑(隨機數)

        List<double> sigma;
        switch (this.sigma_method)
        {
            case "簡單(Sample)":
                sigma = this.Sigma();
                break;
            case "加權(Weight)":
                sigma = this.Weight_Sigma();
                break;
            default:
                sigma = this.Sigma();
                break;
        }


        double mean;
        // 暫存
        double temp;
        // N條不同路徑 陣列
        double[] er = new double[Convert.ToInt16(N)];

        // 常態隨機數計算用
        Random rands;
        double z, u, v;

        List<double> Risk_DayValue = new List<Double>();

        List<double> ConditionRisk_DayValue = new List<Double>();

        for (int i = 0; i < this.Test_day; i++)
        {
            var num = this.fromSql.Return_list.GetRange(i, this.K_day);
            mean = num.Average();
            

            for (int j = 0; j < N; j++)
            {
                temp = 0;
                for (int k = 0; k < t; k++)
                {
                    // 隨機數 常態分配 生成方式
                    // https://zh.wikipedia.org/wiki/%E6%AD%A3%E6%80%81%E5%88%86%E5%B8%83
                    // 英文
                    // https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
                    rands = new Random((i*j*k)+i+j+k); // 用seed來生成不同隨機數
                    u = rands.NextDouble();
                    v = rands.NextDouble();
                    z = Math.Sqrt(-2 * Math.Log(u)) * Math.Cos(2 * Math.PI * v);             
                    temp += (mean-Math.Pow(sigma[i],2)/2)*(1/t) + sigma[i]*(Math.Sqrt((1/t))* z);                    
                }
                er[j] = temp;
            }

            Risk_DayValue.Add(
                er.OrderBy(it => it).Skip(Convert.ToInt16(this.alpha * N)).Take(1).ToArray()[0]
            );

            ConditionRisk_DayValue.Add(
                er.OrderBy(it => it).Take(Convert.ToInt16(this.alpha * N)).Average()
            );
        }
        this.Rv = Risk_DayValue.ToArray();
        this.RCv = ConditionRisk_DayValue.ToArray();

    }
}
/*
//https://blog.xxwhite.com/2017/RandomNum1.html
// 常態分配
public class Rand
{
    private double z;

    public double Z
    {
        get { return z;}
    }
    

    public  Rand()
    {
        // https://zh.wikipedia.org/wiki/%E6%AD%A3%E6%80%81%E5%88%86%E5%B8%83
        // 英文
        // https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
        Random rand = new Random();
        double s = 0, u = 0, v = 0;
        while (s > 1 || s == 0)
        {
            u = rand.NextDouble() * 2 - 1;
            v = rand.NextDouble() * 2 - 1;

            s = u * u + v * v;
        }

        this.z = Math.Sqrt(-2 * Math.Log(s) / s) * u;
        
    }
}
*/

/*
    // https://blog.xxwhite.com/2017/RandomNum1.html
    // 常態分布 N(0,1)
    public double Normal()
    {
        Random rand = new Random();

        double s = 0, u = 0, v = 0;
        while (s > 1 || s == 0)
        {
            u = rand.NextDouble() * 2 - 1;
            v = rand.NextDouble() * 2 - 1;

            s = u * u + v * v;
        }

        var z = Math.Sqrt(-2 * Math.Log(s) / s) * u;
        return (z);
    }

    // N(mean,sigma)
    public double RandomNormal(double mean, double sigma)
    {
        var z = Normal() * sigma + mean;
        return (z);
    }
    */
