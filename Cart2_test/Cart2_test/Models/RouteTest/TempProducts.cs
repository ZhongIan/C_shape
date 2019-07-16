using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart2_test.Models.RouteTest
{
    public class TempProducts
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // 取得所有商品
        public static List<TempProducts> GetAllProducts()
        {
            List<TempProducts> result = new List<Cart2_test.Models.RouteTest.TempProducts>();

            result.Add(new Cart2_test.Models.RouteTest.TempProducts
            {
                ID = 1,
                Name = "自動鉛筆",
                Price = 30.0m
            });

            result.Add(new Cart2_test.Models.RouteTest.TempProducts
            {
                ID = 2,
                Name = "記事本",
                Price = 50.0m
            });

            result.Add(new Cart2_test.Models.RouteTest.TempProducts
            {
                ID = 3,
                Name = "橡皮擦",
                Price = 10.0m
            });

            return result;
        }
    }
}