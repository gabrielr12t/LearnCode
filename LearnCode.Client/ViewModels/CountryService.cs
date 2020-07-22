using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LearnCode.Client.ViewModels
{
    public class CountryService
    {
        public IEnumerable<CountryViewItem> GetCountries()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string filePath = @"Data\countries.json";

            using (StreamReader r = new StreamReader(Path.Combine(projectDirectory, filePath)))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<CountryViewItem>>(json);
            }
        }
    }
}
