using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Filter
{

    public partial class FilterBaseForm : Form
    {
        #region //обработчик потока

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                int dx = 50;
                Bitmap newImage = ((Filters) e.Argument).ProcessImage(baseImage, backgroundWorker1, dx);
                if (backgroundWorker1.CancellationPending != true)
                {
                    baseImage = newImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка вackgroundWorker1_DoWork");
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                progressBar1.Value = e.ProgressPercentage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка backgroundWorker1_ProgressChanged");
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!e.Cancelled)
                {
                    pictureBox.Image = baseImage;
                    pictureBox.Refresh();
                }
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка backgroundWorker1_RunWorkerCompleted");
            }
        }

        #endregion

        #region // Открытие и Сохранение файла

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var imagePath = "";
                OpenFileDialog dialog = new OpenFileDialog()
                {
                    Filter = "image files | *.jpg;*.png;*.bmp;| All files(*.*)|*.*"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    imagePath = dialog.FileName;
                    baseImage = new Bitmap(imagePath);
                    pictureBox.Image = baseImage;
                    pictureBox.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки картинки");
            }
        }


        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                savedialog.OverwritePrompt = true;
                //отображать ли предупреждение, если пользователь указывает несуществующий путь
                savedialog.CheckPathExists = true;
                //список форматов файла, отображаемый в поле "Тип файла"
                savedialog.Filter =
                    "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                //отображается ли кнопка "Справка" в диалоговом окне
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                {
                    try
                    {
                        baseImage.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion


        Bitmap baseImage;

        public FilterBaseForm()
        {
            InitializeComponent();
        }

        // прерывание потока
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1.CancelAsync();
            }
            catch (Exception)
            {
                MessageBox.Show(@"Ошибка ручной останоки потока backgroundWorker1");
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Данная программа выполнена в качестве зачетного задания лабораторной работы ""Фильтры"" командой студентов групп 456/457 Трусовым, Блиновом, Курниковым");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //тут должен быть выход из программы, но мне лень
        }

        #region //выполнение фильтров

        private void иверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new InventFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new BLurFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void размытиеПоГаусссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new GaussianFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void оттенкиСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new GreyFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {

                    Filters filter = new SepiaFilter();

                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void jarcostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new Brightness();

                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new SharpnessFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new EmbossingFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void сдвигToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new Shift();
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void волнаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new Wave();
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void эффектСтеклаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new Glass();
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void медианныйФильтрToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new MedianFilter();
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }

        private void линейнаяКоррекцияToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    Filters filter = new LinearStretching();
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }


        #endregion

    }
}
