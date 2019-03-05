using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yazlab2_1
{
    public partial class cizim : Form
    {
        Pen myPen = new Pen(Color.DarkSlateGray); //LABIRENT ICIN
        Graphics g = null;

        Pen PenPath = new Pen(Color.Crimson); //PATH ICIN
        Graphics gpath = null;

        static int boyut = Form1.boyut;
        public static int[] path;
        static string[] pathString;

        static int baslangic_x = 80;
        static int baslangic_y = 80;
       
        public static int yolSayisi;
       
        static int start_x, start_y;
       

        public cizim()
        {
            InitializeComponent();
            start_x = baslangic_x;
            start_y = baslangic_y;
        }
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            
            myPen.Width = 5;
            g = canvas.CreateGraphics();
            
            paint_lab(boyut);
            //path_olustur();

            //PenPath.Width = 5;
            //gpath = canvas.CreateGraphics();
            //paint_path(path, boyut);
                
            
        }

        private void path_olustur()
        {
            
            pathString = Form1.path.Split(' ');
            yolSayisi=pathString.Length-1;
            
            path = new int[yolSayisi];

            for (int i = 0; i < yolSayisi; i++)
            {
                path[i] = Convert.ToInt16(pathString[i]);
                
            }
            
        }
        private void paint_lab(int boyut)
        {
      
            int sayac = 0;
            double boyutKareKok = Math.Sqrt(boyut);

            for (int i = 0; i < boyutKareKok; i++) ///YATAY CIZEEERR
            {
                if ((Form1.baslangic < boyutKareKok && i == Form1.baslangic && sayac == 0)
                    || (Form1.bitis < boyutKareKok && i == Form1.bitis && sayac == 0))
                {
                    start_x = start_x + 50;
                    if (i == boyutKareKok - 1)
                    {

                        i = -1;
                        start_x = baslangic_x;
                        start_y = start_y + 50;
                        sayac++;
                        
                    }
                  
                    continue;
                }
                if ((Form1.bitis < boyut && Form1.bitis >= boyut - Convert.ToInt16(boyutKareKok) && boyut - Convert.ToInt16(boyutKareKok) + i == Form1.bitis && sayac == Convert.ToInt16(boyutKareKok))
                    || (Form1.baslangic < boyut && Form1.baslangic >= boyut - Convert.ToInt16(boyutKareKok) && boyut - Convert.ToInt16(boyutKareKok) + i == Form1.baslangic && sayac == Convert.ToInt16(boyutKareKok)))
                {
                    start_x = start_x + 50;
                    if (i == boyutKareKok - 1)
                    {

                        i = -1;
                        start_x = baslangic_x;
                        start_y = start_y + 50;
                        sayac++;
                        if (sayac == boyutKareKok + 1)
                        {
                            start_x = baslangic_x;
                            start_y = baslangic_y;
                            sayac = 0;
                            break;
                        }
                      

                    }

                    continue;
                }

                if ((sayac > 0 && sayac < boyutKareKok && Form1.R[i + (sayac - 1) * Convert.ToInt16(boyutKareKok), i + (sayac - 1) * Convert.ToInt16(boyutKareKok) + Convert.ToInt16(boyutKareKok)] == 0)
                    || (sayac > 0 && sayac < boyutKareKok && Form1.R[i + (sayac - 1) * Convert.ToInt16(boyutKareKok), i + (sayac - 1) * Convert.ToInt16(boyutKareKok) + Convert.ToInt16(boyutKareKok)] == 100))
                {
                    start_x = start_x + 50;
                    if (i == boyutKareKok - 1)
                    {

                        i = -1;
                        start_x = baslangic_x;
                        start_y = start_y + 50;
                        sayac++;
                        if (sayac == boyutKareKok + 1)
                        {
                            start_x = baslangic_x;
                            start_y = baslangic_y;
                            sayac = 0;
                            break;
                        }
                    }
                    continue;
                }
                
                drawLineYatay();
                start_x = start_x + 50;

                if (i == boyutKareKok - 1)
                {
                    
                    i = -1;
                    start_x = baslangic_x;
                    start_y = start_y + 50;
                    sayac++;
                    if (sayac == boyutKareKok+1)
                    {
                        start_x = baslangic_x;
                        start_y = baslangic_y;
                        sayac = 0;
                        break;
                    }
                }

                               
            }
             //for (int y = 0; y < boyutKareKok; y++)
             //   {
             //       drawLineDikey();
             //       start_y = start_y + 50;
             //   }
             //for (int i = 0; i < boyutKareKok; i++)
             //{
             //    //int elemanS = path.Length;
             //    //if (path[elemanS - 1] < boyut && path[elemanS - 1] >= (boyutKareKok * (boyutKareKok - 1)) && (boyutKareKok * (boyutKareKok - 1))+(boyutKareKok-1-i) == path[elemanS-1])
             //    //{
             //    //    start_x = start_x - (50);
             //    //    continue;
             //    //}
             //    start_x = start_x - (50);
             //    drawLineYatay();
                
             //}
            for (int y = 0; y < boyutKareKok; y++) /////DIKEY CIZER
            {

                if ((boyut > 4 && y>0 && y<Convert.ToInt32(boyutKareKok)-1 && sayac == 0 && Form1.baslangic % Convert.ToInt32(boyutKareKok) == 0 && Form1.baslangic / Convert.ToInt32(boyutKareKok) == y)
                    || (boyut > 4 && y > 0 && y < Convert.ToInt32(boyutKareKok) - 1 && sayac == 0 && Form1.bitis % Convert.ToInt32(boyutKareKok) == 0 && Form1.bitis / Convert.ToInt32(boyutKareKok) == y))
                {
                    start_y = start_y + 50;
                    continue;
                }

                if ((boyut > 4 && y > 0 && y < Convert.ToInt32(boyutKareKok) - 1 && sayac == Convert.ToInt32(boyutKareKok) && Form1.baslangic % Convert.ToInt32(boyutKareKok) == Convert.ToInt32(boyutKareKok) - 1 && Form1.baslangic / Convert.ToInt32(boyutKareKok) == y)
                    || (boyut > 4 && y>0 && y<Convert.ToInt32(boyutKareKok)-1 && sayac == Convert.ToInt32(boyutKareKok) && Form1.bitis % Convert.ToInt32(boyutKareKok) == Convert.ToInt32(boyutKareKok) - 1 && Form1.bitis / Convert.ToInt32(boyutKareKok) == y))
                {
                    start_y = start_y + 50;
                    continue;
                }

                if ((sayac > 0 && sayac < boyutKareKok && Form1.R[(sayac - 1) + y * Convert.ToInt16(boyutKareKok), (sayac - 1) + y * Convert.ToInt16(boyutKareKok) + 1] == 0)
                    || (sayac > 0 && sayac < boyutKareKok && Form1.R[(sayac - 1) + y * Convert.ToInt16(boyutKareKok), (sayac - 1) + y * Convert.ToInt16(boyutKareKok) + 1] == 100))
                {
                    start_y = start_y + 50;


                    if (y == boyutKareKok - 1)
                    {

                        y = -1;
                        start_y = baslangic_y;
                        start_x = start_x + 50;
                        sayac++;
                        if (sayac == boyutKareKok + 1)
                        {
                            start_x = baslangic_x;
                            start_y = baslangic_y;
                            break;
                        }
                    }

                    continue;
                }
                drawLineDikey();
                start_y = start_y + 50;


                if (y == boyutKareKok - 1)
                {

                    y = -1;
                    start_y = baslangic_y;
                    start_x = start_x + 50;
                    sayac++;
                    if (sayac == boyutKareKok + 1)
                    {
                        start_x = baslangic_x;
                        start_y = baslangic_y;
                        break;
                    }
                }

            }


             
        }

        private void paint_path(int[] path, int boyut)
        {
            start_x = baslangic_x+25;
            start_y = baslangic_y+25;
            double boyutKareKok = Math.Sqrt(boyut);

            for (int i = 0; i < yolSayisi - 1; i++)    //YATAAY ÇİZER
            {
                if (path[i] + 1 == path[i + 1])
                {
                    start_y = start_y + 50 * (path[i] / Convert.ToInt16(boyutKareKok));
                    start_x = start_x + 50 * (path[i] % Convert.ToInt16(boyutKareKok));
                    drawLineYatayPath();
                    start_x = baslangic_x + 25;
                    start_y = baslangic_y + 25;
                }
            }
            for (int i = 1; i < yolSayisi; i++)    //YATAY ÇİZER
            {
                if (path[i - 1] == path[i]+1)
                {
                    start_y = start_y + 50 * (path[i] / Convert.ToInt16(boyutKareKok));
                    start_x = start_x + 50 * (path[i] % Convert.ToInt16(boyutKareKok));
                    drawLineYatayPath();
                    start_x = baslangic_x + 25;
                    start_y = baslangic_y + 25;
                }
            }

            for (int i = 0; i < yolSayisi - 1; i++)    //DİKEY ÇİZER
            {
                if (path[i] + Convert.ToInt16(boyutKareKok) == path[i + 1])
                {
                    start_y = start_y + 50 * (path[i] / Convert.ToInt16(boyutKareKok));
                    start_x = start_x + 50 * (path[i] % Convert.ToInt16(boyutKareKok));
                    drawLineDikeyPath();
                    start_x = baslangic_x + 25;
                    start_y = baslangic_y + 25;
                }
            }

            for (int i = 1; i < yolSayisi; i++)    //DİKEY ÇİZER
            {
                if (path[i - 1] == path[i] + Convert.ToInt16(boyutKareKok))
                {
                    start_y = start_y + 50 * (path[i] / Convert.ToInt16(boyutKareKok));
                    start_x = start_x + 50 * (path[i] % Convert.ToInt16(boyutKareKok));
                    drawLineDikeyPath();
                    start_x = baslangic_x + 25;
                    start_y = baslangic_y + 25;
                }
            }

            //////////////////DIKEY EKLER
            if(Form1.baslangic<Convert.ToInt16(boyutKareKok)) 
                {
                    start_x = baslangic_x+25; //UST TARAFF
                    start_y = baslangic_y;

                    start_x = start_x + 50 * (Form1.baslangic % Convert.ToInt16(boyutKareKok));
                    start_y = start_y + 50 * (Form1.baslangic / Convert.ToInt16(boyutKareKok));
                    drawLineDikeyPathBB();
                   
                }

            if (Form1.bitis >= Convert.ToInt16(boyutKareKok) * (Convert.ToInt16(boyutKareKok) - 1))
            {
                start_x = baslangic_x + 25;   //ALT TARAF
                start_y = baslangic_y + 25;

                start_x = start_x + 50 * (Form1.bitis % Convert.ToInt16(boyutKareKok));
                start_y = start_y + 50 * (Form1.bitis / Convert.ToInt16(boyutKareKok));
                drawLineDikeyPathBB();

            }

            if (Form1.bitis < Convert.ToInt16(boyutKareKok))
            {
                start_x = baslangic_x+25; //UST TARAFF
                start_y = baslangic_y;

                start_x = start_x + 50 * (Form1.bitis % Convert.ToInt16(boyutKareKok));
                start_y = start_y + 50 * (Form1.bitis / Convert.ToInt16(boyutKareKok));
                drawLineDikeyPathBB();

            }

            if (Form1.baslangic >= Convert.ToInt16(boyutKareKok) * (Convert.ToInt16(boyutKareKok) - 1))
            {
                start_x = baslangic_x+25;   //ALT TARAF
                start_y = baslangic_y+25;

                start_x = start_x + 50 * (Form1.baslangic % Convert.ToInt16(boyutKareKok));
                start_y = start_y + 50 * (Form1.baslangic / Convert.ToInt16(boyutKareKok));
                drawLineDikeyPathBB();

            }

            //////////YATAY EKLERR

            if (boyut > 4 && Form1.baslangic >= Convert.ToInt16(boyutKareKok) && Form1.baslangic < Convert.ToInt16(boyutKareKok) * (Convert.ToInt16(boyutKareKok) - 1) && Form1.baslangic % Convert.ToInt16(boyutKareKok) == 0)
            {
                start_x = baslangic_x;   //SOL BASLANGICSA
                start_y = baslangic_y + 25;
                start_x = start_x + 50 * (Form1.baslangic % Convert.ToInt16(boyutKareKok));
                start_y = start_y + 50 * (Form1.baslangic / Convert.ToInt16(boyutKareKok));
                drawLineYatayPathBB();
            }
            if (boyut > 4 && Form1.bitis >= Convert.ToInt16(boyutKareKok) && Form1.bitis < Convert.ToInt16(boyutKareKok) * (Convert.ToInt16(boyutKareKok) - 1) && Form1.bitis % Convert.ToInt16(boyutKareKok) == 0)
            {
                start_x = baslangic_x;   //SOL BITISSE
                start_y = baslangic_y + 25;
                start_x = start_x + 50 * (Form1.bitis % Convert.ToInt16(boyutKareKok));
                start_y = start_y + 50 * (Form1.bitis / Convert.ToInt16(boyutKareKok));
                drawLineYatayPathBB();
            }

            if (boyut > 4 && Form1.baslangic >= Convert.ToInt16(boyutKareKok) && Form1.baslangic < Convert.ToInt16(boyutKareKok) * (Convert.ToInt16(boyutKareKok) - 1) && Form1.baslangic % Convert.ToInt16(boyutKareKok) == Convert.ToInt16(boyutKareKok) - 1)
            {
                start_x = baslangic_x + 25;   //SAG BASLANGICSA
                start_y = baslangic_y + 25;
                start_x = start_x + 50 * (Form1.baslangic % Convert.ToInt16(boyutKareKok));
                start_y = start_y + 50 * (Form1.baslangic / Convert.ToInt16(boyutKareKok));
                drawLineYatayPathBB();
            }

            if (boyut > 4 && Form1.bitis >= Convert.ToInt16(boyutKareKok) && Form1.bitis < Convert.ToInt16(boyutKareKok) * (Convert.ToInt16(boyutKareKok) - 1) && Form1.bitis % Convert.ToInt16(boyutKareKok) == Convert.ToInt16(boyutKareKok) - 1)
            {
                start_x = baslangic_x + 25;   //SAG BITISSE
                start_y = baslangic_y + 25;
                start_x = start_x + 50 * (Form1.bitis % Convert.ToInt16(boyutKareKok));
                start_y = start_y + 50 * (Form1.bitis / Convert.ToInt16(boyutKareKok));
                drawLineYatayPathBB();
            }
           
            
        }
        private void drawLineDikey()
        {
            Point[] points ={
                               new Point(start_x, start_y),
                               new Point(start_x, start_y+50)
                           };
            g.DrawLines(myPen, points);
        }

        private void drawLineDikeyPath()
        {
            Point[] points ={
                               new Point(start_x, start_y),
                               new Point(start_x, start_y+50)
                           };
            gpath.DrawLines(PenPath, points);
        }

        private void drawLineDikeyPathBB()
        {
            Point[] points ={
                               new Point(start_x, start_y),
                               new Point(start_x, start_y+25)
                           };
            gpath.DrawLines(PenPath, points);
        }
        private void drawLineYatay()
        {
            Point[] points ={
                               new Point(start_x, start_y),
                               new Point(start_x+50, start_y)
                           };
            g.DrawLines(myPen, points);
        }

        private void drawLineYatayPath()
        {
            Point[] points ={
                               new Point(start_x, start_y),
                               new Point(start_x+50, start_y)
                           };
            gpath.DrawLines(PenPath, points);
        }

        private void drawLineYatayPathBB()
        {
            Point[] points ={
                               new Point(start_x, start_y),
                               new Point(start_x+25, start_y)
                           };
            gpath.DrawLines(PenPath, points);
        }
        private void cizim_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
                      
            canvas.Refresh();
        }

        private void button1_Click_1(object sender, EventArgs e) // KULLANILMIYOOR
        {

            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            path_olustur();

            PenPath.Width = 5;
            gpath = canvas.CreateGraphics();
            paint_path(path, boyut);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
