using FileViewer.MVVM;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Windows.Media;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        public ImageSource Thumbnail => GetThumbnail();

        private ImageSource GetThumbnail()
        {
            RasterImage image = Image.CreateThumbnail(200, 200, 24, RasterViewPerspective.BottomLeft, RasterSizeFlags.None);
            var imageSource = RasterImageConverter.ConvertToSource(image, ConvertToSourceOptions.None);
            return imageSource;
        }

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
