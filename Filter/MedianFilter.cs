using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
     class MedianFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y)
        {
            //http://lhs-blog.info/programming/dlang/mediannyiy-filtr-v-d/
            //var sourceColor = sourseImage.GetPixel(x, y);
            //var resultColor = Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
            var resultColor = new Color();
            return resultColor;
        }

    }
}

