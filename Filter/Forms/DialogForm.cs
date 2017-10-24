using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filter
{
    public partial class DialogForm : Form
    {
        int startValue;
        enum MatrixType { square, circle, ring }
        int[,] matrix;
        public int Size
        {
            get
            {
                return dataGridView1.RowCount;
            }
        }

        public DialogForm()
        {
            InitializeComponent();
            dataGridView1.Rows.Add(3);
            matrix = new int[3, 3];
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Height = dataGridView1.Height / 3;
                dataGridView1.Rows[i].SetValues(1, 1, 1);
            }
            matrix = new int[,] {
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 1, 1 }
            };
        }

        private void DialogForm_Load(object sender, EventArgs e)
        {

        }

        public int[,] GetMatrix()
        {
            return matrix;
        }

        private void TableChange(int size, int gridSize)
        {
            dataGridView1.Size = new Size(gridSize, gridSize);
            dataGridView1.Columns.Clear();
            for (int i = 0; i < size; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), i.ToString());
                dataGridView1.Columns[i].Width = gridSize / size;
            }
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(size);
            matrix = new int[size, size];
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Height = gridSize / size;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = 1;
                    matrix[i, j] = 1;
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int value;
                value = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                if (value != 0)
                {
                    if (value != 1)
                    {
                        MessageBox.Show("A value can be 1 or 0 only.");
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = startValue.ToString();
                        matrix[e.RowIndex, e.ColumnIndex] = startValue;
                    }
                }
                else
                {
                    matrix[e.RowIndex, e.ColumnIndex] = value;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Parent.Dispose();
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                startValue = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void size1_CheckedChanged(object sender, EventArgs e)
        {
            if (size1.Checked)
            {
                TableChange(3, 120);
            }
        }

        private void size2_CheckedChanged(object sender, EventArgs e)
        {
            if (size2.Checked)
            {
                TableChange(5, 150);
            }
        }

        private void size3_CheckedChanged(object sender, EventArgs e)
        {
            if (size3.Checked)
            {
                TableChange(7, 210);
            }
        }
    }
}
