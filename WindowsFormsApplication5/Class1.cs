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
        public List<List<Point>> Scierzka2;
      //  private int u = 0;


        public Plansza()
        {
            
            Scierzka = new List<Point>();
            Scierzka2 = new List<List<Point>>();
        }

        public void wczytaj(string lokalizacja)
        {
            Pola = new List<string>();
            Scierzka2 = new List<List<Point>>();

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

        public void szukaj_poczatek()
        {

            for (int i=0;i<Pola.Count;i++)
            {
                for (int j = 0; j < Pola[0].Length; j++)
                {
                    if (Pola[i][j] == 'S')
                    {
                        if (Scierzka2.Count > 1)
                            Scierzka2.Add(new List<Point>());
                     szukaj_2(j, i, Scierzka2.Count - 1);
                    }
                }
            }

        }

        public void szukaj_2(int x, int y, int nr)
        {
            int dlugosc = Scierzka2[nr].Count;
            bool bylo = false;
            if (Pola[y][x] == 'W')
            {
                Scierzka2[nr].Add(new Point(x, y));
            }
            else
            {
                if (Pola[y][x] == 'X') { }
                else
                {
                    Scierzka2[nr].Add(new Point(x, y));
                    if (x > 0)
                    {
                        if (Pola[y][x - 1] != 'X')

                        {
                            if (Scierzka2[nr].Count - 1 > 0)
                            {
                                bool kolizja = false;
                                for (int i = 0; i <= dlugosc; i++)
                                {
                                    if (x - 1 == Scierzka2[nr][i].X)
                                    {
                                        if (y == Scierzka2[nr][i].Y)
                                        {
                                            kolizja = true;
                                        }
                                    }
                                }
                                if (kolizja == false)
                                {
                                    szukaj_2(x - 1, y, nr);
                                    bylo = true;
                                }
                            }
                            else
                            {
                                szukaj_2(x - 1, y, nr);
                                bylo = true;
                            }
                        }
                    }


                    if (x < Pola[0].Length - 1)
                    {
                        if (Pola[y][x + 1] != 'X')
                        {
                            if (Scierzka2[nr].Count - 1 > 0)
                            {
                                bool kolizja = false;
                                for (int i = 0; i <= dlugosc; i++)
                                {
                                    if (x + 1 == Scierzka2[nr][i].X)
                                    {
                                        if (y == Scierzka2[nr][i].Y)
                                        {
                                            kolizja = true;
                                        }
                                    }
                                }
                                if (kolizja == false)
                                {
                                    if (bylo == false)
                                    {
                                        szukaj_2(x + 1, y, nr);
                                        bylo = true;
                                    }
                                    else
                                    {
                                        Scierzka2.Add(new List<Point>());
                                        for (int j = 0; j < dlugosc + 1; j++)
                                        {
                                            Scierzka2[Scierzka2.Count - 1].Add(new Point(Scierzka2[nr][j].X, Scierzka2[nr][j].Y));
                                        }
                                        szukaj_2(x + 1, y, Scierzka2.Count - 1);
                                    }
                                }
                            }
                            else
                            {
                                if (bylo == false)
                                {
                                    szukaj_2(x + 1, y, nr);
                                    bylo = true;
                                }
                                else
                                {
                                    Scierzka2.Add(new List<Point>());
                                    for (int j = 0; j < dlugosc + 1; j++)
                                    {
                                        Scierzka2[Scierzka2.Count - 1].Add(new Point(Scierzka2[nr][j].X, Scierzka2[nr][j].Y));
                                    }
                                    szukaj_2(x + 1, y, Scierzka2.Count - 1);
                                }

                            }
                        }
                    }
                    if (y > 0)
                    {
                        if (Pola[y - 1][x] != 'X')
                        {
                            if (Scierzka2[nr].Count - 1 > 0)
                            {
                                bool kolizja = false;
                                for (int i = 0; i < dlugosc + 1; i++)
                                {
                                    if (x == Scierzka2[nr][i].X)
                                    {
                                        if (y - 1 == Scierzka2[nr][i].Y)
                                        {
                                            kolizja = true;
                                        }
                                    }
                                }
                                if (kolizja == false)
                                {
                                    if (bylo == false)
                                    {
                                        szukaj_2(x, y - 1, nr);
                                        bylo = true;
                                    }
                                    else
                                    {
                                        Scierzka2.Add(new List<Point>());
                                        for (int j = 0; j < dlugosc + 1; j++)
                                        {
                                            Scierzka2[Scierzka2.Count - 1].Add(new Point(Scierzka2[nr][j].X, Scierzka2[nr][j].Y));
                                        }
                                        szukaj_2(x, y - 1, Scierzka2.Count - 1);
                                    }
                                }
                            }
                            else
                            {
                                if (bylo == false)
                                {
                                    szukaj_2(x, y - 1, nr);
                                    bylo = true;
                                }
                                else
                                {
                                    Scierzka2.Add(new List<Point>());
                                    for (int j = 0; j < dlugosc + 1; j++)
                                    {
                                        Scierzka2[Scierzka2.Count - 1].Add(new Point(Scierzka2[nr][j].X, Scierzka2[nr][j].Y));
                                    }
                                    szukaj_2(x, y - 1, Scierzka2.Count - 1);
                                }

                            }
                        }
                    }

                    if (y < Pola.Count - 1)
                    {
                        if (Pola[y + 1][x] != 'X')
                        {
                            if (Scierzka2[nr].Count - 1 > 0)
                            {
                                bool kolizja = false;
                                for (int i = 0; i < dlugosc + 1; i++)
                                {
                                    if (x == Scierzka2[nr][i].X)
                                    {
                                        if (y + 1 == Scierzka2[nr][i].Y)
                                        {
                                            kolizja = true;
                                        }
                                    }
                                }
                                if (kolizja == false)
                                {
                                    if (bylo == false)
                                    {
                                        szukaj_2(x, y + 1, nr);
                                        bylo = true;
                                    }
                                    else
                                    {
                                        Scierzka2.Add(new List<Point>());
                                        for (int j = 0; j < dlugosc + 1; j++)
                                        {
                                            Scierzka2[Scierzka2.Count - 1].Add(new Point(Scierzka2[nr][j].X, Scierzka2[nr][j].Y));
                                        }
                                        szukaj_2(x, y + 1, Scierzka2.Count - 1);
                                    }
                                }
                            }
                            else
                            {
                                if (bylo == false)
                                {
                                    szukaj_2(x, y + 1, nr);
                                    bylo = true;
                                }
                                else
                                {
                                    Scierzka2.Add(new List<Point>());
                                    for (int j = 0; j < dlugosc + 1; j++)
                                    {
                                        Scierzka2[Scierzka2.Count - 1].Add(new Point(Scierzka2[nr][j].X, Scierzka2[nr][j].Y));
                                    }
                                    szukaj_2(x, y + 1, Scierzka2.Count - 1);
                                }

                            }
                        }
                    }
                }
            }
        }

        public void sortuj ()
            {
            int i = 0;
            while (i<Scierzka2.Count)
            {
                if (Pola[Scierzka2[i][Scierzka2[i].Count - 1].Y][Scierzka2[i][Scierzka2[i].Count - 1].X] != 'W')
                    Scierzka2.RemoveAt(i);
                else
                    i++;

                
            }
            }

        /*
        public void Szukanie_V2(int x, int y) {
            int xp = x;
            int yp = y;
            char[,] Pola2;
            int u = 0;
            int dlugosc = 0;
            bool koniec = false;
            Pola2 = new char[Pola.Capacity, Pola[0].Length];
            for (int i = 0; i < Pola.Capacity; i++)
            {
                for (int j = 0; j < Pola[0].Length; j++)
                {
                    Pola2[i, j] = Pola[i][j];
                }
            }
            Pola2[x, y] = 'P';
            Scierzka2.Add(new List<Point>());

            while (koniec == false)
            {
            szukanie:
                Scierzka.Add(new Point(y, x));


                if (y > 0)
                {
                    if (Pola2[x, y - 1] == 'O')
                    {
                        Pola2[x, y] = 'P';
                        dlugosc++;
                        y = y - 1;
                        Pola2[x, y] = 'P';
                    }
                }
                else if (x > 0)
                {
                    if (Pola2[x - 1, y] == 'O')
                    {
                        Pola2[x, y] = 'P';
                        dlugosc++;
                        x = x - 1;
                        Pola2[x, y] = 'P';
                    }
                }
                else if (Pola2[x, y + 1] == 'O')
                {
                    Pola2[x, y] = 'P';
                    dlugosc++;
                    y = y + 1;
                    Pola2[x, y] = 'P';
                }
                else if (Pola2[x + 1, y] == 'O')
                {
                    Pola2[x, y] = 'P';
                    dlugosc++;
                    x = x + 1;
                    Pola2[x, y] = 'P';
                }

            wracanie:
                else if (Pola2[x, y - 1] != 'O' || Pola2[x, y + 1] != 'O' || Pola2[x - 1, y] != 'O' || Pola2[x + 1, y] != 'O')
                {
                    Scierzka2[u].Add(new Point(x, y)); //OPIS DROGI KROK PO KROKUu

                    Pola2[x, y] = 'X';

                    dlugosc--;
                    if (Pola2[x, y - 1] == 'P')
                    {
                        y = y - 1;
                        goto szukanie;
                    }
                    else if (Pola2[x - 1, y] == 'P')
                    {
                        x = x - 1;
                        goto szukanie;
                    }
                    else if (Pola2[x, y + 1] == 'P')
                    {
                        x = x + 1;
                        goto szukanie;
                    }
                    else if (Pola2[x + 1, y] == 'P')
                    {
                        x = x + 1;
                        goto szukanie;
                    }
                    else if (x == xp && y == yp)
                    {
                        koniec = true;
                    }
                }
                

                if (Pola2[x,y - 1] == 'W' || Pola2[x,y + 1] == 'W' || Pola2[x - 1,y] == 'W' || Pola2[x + 1,y] == 'W')
                {
                    Scierzka2[u].Add(new Point(x, y)); //OPIS DROGI KROK PO KROKUu
                    dlugosc++;
                    if (Pola2[x,y - 1] == 'W') y = y - 1;
                    else if (Pola2[x - 1,y] == 'W') x = x - 1;
                    else if (Pola2[x,y + 1] == 'W') y = y + 1;
                    else if (Pola2[x + 1,y] == 'W') x = x + 1;

                    Scierzka2.Add(new List<Point>());
                    u++;
                    goto wracanie;

                }
                }
            }
            */
    }
}

