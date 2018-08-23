using FileViewer.MVVM;
using Leadtools.Codecs;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileViewer
{
    public class ImageDocumentViewModel : ObservableObject
    {

        public ImageDocumentViewModel(Stream stream)
        {
            RasterCodecs codecs = new RasterCodecs();
            codecs.Options.Load.PreferVector = true;
            codecs.Options.Load.AllPages = true;
            codecs.Options.Load.Resolution = 72;

            CodecsImageInfo imageInfo = codecs.GetInformation(stream, true);
            Pages = new List<PageViewModel>();
            for (int page = 1; page <= imageInfo.TotalPages; page++)
            {
                Pages.Add(new PageViewModel(stream, codecs, page));
            }

            if (imageInfo.TotalPages > 0)
            {
                SetActivePage(1);
            }
        }

        public IList<PageViewModel> Pages
        {
            get;
        }

        public int PageCount => Pages.Count;

        public bool SetActivePage(int pageNumber)
        {
            if (pageNumber > 0 && pageNumber <= PageCount)
            {
                ActivePage = Pages.ElementAt(pageNumber - 1);
                return true;
            }
            else
            {
                return false;
            }
        }

        private PageViewModel activePage;

        public PageViewModel ActivePage
        {
            get => activePage;
            set
            {
                activePage = value;
                OnPropertyChanged();
            }
        }


    }
}
