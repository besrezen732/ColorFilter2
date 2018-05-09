using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intensity
{
    public abstract class BaseFilter
    {
        protected abstract Color calculateNewPixelColor(Bitmap sourseImage, int x, int y);
        Helper help = new Helper();

        int progress;
        private string fullcolorString;
       
        StringBuilder result = new StringBuilder();

        public Bitmap ProcessImage(Bitmap sourseImage, BackgroundWorker worker, int dx = 0, int dy = 0)
        {
            Bitmap resultImage = new Bitmap(sourseImage.Width, sourseImage.Height);

            for (int i = 0; i < sourseImage.Width; i++)
            {
                if (dx == 50)
                    worker.ReportProgress((int)((float)i / sourseImage.Width * 100.0 / 2.0));
                if (dx == 100)
                    worker.ReportProgress((int)((float)i / sourseImage.Width * 100.0 / 2.0) + 50);
                else
                    worker.ReportProgress((int)((float)i / sourseImage.Width * 100.0));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourseImage.Height; j++)
                {
                    var newColor = calculateNewPixelColor(sourseImage, i, j);
                    var colorString = "pixel width " + i + " pixel height " + j + " intensity " + newColor + "\r\n";
                    result.Append(colorString);
                    resultImage.SetPixel(i, j, newColor);
                }
            }

            help.FileWriter(result.ToString());
            return resultImage;
        }
    }
}
