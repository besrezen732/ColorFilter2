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
        public void RecoveryImage(ref Bitmap picture, ref RichTextBox textBox)
        {


            PointFilters intensyFilters = new PointFilters();

            #region Variable


           // int sizeX = picture.Width, sizeY = picture.Height;

            int sizeX = 300, sizeY = 300;

            const int Xrp0 = 0, Yrp0 = 0;
            const int z = 100;
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

            double _fmax = -99999999999, _fmin = 99999999999;
            StringBuilder returnString = new StringBuilder();

            #endregion

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
                                                    ((Yrp[n] - Yip[j]) * (Yrp[n] - Yrp[j])) +
                                                    z * z)); //расстояние от "внутренней" точки цикла до изображения

                            //нормализация лямбда, поиск dlambdaJ
                            dlambdaJ2[m, n] =
                                Lins[m, n] -
                                _lambda * (int) (Lins[m, n] /
                                                 _lambda); //(int) нужен тк дельта лямбда должна быть меньше 1
                            ;
                            I2[m, n] = (Math.Cos((2 * Math.PI / _lambda) *
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

            Parallel.For(0, sizeX, i =>
            {
                for (int j = 0; j < sizeY; j++)
                {

                    double chislo = (F[i, j] - _fmin) / (_fmax - _fmin);
                    returnString.AppendLine(i + "   " + chislo);
                }
            });
            textBox.Text = returnString.ToString();
        }


      
    }
}

