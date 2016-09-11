using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfhostSample
{
    class Program
    {
        static void Main(string[] args)
        {


            while (Common.IsPortOccupied(Common.Port))
            {
                Console.Write("Port:"+Common.Port+"occupied. Trying next.");
                Common.Port++;
            }

            // Start OWIN host 
            WebApp.Start<Startup>(url: Common.BaseAddress);
            Console.Write("Working on address:" + Common.BaseAddress + "\r\n\r\n\r\n");
            


            // Create HttpCient and make a request to api/products 
            HttpClient client = new HttpClient();
            var response = client.GetAsync(Common.BaseAddress + "api/products").Result;
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);


            Console.ReadLine(); 
        }
    }
}


//var response1 = client.GetAsync(baseAddress + "api/products/1").Result;
//Console.WriteLine(response1);
//Console.WriteLine(response1.Content.ReadAsStringAsync().Result);

//var response2 = client.GetAsync(baseAddress + "api/products/?category=Toys").Result;
//Console.WriteLine(response2);
//Console.WriteLine(response2.Content.ReadAsStringAsync().Result);


////update
//var o = new Product
//{
//    Id = 3,
//    Name = "名字",
//    Category = "分类",
//    Price = 67.3M
//};
//var responseN = client.PutAsJsonAsync(baseAddress + "api/products/1", o).Result;
//Console.WriteLine(responseN);
//Console.WriteLine(responseN.Content.ReadAsStringAsync().Result);


////insert
//var n = new Product
//{
//    Id = 4,
//    Name = "名字",
//    Category = "分类",
//    Price = 67.3M
//};
//var responseI = client.PostAsJsonAsync(baseAddress + "api/products", n).Result;
//Console.WriteLine(responseI);
//Console.WriteLine(responseI.Content.ReadAsStringAsync().Result);
