using System;
using System.Drawing;

namespace Filter
{
    public class InventFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            var sourceColor = sourseImage.GetPixel(x, y);
            var resultColor = Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
            return resultColor;
        }
    }

    public class GreyFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            var sourceColor = sourseImage.GetPixel(x, y);
            var Intensity = 0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
            var resultColor = Color.FromArgb((int) Intensity, (int) Intensity, (int) Intensity);
            return resultColor;
        }
    }

    public class SepiaFilter : Filters
    {

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            var sourceColor = sourseImage.GetPixel(x, y);
            var Intensity = 0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
            var k = 25;
            var resultColor = Color.FromArgb(Clamp((int) (Intensity + 2 * k), 0, 255),
                Clamp((int) (Intensity + 0.5 * k), 0, 255), Clamp((int) (Intensity - 1 * k), 0, 255));
            return resultColor;
        }
    }

    public class Brightness : Filters
    {

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            var sourceColor = sourseImage.GetPixel(x, y);
            var k = 50;
            var resultColor = Color.FromArgb(Clamp((int) (sourceColor.R + k), 0, 255),
                Clamp((int) (sourceColor.G + k), 0, 255), Clamp((int) (sourceColor.B + k), 0, 255));
            return resultColor;
        }
    }

    public class Shift : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            var X = x + 50;
            var resultColor = Color.Black;
            if (X < sourseImage.Width)
            {
                var sourceColor = sourseImage.GetPixel(X, y);
                resultColor = Color.FromArgb(Clamp((int) (sourceColor.R), 0, 255),
                    Clamp((int) (sourceColor.G), 0, 255), Clamp((int) (sourceColor.B), 0, 255));
            }
            return resultColor;
        }
    }

    public class Wave : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            var X = Clamp((int) (x + 20 * (Math.Sin((2 * 3.14 * y) / 60))),0 , sourseImage.Width - 1);
            
            var sourceColor = sourseImage.GetPixel(X, y);
            var resultColor = Color.FromArgb(Clamp((int) (sourceColor.R), 0, 255),
                Clamp((int) (sourceColor.G), 0, 255), Clamp((int) (sourceColor.B), 0, 255));
            return resultColor;
        }
    }

    //доделать
    public class Glass : Filters
    {
        private readonly Random _rand = new Random();

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            var X = Clamp((int)(x + (_rand.Next(2) % 2 - 0.5) * 10), 0, sourseImage.Width - 1);
            var Y = Clamp((int)(y + (_rand.Next(2) % 2 - 0.5) * 10), 0, sourseImage.Height - 1);
            
            var sourceColor = sourseImage.GetPixel(X, Y);
            var resultColor = Color.FromArgb(Clamp((int) (sourceColor.R), 0, 255),
                Clamp((int) (sourceColor.G), 0, 255), Clamp((int) (sourceColor.B), 0, 255));
            return resultColor;
        }
    }
}
