using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Filter
{
    class Dilation : Filters
    {
        int[,] structElem = null;
        protected Color max;

        public Dilation(int[,] structElem)
        {
            this.structElem = structElem;
        }

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            Color result;
            max = Color.Black;
            for (int i = -structElem.GetLength(0) / 2; i < structElem.GetLength(0) / 2; i++)
                for (int j = -structElem.GetLength(0) / 2; j < structElem.GetLength(0) / 2; j++)
                {
                    if ((structElem[i + structElem.GetLength(0) / 2, j + structElem.GetLength(0) / 2] == 1) &&
                        ((sourseImage.GetPixel(Clamp(x + i, 0, sourseImage.Width - 1), (Clamp(y + j, 0, sourseImage.Height - 1))).R > max.R) ||
                        (sourseImage.GetPixel(Clamp(x + i, 0, sourseImage.Width - 1), (Clamp(y + j, 0, sourseImage.Height - 1))).G > max.G) ||
                        (sourseImage.GetPixel(Clamp(x + i, 0, sourseImage.Width - 1), (Clamp(y + j, 0, sourseImage.Height - 1))).B > max.B))
                        )
                    {
                        max = sourseImage.GetPixel(Clamp(x + i, 0, sourseImage.Width - 1), (Clamp(y + j, 0, sourseImage.Height - 1)));
                    }
                }
            result = max;
            return result;
        }
    }



    class Erosion : Filters
    {
        int[,] structElem = null;
        protected Color max;

        public Erosion(int[,] structElem)
        {
            this.structElem = structElem;
        }

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            Color result;
            max = Color.White;
            for (int i = -structElem.GetLength(0) / 2; i < structElem.GetLength(0) / 2; i++)
                for (int j = -structElem.GetLength(0) / 2; j < structElem.GetLength(0) / 2; j++)
                {
                    if ((structElem[i + structElem.GetLength(0) / 2, j + structElem.GetLength(0) / 2] == 1) &&
                        ((sourseImage.GetPixel(Clamp(x + i, 0, sourseImage.Width - 1), (Clamp(y + j, 0, sourseImage.Height - 1))).R < max.R) ||
                        (sourseImage.GetPixel(Clamp(x + i, 0, sourseImage.Width - 1), (Clamp(y + j, 0, sourseImage.Height - 1))).G < max.G) ||
                        (sourseImage.GetPixel(Clamp(x + i, 0, sourseImage.Width - 1), (Clamp(y + j, 0, sourseImage.Height - 1))).B < max.B))
                        )
                    {
                        max = sourseImage.GetPixel(Clamp(x + i, 0, sourseImage.Width - 1), (Clamp(y + j, 0, sourseImage.Height - 1)));
                    }
                }
            result = max;
            return result;
        }
    }
}
