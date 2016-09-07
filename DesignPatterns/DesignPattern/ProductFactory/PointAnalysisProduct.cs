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
    public class PointAnalysisProduct : Product
    {
        string administrativeLevelString, areaName;

        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }

        public string AdministrativeLevelString
        {
            get { return administrativeLevelString; }
            set { administrativeLevelString = value; }
        }
        public PointAnalysisProduct()
        {
            base.Name = "PointAnalysis";
        }
    }
}
