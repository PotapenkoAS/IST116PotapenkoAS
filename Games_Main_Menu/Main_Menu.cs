using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Games_Main_Menu
{
    public partial class Main_Menu : Form
    {
        public Main_Menu()
        {
            InitializeComponent();
        }
 
        private void Tetris_Button_Click(object sender, EventArgs e) //Tetris
        {
            Tetris.Tetris_Main_Form tetrisForm = new Tetris.Tetris_Main_Form();
            tetrisForm.Show(this);
            this.Visible = false;
        }
        private void Snake_Button_Click(object sender, EventArgs e) //Snake
        {
            My_Snake.Snake_Program snakeForm = new My_Snake.Snake_Program();
            snakeForm.Show(this);
            My_Snake.Snake_Program.Restart();
            this.Visible = false;
        }

        private void Tetris_Button_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox2.Visible = true;
        }

        private void Tetris_Button_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
        }

        private void Snake_Button_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Visible = true;
        }

        private void Snake_Button_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }
    }
}
