using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApp1
{
    public class GameModel
    {
        public int[,] game { get; private set; }
        public int Size { get; }
        public event Action<int, int, int> StateChanged;
        public GameModel(int size)
        {
            game = new int[size, size];
            Size = size;
        }
        public void Start()
        {
            var random = new Random();
            for (var column = 0; column < Size; column++)
                for (var row = 0; row < Size; row++)
                    game[column, row] = random.Next(0, 5);
        }

        public void Step(int column, int row)
        {
            var startColorNumber = game[column, row];
            var passedPositions = new HashSet<Tuple<int, int>>();
            var queue = new Queue<Tuple<int,int>>();
            var randomColorNumber = new Random().Next(0, 5);
            queue.Enqueue(new Tuple<int, int>(column,row));
            while (queue.Count != 0)
            {
                var position = queue.Dequeue();
                if (passedPositions.Contains(position))
                    continue;
                if (!InBounds(position.Item1, position.Item2))
                    continue;
                if (startColorNumber != game[position.Item1,position.Item2])
                    continue;
                game[position.Item1, position.Item2] = randomColorNumber;
                SetChanges(position.Item1, position.Item2, randomColorNumber);
                passedPositions.Add(position);

                for (var dx = -1; dx <= 1; dx++)
                    for (var dy = -1; dy <= 1; dy++)
                        if (dx != 0 && dy != 0)
                            continue;
                        else
                            queue.Enqueue(new Tuple<int, int>(position.Item1 + dx, position.Item2 + dy));
            }
        }

        public Color GetColor(int column, int row)
        {
            var colorNumber = game[column, row];
            return GetColor(colorNumber);
        }

        public Color GetRandomColor()
        {
            var random = new Random().Next(0, 5);
            return GetColor(random);
        }

        public Color GetColor(int num)
        {
            switch (num)
            {
                case 0:
                    return Color.Gray;
                case 1:
                    return Color.Yellow;
                case 2:
                    return Color.Red;
                case 3:
                    return Color.BlueViolet;
                case 4:
                    return Color.Green;
            }
            return Color.White;
        }

        private bool InBounds(int column, int row)
        {
            if (column < 0 || column >= game.GetLength(0) || row < 0 || row >= game.GetLength(1))
                return false;
            return true;
        }

        private void SetChanges(int column, int row, int colorNumber)
        {
            StateChanged(column, row, colorNumber);
        }
    }
}
