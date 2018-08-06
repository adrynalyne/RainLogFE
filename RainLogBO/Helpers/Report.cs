using RainLogBO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainLogBO.Helpers
{
    public class Report
    {
        public void CreateReport(Result result)
        {
            //Get html template
        }

        public void CreateCsv(string name, List<GaugeResult> results)
        {
            var dir = Environment.CurrentDirectory + "\\Reports\\";
            var csvStr = String.Join(Environment.NewLine, results.Select(r => r.GaugeId + "," + r.TotalRain + ","));
            if (!Directory.Exists(dir))  
                Directory.CreateDirectory(dir);
            File.WriteAllText(Path.Combine(dir, name + ".txt"), csvStr);
        }
    }
}
