using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace yazlab2_1
{

    public partial class Form1 : Form
    {
        public double guncel;
        public int aksiyom;

        public static int boyut = 0;
        public static int[,] R;
        public double[,] Q;
        public static string dosya_yolu;
        public string[] yollar;

        public static string KayitR;
        public static string KayitQ;
        public static string KayitPath;

        public static int baslangic;
        public static int bitis;
        public static int iterasyon;
        public static string path;

        
        public static void boyutBelirle()
        {
            
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosyanın açılacağını,
            //3.parametre dosyaya erişimin veri okumak için olacağını gösterir.
            StreamReader sw = new StreamReader(fs);
            //Okuma işlemi için bir StreamReader nesnesi oluşturduk.
            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                boyut++;
                
                yazi = sw.ReadLine(); 
            }
            //Satır satır okuma işlemini gerçekleştirdik ve ekrana yazdırdık
            //Son satır okunduktan sonra okuma işlemini bitirdik
            sw.Close();
            fs.Close();
            //İşimiz bitince kullandığımız nesneleri iade ettik.
        }

        public static int Random_aksiyom(int[,] R, int baslangic,int ilk_aksiyom)
        {


            int j = 0;
            int komsu_matris = 0;

            int[] sütun_komsu = new int[4]; //maksimum komşuluk sayısı

            for (int i = 0; i < boyut; i++)
            {
                if ((R[baslangic, i] == 0 || R[baslangic, i] == 100))// && (R[baslangic, i] !=ilk_durum)

                {
                    komsu_matris++;
                }

            }

            for (int i = 0; i < boyut; i++)
            {
                if ((R[baslangic, i] == 0 || R[baslangic, i] == 100))

                {
                    if (komsu_matris > 1)
                    {

                        if (i == ilk_aksiyom)
                        {

                        }

                        else
                        {

                            sütun_komsu[j] = i; //R matrisinin sütunlarını tutuyor
                            j++;
                        }

                    }



                    else
                    {
                        sütun_komsu[j] = i; //R matrisinin sütunlarını tutuyor
                        j++;
                    }

                }

            }



            Random rastgele = new Random();

            int secilen_index = rastgele.Next(0, j);

            int secildi = Convert.ToInt32(sütun_komsu[secilen_index]);

            return secildi;




        }

        /*iLGİLİ AKSİYOMUN SATIRINDAN MAXİMUM DEĞERLİ İNDEX SEÇİMİ*/

        public static double Max(int secilen, int[,] R, double[,] Q)
        {

            int j = 0;
            double[] Q_analizi = new double[4]; //maksimum komşuluk sayısı

            for (int i = 0; i < boyut; i++)
            {
                if (R[secilen, i] == 0 || R[secilen, i] == 100)
                {
                    Q_analizi[j] = Q[secilen, i];
                    j++;
                }

            }

            double max_deger = Max_Deger_Dondur(Q_analizi, j);

            

            return max_deger;


        }




        public static double Max_Deger_Dondur(double[] Q_analizi, int j)
        {
            double buyuk = Q_analizi[0];

            for (int i = 1; i < j; i++)
            {
                if (buyuk < Q_analizi[i])
                {
                    buyuk = Q_analizi[i];
                }

            }

            return buyuk;
        }


        public static int Max_Degerli_Sütun(double[] yol)
        {

            double buyuk = yol[0];

            int max_indis = 0;

            for (int i = 1; i < yol.Length; i++)
            {

                if (buyuk < yol[i])
                {
                    buyuk = yol[i];


                }

            }


            for (int j = 0; j < yol.Length; j++)
            {
                if (buyuk == yol[j])

                    max_indis = j;
            }



            return max_indis;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        
        }



        /* BAŞLANGIÇ VE BİTİŞ DEĞERLERİNE GÖRE R MATRİSİNİN YENİDEN OLUŞUMU VE Q MATRİSİNİN YOL KAZANCI*/

        private void button1_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;

            baslangic = Convert.ToInt32(textBox1.Text);
            bitis = Convert.ToInt32(textBox2.Text);
            iterasyon = Convert.ToInt32(textBox3.Text);
            double Gamma = 0.8;

            if (baslangic < boyut && bitis < boyut)
            {

                button2.Enabled = true;

            
            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    if ((j == bitis && R[i,j]==0) || (j == bitis && i==bitis ))
                    {

                        R[i, j] = 100;
                    }
                    
                }
              
            }


           
            textBox4.Text = textBox4.Text + " ---KAZANÇ R MATRİSİ --- " + Environment.NewLine + Environment.NewLine;

            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    textBox4.Text = textBox4.Text + R[i, j].ToString() + "   ";
                }
                textBox4.Text = textBox4.Text + Environment.NewLine;
            }






            int durum = baslangic;
               
            int ilk_aksiyom = -1; 
                

                for (int i = 0; i < iterasyon; i++)
            {
                    while (durum != bitis)

                    {
                       
                        aksiyom = Random_aksiyom(R, durum,ilk_aksiyom);

                        guncel = Convert.ToDouble(R[durum, aksiyom] + (Gamma * Max(aksiyom, R, Q)));

                        guncel = Math.Round(guncel, 1);


                        Q[durum, aksiyom] = guncel;

                        ilk_aksiyom = durum;
                        durum = aksiyom;
                    }

                    if (durum == bitis)
                    {
                        Q[aksiyom, aksiyom] = guncel;

                    }


                    durum = baslangic;
                    ilk_aksiyom = -1;



                }

            textBox4.Text = textBox4.Text + Environment.NewLine + Environment.NewLine + " --- Q MATRİSİ --- " + Environment.NewLine + Environment.NewLine;

            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    textBox4.Text = textBox4.Text + Q[i, j].ToString() + "   ";
                }
                textBox4.Text = textBox4.Text + Environment.NewLine;
            }



            /* YOL BULMA KISMI */

            double[] yol = new double[boyut];
            int buyuk_indis;
            int sütun;
            int satır;

            cıkıstextbox.Text = cıkıstextbox.Text + baslangic + " ";

            for (satır = baslangic; satır < boyut; satır++)
            {


                for (sütun = 0; sütun < boyut; sütun++)
                {


                    yol[sütun] = Q[satır, sütun];


                    

                }

                buyuk_indis = Max_Degerli_Sütun(yol);

                cıkıstextbox.Text = cıkıstextbox.Text + buyuk_indis + " ";

                satır = buyuk_indis - 1;

                if (buyuk_indis == bitis)
                {
                    break;
                }



            }


        }

            else
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                MessageBox.Show("BAŞLANGIÇ VE BİTİŞ DEĞERİNİ EN FAZLA " + (boyut - 1).ToString() + " GİREBİLİRSİNİZ!");
            }



        }







        /*--------------ÇİZİM------------*/

        private void button2_Click(object sender, EventArgs e)
        {
            path = cıkıstextbox.Text.ToString();
            cizim cizim1 = new cizim();
            cizim1.Show();
            this.Hide();
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }


        /* -------------------------BAŞLANGIÇ--------------------------*/

        private void button3_Click(object sender, EventArgs e)
        {


            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            dosya_yolu = file.FileName;

           
            label1.Enabled = true;
            label2.Enabled = true;
            label3.Enabled = true;
            label4.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            cıkıstextbox.Enabled = true;
            button1.Enabled = true;
            button7.Enabled = true;

            boyutBelirle();
            
            R = new int[boyut, boyut];
            Q = new double[boyut, boyut];
            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    R[i, j] = -1;
                    Q[i, j] = 0;
                }
            }

            




            /* KOMŞULUK MATRİSİ OLUŞTURMAK İÇİN*/

            int k = 0;


            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosyanın açılacağını,
            //3.parametre dosyaya erişimin veri okumak için olacağını gösterir.
            StreamReader sw = new StreamReader(fs);
            //Okuma işlemi için bir StreamReader nesnesi oluşturduk.
            string yazi = sw.ReadLine();
            while (yazi != null)
            {

                //string[] yollar;
                yollar = yazi.Split(',');
                foreach (string i in yollar) //parcalar dizisiniz tüm elemanları listBox' a eklenir.
                {
                    R[k, Convert.ToInt32(i)] = 0;
                }

                yazi = sw.ReadLine();
                k++;
            }
           
            //Satır satır okuma işlemini gerçekleştirdik ve ekrana yazdırdık
            //Son satır okunduktan sonra okuma işlemini bitirdik
            sw.Close();
            fs.Close();
            //İşimiz bitince kullandığımız nesneleri iade ettik.

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileKayit = new SaveFileDialog();
            fileKayit.ShowDialog();
            KayitR = fileKayit.FileName;

            FileStream fs = new FileStream(KayitR, FileMode.OpenOrCreate, FileAccess.Write);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
            //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
            StreamWriter sw = new StreamWriter(fs);
            //Yazma işlemi için bir StreamWriter nesnesi oluşturduk.
            sw.WriteLine(" --- R MATRİSİ ---" + Environment.NewLine + Environment.NewLine);


            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    sw.Write( R[i, j].ToString() + "   ");
                }
                sw.WriteLine(Environment.NewLine);
            }
            sw.WriteLine(Environment.NewLine + Environment.NewLine );
            


                //Dosyaya ekleyeceğimiz iki satırlık yazıyı WriteLine() metodu ile yazacağız.
                sw.Flush();
            //Veriyi tampon bölgeden dosyaya aktardık.
            sw.Close();
            fs.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileKayitQ = new SaveFileDialog();
            fileKayitQ.ShowDialog();
            KayitQ = fileKayitQ.FileName;

            FileStream fs = new FileStream(KayitQ, FileMode.OpenOrCreate, FileAccess.Write);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
            //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
            StreamWriter sw = new StreamWriter(fs);
            //Yazma işlemi için bir StreamWriter nesnesi oluşturduk.
           
            sw.WriteLine(" --- Q MATRİSİ ---" + Environment.NewLine + iterasyon + " iterasyon sonrasi" + Environment.NewLine + Environment.NewLine);

            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    sw.Write(Q[i, j].ToString() + "   ");
                }
                sw.WriteLine(Environment.NewLine);
            }

            sw.WriteLine(Environment.NewLine + Environment.NewLine);
            


            //Dosyaya ekleyeceğimiz iki satırlık yazıyı WriteLine() metodu ile yazacağız.
            sw.Flush();
            //Veriyi tampon bölgeden dosyaya aktardık.
            sw.Close();
            fs.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileKayitPath = new SaveFileDialog();
            fileKayitPath.ShowDialog();
            KayitPath = fileKayitPath.FileName;

            FileStream fs = new FileStream(KayitPath, FileMode.OpenOrCreate, FileAccess.Write);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
            //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
            StreamWriter sw = new StreamWriter(fs);
            //Yazma işlemi için bir StreamWriter nesnesi oluşturduk.
           
            
            sw.WriteLine(" --- PRINTING PATH ---" + Environment.NewLine);
            sw.Write(cıkıstextbox.Text.ToString());


            //Dosyaya ekleyeceğimiz iki satırlık yazıyı WriteLine() metodu ile yazacağız.
            sw.Flush();
            //Veriyi tampon bölgeden dosyaya aktardık.
            sw.Close();
            fs.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
