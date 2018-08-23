using FileViewer.MVVM;
using Leadtools;
using Leadtools.Codecs;
using System.IO;

namespace FileViewer
{
    public class PageViewModel : ObservableObject
    {
        private readonly Stream stream;
        private readonly RasterCodecs rasterCodecs;

        public PageViewModel(Stream stream, RasterCodecs rasterCodecs, int pageNumber)
        {
            this.stream = stream;
            this.rasterCodecs = rasterCodecs;
            PageNumber = pageNumber;
            ZoomLevel = 1.25;
        }

        public int PageNumber { get; }

        public RasterImage Image => GetImageFromStream();

        public RasterImage GetImageFromStream()
        {
            return rasterCodecs.Load(stream, PageNumber);
        }

        public bool SetZoomLevel(double zoomLevel)
        {
            if (zoomLevel > 0)
            {
                ZoomLevel = zoomLevel;
                return true;
            }
            else
            {
                return false;
            }
        }

        private double zoomLevel;

        public double ZoomLevel
        {
            get => zoomLevel;
            private set
            {
                zoomLevel = value;
                OnPropertyChanged();
            }
        }

    }
}
