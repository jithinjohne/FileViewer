using Leadtools;
using Leadtools.Codecs;
using System.IO;

namespace FileViewer
{
    public class Page
    {
        private readonly Stream stream;
        private readonly RasterCodecs rasterCodecs;

        public Page(Stream stream, RasterCodecs rasterCodecs, int pageNumber)
        {
            this.stream = stream;
            this.rasterCodecs = rasterCodecs;
            PageNumber = pageNumber;
            ZoomLevel = 1;
        }

        public int PageNumber { get; }

        public RasterImage Image => GetImageFromStream();

        public RasterImage GetImageFromStream()
        {
            return rasterCodecs.Load(stream, PageNumber);

        }

        public double ZoomLevel { get; set; }
    }
}
