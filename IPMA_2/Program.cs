using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace IPMA_2
{
    class Program
    {
        static void Main(string[] args)
        {
            const string folderPath = @"..\..\..\..\input\";
            const string folderOutput = @"..\..\..\..\output_2\";
            const string localFilePath = @"..\..\..\..\input\distrits-islands.json";

            var matches = GetLocalsMatchCollection(localFilePath);

            Parallel.ForEach(matches, match =>
            {
                try
                {
                    var fileContent = File.ReadAllText(folderPath + match.Groups[1].Value + ".json");

                    var city = JsonConvert.DeserializeObject<City>(fileContent);
                    city.AddLocal(match.Groups[2].Value);

                    var jsonWrite = new Thread(() => JsonWrite(city, folderOutput, match));
                    jsonWrite.Start();

                    var xmlWrite = new Thread(() => XMLWrite(city, folderOutput, match));
                    xmlWrite.Start();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                    throw;
                }
            });

            //Console.ReadKey();
        }

        /// <summary>
        /// Gets Match Collection of all Locals and IDs
        /// </summary>
        /// <param name="localFilePath">Path for the Locals File</param>
        /// <returns>Match Collection of City Names and IDs</returns>
        private static MatchCollection GetLocalsMatchCollection(string localFilePath)
        {
            const string regex =
                "\"globalIdLocal\":\\s(\\d{7}),\\s\"latitude\":\\s\"[\\d*\\.?]*\",\\s\"idDistrito\":\\s\\d*,\\s\"local\":\\s\"([^,]*)\"";

            try
            {
                var fileContent = File.ReadAllText(localFilePath);
                return Regex.Matches(fileContent, regex);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                throw;
            }

        }

        private static void JsonWrite(City city, string folderOutput, Match match)
        {
            try
            {
                using (var file = File.CreateText(folderOutput + match.Groups[1].Value + "-detalhe.json"))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, city);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                throw;
            }
            //Console.WriteLine("Done writing file " + match.Groups[1].Value + "-detalhe.json!\n");
        }

        private static void XMLWrite(City city, string folderOutput, Match match)
        {
            try
            {
                using (var file = File.Create(folderOutput + match.Groups[1].Value + "-detalhe.xml"))
                {
                    var serializer = new XmlSerializer(city.GetType());
                    serializer.Serialize(file, city);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                throw;
            }
            //Console.WriteLine("Done writing file " + match.Groups[1].Value + "-detalhe.xml!\n");
        }
    }
}
