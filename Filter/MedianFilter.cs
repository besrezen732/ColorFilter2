using System.Drawing;

namespace Filter
{
    class MedianFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            const int m = 3;
            const int n = 3;
            const int dm = 3 / 2;
            const int dn = 3 / 2;
            var width = sourseImage.Width;
            var height = sourseImage.Height;

            
            var resultColor = new Color();

            if ((x - dm >= 0 && x + dm < width) && (y - dn >= 0 && y + dn < height)) // проверка на выход за дипазон картинки
            {
                int r = 0, g = 0, b = 0;
                for (var i = x - dm; i <= x + dm; i++)
                {
                    for (var j = y - dn; j <= y + dn; j++)
                    {
                        var sourceColor = sourseImage.GetPixel(i, j);
                        r = r + sourceColor.R;
                        g = g + sourceColor.G;
                        b = b + sourceColor.B;
                    }
                }
                resultColor = Color.FromArgb( Clamp((int)(r / (n * m)),0,255), Clamp((int)(g / (n * m)), 0, 255), Clamp((int)(b / (n * m)), 0, 255));
            }
            else
                resultColor = Color.FromArgb(sourseImage.GetPixel(x, y).R, sourseImage.GetPixel(x, y).G,
                    sourseImage.GetPixel(x, y).B);
            return resultColor;
        }
    }
}

