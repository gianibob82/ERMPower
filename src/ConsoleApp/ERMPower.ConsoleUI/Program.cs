using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ERMPower.ConsoleUI
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Query api");

            try
            {
                Uri apiUrl = new Uri(args.Length > 0 ? args[0] : "http://localhost:55051");
                byte percvalue = byte.Parse(args.Length > 1 ? args[1] : "20");

                Console.WriteLine($"Connecting to API {apiUrl}");
                Console.WriteLine($"Query readings diverging by {percvalue}%");

                RunAsync(apiUrl, percvalue).GetAwaiter().GetResult();
                Console.WriteLine("Finished!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
            }

            Console.ReadKey();
        }

        static async Task RunAsync(Uri apiUrl, byte percvalue)
        {
            client.BaseAddress = apiUrl;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"api/energyreading?percvalue={percvalue}");

            if (response.IsSuccessStatusCode)
            {
                var serializedResponse = await response.Content.ReadAsStringAsync();

                var deserializedResponse = JsonConvert.DeserializeObject<IEnumerable<EnergyReadingGroup>>(serializedResponse);

                int totalItems = 0;

                foreach (var g in deserializedResponse)
                {
                    totalItems += g.readings.Count();

                    foreach (var r in g.readings)
                        Console.WriteLine($"{g.name} {r.date} {r.value} {g.medianvalue}");
                }

                Console.WriteLine($"Found {totalItems} readings");
            }
            else {
                var reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error:{response.StatusCode} {reason}");
            }
        }
    }

    public class EnergyReadingGroup
    {
        public EnergyReadingGroup()
        {
            readings = new List<EnergyReading>();
        }
        public string name { get; set; }
        public decimal medianvalue { get; set; }

        public IEnumerable<EnergyReading> readings { get; set; }
    }

    public class EnergyReading
    {
        public DateTime date { get; set; }
        public decimal value { get; set; }
    }
}
