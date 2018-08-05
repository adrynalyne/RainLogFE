using Newtonsoft.Json;
using RainLogBO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RainLogBO
{
    /// <summary>
    /// Pull in Json data for lists
    /// </summary>
    public class SelectionData
    {
        /// <summary>
        /// Get json data by file path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ObservableCollection<Selection> GetData(string fileName)
        {
            var jsonData = new ObservableCollection<Selection>();
            var dataLocation = Environment.CurrentDirectory + "\\Data\\" +  fileName;
            using (StreamReader reader = new StreamReader(dataLocation))
            {
                var jsonStr = reader.ReadToEnd();

                jsonData = JsonConvert.DeserializeObject<ObservableCollection<Selection>>(jsonStr);
            }
            return jsonData;
        }
    }
}
