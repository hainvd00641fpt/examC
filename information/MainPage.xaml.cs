using information.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace information
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Member currentMember;
        public MainPage()
        {
            this.currentMember = new Member();
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }


        private async void BtnSave_Save(object sender, RoutedEventArgs e)
        {
            this.currentMember.Name = this.name.Text;
            this.currentMember.Email = this.email.Text;
            this.currentMember.Phone = this.phone.Text;

            string jsonMember = JsonConvert.SerializeObject(this.currentMember);

            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "New Document";
            StorageFile file = await savePicker.PickSaveFileAsync();
            await FileIO.WriteTextAsync(file, jsonMember);
        }
    
        private async System.Threading.Tasks.Task BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;

            openPicker.FileTypeFilter.Add(".txt");
            StorageFile file = await openPicker.PickSingleFileAsync();

            string content = await Windows.Storage.FileIO.ReadTextAsync(file);
            currentMember = JsonConvert.DeserializeObject<Member>(content);
            name.Text = currentMember.Name;
            email.Text = currentMember.Email;
            phone.Text = currentMember.Phone;

        }
    }
}



       