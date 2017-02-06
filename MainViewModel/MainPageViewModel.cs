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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Windows.Storage;

namespace HyperFileChanger.MainViewModel
{
    public class MainPageViewModel : ModelBase
    {
        public FileModel Files { get; set; }
        public DelegateCommand<string> DateiNameCommand { get; set; }
        public DelegateCommand<string> FolderPicker { get; set; }
        public ObservableCollection<IStorageItem> FileList { get; set; }
        public MainPageViewModel()
        {
            
            Files = new FileModel();
            FileList = new ObservableCollection<IStorageItem>();
            DateiNameCommand = new DelegateCommand<string>
            (
                
                async (s) => await Files.PickFile().ContinueWith(async (f) => await Files.GetPartentFolder(FileList)) ,
                (s) => true
            );

            FolderPicker = new DelegateCommand<string>
            (
                async (s) => await Files.PickFolder(),
                (s) => true
            );


        }
       
    }
    
   
    
    
    
}
