using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FileViewer
{
    public class Page
    {
        public int PageNumber { get; set; }
        public ImageSource Image { get; set; }
        public double ZoomLevel { get; set; }
    }
}
