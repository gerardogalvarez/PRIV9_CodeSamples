namespace ExtForms900.UI
{
    partial class frmGerais
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
            this.cmdConfirnar = new System.Windows.Forms.Button();
            this.cmdCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdConfirnar
            // 
            this.cmdConfirnar.Location = new System.Drawing.Point(275, 110);
            this.cmdConfirnar.Name = "cmdConfirnar";
            this.cmdConfirnar.Size = new System.Drawing.Size(83, 25);
            this.cmdConfirnar.TabIndex = 0;
            this.cmdConfirnar.Text = "Confirmar";
            this.cmdConfirnar.UseVisualStyleBackColor = true;
            // 
            // cmdCancelar
            // 
            this.cmdCancelar.Location = new System.Drawing.Point(364, 110);
            this.cmdCancelar.Name = "cmdCancelar";
            this.cmdCancelar.Size = new System.Drawing.Size(83, 25);
            this.cmdCancelar.TabIndex = 1;
            this.cmdCancelar.Text = "Cancelar";
            this.cmdCancelar.UseVisualStyleBackColor = true;
            this.cmdCancelar.Click += new System.EventHandler(this.cmdCancelar_Click);
            // 
            // frmGerais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 141);
            this.Controls.Add(this.cmdCancelar);
            this.Controls.Add(this.cmdConfirnar);
            this.Name = "frmGerais";
            this.Text = "frmGerais";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdConfirnar;
        private System.Windows.Forms.Button cmdCancelar;
    }
}