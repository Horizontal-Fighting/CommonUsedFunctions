using Gijgo.Asp.NET.Examples.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Gijgo.Asp.NET.Examples.Controllers
{
    public class LocationsController : Controller
    {
        public JsonResult Get()
        {
            List<Location> locations = new List<Models.Entities.Location>();
            //List<Models.DTO.Location> records;

            //using (ApplicationDbContext context = new ApplicationDbContext())
            //{
            //    locations = context.Locations.ToList();

            //    records = locations.Where(l => l.ParentID == null).OrderBy(l => l.OrderNumber)
            //        .Select(l => new Models.DTO.Location
            //        {
            //            id = l.ID,
            //            text = l.Name,
            //            @checked = l.Checked,
            //            population = l.Population,
            //            flagUrl = l.FlagUrl,
            //            children = GetChildren(locations, l.ID)
            //        }).ToList();

            List<Models.DTO.Location> resultNodes = new List<Models.DTO.Location>();
            //new the first level nodes
            int firstLevelNodesNum = 300;
            for (int i = 0; i < firstLevelNodesNum; i++)
            {
                var tmpLocation = new Models.DTO.Location();
                tmpLocation.@checked = false;
                tmpLocation.id = i+1;
                tmpLocation.hasChildren = true;
                tmpLocation.text = i.ToString();

                if (tmpLocation.id < 150)
                {
                    tmpLocation.children = GenerateSecondLevelChildenNodes(tmpLocation, firstLevelNodesNum, false);
                    tmpLocation.@checked = false;
                }
                else
                {
                    tmpLocation.children = GenerateSecondLevelChildenNodes(tmpLocation, firstLevelNodesNum, true);
                    tmpLocation.@checked = true;
                }                   
                resultNodes.Add(tmpLocation);
            }

            resultNodes = resultNodes.OrderByDescending(x => x.@checked==true).ToList();

            return this.Json(resultNodes, JsonRequestBehavior.AllowGet);
        }

        private List<Models.DTO.Location> GenerateSecondLevelChildenNodes(Models.DTO.Location firstLevelNode,int firstLevelNodesNum,bool selected, int childNumber = 3)
        {
            if (firstLevelNode == null)
                return null;

            List<Models.DTO.Location> locations = new List<Models.DTO.Location>();

            int idStart = firstLevelNodesNum + 1;
            for (int i = 0; i < childNumber; i++)
            {
                Models.DTO.Location node = new Models.DTO.Location();
                node.id = firstLevelNode.id + firstLevelNodesNum;
                node.text = node.id.ToString();
                node.hasChildren = true;
                node.children = GenerateThirdLevelChildenNodes(node,firstLevelNodesNum,childNumber, selected);
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
        private List<Models.DTO.Location> GenerateThirdLevelChildenNodes(Models.DTO.Location secondLevelNode, int firstLevelNodesNum,int secondLevelPerNodeNum, bool selected, int childNumber = 3)
        {
            if (secondLevelNode == null)
                return null;

            List<Models.DTO.Location> thirdLevelNodes = new List<Models.DTO.Location>();
            int idStart = firstLevelNodesNum * secondLevelPerNodeNum + 1;

            for (int i = 0; i < childNumber; i++)
            {
                Models.DTO.Location tmpLocation = new Models.DTO.Location();
                tmpLocation.id = idStart + i;
                tmpLocation.text = tmpLocation.id.ToString();
                tmpLocation.@checked = selected;
                tmpLocation.children = null;
                tmpLocation.hasChildren = false;
                thirdLevelNodes.Add(tmpLocation);
            }
            return thirdLevelNodes;
        }

        public JsonResult LazyGet(int? parentId)
        {
            List<Location> locations;
            List<Models.DTO.Location> records;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                locations = context.Locations.ToList();

                records = locations.Where(l => l.ParentID == parentId).OrderBy(l => l.OrderNumber)
                    .Select(l => new Models.DTO.Location
                    {
                        id = l.ID,
                        text = l.Name,
                        @checked = l.Checked,
                        population = l.Population,
                        flagUrl = l.FlagUrl,
                        hasChildren = locations.Any(l2 => l2.ParentID == l.ID)
                    }).ToList();
            }

            return this.Json(records, JsonRequestBehavior.AllowGet);
        }

        private List<Models.DTO.Location> GetChildren(List<Location> locations, int parentId)
        {
            return locations.Where(l => l.ParentID == parentId).OrderBy(l => l.OrderNumber)
                .Select(l => new Models.DTO.Location
                {
                    id = l.ID,
                    text = l.Name,
                    population = l.Population,
                    flagUrl = l.FlagUrl,
                    @checked = l.Checked,
                    children = GetChildren(locations, l.ID)
                }).ToList();
        }

        [HttpPost]
        public JsonResult SaveCheckedNodes(List<int> checkedIds)
        {
            if (checkedIds != null)
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var locations = context.Locations.ToList();
                    foreach (var location in locations)
                    {
                        location.Checked = checkedIds.Contains(location.ID);
                    }
                    context.SaveChanges();
                }
            }

            return this.Json(true);
        }

        [HttpPost]
        public JsonResult ChangeNodePosition(int id, int parentId, int orderNumber)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var location = context.Locations.First(l => l.ID == id);

                var newSiblingsBelow = context.Locations.Where(l => l.ParentID == parentId && l.OrderNumber >= orderNumber);
                foreach (var sibling in newSiblingsBelow)
                {
                    sibling.OrderNumber = sibling.OrderNumber + 1;
                }

                var oldSiblingsBelow = context.Locations.Where(l => l.ParentID == location.ParentID && l.OrderNumber > location.OrderNumber);
                foreach (var sibling in oldSiblingsBelow)
                {
                    sibling.OrderNumber = sibling.OrderNumber - 1;
                }


                location.ParentID = parentId;
                location.OrderNumber = orderNumber;

                context.SaveChanges();
            }

            return this.Json(true);
        }
    }
}