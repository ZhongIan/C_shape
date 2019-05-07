using System;
using System.Collections.Generic;
// ConfigurationManager
using System.Configuration;
// DataSet
using System.Data;
// SqlConnection
using System.Data.SqlClient;
using System.Linq;
using System.Web;
// JavaScriptSerializer
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        
        string stock = Request.Form["stock"];
        double alpha = Convert.ToDouble(Request.Form["alpha"]);
        string sigma = Request.Form["sigma"];
        string method_fun = Request.Form["method"];

        
        var responseEntities = new List<Method>()
        {
            new Method(stock_name:stock,alpha:alpha, method_fun:method_fun)
                
        };
        var result = serializer.Serialize(responseEntities);
        Response.Write(result);
        
        Response.End();
    }
}

/*
// json 輸出格式
public class Risk_Value
{
    public string[] date { get; set; }
    public double[] Rt { get; set; }
    public double[] Rv { get; set; }
    public double[] RCv { get; set; }
}*/

/*
// 序列化
 var responseEntities = new List<DataFromSql>()
{
    new DataFromSql("1201")
    {
        date = fromSql.Date_list.GetRange(K_day,Test_day).ToArray(),
        Rt = fromSql.Return_list.GetRange(K_day,Test_day).ToArray(),
        Rv = Risk_DayValue.ToArray(),
        RCv = ConditionRisk_DayValue.ToArray()
    }
};
var result = serializer.Serialize(responseEntities);
Response.Write(result);
 */


