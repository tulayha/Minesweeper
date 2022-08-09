namespace MinesweeperV1
{
    partial class ucBtnMine
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMine = new System.Windows.Forms.Button();
            this.lblMine = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMine
            // 
            this.btnMine.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnMine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMine.Location = new System.Drawing.Point(-1, -2);
            this.btnMine.Name = "btnMine";
            this.btnMine.Size = new System.Drawing.Size(30, 30);
            this.btnMine.TabIndex = 0;
            this.btnMine.UseVisualStyleBackColor = false;
            this.btnMine.Click += new System.EventHandler(this.btnMine_Click);
            // 
            // lblMine
            // 
            this.lblMine.AutoSize = true;
            this.lblMine.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMine.Location = new System.Drawing.Point(5, 4);
            this.lblMine.Name = "lblMine";
            this.lblMine.Size = new System.Drawing.Size(18, 19);
            this.lblMine.TabIndex = 1;
            this.lblMine.Text = "8";
            // 
            // ucBtnMine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMine);
            this.Controls.Add(this.lblMine);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucBtnMine";
            this.Size = new System.Drawing.Size(28, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMine;
        private System.Windows.Forms.Label lblMine;
    }
}
