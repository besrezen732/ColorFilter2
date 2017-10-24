using System;
using System.Drawing;


namespace Filter
{
    public class GreyWorld : Filters
    {
        public float[] mass;

        public GreyWorld(Bitmap sourseImage)
        {
            mass = GetMediumBrightness(sourseImage);
        }

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            float mediumR = mass[0];
            float mediumG = mass[1];
            float mediumB = mass[2];
            var sourceColor = sourseImage.GetPixel(x, y);

            var resultColor = Color.FromArgb(Clamp((int) (mediumR * sourceColor.R), 0, 255),
                Clamp((int) (mediumG * sourceColor.G), 0, 255), Clamp((int) (mediumB * sourceColor.B), 0, 255));
            return resultColor;
        }
    }

    public class PerfectReflector : Filters
    {
        public float[] maxBrightness;

        public PerfectReflector(Bitmap sourseImage)
        {
            maxBrightness = GetMaxBrightness(sourseImage);
        }

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            float paramR = 255 / maxBrightness[0];
            float paramG = 255 / maxBrightness[1];
            float paramB = 255 / maxBrightness[2];
            var sourceColor = sourseImage.GetPixel(x, y);

            var resultColor = Color.FromArgb(Clamp((int) (paramR * sourceColor.R), 0, 255),
                Clamp((int) (paramG * sourceColor.G), 0, 255), Clamp((int) (paramB * sourceColor.B), 0, 255));
            return resultColor;
        }

    }

    public class ReferenceColor : Filters
    {
        public Color refColor;
        public float[] mass;

        public ReferenceColor(Color referenceColor, Bitmap sourseImage)
        {
            refColor = referenceColor;
            mass = GetMaxBrightness(sourseImage);
        }

        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            var sourceColor = sourseImage.GetPixel(x, y);
            float resR = refColor.R / mass[0];
            float resG = refColor.G / mass[1];
            float resB = refColor.B / mass[2];

            var resultColor = Color.FromArgb(
                Clamp((int) (sourceColor.R * resR), 0, 255),
                Clamp((int) (sourceColor.G * resG), 0, 255), 
                Clamp((int) (sourceColor.B * resB), 0, 255));
            return resultColor;
        }
    }
}
