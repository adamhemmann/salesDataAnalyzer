using System;
using System.Reflection;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SalesDataAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string inputFilePath = args[0];
            string reportFilePath = args[1];

            List<SalesData> salesList = null;
            try
            {
                salesList = DataLoader.Load(inputFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(2);
            }

            List<Task> queriesList = new List<Task>();

            var q1 = Task.Run(() => Queries.Query1(salesList));
            queriesList.Add(q1);            
            var q2 = Task.Run(() => Queries.Query2(salesList));
            queriesList.Add(q2);
            var q3 = Task.Run(() => Queries.Query3(salesList));
            queriesList.Add(q3);
            var q4 = Task.Run(() => Queries.Query4(salesList));
            queriesList.Add(q4);
            var q5 = Task.Run(() => Queries.Query5(salesList));
            queriesList.Add(q5);
            var q6 = Task.Run(() => Queries.Query6(salesList));
            queriesList.Add(q6);
            var q7 = Task.Run(() => Queries.Query7(salesList));
            queriesList.Add(q7);
            var q8 = Task.Run(() => Queries.Query8(salesList));
            queriesList.Add(q8);
            var q9 = Task.Run(() => Queries.Query9(salesList));
            queriesList.Add(q9);
            var q10 = Task.Run(() => Queries.Query10(salesList));
            queriesList.Add(q10);
            var q11 = Task.Run(() => Queries.Query11(salesList));
            queriesList.Add(q11);
            var q12 = Task.Run(() => Queries.Query12(salesList));
            queriesList.Add(q12);

            Task.WaitAll(queriesList.ToArray());

            WriteFile.Write(WriteFile.CollectTasks(queriesList), reportFilePath);
            
            sw.Stop();
            Console.WriteLine("Elapsed Time is {0} ms", sw.ElapsedMilliseconds);
        }
    }
}
