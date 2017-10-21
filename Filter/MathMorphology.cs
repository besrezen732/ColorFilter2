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
        
        protected override Color calculateNewPixelColor(Bitmap sourseImage, int x, int y) {
            Color result = sourseImage.GetPixel(x, y);

            return result;
        }
    }
}
