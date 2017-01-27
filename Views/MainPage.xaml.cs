using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HyperFileChanger.Views;


// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace HyperFileChanger
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel.MainPageViewModel MainView;
        public MainPage()
        {
            this.InitializeComponent();
            MainView = new MainViewModel.MainPageViewModel();
            this.DataContext = MainView;
            
        }
        
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NaviHelper(object sender, TextChangedEventArgs e)
        {
            if(DataTyp.Text == "JPG-Datei" || DataTyp.Text == "PNG-Datei")
            {
                ViewFrame.Navigate(typeof(ImageFrame));
                InformationFrame.Navigate(typeof(ImageInformation));
                
            }
            else if(DataTyp.Text == "Textdokument")
            {
                ViewFrame.Navigate(typeof(TextFrame));
            }
            
        }
    }
}
