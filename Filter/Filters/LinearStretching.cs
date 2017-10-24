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

    }

}
