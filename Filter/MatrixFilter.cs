using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    class MatrixFilter : Filters
    {
        protected float[,] kernel = null;

        protected MatrixFilter()
        {
        }

        public MatrixFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            int idX, idY;
            Color neighborColor;
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;
            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    idX = Clamp(x + k, 0, sourseImage.Width - 1);
                    idY = Clamp(y + l, 0, sourseImage.Height - 1);
                    neighborColor = sourseImage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                }
            Color colorResult = Color.FromArgb(Clamp((int)resultR, 0, 255),
                Clamp((int)resultG, 0, 255),
                Clamp((int)resultB, 0, 255));
            return colorResult;
        }
    }

    //размытие перестановкой
    class BLurFilter : MatrixFilter
    {
        public BLurFilter()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            for (int j = 0; j < sizeY; j++)
                kernel[i, j] = 1.0f / (sizeX * sizeY);
        }
    }

    //размытие по методу Гаусса
    class GaussianFilter : MatrixFilter
    {
        public GaussianFilter()
        {
            createGaussianKernel(3, 2);
        }
        public void createGaussianKernel(int radius, float sigma)
        {
            int size = 2 * radius + 1;
            kernel = new float[size, size];
            float norm = 0;
            for (int i = -radius; i <= radius; i++)
            for (int j = -radius; j <= radius; j++)
            {
                kernel[i + radius, j + radius] =
                    (float) Math.Exp((-(Math.Pow(i, 2) + Math.Pow(j, 2)) / Math.Pow(sigma, 2)));
                norm += kernel[i + radius, j + radius];
            }
            for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                kernel[i, j] /= norm;
        }
    }

    class SharpnessFilter : MatrixFilter
    {
        public SharpnessFilter()
        {
            float[,] kernelMat =
            {
                {-1, -1, -1},
                {-1, 9, -1},
                {-1, -1, -1}
            };
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
            kernel = kernelMat;
        }
    }
    class EmbossingFilter : MatrixFilter
    {
        public EmbossingFilter()
        {
            float[,] kernelMat =
            {
                {0, 1, 0},
                {1, 0, -1},
                {0, -1, 0}
            };
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
            kernel = kernelMat;
        }
    }
}
