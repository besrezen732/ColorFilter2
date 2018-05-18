using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            #region Veriable

           
            int sizeX = picture.Width, sizeY = picture.Height;

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

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)

                {
                    Lout[i, j] = Math.Sqrt((((Xrp0 - Xip[i]) * (Xrp0 - Xip[i])) +
                                            ((Yrp0 - Yip[j]) * (Yrp0 - Yip[j])) +
                                            z * z)); //расстояние от "внешней" точки цикла до изображения

                    //нормализация лямбда, поиск dlambdaI2
                    dlambdaI2[i, j] =
                        Lout[i, j] -
                        _lambda * (int) (Lout[i, j] / _lambda); //(int) нужен тк дельта лямбда должна быть меньше 1
                   
                    for (int m = 0; m < sizeX; m++) //вторичный цикл (пересчет всех источников для одной точки экрана)
                    {
                        for (int n = 0; n < sizeY; n++)
                        {
                            Lins[m, n] = Math.Sqrt((((Xrp[m] - Xip[i]) * (Xrp[m] - Xip[i])) +
                                                    ((Yrp[n] - Yip[j]) * (Yrp[n] - Yrp[j])) +
                                                    z * z)); //расстояние от "внутренней" точки цикла до изображения

                            //нормализация лямбда, поиск dlambdaJ
                            dlambdaJ2[m, n] =
                                Lins[m, n] -
                                _lambda * (int) (Lins[m, n] /
                                                 _lambda); //(int) нужен тк дельта лямбда должна быть меньше 1
                            ;
                            I2[m, n] = (Math.Cos((2 * Math.PI / _lambda) * (dlambdaJ2[m, n] - dlambdaI2[i, j])));// домножить на интенсивность ij

                            F[i, j] = F[i, j] + I2[m, n];


                        }


                    }

                }

            }

            #endregion



            //перевод полученных величин интерференционной картины к отрезку [0;1]   
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (F[i, j] > _fmax)
                        _fmax = F[i, j];
                }
            }

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (F[i, j] < _fmin)
                        _fmin = F[i, j];
                }
            }

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    //F[i,j] = (F[i,j] - _fmin) / (_fmax - _fmin);
                    double chislo = (F[i, j] - _fmin) / (_fmax - _fmin);
                    returnString.AppendLine(i + "   " + chislo.ToString());

                    //вывод
                }
            }
            textBox.Text = returnString.ToString();
        }

       
    }
}

