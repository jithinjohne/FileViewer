using FileViewer.MVVM;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Windows.Media;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace FileViewer
{
    public class FileViewerViewModel : ObservableObject
    {
        private List<Page> pages = new List<Page>();
        private Page selectedPage;
        private string sourceFileName;
        private double zoomLevel;

        public FileViewerViewModel()
        {
            SelectFileCommand = new RelayCommand((x) => SelectFile(x));

            NextPage = new RelayCommand(p => SetSelectedPageToNext(p));
            PreviousPage = new RelayCommand(p => SetSelectedPageToPrevious(p));

            ZoomIn = new RelayCommand(x => InCreaseZoomLevel(x));
            ZoomOut = new RelayCommand(x => DecreaseZoomLevel(x));
            ZoomLevel = 1;

            Fit = new RelayCommand(x => FitImage(x));
        }

        public ICommand Fit { get; set; }

        public ICommand NextPage { get; set; }

        public ICommand PreviousPage { get; set; }

        public Page SelectedPage
        {
            get => selectedPage;
            set
            {
                selectedPage = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectFileCommand { get; set; }

        public ICommand ZoomIn { get; set; }

        public double ZoomLevel
        {
            get => zoomLevel;
            set
            {
                zoomLevel = value;
                OnPropertyChanged();
            }
        }

        public ICommand ZoomOut { get; set; }

        private void DecreaseZoomLevel(object x)
        {
            ZoomLevel = ZoomLevel - 0.25;
        }

        private void FitImage(object x)
        {
            ZoomLevel = 1;
        }

        private void InCreaseZoomLevel(object x)
        {
            ZoomLevel = ZoomLevel + 0.25;
        }

        private void LoadFileAsPages()
        {
            pages.Clear();

            RasterCodecs codecs = new RasterCodecs();
            using (RasterImage rasterImage = codecs.Load(sourceFileName, 0, CodecsLoadByteOrder.Bgr, 1, 1))
            {
                for (int page = 0; page < rasterImage.PageCount; page++)
                {
                    System.Windows.Media.ImageSource source = RasterImageConverter.ConvertToSource(rasterImage, ConvertToSourceOptions.None);
                    pages.Add(new Page { PageNumber = page, Image = source });
                }
            }

            SelectedPage = pages.FirstOrDefault();
            OnPropertyChanged(nameof(ZoomLevel));
        }

        private void SelectFile(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.pdf, *.tiff, *.png) | *.*"
            };
            dialog.ShowDialog();

            sourceFileName = dialog.FileName;

            LoadFileAsPages();
        }

        private void SetSelectedPageToNext(object input)
        {
            if (pages.Count - 1 > selectedPage.PageNumber)
            {
                SelectedPage = pages.ElementAt(selectedPage.PageNumber + 1);
            }
        }

        private void SetSelectedPageToPrevious(object input)
        {
            if (selectedPage.PageNumber > 0)
            {
                selectedPage = pages.ElementAt(selectedPage.PageNumber - 1);
            }
        }
    }
}