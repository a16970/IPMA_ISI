using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommonServiceLocator;
using IPMA_3.ViewModel;

namespace IPMA_3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void AppStartUp(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.CitiesList.ItemsSource =
                ServiceLocator.Current.GetInstance<MainViewModel>().Cities;
            mainWindow.CitiesList.SelectedItem = 0;
            mainWindow.Show();
        }
    }
}
