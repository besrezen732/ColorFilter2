using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    public class LinearStretching : Filters
    {
        private static int Y(int x, int xMax, int xMin)
        {
            int y = 0;
            y = (x - xMin) * (255 / (xMax - xMin));
            return y;
        }

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            int width = sourseImage.Width - 1;
            int height = sourseImage.Height - 1;
            int xminR = sourseImage.GetPixel(0, 0).R;
            int xmaxR = sourseImage.GetPixel(width, height).R;
            int xminG = sourseImage.GetPixel(0, 0).G;
            int xmaxG = sourseImage.GetPixel(width, height).G;
            int xminB = sourseImage.GetPixel(0, 0).B;
            int xmaxB = sourseImage.GetPixel(width, height).B;

            Color sourceColor = sourseImage.GetPixel(x, y);

            int resR = 0;
            int resG = 0;
            int resB = 0;
            if (xminR > sourceColor.R) xminR = sourceColor.R;
            if (xmaxR < sourceColor.R) xmaxR = sourceColor.R;
            if (xminG > sourceColor.G) xminG = sourceColor.G;
            if (xmaxG < sourceColor.G) xmaxG = sourceColor.G;
            if (xminB > sourceColor.B) xminB = sourceColor.B;
            if (xmaxB < sourceColor.B) xmaxB = sourceColor.B;

            resR = (xmaxR != xminR) ? Y(sourceColor.R, xmaxR, xminR) : xmaxR;
            resG = (xmaxG != xminG) ? Y(sourceColor.G, xmaxG, xminG) : xmaxG;
            resB = (xmaxB != xminB) ? Y(sourceColor.B, xmaxB, xminB) : xmaxB;

            Color resultColor = Color.FromArgb(Clamp(resR, 0, 255), Clamp(resG, 0, 255), Clamp(resB, 0, 255));

            return resultColor;
        }
    }
}
