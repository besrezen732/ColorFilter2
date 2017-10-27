using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Filter
{
    class MathMorphology : Filters
    {
        int[,] structElem = null;
        protected Color startColor;

        public MathMorphology(int[,] structElem)
        {
            this.structElem = structElem;
        }

        public static bool ColorComparation(Color color1, Color color2)
        {
            if (color1.R > color2.R || color1.G > color2.G || color1.B > color2.B)
            {
                return true;
            }
            else
                return false;
        }

        Color Calcuation(Bitmap sourceImage, int x, int y, Color startColor)
        {
            Color resultColor = startColor;
            bool comparationResult;
            for (int i = -structElem.GetLength(0) / 2; i < structElem.GetLength(0) / 2; i++)
                for (int j = -structElem.GetLength(0) / 2; j < structElem.GetLength(0) / 2; j++)
                {
                    comparationResult = false;
                    if (structElem[i + structElem.GetLength(0) / 2, j + structElem.GetLength(0) / 2] == 1)
                    {
                        if (startColor == Color.Black)
                            comparationResult = ColorComparation(sourceImage.GetPixel(Clamp(x + i, 0, sourceImage.Width - 1), (Clamp(y + j, 0, sourceImage.Height - 1))), resultColor);
                        else if(startColor == Color.White)
                            comparationResult = ColorComparation(resultColor, sourceImage.GetPixel(Clamp(x + i, 0, sourceImage.Width - 1), (Clamp(y + j, 0, sourceImage.Height - 1))));
                        if (comparationResult)
                            resultColor = sourceImage.GetPixel(Clamp(x + i, 0, sourceImage.Width - 1), (Clamp(y + j, 0, sourceImage.Height - 1)));
                    }
                }
            return resultColor;

        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return Calcuation(sourceImage, x, y, startColor);
        }
    }



    class Erosion : MathMorphology
    {
        public Erosion(int[,] structElem) : base(structElem)
        {
            startColor = Color.White;
        }
    }

    class Dilation : MathMorphology
    {
        public Dilation(int[,] structElem) : base(structElem)
        {
            startColor = Color.Black;
        }
    }
}
