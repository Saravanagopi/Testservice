using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{
    public class TestOneController : ApiController
    {
        static void TaskCancel()
        {
            Console.WriteLine("Task Cancelled!!");
        }

        List<Vehicle> emp = new List<Vehicle>()
        {
            new Vehicle { Vid = 1, Name = "Benz"},
            new Vehicle { Vid = 2, Name = "BMW"},
        };

        //public List<Vehicle> GetVehicle()
        //{
        //    var CancelTokensrc = new CancellationTokenSource();
        //    var cancelToken = CancelTokensrc.Token;
        //    cancelToken.Register(() => TaskCancel());

        //    var t = Task.Factory.StartNew(() => TestMethod(cancelToken), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        //    return emp;

        //}
        public string GetVehicle()
        {
            var CancelTokensrc = new CancellationTokenSource();
            var cancelToken = CancelTokensrc.Token;
            cancelToken.Register(() => TaskCancel());

            var t = Task.Factory.StartNew(() => TestMethod(cancelToken), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            return "Your request was under process please wait...";
        }
        static async void TestMethod(CancellationToken cancel)
        {
            for (int i = 0; i < 10000000; i++)
            {
                if (cancel.IsCancellationRequested)
                {
                    break;
                }
                string lines = i.ToString() + " - " + DateTime.Now; 
                System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\Sample/Process.txt");
                file.WriteLine(lines);
                file.Close();
            }
        }



    }
}
