using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SudokuByPoltavets
{
    public partial class Form1 : Form
    {
        const int n = 3;
        const int BSize = 50;
        public int[,] map = new int[n * n, n * n];
        public Button[,] buttons = new Button[n * n, n * n];
        public Form1()
        {
            InitializeComponent();
            MapGen();
        }

        public void MapGen()
        {
            for(int i = 0; i < n * n; i++) 
            {
                for(int j = 0; j < n * n; j++) 
                {
                    map[i, j] = (i * n + i / n + j) % (n * n) + 1;
                    buttons[i, j] = new Button();
                }
            }
            //MatrixTransp();
            //RowSwap();
            //ColumSwap();
            //SwapBlockR();
            //SwapBlockC();
            Random r = new Random();
            for (int i = 0; i < 40; i++)
            {
                Shuffle(r.Next(0, 5));
            }
            MapC();
            HideC();
        }

        public void RowSwap()
        {
            Random r = new Random();
            var block = r.Next(0, n);
            var row1 = r.Next(0, n);
            var line1 = block * n + row1;
            var row2 = r.Next(0, n);
            while (row1 == row2)
                row2 = r.Next(0, n);
            var line2 = block * n + row2;
            for(int i=0; i < n * n; i++) 
            {
                var temp = map[line1, i];
                map[line1, i] = map[line2,i];
                map[line2, i] = temp;
            }
        }

        public void SwapBlockR() 
        {
            Random r = new Random();
            var block1 = r.Next(0, n);
            var block2 = r.Next(0, n);
            while(block1==block2)
                block2=r.Next(0, n);
            block1 *= n;
            block2 *= n;
            for (int i = 0; i < n * n; i++)
            {
                var k = block2;
                for (int j = block1; j < block1 + n; j++)
                {
                    var temp = map[j, i];
                    map[j,i] = map[k,j];
                    map[k,j] = temp;
                    k++;
                }
            }
        }
        public void SwapBlockC()
        {
            Random r = new Random();
            var block1 = r.Next(0, n);
            var block2 = r.Next(0, n);
            while (block1 == block2)
                block2 = r.Next(0, n);
            block1 *= n;
            block2 *= n;
            for (int i = 0; i < n * n; i++)
            {
                var k = block2;
                for (int j = block1; j < block1 + n; j++)
                {
                    var temp = map[i,j];
                    map[i,j] = map[i,k];
                    map[i,k] = temp;
                    k++;
                }
            }
        }
        public void ColumSwap()
        {
            Random r = new Random();
            var block = r.Next(0, n);
            var row1 = r.Next(0, n);
            var line1 = block * n + row1;
            var row2 = r.Next(0, n);
            while (row1 == row2)
                row2 = r.Next(0, n);
            var line2 = block * n + row2;
            for (int i = 0; i < n * n; i++)
            {
                var temp = map[i,line1];
                map[i,line1] = map[i,line2];
                map[i,line2]= temp;
            }
        }
        public void MatrixTransp()
        {
            int[,] Mapt = new int[n * n, n * n];
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    Mapt[i, j] = map[j, i];
                }
            }
            map = Mapt;
        }
        public void MapC()
        {
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    Button button =new Button();
                    button.Size = new Size(BSize, BSize);
                    button.Text = map[i,j].ToString();
                    button.Location = new Point(j*BSize, i*BSize);
                    this.Controls.Add(button);
                }
            }
        }
        public void HideC()
        {
            int N = 40;
            Random r = new Random();
            while (N > 0)
            {
                for (int i = 0; i < n * n; i++)
                {
                    for (int j = 0; j < n * n; j++)
                    {
                        if (!string.IsNullOrEmpty(buttons[i, j].Text))
                        {
                            int a = r.Next(0, 3);
                            buttons[i, j].Text = a == 0 ? "" : buttons[i, j].Text;
                            buttons[i, j].Enabled = a == 0 ? true : false;

                            if (a == 0)
                                N--;
                            if (N <= 0)
                                break;
                        }
                    }
                    if (N <= 0)
                        break;
                }
            }
        }
        public void Shuffle(int i)
        {
            switch (i)
            {
                case 0:
                    MatrixTransp();
                    break;
                case 1:
                    RowSwap();
                    break;
                case 2:
                    ColumSwap();
                    break;
                case 3:
                    SwapBlockR();
                    break;
                case 4:
                    SwapBlockC();
                    break;
                default:
                    MatrixTransp();
                    break;
            }
        }
        public void CellPre(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            string buttonText = pressedButton.Text;
            if (string.IsNullOrEmpty(buttonText))
            {
                pressedButton.Text = "1";
            }
            else
            {
                int num = int.Parse(buttonText);
                num++;
                if (num == 10)
                    num = 1;
                pressedButton.Text = num.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    var btnText = buttons[i, j].Text;
                    if (btnText != map[i, j].ToString())
                    {
                        MessageBox.Show("Неправильно!");
                        return;
                    }
                }
            }
            MessageBox.Show("Правильно!");
            for (int i = 0; i < n * n; i++)
            {
                for (int j = 0; j < n * n; j++)
                {
                    this.Controls.Remove(buttons[i, j]);
                }
            }
            MapGen();
        }
    }
}
