using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Filter
{

    public partial class FilterBaseForm : Form
    {
        public Color serviceColor = new Color();
        Bitmap baseImage;

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
                MessageBox.Show("Ошибка вackgroundWorker1_DoWork\n" + ex.TargetSite + " " + ex.Message);
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

        public void SavePicture()
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

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePicture();
        }

        #endregion

        //Constructor
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
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка ручной останоки потока backgroundWorker1");
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Данная программа выполнена в качестве зачетного задания лабораторной работы ""Фильтры"" командой студентов 456 457 групп Трусовым, Блиновым,Курниковым");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region //выполнение фильтров

        private void Filtering(Filters filter, EventArgs e)
        {
            if (baseImage == null)
                MessageBox.Show("Выберите изображение для обработки");
            else
            {
                try
                {
                    backgroundWorker1.RunWorkerAsync(filter);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
            }
        }


        private void иверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new PointFilters(), e);
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new BLurFilter(), e);
        }

        private void размытиеПоГаусссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new GaussianFilter(), e);
        }

        private void оттенкиСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new GreyFilter(), e);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new SepiaFilter(), e);

        }

        private void jarcostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new Brightness(), e);
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new SharpnessFilter(), e);
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new EmbossingFilter(), e);
        }

        private void сдвигToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new Shift(), e);
        }

        private void волнаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new Wave(), e);
        }

        private void эффектСтеклаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new Glass(), e);
        }

        private void линейнаяКоррекцияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new LinearStretching(baseImage), e);
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new MedianFilter(), e);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new GreyWorld(baseImage), e);
        }

        private void идеальныйОтражательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new PerfectReflector(baseImage), e);
        }

        #endregion

        private void расширениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage != null)
            {
                DialogForm dialog = new DialogForm();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Filtering(new Dilation(dialog.GetMatrix()), e);
                }
            }
        }

        private void эрозияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (baseImage != null)
            {
                DialogForm dialog = new DialogForm();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Filtering(new Erosion(dialog.GetMatrix()), e);
                }
            }
        }

        private void коррекцияСОпорнымЦветомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Color refColor = colorDialog1.Color;

            Filtering(new ReferenceColor(refColor, baseImage), e);
        }


        public void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            float cPictureImageX = (pictureBox.Width / 2);
            float cPictureImageY = (pictureBox.Height / 2);
            float cImageX = (baseImage.Width / 2);
            float cImageY = (baseImage.Height / 2);

            float param = Math.Min(cPictureImageX / cImageX, cPictureImageY / cImageY);
            cImageX *= param;
            cImageY *= param;

            if ((((int) (cPictureImageX - cImageX + 1) <= e.X) && ((int) (cPictureImageX + cImageX - 1) >= e.X)) &&
                (((int) (cPictureImageY - cImageY + 1) <= e.Y) && ((int) (cPictureImageY + cImageY - 1) >= e.Y)))
            {

                var newX = (int) ((e.X - (int) (cPictureImageX - cImageX - 1)) / param);
                var newY = (int) ((e.Y - (int) (cPictureImageY - cImageY - 1)) / param);
                serviceColor =
                    baseImage.GetPixel(newX, newY);

            }
            else
                serviceColor = pictureBox.BackColor;
            pictureBox.BackColor = serviceColor;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SavePicture();
        }
    }
}
