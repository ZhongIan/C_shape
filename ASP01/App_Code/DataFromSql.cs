using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// DataFromSql 的摘要描述
/// </summary>
public class DataFromSql
{
    private List<string> date_list= new List<string>();
    private List<double> return_list= new List<double>();

    public List<string> Date_list
    {
        get { return date_list; }
    }

    public List<double> Return_list
    {
        get { return return_list; }
    }

    public DataFromSql(string tablename)
    {
        // tablename = 1201
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["股票ConnectionString1"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter($"exec dbo.風險值運算資料 @s_name='{tablename}'", Conn);
        // dbo.風險值運算資料 預存程序
        /*
        create proc 風險值運算資料
            @s_name varchar(12) = '1201'
        as
        begin

            exec('select [date], [return] from' + '[' + @s_name + ']')

        end
        */
        DataSet ds = new DataSet();
        da.Fill(ds, $"[{tablename}]");
        // DataSet寫入List
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            this.date_list.Add(
                // js 時間格式 2014-9-15
                row[0].ToString().Replace("/", "-")
            );
            this.return_list.Add(
                Convert.ToDouble(row[1])
            );
        }

        /* // DataSet寫入array
        double[] ds_array = new double[ds.Tables[0].Rows.Count];

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ds_array[i] = Convert.ToDouble(ds.Tables[0].Rows[i][0].ToString());
        }
        */

        /* // SqlDataReader
        SqlCommand Cmd = new SqlCommand("select [return] from [1201]", Conn);

        SqlDataReader rd;

        Conn.Open();
        rd = Cmd.ExecuteReader();

        rd["return"]

        while (rd.Read())
        {
            Response.Write(rd["return"] + "<br>");
        }

        Conn.Close();
        */
    }
}