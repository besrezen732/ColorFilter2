using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace intensity
{
    public partial class BaseForm : Form
    {
        private Stack<Bitmap> bitmapStack;
        public Color serviceColor = new Color();
        Bitmap baseImage;
        public BaseForm()
        {
            InitializeComponent();
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Bitmap image = baseImage;
                Bitmap newImage = ((BaseFilter)e.Argument).ProcessImage(image, backgroundWorker1, 0, 4);
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


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPicture();
        }
        private void Filtering(BaseFilter filter, EventArgs e)
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки картинки");
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void интенсивностьToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
                Filtering(new PointFilters(), e);
           
        }

        private void Cancel_Click(object sender, EventArgs e)
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
    }
}
