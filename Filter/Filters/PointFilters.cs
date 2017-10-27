using System;
using System.Drawing;

namespace Filter
{
    public class PointFilters : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            Color sourceColor = sourseImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
            return resultColor;
        }
    }

    public class GreyFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            Color sourceColor = sourseImage.GetPixel(x, y);
            double Intensity = 0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
            Color resultColor = Color.FromArgb((int) Intensity, (int) Intensity, (int) Intensity);
            return resultColor;
        }
    }

    public class SepiaFilter : Filters
    {

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            Color sourceColor = sourseImage.GetPixel(x, y);
            double Intensity = 0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
            int k = 25;
            Color resultColor = Color.FromArgb(Clamp((int) (Intensity + 2 * k), 0, 255),
                Clamp((int) (Intensity + 0.5 * k), 0, 255), Clamp((int) (Intensity - 1 * k), 0, 255));
            return resultColor;
        }
    }

    public class Brightness : Filters
    {

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            Color sourceColor = sourseImage.GetPixel(x, y);
            int k = 50;
            Color resultColor = Color.FromArgb(Clamp((int) (sourceColor.R + k), 0, 255),
                Clamp((int) (sourceColor.G + k), 0, 255), Clamp((int) (sourceColor.B + k), 0, 255));
            return resultColor;
        }
    }

    public class Shift : Filters
    {
        //public int dX;
        //public int dY;

        //public Shift(int dx, int dy)
        //{
        //    dX = dx;
        //    dY = dy;
        //}

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            int X = x + 50;
            int Y = y + 50;
            Color resultColor = Color.Black;
            if (X < sourseImage.Width && Y < sourseImage.Height)
            {
                Color sourceColor = sourseImage.GetPixel(X, Y);
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
            int X = Clamp((int) (x + 20 * (Math.Sin((2 * 3.14 * y) / 60))), 0, sourseImage.Width - 1);

            Color sourceColor = sourseImage.GetPixel(X, y);
            Color resultColor = Color.FromArgb(Clamp((int) (sourceColor.R), 0, 255),
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
            int X = Clamp((int) (x + (_rand.Next(2) % 2 - 0.5) * 10), 0, sourseImage.Width - 1);
            int Y = Clamp((int) (y + (_rand.Next(2) % 2 - 0.5) * 10), 0, sourseImage.Height - 1);

            Color sourceColor = sourseImage.GetPixel(X, Y);
            Color resultColor = Color.FromArgb(Clamp((int) (sourceColor.R), 0, 255),
                Clamp((int) (sourceColor.G), 0, 255), Clamp((int) (sourceColor.B), 0, 255));
            return resultColor;
        }
    }

    public class MirrorFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            int newX = sourseImage.Width - x-1;
           // int newY = sourseImage.Height - y;

            Color sourceColor = sourseImage.GetPixel(newX, y);

            Color resultColor = Color.FromArgb(sourceColor.R, sourceColor.G, sourceColor.B);
            return resultColor;
        }
    }
}
