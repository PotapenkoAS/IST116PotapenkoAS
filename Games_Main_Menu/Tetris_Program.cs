using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    static class Tetris_Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        public static void StartTetris()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
          
        }
    }
}
