using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{

    public class MatrixType
    {
        public bool value;
        public Brush color;

        public MatrixType(bool val)
        {
            value = val;
        }
    }

    public class Block_Coord
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool WasRotated { get; set; }

        public Block_Coord(int x, int y)
        {
            X = x;
            Y = y;
        }


        public bool IsEqual(Block_Coord el)
        {
            if (X == el.X && Y == el.Y)
            {
                return true;
            }
            return false;
        }
    }


    public abstract class Figure
    {
        public List<Block_Coord> coords = new List<Block_Coord>();
        public int centerID;
        public Brush brush;

        public Block_Coord FindLike(Block_Coord coord)
        {
            foreach (Block_Coord el in coords)
            {
                if (coord.X == el.X && coord.Y == el.Y)
                {
                    return el;
                }
                return null;
            }
            return null;
        }

        public void Rotate(bool flag) // true=left false =right
        {
            foreach (Block_Coord el in coords)
            {
                if (!el.WasRotated)
                {
                    if (flag)
                    {
                        RotateLeft(Calc(el), el);
                    }
                    else
                    {
                        RotateRight(Calc(el), el);
                    }
                }
            }
        }

        public void RotateRight(int i, Block_Coord el)
        {
            if (i % 2 == 0)
            {
                switch (i)
                {
                    case 2:
                        {
                            var tmp = FindLike(new Block_Coord(el.X + 1, el.Y + 1));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X += 1;
                            el.Y += 1;
                            el.WasRotated = true;
                            break;
                        }
                    case 4:
                        {
                            var tmp = FindLike(new Block_Coord(el.X + 1, el.Y - 1));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X += 1;
                            el.Y -= 1;
                            el.WasRotated = true;
                            break;
                        }
                    case 6:
                        {
                            var tmp = FindLike(new Block_Coord(el.X - 1, el.Y + 1));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X -= 1;
                            el.Y += 1;
                            el.WasRotated = true;
                            break;
                        }
                    case 8:
                        {
                            var tmp = FindLike(new Block_Coord(el.X - 1, el.Y - 1));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X -= 1;
                            el.Y -= 1;
                            el.WasRotated = true;
                            break;
                        }
                    default: break;
                }
            }
            else
            {
                switch (i)
                {
                    case 1:
                        {
                            var tmp = FindLike(new Block_Coord(el.X + 2, el.Y));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X += 2;
                            el.WasRotated = true;
                            break;
                        }
                    case 3:
                        {
                            var tmp = FindLike(new Block_Coord(el.X, el.Y + 2));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.Y += 2;
                            el.WasRotated = true;
                            break;
                        }
                    case 7:
                        {
                            var tmp = FindLike(new Block_Coord(el.X, el.Y - 2));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.Y -= 2;
                            el.WasRotated = true;
                            break;
                        }
                    case 9:
                        {
                            var tmp = FindLike(new Block_Coord(el.X - 2, el.Y));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X -= 2;
                            el.WasRotated = true;
                            break;
                        }
                    default:
                        break;
                }
            }
            foreach (Block_Coord element in coords)
            {
                element.WasRotated = false;
            }
        }

        public void RotateLeft(int i, Block_Coord el)
        {
            if (i % 2 == 0)
            {
                switch (i)
                {
                    case 2:
                        {
                            var tmp = FindLike(new Block_Coord(el.X - 1, el.Y + 1));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X -= 1;
                            el.Y += 1;
                            el.WasRotated = true;
                            break;
                        }
                    case 4:
                        {
                            var tmp = FindLike(new Block_Coord(el.X + 1, el.Y + 1));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X += 1;
                            el.Y += 1;
                            el.WasRotated = true;
                            break;
                        }
                    case 6:
                        {
                            var tmp = FindLike(new Block_Coord(el.X - 1, el.Y - 1));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X -= 1;
                            el.Y -= 1;
                            el.WasRotated = true;
                            break;
                        }
                    case 8:
                        {
                            var tmp = FindLike(new Block_Coord(el.X + 1, el.Y - 1));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X += 1;
                            el.Y -= 1;
                            el.WasRotated = true;
                            break;
                        }


                    default: break;
                }


            }
            else
            {
                switch (i)
                {
                    case 1:
                        {
                            var tmp = FindLike(new Block_Coord(el.X, el.Y + 2));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.Y += 2;
                            el.WasRotated = true;
                            break;
                        }
                    case 3:
                        {
                            var tmp = FindLike(new Block_Coord(el.X - 2, el.Y));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X -= 2;
                            el.WasRotated = true;
                            break;
                        }
                    case 7:
                        {
                            var tmp = FindLike(new Block_Coord(el.X + 2, el.Y));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.X += 2;
                            el.WasRotated = true;
                            break;
                        }
                    case 9:
                        {
                            var tmp = FindLike(new Block_Coord(el.X, el.Y - 2));
                            if (tmp != null)
                            {
                                RotateRight(Calc(tmp), tmp);
                            }
                            el.Y -= 2;
                            el.WasRotated = true;
                            break;
                        }
                    default:
                        break;
                }
            }
            foreach (Block_Coord element in coords)
            {
                element.WasRotated = false;
            }
        }

        protected int Calc(Block_Coord el)
        {
            int xSlip = el.X - coords[centerID].X;
            int ySlip = el.Y - coords[centerID].Y;
            return ((ySlip + 1) * 3 + xSlip + 2);
        }

    }

    public class Figure_I : Figure
    {
        public Figure_I(Block_Coord top)
        {
            Block_Coord top1 = new Block_Coord(top.X, top.Y + 1);
            Block_Coord bot1 = new Block_Coord(top.X, top.Y + 2);
            Block_Coord bot = new Block_Coord(top.X, top.Y + 3);
            coords.Add(top);
            coords.Add(top1);
            coords.Add(bot1);
            coords.Add(bot);
            brush = Brushes.HotPink;
        }
    }


    public class Figure_J : Figure
    {
        public Figure_J(Block_Coord top)
        {
            Block_Coord top1 = new Block_Coord(top.X, top.Y + 1);
            Block_Coord bot1 = new Block_Coord(top.X, top.Y + 2);
            Block_Coord bot = new Block_Coord(top.X - 1, top.Y + 2);
            coords.Add(top);
            coords.Add(top1);
            coords.Add(bot1);
            coords.Add(bot);
            centerID = 1;
            brush = Brushes.CornflowerBlue;
        }
    }

    public class Figure_L : Figure
    {
        public Figure_L(Block_Coord top)
        {
            Block_Coord top1 = new Block_Coord(top.X, top.Y + 1);
            Block_Coord bot1 = new Block_Coord(top.X, top.Y + 2);
            Block_Coord bot = new Block_Coord(top.X + 1, top.Y + 2);
            coords.Add(top);
            coords.Add(top1);
            coords.Add(bot1);
            coords.Add(bot);
            centerID = 1;
            brush = Brushes.Plum;
        }
    }

    public class Figure_S : Figure
    {
        public Figure_S(Block_Coord center_top)
        {
            Block_Coord center_bot = new Block_Coord(center_top.X, center_top.Y + 1);
            Block_Coord top_right = new Block_Coord(center_top.X + 1, center_top.Y);
            Block_Coord bot_left = new Block_Coord(center_top.X - 1, center_top.Y + 1);
            coords.Add(center_top);
            coords.Add(center_bot);
            coords.Add(top_right);
            coords.Add(bot_left);
            centerID = 0;
            brush = Brushes.Aqua;
        }
    }

    public class Figure_T : Figure
    {
        public Figure_T(Block_Coord center)
        {
            Block_Coord left = new Block_Coord(center.X - 1, center.Y);
            Block_Coord right = new Block_Coord(center.X + 1, center.Y);
            Block_Coord bot = new Block_Coord(center.X, center.Y + 1);
            coords.Add(center);
            coords.Add(left);
            coords.Add(right);
            coords.Add(bot);
            centerID = 0;
            brush = Brushes.Violet;
        }
    }

    public class Figure_Z : Figure
    {
        public Figure_Z(Block_Coord center_top)
        {
            Block_Coord center_bot = new Block_Coord(center_top.X, center_top.Y + 1);
            Block_Coord top_left = new Block_Coord(center_top.X - 1, center_top.Y);
            Block_Coord bot_right = new Block_Coord(center_top.X + 1, center_top.Y + 1);
            coords.Add(center_top);
            coords.Add(center_bot);
            coords.Add(top_left);
            coords.Add(bot_right);
            centerID = 0;
            brush = Brushes.Peru;
        }

    }
    public class Figure_O : Figure
    {
        public Figure_O(Block_Coord top_left)
        {
            Block_Coord top_right = new Block_Coord(top_left.X + 1, top_left.Y);
            Block_Coord bot_left = new Block_Coord(top_left.X, top_left.Y + 1);
            Block_Coord bot_right = new Block_Coord(top_left.X + 1, top_left.Y + 1);
            coords.Add(top_left);
            coords.Add(top_right);
            coords.Add(bot_left);
            coords.Add(bot_right);
            centerID = 0;
            brush = Brushes.GreenYellow;

        }
    }
}
