/*Created by:Rong Fan
 *email:rong.fan1031@gmail.com
 *Desc: using Parallel programming to optimize performance
 *Desc: 
 *Dt: 2016-9-30
 *Version:.NET 4.5
 */



using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using IO.Swagger.Model;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Answer answer = new Answer();
            ConcurrentBag<DealerAnswer> dealersAnswer = new ConcurrentBag<DealerAnswer>();
            ConcurrentBag<string> dealerIDs = new ConcurrentBag<string>();
            ConcurrentBag<VehicleAnswer> vehiclesAnswer = new ConcurrentBag<VehicleAnswer>();
            ConcurrentBag<MyVehicleResponse> vehiclesResponse = new ConcurrentBag<MyVehicleResponse>();

            //1 dataSetIdString
            string dataSetIdJsonStr = Global.Get("/api/datasetId");
            JObject results = JObject.Parse(dataSetIdJsonStr);
            JToken dataSetIdStr = results["datasetId"];
            string dataSetIdString = dataSetIdStr.ToString();
            Console.WriteLine("dataSetIdStr:"+ dataSetIdStr.ToString());


            //2 vehiclesAnswer
            string vehiclesJsonStr = Global.Get("/api/" + dataSetIdStr.ToString() + "/vehicles");
            results = JObject.Parse(vehiclesJsonStr);
            JToken vehicles = results["vehicleIds"];
            Parallel.For(0, vehicles.Count(), i =>
            {
                string vehicleId = vehicles.ToList()[i].ToString();
                string tmpVehicleJsonStr = GetVehicle(dataSetIdString, vehicleId);
                JObject resultsTmp = JObject.Parse(tmpVehicleJsonStr);
                string dealerID = resultsTmp["dealerId"].ToString();
                dealerIDs.Add(dealerID);
                VehicleAnswer tmpVehicle = GetVehicleAnswerFromJsonStr(tmpVehicleJsonStr);    
                vehiclesAnswer.Add(tmpVehicle);

                MyVehicleResponse tmpVehicleResponse = GetVehicleResponseFromJsonStr(tmpVehicleJsonStr);
                vehiclesResponse.Add(tmpVehicleResponse);
            });



            //3 dealersAnswer and dealersAnswerJson
            var dealerIds = dealerIDs.Distinct().ToList();
            Parallel.For(0, dealerIds.Count(), i =>
            {
                string tmpDealerJsonStr=GetDealer(dataSetIdString, dealerIds[i]);
                JObject resultsTmp = JObject.Parse(tmpDealerJsonStr);            
                DealerAnswer dealerAnswer = GetDealerAnswerFromJsonStr(tmpDealerJsonStr, vehiclesResponse);
                dealersAnswer.Add(dealerAnswer);
            });
            answer.Dealers = new List<DealerAnswer>();
            answer.Dealers.AddRange(dealersAnswer);
            string answerJsonStr = JsonConvert.SerializeObject(answer);
            Console.WriteLine(answerJsonStr);



            //4 Post dealersAnswerJson
            string url = "/api/"+dataSetIdString+"/answer";
            string jsonContent = answerJsonStr;
            string postResultStr = Global.Post(url, jsonContent);
            Console.WriteLine("Post result:" + postResultStr);


            sw.Stop();
            Console.WriteLine("Time Eclipsed:" + Math.Round(sw.Elapsed.TotalSeconds, 2) + " Seconds");
            Console.ReadKey();

        }

        private static string GetVehicle(string dataSetId, string vehicleId)
        {
            string vehicleJsonStr = Global.Get("/api/" + dataSetId + "/vehicles/" + vehicleId);
            return vehicleJsonStr;
        }

 

        private static  string GetDealer(string dataSetId, string dealerid)
        {
            string dealerJsonStr = Global.Get("/api/" + dataSetId + "/dealers/" + dealerid);
            return dealerJsonStr;
        }


        private static VehicleAnswer GetVehicleAnswerFromJsonStr(string jsonStr)
        {
            JObject resultsTmp = JObject.Parse(jsonStr);
            VehicleAnswer result = new VehicleAnswer();
            result.VehicleId = Convert.ToInt32(resultsTmp["vehicleId"]);
            result.Year = Convert.ToInt32(resultsTmp["year"]);
            result.Make = Convert.ToString(resultsTmp["make"]);
            result.Model = Convert.ToString(resultsTmp["model"]);
            return result;
        }

        private static MyVehicleResponse GetVehicleResponseFromJsonStr(string jsonStr)
        {
            JObject resultsTmp = JObject.Parse(jsonStr);
            MyVehicleResponse result = new MyVehicleResponse();
            result.VehicleId = Convert.ToInt32(resultsTmp["vehicleId"]);
            result.Year = Convert.ToInt32(resultsTmp["year"]);
            result.Make = Convert.ToString(resultsTmp["make"]);
            result.Model = Convert.ToString(resultsTmp["model"]);
            result.DealerId = Convert.ToInt32(resultsTmp["dealerId"]);
            return result;
        }

        private static DealerAnswer GetDealerAnswerFromJsonStr(string dealerStr, IEnumerable<MyVehicleResponse> vehicles)
        {
            JObject resultsTmp = JObject.Parse(dealerStr);
            DealerAnswer result = new DealerAnswer();
            result.DealerId = Convert.ToInt32(resultsTmp["dealerId"]);
            result.Name = Convert.ToString(resultsTmp["name"]);

            var query = vehicles
                .Where(r => r.DealerId == result.DealerId)
                .Select(r => r)
                .ToList();
            result.Vehicles = new List<VehicleAnswer>();
            result.Vehicles.AddRange(query);
            return result;
        }

    }

    
}
