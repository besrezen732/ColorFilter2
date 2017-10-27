using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using  System.Collections.Generic;

namespace Filter
{

    public partial class FilterBaseForm : Form
    {
        private Stack<Bitmap> bitmapStack;
        public Color serviceColor = new Color();
        Bitmap baseImage;

        #region //обработчик потока

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Bitmap image = baseImage;
                bitmapStack.Push(image);
                Bitmap newImage = ((Filters)e.Argument).ProcessImage(image, backgroundWorker1,0,4);
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

        private void backgroundWorker1_DoWork1(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Bitmap image = baseImage;
                Bitmap newImage = ((Filters[])e.Argument)[0].ProcessImage(image, backgroundWorker1, 50, 4);
                image = newImage;
                newImage = ((Filters[])e.Argument)[1].ProcessImage(image, backgroundWorker1, 100,4);
                if (backgroundWorker1.CancellationPending != true)
                {
                    baseImage = newImage;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка вackgroundWorker1_DoWork1\n" + ex.TargetSite + " " + ex.Message);
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
                //toolStripButton3.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка backgroundWorker1_RunWorkerCompleted");
            }
        }

        #endregion

        #region // Открытие и Сохранение файла

        public void OpenPicture()
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

                    матричныеToolStripMenuItem.Enabled = true;
                    точечныеToolStripMenuItem.Enabled = true;
                    медианныйФильтрToolStripMenuItem.Enabled = true;
                    линейнаяКоррекцияToolStripMenuItem.Enabled = true;
                    матМорфологииToolStripMenuItem.Enabled = true;
                    специальныеФильтрыToolStripMenuItem.Enabled = true;
                    bitmapStack.Push(baseImage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки картинки");
            }
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPicture();
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
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenPicture();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SavePicture();
        }

        #endregion

        //Constructor
        public FilterBaseForm()
        {
            bitmapStack = new Stack<Bitmap>();
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

        private void Filtering(Filters filter, EventArgs e)
        {
            try
            {
                //toolStripButton3.Enabled = false;
                backgroundWorker1.RunWorkerAsync(filter);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка запуска потока");
            }
        }

        #region //выполнение фильтров

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
            DialogForm dialog = new DialogForm();
            if (dialog.ShowDialog() == DialogResult.OK)
                Filtering(new Dilation(dialog.GetMatrix()), e);
        }

        private void эрозияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogForm dialog = new DialogForm();
            if (dialog.ShowDialog() == DialogResult.OK)
                Filtering(new Erosion(dialog.GetMatrix()), e);
        }

        private void коррекцияСОпорнымЦветомToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color refColor = colorDialog1.Color;

                Filtering(new ReferenceColor(refColor, baseImage), e);
            }
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

            if ((((int)(cPictureImageX - cImageX + 1) <= e.X) && ((int)(cPictureImageX + cImageX - 1) >= e.X)) &&
                (((int)(cPictureImageY - cImageY + 1) <= e.Y) && ((int)(cPictureImageY + cImageY - 1) >= e.Y)))
            {

                var newX = (int)((e.X - (int)(cPictureImageX - cImageX - 1)) / param);
                var newY = (int)((e.Y - (int)(cPictureImageY - cImageY - 1)) / param);
                serviceColor =
                    baseImage.GetPixel(newX, newY);

            }
            else
                serviceColor = Color.Green;
            pictureBox.BackColor = serviceColor;
        }

        private void открытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogForm dialog = new DialogForm();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                backgroundWorker1.DoWork -= backgroundWorker1_DoWork;
                backgroundWorker1.DoWork += backgroundWorker1_DoWork1;
                Filters[] filters = { new Erosion(dialog.GetMatrix()), new Dilation(dialog.GetMatrix()) };
                try
                {
                    backgroundWorker1.RunWorkerAsync(filters);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
                Thread.Sleep(50);
                backgroundWorker1.DoWork -= backgroundWorker1_DoWork1;
                backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            }
        }

        private void закрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogForm dialog = new DialogForm();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                backgroundWorker1.DoWork -= backgroundWorker1_DoWork;
                backgroundWorker1.DoWork += backgroundWorker1_DoWork1;
                Filters[] filters = { new Dilation(dialog.GetMatrix()), new Erosion(dialog.GetMatrix()) };
                try
                {
                    backgroundWorker1.RunWorkerAsync(filters);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка запуска потока");
                }
                Thread.Sleep(50);
                backgroundWorker1.DoWork -= backgroundWorker1_DoWork1;
                backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!(bitmapStack.Count == 0))
                pictureBox.Image = new Bitmap(bitmapStack.Pop());
        }

        private void зеркалоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filtering(new MirrorFilter(),e );
        }
    }
}
