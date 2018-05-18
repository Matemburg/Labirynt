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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // pictureBox1 = new PictureBox();
            pictureBox1.Image = new Bitmap(500, 500);
            g = Graphics.FromImage(pictureBox1.Image);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            Labirynt1.szukaj(2, 0);
            MessageBox.Show(Labirynt1.Scierzka.Count.ToString());
            for (int j = 0; j < Labirynt1.Scierzka.Count - 2; j++)
            {
                g.DrawLine(red, (Labirynt1.Scierzka[j].Y * 500 / Labirynt1.Pola[0].Length+ 250/Labirynt1.Pola[0].Length), Labirynt1.Scierzka[j].X * 500 / Labirynt1.Pola.Count + (250/Labirynt1.Pola.Count), Labirynt1.Scierzka[j + 1].Y * 500 / Labirynt1.Pola[0].Length + 250/Labirynt1.Pola[0].Length, Labirynt1.Scierzka[j + 1].X * 500 / Labirynt1.Pola.Count + (250/Labirynt1.Pola.Count));
            }
            pictureBox1.Refresh();

        }
    }
}