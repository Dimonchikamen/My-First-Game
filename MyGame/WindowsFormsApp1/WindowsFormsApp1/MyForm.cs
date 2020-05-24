using System;
using System.Collections.Generic;
using System.Drawing.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp1
{
    class MyForm : Form
    {
        readonly TableLayoutPanel table;

        public MyForm(GameModel game)
        {
            game.Start();
            table = new TableLayoutPanel();
            for (var i = 0; i < game.Size; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / game.Size));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / game.Size));
            }

            for(var column = 0; column < game.Size; column++)
                for(var row = 0; row < game.Size; row++)
                {
                    var thisColumn = column;
                    var thisRow = row;
                    var button = new Button();
                    button.Dock = DockStyle.Fill;
                    button.Click += (sender, args) => game.Step(thisColumn, thisRow);
                    button.BackColor = game.GetColor(thisColumn, thisRow);
                    table.Controls.Add(button, thisColumn, thisRow);
                }
            table.Dock = DockStyle.Fill;
            Controls.Add(table);
            game.StateChanged += (column, row, colorNumber) => ((Button)table.GetControlFromPosition(column, row)).BackColor = game.GetColor(colorNumber);
        }
    }
}
