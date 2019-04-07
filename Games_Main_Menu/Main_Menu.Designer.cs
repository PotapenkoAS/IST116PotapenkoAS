using System.Drawing;
using System.Windows.Forms;

namespace Games_Main_Menu
{
    partial class Main_Menu
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Menu));
            this.Snake_Button = new System.Windows.Forms.Button();
            this.Tetris_Button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Snake_Button
            // 
            this.Snake_Button.BackColor = System.Drawing.SystemColors.MenuText;
            this.Snake_Button.Font = new System.Drawing.Font("Consolas", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Snake_Button.ForeColor = System.Drawing.SystemColors.Menu;
            this.Snake_Button.Location = new System.Drawing.Point(152, 595);
            this.Snake_Button.Name = "Snake_Button";
            this.Snake_Button.Size = new System.Drawing.Size(176, 69);
            this.Snake_Button.TabIndex = 0;
            this.Snake_Button.Text = "Snake";
            this.Snake_Button.UseVisualStyleBackColor = false;
            this.Snake_Button.Click += new System.EventHandler(this.Snake_Button_Click);
            this.Snake_Button.MouseLeave += new System.EventHandler(this.Snake_Button_MouseLeave);
            this.Snake_Button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Snake_Button_MouseMove);
            // 
            // Tetris_Button
            // 
            this.Tetris_Button.BackColor = System.Drawing.SystemColors.MenuText;
            this.Tetris_Button.Font = new System.Drawing.Font("Consolas", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Tetris_Button.ForeColor = System.Drawing.Color.White;
            this.Tetris_Button.Location = new System.Drawing.Point(1009, 595);
            this.Tetris_Button.Name = "Tetris_Button";
            this.Tetris_Button.Size = new System.Drawing.Size(188, 69);
            this.Tetris_Button.TabIndex = 1;
            this.Tetris_Button.Text = "Tetris";
            this.Tetris_Button.UseVisualStyleBackColor = false;
            this.Tetris_Button.Click += new System.EventHandler(this.Tetris_Button_Click);
            this.Tetris_Button.MouseLeave += new System.EventHandler(this.Tetris_Button_MouseLeave);
            this.Tetris_Button.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Tetris_Button_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(1);
            this.pictureBox1.Size = new System.Drawing.Size(500, 400);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(858, 58);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(500, 400);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // Main_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1406, 781);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Tetris_Button);
            this.Controls.Add(this.Snake_Button);
            this.MaximizeBox = false;
            this.Name = "Main_Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Games";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Snake_Button;
        private System.Windows.Forms.Button Tetris_Button;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

