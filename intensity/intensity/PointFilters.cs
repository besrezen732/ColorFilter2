using System.Drawing;

namespace intensity
{
    public class PointFilters : BaseFilter
    {
            protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
            {
                Color sourceColor = sourseImage.GetPixel(x, y);
                Color resultColor = Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
                return resultColor;
            }
        
    }
}
