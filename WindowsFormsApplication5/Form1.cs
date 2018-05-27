using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labirynt
{
    public partial class Form1 : Form
    {
        Plansza Labirynt1 = new Plansza();
        Graphics g;
        Pen black = new Pen(Color.Black, 2);
        Pen gold = new Pen(Color.Gold, 7);
        Pen red = new Pen(Color.Red, 2);
        Pen brown = new Pen(Color.Brown, 7);
        int l = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // pictureBox1 = new PictureBox();
            pictureBox1.Image = new Bitmap(500, 500);
            g = Graphics.FromImage(pictureBox1.Image);
            label1.Text = "";
            label4.Text = "";
            label6.Text = "";
            label10.Text = "";
            label9.Text = "";
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            l = 0;
            g.Clear(Color.White);
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        Labirynt1.wczytaj(openFileDialog1.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            for (int i = 0; i < Labirynt1.Pola[0].Length; i++)
            {
                g.DrawLine(black, 500 - (i * 500 / Labirynt1.Pola[0].Length), 0, 500 - (i * 500 / Labirynt1.Pola[0].Length), 500);
            }
            for (int i = 0; i < Labirynt1.Pola.Count; i++)
            {
                g.DrawLine(black, 0, 500 - (i * 500 / Labirynt1.Pola.Count), 500, 500 - (i * 500 / Labirynt1.Pola.Count));
            }

            for (int i = 0; i < Labirynt1.Pola[0].Length; i++)
            {
                for (int j = 0; j < Labirynt1.Pola.Count; j++)
                {
                    if (Labirynt1.Pola[j][i] == 'X')
                        g.DrawEllipse(black, (i * 500 / Labirynt1.Pola[0].Length), (j * 500 / Labirynt1.Pola.Count), 500 / Labirynt1.Pola[0].Length, 500 / Labirynt1.Pola.Count);
                    if (Labirynt1.Pola[j][i] == 'S')
                        g.DrawEllipse(gold, (i * 500 / Labirynt1.Pola[0].Length), (j * 500 / Labirynt1.Pola.Count), 500 / Labirynt1.Pola[0].Length, 500 / Labirynt1.Pola.Count);
                    if (Labirynt1.Pola[j][i] == 'W')
                        g.DrawEllipse(brown, (i * 500 / Labirynt1.Pola[0].Length), (j * 500 / Labirynt1.Pola.Count), 500 / Labirynt1.Pola[0].Length, 500 / Labirynt1.Pola.Count);

                }
            }

            pictureBox1.Refresh();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Labirynt1.Scierzka2.Add(new List<Point>());
            Labirynt1.szukaj_poczatek();
            Labirynt1.sortuj();

            MessageBox.Show("Liczba scierzeka do wyjscia " + Labirynt1.Scierzka2.Count.ToString());
            for (int j = 0; j < Labirynt1.Scierzka2[l].Count - 2; j++)
            {
                g.DrawLine(red, (Labirynt1.Scierzka2[l][j].X * 500 / Labirynt1.Pola[0].Length + 250 / Labirynt1.Pola[0].Length), Labirynt1.Scierzka2[l][j].Y * 500 / Labirynt1.Pola.Count + (250 / Labirynt1.Pola.Count), Labirynt1.Scierzka2[l][j + 1].X * 500 / Labirynt1.Pola[0].Length + 250 / Labirynt1.Pola[0].Length, Labirynt1.Scierzka2[l][j + 1].Y * 500 / Labirynt1.Pola.Count + (250 / Labirynt1.Pola.Count));
            }
            g.DrawLine(red, (Labirynt1.Scierzka2[l][Labirynt1.Scierzka2[l].Count - 2].X * 500 / Labirynt1.Pola[0].Length + 250 / Labirynt1.Pola[0].Length), Labirynt1.Scierzka2[l][Labirynt1.Scierzka2[l].Count - 2].Y * 500 / Labirynt1.Pola.Count + (250 / Labirynt1.Pola.Count), Labirynt1.Scierzka2[l][Labirynt1.Scierzka2[l].Count - 1].X * 500 / Labirynt1.Pola[0].Length + 250 / Labirynt1.Pola[0].Length, Labirynt1.Scierzka2[l][Labirynt1.Scierzka2[l].Count - 1].Y * 500 / Labirynt1.Pola.Count + (250 / Labirynt1.Pola.Count));
            pictureBox1.Refresh();
            label1.Text = "1";
            label4.Text = Labirynt1.Scierzka2.Count().ToString();
            label6.Text = Labirynt1.Scierzka2[l].Count().ToString();
            int mini = Labirynt1.Scierzka2[0].Count;
            int mininr = 0;
            for (int j = 1; j < Labirynt1.Scierzka2.Count; j++)
            {
                if (mini > Labirynt1.Scierzka2[j].Count) {
                    mini = Labirynt1.Scierzka2[j].Count;
                    mininr = j;
                } }
            label10.Text = mini.ToString();
            label9.Text = (mininr+1).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            if (l < Labirynt1.Scierzka2.Count-1)
                l++;
            else
                l = 0;

            for (int i = 0; i < Labirynt1.Pola[0].Length; i++)
            {
                g.DrawLine(black, 500 - (i * 500 / Labirynt1.Pola[0].Length), 0, 500 - (i * 500 / Labirynt1.Pola[0].Length), 500);
            }
            for (int i = 0; i < Labirynt1.Pola.Count; i++)
            {
                g.DrawLine(black, 0, 500 - (i * 500 / Labirynt1.Pola.Count), 500, 500 - (i * 500 / Labirynt1.Pola.Count));
            }

            for (int i = 0; i < Labirynt1.Pola[0].Length; i++)
            {
                for (int j = 0; j < Labirynt1.Pola.Count; j++)
                {
                    if (Labirynt1.Pola[j][i] == 'X')
                        g.DrawEllipse(black, (i * 500 / Labirynt1.Pola[0].Length), (j * 500 / Labirynt1.Pola.Count), 500 / Labirynt1.Pola[0].Length, 500 / Labirynt1.Pola.Count);
                    if (Labirynt1.Pola[j][i] == 'S')
                        g.DrawEllipse(gold, (i * 500 / Labirynt1.Pola[0].Length), (j * 500 / Labirynt1.Pola.Count), 500 / Labirynt1.Pola[0].Length, 500 / Labirynt1.Pola.Count);
                    if (Labirynt1.Pola[j][i] == 'W')
                        g.DrawEllipse(brown, (i * 500 / Labirynt1.Pola[0].Length), (j * 500 / Labirynt1.Pola.Count), 500 / Labirynt1.Pola[0].Length, 500 / Labirynt1.Pola.Count);

                }

                for (int j = 0; j < Labirynt1.Scierzka2[l].Count - 2; j++)
                {
                    g.DrawLine(red, (Labirynt1.Scierzka2[l][j].X * 500 / Labirynt1.Pola[0].Length + 250 / Labirynt1.Pola[0].Length), Labirynt1.Scierzka2[l][j].Y * 500 / Labirynt1.Pola.Count + (250 / Labirynt1.Pola.Count), Labirynt1.Scierzka2[l][j + 1].X * 500 / Labirynt1.Pola[0].Length + 250 / Labirynt1.Pola[0].Length, Labirynt1.Scierzka2[l][j + 1].Y * 500 / Labirynt1.Pola.Count + (250 / Labirynt1.Pola.Count));
                }
                g.DrawLine(red, (Labirynt1.Scierzka2[l][Labirynt1.Scierzka2[l].Count - 2].X * 500 / Labirynt1.Pola[0].Length + 250 / Labirynt1.Pola[0].Length), Labirynt1.Scierzka2[l][Labirynt1.Scierzka2[l].Count - 2].Y * 500 / Labirynt1.Pola.Count + (250 / Labirynt1.Pola.Count), Labirynt1.Scierzka2[l][Labirynt1.Scierzka2[l].Count - 1].X * 500 / Labirynt1.Pola[0].Length + 250 / Labirynt1.Pola[0].Length, Labirynt1.Scierzka2[l][Labirynt1.Scierzka2[l].Count-1].Y * 500 / Labirynt1.Pola.Count + (250 / Labirynt1.Pola.Count));
                pictureBox1.Refresh();
                label1.Text = (l + 1).ToString();
                label6.Text = Labirynt1.Scierzka2[l].Count().ToString();

            }
        }
    }
}