using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SalesDataAnalyzer
{
    class WriteFile
    {
        public static void Write(List<string> queryOutputs, string reportFilePath)
        {
            string salesReport = "Sales Data Report\n" +
                                 "-----------------\n\n";
                                
            foreach (string s in queryOutputs)
            {
                salesReport += s;
            }

            File.WriteAllText(reportFilePath, salesReport);
        }

        public static List<string> CollectTasks(List<Task> completedTasks)
        {
            List<string> results = new List<string>();
            foreach (Task<string> t in completedTasks)
            {
                results.Add(t.Result);
            }
            return results;
        }
    }
}