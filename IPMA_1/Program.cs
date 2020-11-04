using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IPMA_1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string folderPath = @"..\..\..\..\input\";
            const string folderOutput = @"..\..\..\..\output\";
            const string localFilePath = @"..\..\..\..\input\distrits-islands.json";

            var matches = GetLocalsMatchCollection(localFilePath);
            //Console.WriteLine(matches.Count);

            Parallel.ForEach(matches, match => {
                try
                {

                    var fileContent = File.ReadAllText(folderPath + match.Groups[1].Value + ".json");

                    fileContent = fileContent.Remove(fileContent.Length - 1);

                    fileContent = fileContent + ", \"local\": \"" + match.Groups[2].Value + "\"}";

                    File.WriteAllText(folderOutput + match.Groups[1].Value + "-detalhe.json", fileContent);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                    throw;
                }

                Console.WriteLine("Done writing file " + match.Groups[1].Value + "-detalhe.json!\n");
            });

            Console.ReadKey();

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

        ///// <summary>
        ///// Get the File Name without extension
        ///// </summary>
        ///// <param name="folderPath">Folder Path that contains the Input Files</param>
        ///// <returns>All File Names without extension</returns>
        //private static IEnumerable<string> GetInputFileNames(string folderPath)
        //{
        //    try
        //    {
        //        var directoryFiles = Directory.EnumerateFiles(folderPath);

        //        return (directoryFiles
        //            .Select(fileName => Regex.Match(fileName, @".*\\(.+)\.json$").Groups[1].Value)
        //            .Where(name => Regex.IsMatch(name, @"^\d{7}$")));

        //    }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine(e.Message);
        //        throw;
        //    }
        //}

    }
}
