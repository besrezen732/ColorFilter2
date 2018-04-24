using System;
using System.Drawing;

namespace HoloSharp
{
    abstract class Filters
    {
        protected abstract Color massiveRGBtoList(int[,,] massive);

        public int [,,] processImage(Bitmap sourceImage)
        {
            int[,,] rgbMass = new int[3, sourceImage.Width, sourceImage.Height];

            Color  colorPixel = new Color();
            
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    colorPixel = sourceImage.GetPixel(i,j);
                    rgbMass[0, i, j] = colorPixel.R;
                    rgbMass[1, i, j] = colorPixel.G;
                    rgbMass[2, i, j] = colorPixel.B;
                }
            }
            return rgbMass;
        }
    }

    class RgbMassive : Filters
    {

        protected override Color massiveRGBtoList(int[,,] massive)
        {
            throw new NotImplementedException();
        }
    }
}
