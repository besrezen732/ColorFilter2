using System.Drawing;
using System.ComponentModel;
using System;

namespace Filter
{
    abstract public class Filters //общая часть обход всех элементов
    {
        protected abstract Color calculateNewPixelColor(Bitmap sourseImage, int x, int y );

        public float[] mass;

        public Bitmap ProcessImage(Bitmap sourseImage, BackgroundWorker worker, int dx = 0 , int dy = 0)
        {
            var resultImage = new Bitmap(sourseImage.Width, sourseImage.Height);

            mass = GetMediumBrightness(sourseImage);

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
            float mediumR =sumR / (width * height);
            float mediumG =sumG / (width * height);
            float mediumB =sumB / (width * height);

            float aVg = (mediumR + mediumG + mediumB) / 3;

            float[] result = new float[3];
            result[0] = mediumR / aVg;
            result[1] = mediumR / aVg;
            result[2] = mediumR / aVg;

            return result;
        }
    }
}
