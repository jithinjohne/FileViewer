using Leadtools;
using Leadtools.Controls;
using Leadtools.Windows.Media;
using System;
using System.Windows;
using System.Windows.Media;

namespace FileViewer.LeadToolsHelpers
{
    public static class LeadToolsAttachedProperties
    {
        #region ZoomLevelProperty

        public static double GetZoomLevel(DependencyObject obj)
        {
            return (double)obj.GetValue(ZoomLevelProperty);
        }

        public static void SetZoomLevel(DependencyObject obj, double value)
        {
            obj.SetValue(ZoomLevelProperty, value);
        }

        // Using a DependencyProperty as the backing store for ZoomLevel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZoomLevelProperty =
            DependencyProperty.RegisterAttached("ZoomLevel", typeof(double), typeof(LeadToolsAttachedProperties), new PropertyMetadata(default(double), OnZoomLevelChanged));

        private static void OnZoomLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageViewer imageViewer && e.NewValue is double zoomLevel && zoomLevel > 0)
            {
                imageViewer.UseDpi = true;
                imageViewer.Zoom(ControlSizeMode.Fit, zoomLevel, imageViewer.DefaultZoomOrigin);
                imageViewer.DefaultInteractiveMode = new ImageViewerPanZoomInteractiveMode();
            }
        }

        #endregion ZoomLevelProperty

        #region ImageProperty

        public static ImageSource GetImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageProperty);
        }

        public static void SetImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageProperty, value);
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.RegisterAttached("Image", typeof(ImageSource), typeof(LeadToolsAttachedProperties), new PropertyMetadata(default(RasterImage), OnImageChanged));

        private static void OnImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageViewer imageViewer && e.NewValue is ImageSource imageSource)
            {
                imageViewer.Image = RasterImageConverter.ConvertFromSource(imageSource, ConvertFromSourceOptions.AutoDetectAlpha);
            }
        }

        #endregion ImageProperty

        #region RasterImageSource


        public static RasterImage GetRasterImage(DependencyObject obj)
        {
            return (RasterImage)obj.GetValue(RasterImageProperty);
        }

        public static void SetRasterImage(DependencyObject obj, RasterImage value)
        {
            obj.SetValue(RasterImageProperty, value);
        }

        // Using a DependencyProperty as the backing store for RasterImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RasterImageProperty =
            DependencyProperty.RegisterAttached("RasterImage", typeof(RasterImage), typeof(LeadToolsAttachedProperties), new PropertyMetadata(default(RasterImage), OnImageSourceChanged));

        private static void OnImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageViewer imageViewer && e.NewValue is RasterImage rasterImage)
            {
                imageViewer.Image = rasterImage;
            }
        }


        #endregion
    }
}