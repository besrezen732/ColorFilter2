using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace intensity
{
    public class ImageRecovery
    {
        public void RecoveryImage(ref Bitmap picture, ref RichTextBox textBox, ref PictureBox pictureBoxRecovery)
        {


            PointFilters intensyFilters = new PointFilters();

            #region Variable


            int sizeX = picture.Width, sizeY = picture.Height;

            //int sizeX = 100, sizeY = 100;
            var tranformationPicture = picture;
            const int Xrp0 = 0, Yrp0 = 0;
            const int Xbp = 0, Ybp = -300, zbp = -40000;


            //const double z = 415.748;
            const double z = 1000;
            int _lambda = 20;

            double[] Xip = new double[sizeX];
            double[] Yip = new double[sizeY];
            double[] Xrp = new double[sizeX];
            double[] Yrp = new double[sizeY];
            double[,] dlambdaI2 = new double[sizeX, sizeY];
            double[,] dlambdaJ2 = new double[sizeX, sizeY];
            double[,] Lout = new double[sizeX, sizeY];
            double[,] Lins = new double[sizeX, sizeY];
            double[,] I2 = new double[sizeX, sizeY];

            double[,] F = new double[sizeX, sizeY];


            Color[,] colorsPixelPictrure = new Color[sizeX, sizeY];



            double _fmax = -99999999999, _fmin = 99999999999;
            StringBuilder returnString = new StringBuilder();

            #endregion

            //массив цветов картинки




            for (int i = 0; i < sizeX; i++) //задание координат экрана и голограммы   (абсцисса)
            {
                for (int j = 0; j < sizeY; j++) //задание координат экрана и голограммы   (ордината)
                {
                    var getPixel = tranformationPicture.GetPixel(i, j);
                    colorsPixelPictrure[i, j] = getPixel;
                }
            }


            for (int i = 0; i < sizeX; i++) //задание координат экрана и голограммы   (абсцисса)
            {
                Xip[i] = i;
                Xrp[i] = i;
            }
            for (int j = 0; j < sizeY; j++) //задание координат экрана и голограммы   (ордината)
            {
                Yip[j] = j;
                Yrp[j] = j;
            }

            #region //НАЧАЛО ГЛАВНОГО ЦИКЛА!!!

            Parallel.For(0, sizeX, iX =>
            {
                for (int j = 0; j < sizeY; j++)

                {
                    Lout[iX, j] = Math.Sqrt((((Xrp0 - Xip[iX]) * (Xrp0 - Xip[iX])) +
                                             ((Yrp0 - Yip[j]) * (Yrp0 - Yip[j])) +
                                             z * z)); //расстояние от "внешней" точки цикла до изображения 

                    //Lout[iX, j] = Math.Sqrt((((Xbp - Xip[iX]) * (Xbp - Xip[iX])) +
                    //                         ((Ybp - Yip[j]) * (Ybp - Yip[j])) +
                    //                         zbp * zbp)); //расстояние от "внешней" точки цикла до изображения 
                    //нормализация лямбда, поиск dlambdaI2
                    dlambdaI2[iX, j] =
                        Lout[iX, j] -
                        _lambda * (int) (Lout[iX, j] / _lambda); //(int) нужен тк дельта лямбда должна быть меньше 1

                    for (int m = 0;
                        m < sizeX;
                        m++) //вторичный цикл (пересчет всех источников для одной точки экрана)
                    {
                        for (int n = 0; n < sizeY; n++)
                        {

                            Lins[m, n] = Math.Sqrt((((Xrp[m] - Xip[iX]) * (Xrp[m] - Xip[iX])) +
                                                    ((Yrp[n] - Yip[j]) * (Yrp[n] - Yip[j])) +
                                                    z * z)); //расстояние от "внутренней" точки цикла до изображения

                            //нормализация лямбда, поиск dlambdaJ
                            dlambdaJ2[m, n] =
                                Lins[m, n] -
                                _lambda * (int) (Lins[m, n] /
                                                 _lambda); //(int) нужен тк дельта лямбда должна быть меньше 1

                            var colorLocalPixel = colorsPixelPictrure[iX, j];
                            var Int0 = 0.36 * colorLocalPixel.R + 0.53 * colorLocalPixel.G + 0.11 * colorLocalPixel.B;
                            ;
                            I2[m, n] = Int0 * (Math.Cos((2 * Math.PI / _lambda) *
                                                        (dlambdaJ2[m, n] - dlambdaI2[iX, j]))
                                       ); // домножить на интенсивность ij

                            F[iX, j] = F[iX, j] + I2[m, n];


                        }
                    }
                }
            });

            #endregion



            //перевод полученных величин интерференционной картины к отрезку [0;1]   
            Parallel.For(0, sizeX, i =>
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (F[i, j] > _fmax)
                        _fmax = F[i, j];
                }
            });

            Parallel.For(0, sizeX, i =>
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (F[i, j] < _fmin)
                        _fmin = F[i, j];
                }
            });



            Bitmap recImage = new Bitmap(picture.Width, picture.Height);
            for (int i = 0; i < sizeX; i++) //задание координат экрана и голограммы   (абсцисса)
            {
                for (int j = 0; j < sizeY; j++)
                {

                    double normIntens = (F[i, j] - _fmin) / (_fmax - _fmin);
                    int intenstToRgb = (int) (normIntens * 255);

                    returnString.AppendLine(i + "   " + normIntens);
                    Color greyColor = Color.FromArgb(intenstToRgb, intenstToRgb, intenstToRgb);
                    recImage.SetPixel(i, j, greyColor);
                }
            }

            textBox.Text = returnString.ToString();
            var writeText = new Helper();
            writeText.FileWriter(returnString.ToString());
            pictureBoxRecovery.Image = recImage;


        }



    }
}
