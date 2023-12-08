using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_Mids
{
    internal class DLACluster
    {
        int size = 100;
        int[,] cluster;
        float dx, dy, prob;
        int x, y;
        bool check = false;

        Random obj = new Random();
        Graphics gg;
        SolidBrush sb = new SolidBrush(Color.Red);
        Pen pn = new Pen(Color.Black);

        public DLACluster(Form1 frm, int flag)
        {
            gg = frm.textBox3.CreateGraphics();
            dx = frm.textBox3.Width / (2 * size);
            dy = frm.textBox3.Height / (2 * size);

            cluster = new int[2 * size + 1, 2 * size + 1];

            for (int i = 0; i < (2 * size + 1); i++)
            {
                for (int j = 0; j < (2 * size + 1); j++)
                {
                    cluster[i, j] = 0;
                    gg.DrawEllipse(pn, i * dx, j * dy, 5, 5);
                    if(flag == 0)
                    {
                        if(i == size && j == size)
                        {
                            cluster[i, j] = 1;
                            gg.FillEllipse(sb, i * dx, j * dy, 5, 5);
                        }
                    }
                    if (flag == 1)
                    {
                        if (i == size)
                        {
                            cluster[i, j] = 1;
                            gg.FillEllipse(sb, i * dx, j * dy, 5, 5);
                        }
                    }
                    if (flag == 2)
                    {
                        if (j == size)
                        {
                            cluster[i, j] = 1;
                            gg.FillEllipse(sb, i * dx, j * dy, 5, 5);
                        }
                    }
                    if (flag == 3)
                    {
                        int temp1 = i - size;
                        int temp2 = j - size;
                        if (Math.Abs((temp1 * temp1)+(temp2 * temp2)-1000) <  0.1)
                        {
                            cluster[i, j] = 1;
                            gg.FillEllipse(sb, i * dx, j * dy, 5, 5);
                        }
                    }
                }
            }
        }
        public void Growth(bool filled)
        {
            while(!filled)
            {
                for (int i = 0; i < (2 * size); i++)
                {
                    for (int j = 0; j < (2 * size); j++)
                    {
                        x = obj.Next(1, 2 * size);
                        y = obj.Next(1, 2 * size);
                        if (cluster[x, y] == 0)
                        {
                            if(decide(x, y))
                            {
                                cluster[x, y] = 1;
                                gg.FillEllipse(sb, x * dx, y * dy, 5, 5);
                            }
                            else
                            {
                                while(!decide(x, y))
                                {
                                    prob = (float)obj.NextDouble();
                                    if(prob < 0.25)
                                    {
                                        if(x < 2 * size - 1)
                                        {
                                            x++;
                                        }
                                    }
                                    if(prob >= 0.25 && prob < 0.5)
                                    {
                                        if(x > 1)
                                        {
                                            x--;
                                        }
                                    }
                                    if(prob >= 0.5 && prob < 0.75)
                                    {
                                        if(y < 2 * size - 1)
                                        {
                                            y++;
                                        }
                                    }
                                    if(prob >= 0.75)
                                    {
                                        if(y > 1)
                                        {
                                            y--;
                                        }
                                    }
                                }
                            }
                            cluster[x, y] = 1;
                            gg.FillEllipse(sb, x * dx, y * dy, 5, 5);
                        }
                    }
                }
            }
        }
        public bool decide(int x, int y)
        {
            if (cluster[x - 1, y] == 1)
            {
                check = true;
            }
            else if (cluster[x + 1, y] == 1)
            {
                check = true;
            }
            else if (cluster[x, y - 1] == 1)
            {
                check = true;
            }
            else if (cluster[x, y + 1] == 1)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            return check;
        }
    }
}
