using MyProject.Tests.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Tests.Tests.SampleData
{
    public class ProductListFromJSON : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDir = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var jsonPath = Path.Combine(projectDir, "Tests\\SampleData\\OrderProductsData.json");
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException("JSON file not found! File at " + jsonPath.ToString());
            var productsText = File.ReadAllText(jsonPath);
            var products = JsonConvert.DeserializeObject<Product[]>(productsText);

            foreach (var product in products)
            {
                yield return new object[] { product };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
