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
    }
}
