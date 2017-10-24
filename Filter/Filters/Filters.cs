using System.Drawing;
using System.ComponentModel;
using System;
using System.Windows.Forms;

namespace Filter
{
    abstract public class Filters //общая часть обход всех элементов
    {
        protected abstract Color calculateNewPixelColor(Bitmap sourseImage, int x, int y );

        public Bitmap ProcessImage(Bitmap sourseImage, BackgroundWorker worker, int dx = 0 , int dy = 0)
        {
            Bitmap resultImage = new Bitmap(sourseImage.Width, sourseImage.Height);

            for (int i = 0; i < sourseImage.Width; i++)
            {
                worker.ReportProgress((int) ((float) i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourseImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourseImage, i, j));
                }
            }
            return resultImage;
        }

        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                value = min;
            if (value > max)
                value = max;
            return value;
        }

        public float[] GetMaxBrightness(Bitmap sourseImage)
        {
            float[] maxBrightness = new float[3];
            var height = sourseImage.Height;
            var width = sourseImage.Width;
            int maxR = sourseImage.GetPixel(0, 0).R;
            int maxG = sourseImage.GetPixel(0, 0).G;
            int maxB = sourseImage.GetPixel(0, 0).B;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var service = sourseImage.GetPixel(i, j);
                    maxR = Math.Max(maxR, service.R);
                    maxG = Math.Max(maxG, service.G);
                    maxB = Math.Max(maxB, service.B);
                }
            }
            maxBrightness[0] = maxR;
            maxBrightness[1] = maxG;
            maxBrightness[2] = maxB;

            return maxBrightness;
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
            float mediumR = (float)sumR / (width * height);
            float mediumG = (float)sumG / (width * height);
            float mediumB = (float)sumB / (width * height);

            float aVg = (mediumR + mediumG + mediumB) / 3;

            float[] result = new float[3];
            result[0] = mediumR / aVg;
            result[1] = mediumG / aVg;
            result[2] = mediumB / aVg;

            return result;
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
            max[0] = (float)xmaxR;
            max[1] = (float)xmaxG;
            max[2] = (float)xmaxB;

            min[0] = (float)xminR;
            min[1] = (float)xminG;
            min[2] = (float)xminB;
        }
    }
}
