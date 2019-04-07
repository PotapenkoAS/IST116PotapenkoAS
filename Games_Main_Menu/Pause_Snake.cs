using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Snake
{
    public class CustomMessageBox : System.Windows.Forms.Form
    {
        Label message = new Label();
        Button b1 = new Button();
        Button b2 = new Button();
        Button b3 = new Button();

        public CustomMessageBox()
        {
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Text = "Pause";

            b1.Location = new System.Drawing.Point(211, 112);
            b1.Size = new System.Drawing.Size(75, 23);
            b1.Text = "Retry";
            b1.BackColor = Control.DefaultBackColor;
            b1.Click += new EventHandler(Retry_Click);

            b2.Location = new System.Drawing.Point(98, 80);
            b2.Size = new System.Drawing.Size(100, 50);
            b2.Text = "Continue";
            b2.BackColor = Control.DefaultBackColor;
            b2.Click +=new EventHandler(ContinueMet);

            b3.Location = new System.Drawing.Point(11, 112);
            b3.Size = new System.Drawing.Size(75, 23);
            b3.Text = "Main menu";
            b3.BackColor = Control.DefaultBackColor;
            b3.Click += new EventHandler(Main_Menu_Click);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.ShowIcon = false;

            this.Controls.Add(b1);
            this.Controls.Add(b2);
            this.Controls.Add(b3);
            this.Controls.Add(message);
        }

        public void ContinueMet(object sender, EventArgs e)
        {
            Snake_Program.timer.Enabled = true;
            this.Dispose();
        }

        public void Main_Menu_Click(object sender,EventArgs e)
        {
            this.Owner.Owner.Visible = true;
            this.Owner.Close();
            this.Dispose();
        }

        public void Retry_Click(object sender, EventArgs e)
        {
            Snake_Program.Restart();
            this.Dispose();
        }
    }
}