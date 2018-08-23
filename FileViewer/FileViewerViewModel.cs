using FileViewer.MVVM;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;

namespace FileViewer
{
    public class FileViewerViewModel : ObservableObject
    {
        private string sourceFileName;
        private readonly double zoomLevel;

        public FileViewerViewModel()
        {
            SelectFileCommand = new RelayCommand((x) => SelectFile(x));

            NextPage = new RelayCommand(p => SetSelectedPageToNext(p));
            PreviousPage = new RelayCommand(p => SetSelectedPageToPrevious(p));

            ZoomIn = new RelayCommand(x => InCreaseZoomLevel(x));
            ZoomOut = new RelayCommand(x => DecreaseZoomLevel(x));

            Fit = new RelayCommand(x => FitImage(x));
        }

        public ICommand Fit { get; set; }

        public ICommand NextPage { get; set; }

        public ICommand PreviousPage { get; set; }

        private PageViewModel ActivePage => ImageDocument?.ActivePage;

        public ImageDocumentViewModel ImageDocument { get; private set; }

        public ICommand SelectFileCommand { get; set; }

        public ICommand ZoomIn { get; set; }

        public ICommand ZoomOut { get; set; }

        private void DecreaseZoomLevel(object x)
        {
            ActivePage.SetZoomLevel(ActivePage.ZoomLevel - 0.25);
        }

        private void FitImage(object x)
        {
            ActivePage.SetZoomLevel(1);
        }

        private void InCreaseZoomLevel(object x)
        {
            ActivePage.SetZoomLevel(ActivePage.ZoomLevel + 0.25);
        }

        private void LoadFileAsPages()
        {
            FileStream fileStream = File.Open(sourceFileName, FileMode.Open);
            MemoryStream memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);

            ImageDocument = new ImageDocumentViewModel(memoryStream);

            OnPropertyChanged(nameof(ImageDocument));
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
            int nextPage = ActivePage.PageNumber + 1;
            if (nextPage < ImageDocument.PageCount)
            {
                ImageDocument.SetActivePage(nextPage);
            }
        }

        private void SetSelectedPageToPrevious(object input)
        {
            int previousPage = ActivePage.PageNumber - 1;
            if (previousPage > 0)
            {
                ImageDocument.SetActivePage(previousPage);
            }
        }
    }
}