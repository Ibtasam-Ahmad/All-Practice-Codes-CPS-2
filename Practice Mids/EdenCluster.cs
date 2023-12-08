using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Mids
{
    internal class EdenCluster
    {
        int size = 100;
        int[,] cluster;
        float dx, dy;
        int x, y;
        bool check = false;

        Random obj = new Random();
        Graphics gg;
        SolidBrush sb = new SolidBrush(Color.Red);
        Pen pn = new Pen(Color.Black);

        public EdenCluster(Form1 frm)
        {
            gg = frm.textBox3.CreateGraphics();
            dx = (float)frm.textBox3.Width / (2 * size);
            dy = (float)frm.textBox3.Height / (2 * size);

            cluster = new int[2 * size + 1, 2 * size + 1];

            for (int i = 0; i < (2 * size + 1); i++)
            {
                for (int j = 0; j < (2 * size + 1); j++)
                {
                    cluster[i, j] = 0;
                    gg.DrawEllipse(pn, i * dx, j * dy, 5, 5);
                    if(i == size && j == size)
                    {
                        cluster[i, j] = 1;
                        gg.FillEllipse(sb, i * dx, j * dx, 5, 5);
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
