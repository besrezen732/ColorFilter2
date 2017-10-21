using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace Filter
{
    public class GreyWorld : Filters
    {
        public float[] mass;

        public GreyWorld(Bitmap sourseImage)
        {
            mass = GetMediumBrightness(sourseImage);
        }

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            var sourceColor = sourseImage.GetPixel(x, y);
            var result = mass;
            var resultColor = Color.FromArgb(Clamp((int)(result[0] * sourceColor.R), 0, 255),
                Clamp((int)(result[1] * sourceColor.G), 0, 255), Clamp((int)(result[2] * sourceColor.B), 0, 255));
            return resultColor;
        }

        public float[] GetMediumBrightness(Bitmap sourseImage)
        {
            var width = sourseImage.Width;
            var height = sourseImage.Height;
            var sumR = 0;
            var sumG = 0;
            var sumB = 0;
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    var service = sourseImage.GetPixel(i, j);
                    sumR += service.R;
                    sumG += service.G;
                    sumB += service.B;
                }
            }
            float mediumR = sumR / (width * height);
            float mediumG = sumG / (width * height);
            float mediumB = sumB / (width * height);

            float aVg = (mediumR + mediumG + mediumB) / 3;

            float[] result = new float[3];
            result[0] = mediumR / aVg;
            result[1] = mediumR / aVg;
            result[2] = mediumR / aVg;

            return result;
        }
    }
}
