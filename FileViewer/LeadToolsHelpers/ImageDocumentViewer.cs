using Leadtools.Document;
using Leadtools.Document.Viewer;
using System.Windows;
using System.Windows.Controls;

namespace FileViewer.LeadToolsHelpers
{
    [TemplatePart(Name = ViewerPanel, Type = typeof(Grid))]
    [TemplatePart(Name = ThumbnailPanel, Type = typeof(Grid))]
    [TemplatePart(Name = BookmarkPanel, Type = typeof(Grid))]
    public class ImageDocumentViewer : Control
    {
        private const string ViewerPanel = "ViewerPanel";
        private const string ThumbnailPanel = "ThumbnailPanel";
        private const string BookmarkPanel = "BookmarkPanel";
        private Grid _viewerPanel;
        private Grid _thumbnailPanel;
        private Grid _bookmarkPanel;

        private Leadtools.Document.Viewer.DocumentViewer _documentViewer;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _viewerPanel = GetTemplateChild(ViewerPanel) as Grid;
            _thumbnailPanel = GetTemplateChild(ThumbnailPanel) as Grid;
            _bookmarkPanel = GetTemplateChild(BookmarkPanel) as Grid;

            var createOptions = new DocumentViewerCreateOptions
            {
                // The middle panel for the view 
                ViewContainer = _viewerPanel,
                // The left panel for the thumbnails 
                ThumbnailsContainer = _thumbnailPanel,
                // The right panel is for bookmarks 
                BookmarksContainer = _bookmarkPanel,
                // Not using annotations for now 
                UseAnnotations = false
            };

            _documentViewer = DocumentViewerFactory.CreateDocumentViewer(createOptions);
            // We prefer SVG viewing 
            _documentViewer.View.PreferredItemType = DocumentViewerItemType.Svg;

            // Load a document 
            var fileName = @"C:\Users\Public\Documents\LEADTOOLS Images\Leadtools.pdf";
            var document = DocumentFactory.LoadFromFile(
               fileName,
               new LoadDocumentOptions { UseCache = DocumentFactory.Cache != null });

           
            // Set it in the viewer 
            _documentViewer.SetDocument(document);

        }
    }
}
