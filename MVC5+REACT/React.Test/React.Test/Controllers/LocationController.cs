using React.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace React.Test.Controllers
{
    public class LocationController : Controller
    {
        public JsonResult Get()
        {
            List<Location> locations = new List<Location>();

            List<Location> resultNodes = new List<Location>();
            //new the first level nodes
            int firstLevelNodesNum = 10;
            for (int i = 0; i < firstLevelNodesNum; i++)
            {
                var tmpLocation = new Location();
                tmpLocation.@checked = false;
                tmpLocation.id = i + 1;
                tmpLocation.hasChildren = true;
                tmpLocation.text = i.ToString();

                //if (tmpLocation.id < 150)
                //{
                //    tmpLocation.children = GenerateSecondLevelChildenNodes(tmpLocation, firstLevelNodesNum, false);
                //    tmpLocation.@checked = false;
                //}
                //else
                //{
                //    tmpLocation.children = GenerateSecondLevelChildenNodes(tmpLocation, firstLevelNodesNum, true);
                //    tmpLocation.@checked = true;
                //}
                resultNodes.Add(tmpLocation);
            }

            //resultNodes = resultNodes.OrderByDescending(x => x.@checked == true).ToList();

            return this.Json(resultNodes, JsonRequestBehavior.AllowGet);
        }

        private List<Location> GenerateSecondLevelChildenNodes(Location firstLevelNode, int firstLevelNodesNum, bool selected, int childNumber = 3)
        {
            if (firstLevelNode == null)
                return null;

            List<Location> locations = new List<Location>();

            int idStart = firstLevelNodesNum + 1;
            for (int i = 0; i < childNumber; i++)
            {
                Location node = new Location();
                node.id = firstLevelNode.id + firstLevelNodesNum;
                node.text = node.id.ToString();
                node.hasChildren = true;
                node.children = GenerateThirdLevelChildenNodes(node, firstLevelNodesNum, childNumber, selected);
                locations.Add(node);
            }

            return locations;
        }

        /// <summary>
        /// generate the third level nodes, default nodes number is 3;
        /// </summary>
        /// <param name="secondLevelNode"></param>
        /// <param name="childNumber"></param>
        /// <returns></returns>
        private List<Location> GenerateThirdLevelChildenNodes(Location secondLevelNode, int firstLevelNodesNum, int secondLevelPerNodeNum, bool selected, int childNumber = 3)
        {
            if (secondLevelNode == null)
                return null;

            List<Location> thirdLevelNodes = new List<Location>();
            int idStart = firstLevelNodesNum * secondLevelPerNodeNum + 1;

            for (int i = 0; i < childNumber; i++)
            {
                Location tmpLocation = new Location();
                tmpLocation.id = idStart + i;
                tmpLocation.text = tmpLocation.id.ToString();
                tmpLocation.@checked = selected;
                tmpLocation.children = null;
                tmpLocation.hasChildren = false;
                thirdLevelNodes.Add(tmpLocation);
            }
            return thirdLevelNodes;
        }

    }
}