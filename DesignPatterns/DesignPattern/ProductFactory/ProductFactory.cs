/* Created by:Rong Fan
 * email:rong.fan1031@gmail.com
 * Desc: sigleton design pattern
 * Dt: 2016-9-6
 * Version:.NET 2.0
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public class ProductFactory
    {
        static Product p;
        public static Product CreateProduct(string productName)
        {
            if (productName.Equals("PointAnalysis"))
                p = new PointAnalysisProduct();
            else
                throw new ArgumentOutOfRangeException("product name not found");
            return p;
        }
    }
}
