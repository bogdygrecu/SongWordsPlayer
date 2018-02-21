using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SongWordsPlayer.Frames
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyMusicFrame : Page
    {
        private ObservableCollection<StorageFile> myMusic;  //consider moving this to main page as public (avoid losing everything when reload the page)
        private ObservableCollection<StorageFolder> myLocations;

        public MyMusicFrame()
        {
            this.InitializeComponent();
            myLocations = new ObservableCollection<StorageFolder>();
            myMusic = new ObservableCollection<StorageFile>();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            myLocations.Add(KnownFolders.MusicLibrary);
            await UpdateMusicCollectionAsync();
        }

        private bool IsTargetedFileType(StorageFile file)
        {
            if(file.FileType == ".mp3") //TODO: add more types later (document what types can be played)
            {
                return true;
            }
            return false;
        }

        private async Task SearchForMusicFilesInLocationAsync(StorageFolder parent)
        {
            foreach (StorageFile item in await parent.GetFilesAsync())
            {
                if(IsTargetedFileType(item))
                {
                    myMusic.Add(item);
                }
            }
            foreach (StorageFolder item in await parent.GetFoldersAsync())
            {
                await SearchForMusicFilesInLocationAsync(item);
            }
        }

        private async Task UpdateMusicCollectionAsync()
        {
            foreach(var location in myLocations)
            {
                await SearchForMusicFilesInLocationAsync(location);
            }
        }

        private void MySongsListview_ItemClick(object sender, ItemClickEventArgs e)
        {
            MainPage.Instance.GetSelectedSong(e.ClickedItem);
        }
    }
}
