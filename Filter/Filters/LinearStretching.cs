using System;
using System.Drawing;


namespace Filter
{
    public class LinearStretching : Filters
    {
        public float[] minBrightness;
        public float[] maxBrightness;

        public LinearStretching(Bitmap sourseImage)
        {
            GetMinAndMaxBrightness(sourseImage, out minBrightness, out maxBrightness);
        }

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            float minR = minBrightness[0];
            float minG = minBrightness[1];
            float minB = minBrightness[2];

            float maxR = maxBrightness[0];
            float maxG = maxBrightness[1];
            float maxB = maxBrightness[2];

            Color source = sourseImage.GetPixel(x, y);

            int paramR = (maxR != minR) ? (int) ((source.R - minR) * (255 / (maxR - minR))) : source.R;
            int paramG = (maxG != minG) ? (int) ((source.G - minG) * (255 / (maxG - minG))) : source.G;
            int paramB = (maxB != minB) ? (int) ((source.B - minB) * (255 / (maxB - minB))) : source.B;

            Color resultColor = Color.FromArgb(paramR, paramG, paramB);
            return resultColor;
        }


        public void GetMinAndMaxBrightness(Bitmap sourseImage, out float[] min, out float[] max)
        {
            var width = sourseImage.Width;
            var height = sourseImage.Height;

            int xminR = sourseImage.GetPixel(0, 0).R;
            int xminG = sourseImage.GetPixel(0, 0).G;
            int xminB = sourseImage.GetPixel(0, 0).B;
            int xmaxR = sourseImage.GetPixel(0, 0).R;
            int xmaxG = sourseImage.GetPixel(0, 0).G;
            int xmaxB = sourseImage.GetPixel(0, 0).B;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var service = sourseImage.GetPixel(i, j);
                    xminR = Math.Min(xminR, service.R);
                    xminG = Math.Min(xminG, service.G);
                    xminB = Math.Min(xminB, service.B);
                    xmaxR = Math.Max(xmaxR, service.R);
                    xmaxG = Math.Max(xmaxG, service.G);
                    xmaxB = Math.Max(xmaxB, service.B);
                }
            }
            min = new float[3];
            max = new float[3];
            max[0] = (float) xmaxR;
            max[1] = (float) xmaxG;
            max[2] = (float) xmaxB;

            min[0] = (float) xminR;
            min[1] = (float) xminG;
            min[2] = (float) xminB;
        }

    }

}
