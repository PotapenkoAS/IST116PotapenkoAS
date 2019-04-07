using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_Snake
{

    public class Snake_Program : Form
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Start_Snake()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public class Coord
        {
            public int x;
            public int y;
            public Coord(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        static List<Coord> snake = new List<Coord>();
        static Random random = new Random();
        static int W = 80;
        static int H = 60;
        static int S = 10;

        static Coord apple = new Coord(random.Next(W - 5), random.Next(H - 5));
        static int apples = 0;
        static int score = 0;
        static int level = 1;
        static int way = 0; //0 -up  1-right 2-down 3-left 
        public static Timer timer = new Timer();
        public Snake_Program()
        {
           // Image img = new Bitmap(@"C:\Users\AtApse\Desktop\Temp\harold.jpg");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Snake_Program));
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // this.BackgroundImage = (img);
            this.BackColor = Color.Black;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Snake";
            this.ResumeLayout(false);
            int caption_size = SystemInformation.CaptionHeight; // высота шапки формы
            int frame_size = SystemInformation.FrameBorderSize.Height; // ширина границы формы
            int W = 80; int H = 60; int S = 10;
            this.Size = new Size(W * S + 2 * frame_size, H * S + caption_size + 2 * frame_size);
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            snake = new List<Coord>
            {
                new Coord(W / 2, H - 3),
                new Coord(W / 2, H - 2),
                new Coord(W / 2, H - 1)
            };
            this.KeyDown += new KeyEventHandler(Movement);
            this.Paint += new PaintEventHandler(Painter);
            this.FormClosing += Snake_Program_Form_Closing;
        }

        private void Snake_Program_Form_Closing(object sender, EventArgs e)
        {
            this.Owner.Visible = true;
            timer.Enabled = false;
        }

        public void Painter(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle(apple.x * S, apple.y * S, S, S));
            e.Graphics.FillRectangle(Brushes.Blue, new Rectangle(snake[0].x * S, snake[0].y * S, S, S));
            for (int i = 1; i < snake.Count(); i++)
            {
                e.Graphics.FillRectangle(Brushes.Red, new Rectangle(snake[i].x * S, snake[i].y * S, S, S));
            }
            string alert = "Apples:" + apples.ToString() + "\n" +
               "Level:" + level.ToString() + "\n" + "Scores:" + score.ToString();
            e.Graphics.DrawString(alert, new Font("Arial", 10, FontStyle.Italic), Brushes.White, new Point(5, 5));
        }


        public void Movement(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    if (way != 2)
                        way = 0;
                    break;
                case Keys.Right:
                    if (way != 3)
                        way = 1;
                    break;
                case Keys.Down:
                    if (way != 0)
                        way = 2;
                    break;
                case Keys.Left:
                    if (way != 1)
                        way = 3;
                    break;
            }

            if (e.KeyCode == Keys.P)
            {
                timer.Enabled = false;
                CustomMessageBox messageBox = new CustomMessageBox();
                messageBox.Show(this);
            }

            if (e.KeyCode == Keys.Enter)
            {
                timer.Enabled = true;
            }
        }

        public void timer_Tick(Object sender, EventArgs e)
        {
            bool flag = false;
            int x = snake[0].x, y = snake[0].y;
            for (int i = 1; i < snake.Count; i++)
            {
                if ((x == snake[i].x) && (y == snake[i].y))
                {
                    flag = true;
                    timer.Enabled = false;
                    break;
                }
            }
            if (flag)
            {
                if (MessageBox.Show("You Died", "You lose") == DialogResult.OK)
                {
                    Restart();
                    return;
                }
            }
            switch (way)
            {
                case 0:
                    y--;
                    if (y < 0)
                    {
                        timer.Enabled = false;
                    }
                    break;
                case 1:
                    x++;
                    if (x >= W)
                    {
                        timer.Enabled = false;
                    }
                    break;
                case 2:
                    y++;
                    if (y >= H)
                    {
                        timer.Enabled = false;
                    }
                    break;
                case 3:
                    x--;
                    if (x < 0)
                    {
                        timer.Enabled = false;
                    }
                    break;
            }
            if (timer.Enabled == false)
            {
                if (MessageBox.Show("You Died", "You lose") == DialogResult.OK)
                {
                    Restart();
                    return;
                }
            }
            Coord c = new Coord(x, y);
            snake.Insert(0, c);
            if (snake[0].x == apple.x && snake[0].y == apple.y)
            {
                apple = new Coord(random.Next(W - 5), random.Next(H - 5));
                apples++;
                score += level;
                if (apples % 10 == 0)
                {
                    level++;
                }
                if (timer.Interval > 10)
                {
                    timer.Interval -= 5;
                }
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
            Invalidate();
        }

        public static void Restart()
        {
            apple = new Coord(random.Next(W - 5), random.Next(H - 5));
            apples = 0;
            score = 0;
            level = 1;
            way = 0;
            snake = new List<Coord>
            {
                new Coord(W / 2, H - 3),
                new Coord(W / 2, H - 2),
                new Coord(W / 2, H - 1)
            };
            timer.Interval = 100;
            timer.Enabled = true;
        }
    }
}