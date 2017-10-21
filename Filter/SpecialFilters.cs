using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace Filter
{
    public class GreyWorld : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            var sourceColor = sourseImage.GetPixel(x, y);
            var result = mass;
            var resultColor = Color.FromArgb(Clamp((int)(result[0] * sourceColor.R), 0, 255),
                Clamp((int)(result[1] * sourceColor.G), 0, 255), Clamp((int)(result[2] * sourceColor.B), 0, 255));
            return resultColor;
        }
    }
}
