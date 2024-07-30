using Microsoft.Maui.Graphics;
using System.Collections.Generic;

namespace FingerPaintSummer24.Models
{
    public class DrawnLine
    {
        public List<PointF> Points { get; set; }
        public Color Color { get; set; }
        public float Width { get; set; }

        public DrawnLine(Color color, float width)
        {
            Points = new List<PointF>();
            Color = color;
            Width = width;
        }

        public void AddPoint(float x, float y)
        {
            Points.Add(new PointF(x, y));
        }
    }
}