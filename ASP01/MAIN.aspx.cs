using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    /*
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["股票ConnectionString1"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("select * from [1201]", Conn);

        DataSet ds = new DataSet();
        da.Fill(ds, "[1201]");

        // DataSet寫入List
        List<string> date_list = new List<string>();
        List<Double> return_list = new List<Double>();

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            date_list.Add(row[0].ToString());
            return_list.Add(Convert.ToDouble(row[1]));
        }

        double alpha = 0.05;
        int K_day = 500;
        int Test_day = return_list.Count() - K_day;
        int Risk_order = Convert.ToInt16(alpha * K_day);
  
        List<double> Risk_DayValue = new List<Double>();

        for (int i = 0; i < Test_day; i++)
        { 
            var num = return_list.GetRange(i, K_day).OrderBy(it => it);
            Risk_DayValue.Add(
                num.Skip(Risk_order).Take(1).ToList()[0]
            );
        }

        var result = "[" + String.Join(", ", Risk_DayValue) + "]";
        Response.Write(result);
    }
    */
}