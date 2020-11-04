using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using IPMA_3.Model;
using Newtonsoft.Json;

namespace IPMA_3.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        const string FolderPath = @"..\..\..\input\";
        const string FolderOutput = @"..\..\..\output_3\";
        const string LocalFilePath = @"..\..\..\input\distrits-islands.json";
        private MatchCollection _matches;

        public MainViewModel()
        {
            ExportCityCommand = new RelayCommand(ExportCityAction);
            ExportDaysCommand = new RelayCommand(ExportDaysAction);

            _matches = GetLocalsMatchCollection(LocalFilePath);
            foreach (Match match in _matches)
            {
                try
                {
                    var fileContent = File.ReadAllText(FolderPath + match.Groups[1].Value + ".json");

                    var city = JsonConvert.DeserializeObject<City>(fileContent);
                    city.AddLocal(match.Groups[2].Value);
                    Cities.Add(city);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                    throw;
                }
            }

            City = Cities[0];
        }
        public MainWindow MainWindow { get; set; }
        public ICommand ExportCityCommand { get; set; }
        public ICommand ExportDaysCommand { get; set; }

        public ObservableCollection<City> Cities { get; set; } = new ObservableCollection<City>();

        private City city;
        public City City
        {
            get => city;
            set => Set(ref city, value);
        }

        public void UpdateCity()
        {
            if (City.globalIdLocal != ((City) MainWindow.CitiesList.SelectedItem).globalIdLocal)
            {
                City = (City) MainWindow.CitiesList.SelectedItem;
            }
        }

        private void ExportCityAction()
        {
            JsonWriteCity(city, FolderOutput);
        }
        private static void JsonWriteCity(City city, string folderOutput)
        {
            try
            {
                if (!Directory.Exists(FolderOutput))
                {
                    Directory.CreateDirectory(FolderOutput);
                }
                using (var file = File.CreateText(folderOutput + city.globalIdLocal + "-detalhe.json"))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(file, city);
                }

                MessageBox.Show("Ficheiro exportado com sucesso!", "Sucesso");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                throw;
            }
        }

        private void ExportDaysAction()
        {
            JsonWriteDays(Cities, FolderOutput);
        }
        private static void JsonWriteDays(IEnumerable<City> cities, string folderOutput)
        {
            try
            {
                if (!Directory.Exists(FolderOutput + "\\dias\\"))
                {
                    Directory.CreateDirectory(FolderOutput + "\\dias\\");
                }

                for (var i = 0; i < 5; i++)
                {
                    var day = cities.Select(city => city.data[i]).ToList();
                    using (var file = File.CreateText(folderOutput + "\\dias\\dia0"+ i + ".json"))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Serialize(file, day);
                    }
                }

                MessageBox.Show("Ficheiro exportado com sucesso!", "Sucesso");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                throw;
            }
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
    }
}