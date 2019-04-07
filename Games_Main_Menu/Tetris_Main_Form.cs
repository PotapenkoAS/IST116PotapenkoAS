using Games_Main_Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Tetris_Main_Form : Form
    {
        static int S = 40;
        static int H = 20;
        static int W = 10;
        Timer paint_timer = new Timer();
        public Figure active_figure;
        List<List<MatrixType>> mat = new List<List<MatrixType>>();
        List<MatrixType> tmpList = new List<MatrixType>();

        int figuresNumber = 0; //5 points
        int level = 1; //each 100 score points +1 and speedup
        int score = 0;
        int deletedRows = 0; //25 points

        public Tetris_Main_Form()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tetris_Main_Form));
            this.SuspendLayout();
            // 
            // Tetris_Main_Form
            // 

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            // this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackColor = Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 1200);
            this.Size = new Size(800, 1247);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Tetris_Main_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.ResumeLayout(false);
            paint_timer.Interval = 500;
            paint_timer.Tick += Paint_timer_Tick;
            paint_timer.Start();
            this.Paint += new PaintEventHandler(Painter);
            this.KeyDown += new KeyEventHandler(Key_Controls);
            this.KeyUp += new KeyEventHandler(Space_Control);
            active_figure = GetNewFigure();
            this.FormClosing += Tetris_Main_Form_FormClosing;

            for (int j = 0; j < H; j++)
            {
                for (int i = 0; i < W; i++)
                {
                    tmpList.Add(new MatrixType(false));
                }
                mat.Add(tmpList);
                tmpList = new List<MatrixType>();
            }
        }

        private void Tetris_Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Visible = true;
        }

        public void Paint_timer_Tick(object sender, EventArgs e)
        {
            bool flag = false;
            int MinY = H - 1;
            foreach (Block_Coord el in active_figure.coords)
            {
                if (MinY > el.Y)
                {
                    MinY = el.Y;
                }
                if (el.Y + 1 == H || mat[el.Y + 1][el.X].value)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                MatrixValidate(MinY);
                active_figure = GetNewFigure();
                figuresNumber++;
                score = 5 * figuresNumber + 25 * deletedRows;
                if (score - level * 100 >= 0)
                {
                    level++;
                    if (level > 13 && level <= 22)
                    {
                        paint_timer.Interval = 10 - (level - 13);
                    }
                    if (level <= 13 && level > 9)
                    {
                        paint_timer.Interval = 50 - (10 * (level - 9));
                    }
                    if (level <= 9)
                    {
                        paint_timer.Interval = 500 - 50 * level;
                    }
                    //500-450 on 9 lvl                  500 450 400 350...50 
                    //50-(10*level-9) on levels 9-13    50 40 30 20 10
                    //10-(lvl-13) on levels 13-22       10 9 8 7 6 5 4 3 2 1 
                }
            }
            else
            {
                foreach (Block_Coord el in active_figure.coords)
                {
                    el.Y++;
                }
            }
            Invalidate();
        }

        public Figure GetNewFigure()
        {
            Random rnd = new Random();
            int i = rnd.Next(100);
            if (i >= 0 && i < 20)
                return new Figure_I(new Block_Coord(W / 2, 0));
            else if (i > 20 && i < 35)
                return new Figure_L(new Block_Coord(W / 2, 0));

            else if (i > 35 && i < 50)
                return new Figure_J(new Block_Coord(W / 2, 0));

            else if (i > 50 && i < 60)
                return new Figure_S(new Block_Coord(W / 2, 0));

            else if (i > 60 && i < 70)
                return new Figure_Z(new Block_Coord(W / 2, 0));

            else if (i > 80 && i < 90)
                return new Figure_T(new Block_Coord(W / 2, 0));
            else
                return new Figure_O(new Block_Coord(W / 2, 0));
        }

        public void MatrixValidate(int MinY)
        {
            foreach (Block_Coord el in active_figure.coords)
            {
                mat[el.Y][el.X].value = true;
                mat[el.Y][el.X].color = active_figure.brush;

                if (el.Y == 1)
                {
                    paint_timer.Enabled = false;
                    if (MessageBox.Show("You are loser with : " + score + " points", "You Lose!", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    {

                        foreach (List<MatrixType> list in mat)
                        {
                            foreach (MatrixType item in list)
                            {
                                item.value = false;
                            }
                        }
                        paint_timer.Enabled = true;
                        paint_timer.Interval = 500;
                        level = 1;
                        score = 0;
                        deletedRows = 0;
                        figuresNumber = -1;
                        active_figure = GetNewFigure();
                        break;
                    }
                    else
                    {
                        this.Owner.Visible = true;
                        this.Close();
                    }
                }
            }
            for (int j = H - 1; j >= MinY; j--)
            {
                bool flag = true;
                for (int i = 0; i < W; i++)
                {
                    if (!mat[j][i].value)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    deletedRows++;
                    mat.RemoveAt(j);
                    for (int i = 0; i < W; i++)
                    {
                        tmpList.Add(new MatrixType(false));
                    }
                    mat.Insert(0, tmpList);
                    tmpList = new List<MatrixType>();
                    j++;
                }
            }
        }


        public void Painter(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.White, new Point(W * S, 0), new Point(W * S, H * S));
            e.Graphics.DrawString("Scores: " + score, Font, Brushes.White, new Point(W * S + 50, S));
            e.Graphics.DrawString("Level: " + level, Font, Brushes.White, new Point(W * S + 50, S * 2));
            e.Graphics.DrawString("Deleted Rows: " + deletedRows, Font, Brushes.White, new Point(W * S + 50, S * 3));
            e.Graphics.DrawString("Speed: " + (500 - paint_timer.Interval), Font, Brushes.White, new Point(W * S + 50, S * 4));
            foreach (Block_Coord el in active_figure.coords)
            {
                e.Graphics.FillRectangle(active_figure.brush, new Rectangle(el.X * S, el.Y * S, S, S));
                e.Graphics.DrawRectangle(Pens.Black, new Rectangle(el.X * S, el.Y * S, S, S));
            }
            for (int j = 0; j < H; j++)
            {
                for (int i = 0; i < W; i++)
                    if (mat[j][i].value)
                    {
                        e.Graphics.FillRectangle(mat[j][i].color, new Rectangle(i * S, j * S, S, S));
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle(i * S, j * S, S, S));
                    }
            }
        }


        public void Key_Controls(object sender, KeyEventArgs e)
        {
            bool flag = false;
            bool flagLeft = false;
            bool flagRight = false;

            switch (e.KeyData)
            {
                case Keys.A:
                case Keys.Left:
                    {
                        foreach (Block_Coord el in active_figure.coords)
                        {
                            if (el.X - 1 < 0 || mat[el.Y][el.X - 1].value)
                            {
                                flag = true;
                            }
                        }
                        if (flag) break;
                        foreach (Block_Coord el in active_figure.coords)
                        {
                            el.X -= 1;
                        }
                        break;
                    }
                case Keys.D:
                case Keys.Right:
                    {
                        foreach (Block_Coord el in active_figure.coords)
                        {
                            if (el.X + 1 > W - 1 || mat[el.Y][el.X + 1].value)
                            {
                                flag = true;
                            }
                        }
                        if (flag) break;
                        foreach (Block_Coord crd in active_figure.coords)
                        {
                            crd.X += 1;
                        }
                        break;
                    }
                case Keys.Q:
                case Keys.Up:
                    {
                        foreach (Block_Coord el in active_figure.coords)
                        {
                            if (el.Y == H - 1)
                                return;
                        }

                        if (active_figure.GetType().ToString() == "Tetris.Figure_I")
                        {
                            if (active_figure.coords[3].Y - active_figure.coords[0].Y > 0)
                            {
                                if (active_figure.coords[0].X - 2 < 0 || active_figure.coords[3].X - 2 < 0)
                                {
                                    foreach (Block_Coord el in active_figure.coords)
                                    {
                                        el.X += 2;
                                    }
                                }
                                if (active_figure.coords[0].X + 1 > W - 1 || active_figure.coords[3].X + 1 > W - 1)
                                {
                                    foreach (Block_Coord el in active_figure.coords)
                                    {
                                        el.X--;
                                    }
                                }
                                active_figure.coords[0].X -= 2;
                                active_figure.coords[0].Y += 1;

                                active_figure.coords[1].X -= 1;

                                active_figure.coords[2].Y -= 1;

                                active_figure.coords[3].X += 1;
                                active_figure.coords[3].Y -= 2;
                            }
                            else
                            {
                                active_figure.coords[0].X += 2;
                                active_figure.coords[0].Y -= 1;

                                active_figure.coords[1].X += 1;

                                active_figure.coords[2].Y += 1;

                                active_figure.coords[3].X -= 1;
                                active_figure.coords[3].Y += 2;
                            }
                        }
                        else
                        {
                            foreach (Block_Coord el in active_figure.coords)
                            {
                                if (el.X - 1 < 0) flagLeft = true;
                                if (el.X + 1 > W - 1) flagRight = true;
                            }
                            if (flagLeft)
                            {
                                foreach (Block_Coord el in active_figure.coords)
                                {
                                    el.X++;
                                }
                            }
                            if (flagRight)
                            {
                                foreach (Block_Coord el in active_figure.coords)
                                {
                                    el.X--;
                                }
                            }
                            active_figure.Rotate(true);
                        }

                        break;
                    }
                case Keys.E:
                case Keys.Down:
                    {
                        foreach (Block_Coord el in active_figure.coords)
                        {
                            if (el.Y == H - 1)
                                return;
                        }
                        if (active_figure.GetType().ToString() == "Tetris.Figure_I")
                        {


                            if (active_figure.coords[3].Y - active_figure.coords[0].Y == 0)
                            {


                                active_figure.coords[0].X += 2;
                                active_figure.coords[0].Y -= 1;

                                active_figure.coords[1].X += 1;

                                active_figure.coords[2].Y += 1;

                                active_figure.coords[3].X -= 1;
                                active_figure.coords[3].Y += 2;
                            }
                            else
                            {
                                if (active_figure.coords[0].X + 1 > W - 1 || active_figure.coords[3].X + 1 > W - 1)
                                {
                                    foreach (Block_Coord el in active_figure.coords)
                                    {
                                        el.X--;
                                    }
                                }
                                if (active_figure.coords[0].X - 1 < 0 || active_figure.coords[3].X - 1 < 0)
                                {
                                    foreach (Block_Coord el in active_figure.coords)
                                    {
                                        el.X += 2;
                                    }
                                }

                                active_figure.coords[0].X -= 2;
                                active_figure.coords[0].Y += 1;

                                active_figure.coords[1].X -= 1;

                                active_figure.coords[2].Y -= 1;

                                active_figure.coords[3].X += 1;
                                active_figure.coords[3].Y -= 2;
                            }
                        }
                        else
                        {
                            foreach (Block_Coord el in active_figure.coords)
                            {
                                if (el.X - 1 < 0) flagLeft = true;
                                if (el.X + 1 > W - 1) flagRight = true;
                            }
                            if (flagLeft)
                            {
                                foreach (Block_Coord el in active_figure.coords)
                                {
                                    el.X++;
                                }
                            }
                            if (flagRight)
                            {
                                foreach (Block_Coord el in active_figure.coords)
                                {
                                    el.X--;
                                }
                            }
                            active_figure.Rotate(false);

                        }
                        break;
                    }
                case Keys.Space:
                    {
                        paint_timer.Interval = 5;
                        break;
                    }
                default: break;
            }
            Invalidate();
        }

        public void Space_Control(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                if (level > 13 && level <= 22)
                {
                    paint_timer.Interval = 10 - (level - 13);
                }
                if (level <= 13 && level > 9)
                {
                    paint_timer.Interval = 50 - (10 * (level - 9));
                }
                if (level <= 9)
                {
                    paint_timer.Interval = 500 - 50 * level;
                }
            }
        }
    }
}

