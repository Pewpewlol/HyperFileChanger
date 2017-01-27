using XamlStandardsLib;
using System.Threading.Tasks;
using System;
using Windows.System;
using System.Windows;
using Windows.UI.Popups;
using Windows.UI;
using HyperFileChanger.Class;
using HyperFileChanger.Views;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;

namespace HyperFileChanger.MainViewModel
{
    public class MainPageViewModel : ModelBase
    {
        public FileModel Files { get; set; }
        public DelegateCommand<string> DateiNameCommand { get; set; }
        
        public MainPageViewModel()
        {
            
            Files = new FileModel();

            this.DateiNameCommand = new DelegateCommand<string>
            (
                
                async (s) => await Files.PickFile(),
                (s) => true
            );
            



        }
    }
   
    
    
    
}
