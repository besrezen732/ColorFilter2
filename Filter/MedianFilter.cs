using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Filter
{
    class MedianFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            const int m = 3;
            const int n = 3;
            const int dm = m / 2;
            const int dn = n / 2;
            int width = sourseImage.Width;
            int height = sourseImage.Height;

            
            Color resultColor = new Color();

            if ((x - dm >= 0 && x + dm < width) && (y - dn >= 0 && y + dn < height)
            ) // проверка на выход за диапазон картинки
            {
                int[] r = new int[m * n];
                int[] g = new int[m * n];
                int[] b = new int[m * n];
               
                int k = 0; // счетчик для заполнения массивов цветом;
                for (int i = x - dm; i <= x + dm; i++)
                {
                    for (int j = y - dn; j <= y + dn; j++)
                    {
                        
                        Color sourceColor = sourseImage.GetPixel(i, j);
                        r[k] = sourceColor.R;
                        g[k] = sourceColor.G;
                        b[k] = sourceColor.B;
                        k++;
                    }
                }
                //Начинаем сортировку
                Array.Sort(r);
                Array.Sort(g);
                Array.Sort(b);
                resultColor = Color.FromArgb((int)r[(m * n) / 2], (int)g[m * n / 2], (int)b[m * n / 2]);

            }
            else
            resultColor = Color.FromArgb(sourseImage.GetPixel(x, y).R, sourseImage.GetPixel(x, y).G,
                    sourseImage.GetPixel(x, y).B);
            return resultColor;
        }
    }
}

