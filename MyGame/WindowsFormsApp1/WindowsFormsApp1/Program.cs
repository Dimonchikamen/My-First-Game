using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    { 
        static void Main()
        {
            var game = new GameModel(10);
            Application.Run(new MyForm(game) { ClientSize = new Size(500, 500) });
        }
    }
}
