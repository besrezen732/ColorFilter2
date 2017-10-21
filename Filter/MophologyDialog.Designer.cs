using System;

namespace Filter
{
    partial class MophologyDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.qubeMatrix = new System.Windows.Forms.RadioButton();
            this.pentaMatrix = new System.Windows.Forms.RadioButton();
            this.septimaMatrix = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.matrix = new System.Windows.Forms.DataGridView();
            this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.matrix)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(116, 227);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // qubeMatrix
            // 
            this.qubeMatrix.AutoSize = true;
            this.qubeMatrix.Checked = true;
            this.qubeMatrix.Location = new System.Drawing.Point(11, 19);
            this.qubeMatrix.Name = "qubeMatrix";
            this.qubeMatrix.Size = new System.Drawing.Size(48, 17);
            this.qubeMatrix.TabIndex = 3;
            this.qubeMatrix.TabStop = true;
            this.qubeMatrix.Text = "3 x 3";
            this.qubeMatrix.UseVisualStyleBackColor = true;
            // 
            // pentaMatrix
            // 
            this.pentaMatrix.AutoSize = true;
            this.pentaMatrix.Location = new System.Drawing.Point(11, 42);
            this.pentaMatrix.Name = "pentaMatrix";
            this.pentaMatrix.Size = new System.Drawing.Size(48, 17);
            this.pentaMatrix.TabIndex = 4;
            this.pentaMatrix.TabStop = true;
            this.pentaMatrix.Text = "5 x 5";
            this.pentaMatrix.UseVisualStyleBackColor = true;
            // 
            // septimaMatrix
            // 
            this.septimaMatrix.AutoSize = true;
            this.septimaMatrix.Location = new System.Drawing.Point(11, 65);
            this.septimaMatrix.Name = "septimaMatrix";
            this.septimaMatrix.Size = new System.Drawing.Size(48, 17);
            this.septimaMatrix.TabIndex = 5;
            this.septimaMatrix.TabStop = true;
            this.septimaMatrix.Text = "7 x 7";
            this.septimaMatrix.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.qubeMatrix);
            this.groupBox1.Controls.Add(this.septimaMatrix);
            this.groupBox1.Controls.Add(this.pentaMatrix);
            this.groupBox1.Location = new System.Drawing.Point(197, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(75, 91);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Matrix Size";
            // 
            // matrix
            // 
            this.matrix.AllowDrop = true;
            this.matrix.AllowUserToDeleteRows = false;
            this.matrix.AllowUserToResizeColumns = false;
            this.matrix.AllowUserToResizeRows = false;
            this.matrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.matrix.ColumnHeadersVisible = false;
            this.matrix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1,
            this.column2,
            this.column3});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.matrix.DefaultCellStyle = dataGridViewCellStyle1;
            this.matrix.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.matrix.ImeMode = System.Windows.Forms.ImeMode.On;
            this.matrix.Location = new System.Drawing.Point(13, 12);
            this.matrix.Name = "matrix";
            this.matrix.RowHeadersVisible = false;
            this.matrix.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.matrix.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.matrix.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.matrix.Size = new System.Drawing.Size(120, 120);
            this.matrix.TabIndex = 7;
            // 
            // column1
            // 
            this.column1.MaxInputLength = 1;
            this.column1.MinimumWidth = 40;
            this.column1.Name = "column1";
            this.column1.Width = 40;
            // 
            // column2
            // 
            this.column2.MaxInputLength = 1;
            this.column2.MinimumWidth = 40;
            this.column2.Name = "column2";
            this.column2.Width = 40;
            // 
            // column3
            // 
            this.column3.MaxInputLength = 1;
            this.column3.MinimumWidth = 40;
            this.column3.Name = "column3";
            this.column3.Width = 40;
            // 
            // MophologyDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.matrix);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MophologyDialog";
            this.Text = "MophologyDialog";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.matrix)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton qubeMatrix;
        private System.Windows.Forms.RadioButton pentaMatrix;
        private System.Windows.Forms.RadioButton septimaMatrix;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn column7;
        private System.Windows.Forms.DataGridView matrix;
    }
}