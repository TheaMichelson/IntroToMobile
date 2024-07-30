using FingerPaintSummer24.Models;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using FingerPaintSummer24.ViewModels;
using System.Collections.Specialized;

namespace FingerPaintSummer24.Views
{
    public partial class DrawingCanvas : ContentView
    {
        public static readonly BindableProperty LinesProperty = BindableProperty.Create(
            nameof(Lines),
            typeof(ObservableCollection<DrawnLine>),
            typeof(DrawingCanvas),
            new ObservableCollection<DrawnLine>(),
            propertyChanged: OnLinesChanged);

        public ObservableCollection<DrawnLine> Lines
        {
            get => (ObservableCollection<DrawnLine>)GetValue(LinesProperty);
            set => SetValue(LinesProperty, value);
        }

        public DrawingCanvas()
        {
            InitializeComponent();
            CanvasView.Drawable = new DrawingCanvasDrawable(this);
            CanvasView.StartInteraction += OnStartInteraction;
            CanvasView.DragInteraction += OnDragInteraction;
            CanvasView.EndInteraction += OnEndInteraction;
        }

        private static void OnLinesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var canvas = (DrawingCanvas)bindable;
            if (oldValue is ObservableCollection<DrawnLine> oldCollection)
            {
                oldCollection.CollectionChanged -= canvas.OnLinesCollectionChanged;
            }
            if (newValue is ObservableCollection<DrawnLine> newCollection)
            {
                newCollection.CollectionChanged += canvas.OnLinesCollectionChanged;
            }
            canvas.CanvasView.Invalidate();
        }

        private void OnLinesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CanvasView.Invalidate();
        }

        private void OnStartInteraction(object sender, TouchEventArgs e)
        {
            var point = e.Touches[0];
            ((DrawingViewModel)BindingContext).StartLine((float)point.X, (float)point.Y);
        }

        private void OnDragInteraction(object sender, TouchEventArgs e)
        {
            var point = e.Touches[0];
            ((DrawingViewModel)BindingContext).ContinueLine((float)point.X, (float)point.Y);
        }

        private void OnEndInteraction(object sender, TouchEventArgs e)
        {
            ((DrawingViewModel)BindingContext).EndLine();
        }

        /*public async Task SaveDrawingAsync(string fileName)
        {
            var graphicsView = CanvasView;

            // Define the size for the output image
            var size = new Size(graphicsView.Width, graphicsView.Height);

            // Create a new image with the size of the GraphicsView

            var canvas = new Canvas();

            // Draw the contents of GraphicsView onto the new image
            graphicsView.Drawable.Draw((ICanvas)canvas, new RectF(0, 0, (float)size.Width, (float)size.Height));

            // Save the image to a file
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName + ".png");
            using (var fileStream = File.Create(filePath))
            {
                // Save the image as PNG
                await canvas.SaveAsync(fileStream, ImageFormat.Png);
            }
        }*/

    /*private void SaveStreamToFile(Stream stream, string fileName)
        {
            // Get the path to the device's storage
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName + ".png");

            using var fileStream = File.Create(filePath);
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);
        }*/

        private class DrawingCanvasDrawable : IDrawable
        {
            private readonly DrawingCanvas _canvas;

            public DrawingCanvasDrawable(DrawingCanvas canvas)
            {
                _canvas = canvas;
            }

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                foreach (var line in _canvas.Lines)
                {
                    if (line.Points.Count > 1)
                    {
                        canvas.StrokeColor = line.Color;
                        canvas.StrokeSize = line.Width;

                        var path = new PathF();
                        path.MoveTo(line.Points[0]);
                        for (int i = 1; i < line.Points.Count; i++)
                        {
                            path.LineTo(line.Points[i]);
                        }
                        canvas.DrawPath(path);
                    }
                }
            }
        }
    }
}
