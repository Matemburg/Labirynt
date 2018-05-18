using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Labirynt
{

    public class Plansza

    {

        public List<string> Pola;
        public List<Point> Scierzka;

        public Plansza()
        {
            Pola = new List<string>();
            Scierzka = new List<Point>();
        }

        public void wczytaj(string lokalizacja)
        {
            using (StreamReader sr = File.OpenText(lokalizacja))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Pola.Add(s);
                }
            }
        }
        public void szukaj(int x, int y)
        {
            szukaj(x, y, x, y);
        }
        private bool szukaj(int x, int y, int xp, int yp)
        {
          //  int nr = 0;
            if (Pola[y][x] == 'W')
            {
                Scierzka.Add(new Point(y, x));
                return true;
            }
            else if (Pola[y][x] == 'X')
            {
                return false;
            }
            else
            {
                if (x > 0)
                    if (x - 1 != xp)
                        if (szukaj(x - 1, y, x, y))
                        {
                            Scierzka.Add(new Point(y, x));
                            return true;
                        }
                if (x < Pola[y].Length)
                    if (x + 1 != xp)
                        if (szukaj(x + 1, y, x, y))
                        {
                            Scierzka.Add(new Point(y, x));
                            return true;
                        }

                if (y > 0)
                    if (y - 1 != yp)
                        if (szukaj(x, y - 1, x, y))
                        {
                            Scierzka.Add(new Point(y, x));
                            return true;
                        }
                if (y < Pola.Count - 1)
                    if (y + 1 != yp)
                        if (szukaj(x, y + 1, x, y))
                        {
                            Scierzka.Add(new Point(y, x));
                            return true;
                        }

                return false;

            }
        }
            public void na_graf (int x, int y)
        {

                
        }
        }

    }
