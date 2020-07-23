using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LearnCode.Client.ViewModels
{
    public class CountryService
    {
        public IEnumerable<CountryViewItem> GetCountries(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string filePath = @"Data\countries.json";

            using (StreamReader r = new StreamReader(Path.Combine(projectDirectory, filePath)))
            {
                string json = r.ReadToEnd();
                var data = JsonConvert.DeserializeObject<IEnumerable<CountryViewItem>>(json);             
                return data.Skip(pageIndex).Take(pageSize);
            }
        }
    }
}
