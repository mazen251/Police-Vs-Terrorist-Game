using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LargeGame_MM
{

    class Hero
    {
        public int X, Y;
        public List<Bitmap> img;
        public int iF;

    }

    class Enemy
    {
        public int X, Y;
        public List<Bitmap> img;
        public int iF;
        public int done = 0;
    }

    class Map
    {
        public Rectangle rcDst;
        public Rectangle rcSrc;
        public List<Bitmap> img;

    }

    class ele
    {
        public int X, Y;

        public Bitmap img;
    }

    class bullet
    {
        public int x, y;
        public int dx = -1;
        public int dy = 0;
        public Bitmap img;
    }


    public partial class Form1 : Form
    {
        Bitmap off;

        List<Hero> Lhero = new List<Hero>();
        List<ele> lele = new List<ele>();
        List<ele> lladder = new List<ele>();
        List<ele> lland = new List<ele>();
        //List<ele> lland2 = new List<ele>();
        List<Enemy> Lenemy = new List<Enemy>();
        List<Enemy> Lenemy2 = new List<Enemy>();
        List<Map> Lmap = new List<Map>();
        List<bullet> lbullet = new List<bullet>();
        List<bullet> lbullet2 = new List<bullet>();
        bullet single = new bullet();
        List<Hero> llaser = new List<Hero>();

        int attackframe = 29;
        int secondattackframe = 35;
        int idleframe = 0;
        int runrightframe = 8;
        int runleftframe = 15;
        int XScroll = 0;
        int YScroll = 0;
        int flag = 1;
        int jumpframe = 21;
        int jumpdis = 10;
        int cttick = 0;
        int enemyoneframeattack = 0;
        int enemytwoframerun = 6;
        int enemytwoframeattack = 0;
        int enemytwoattackframe2 = 0;
        int over = 0;
        int score = 0;
        int enemydis2 = 0;
        int count = 0;
        int donehit = 0;
        int boxwidth = 26;
        int flag2 = 0;
        bool laserishit = false;
        int close = 0;
        int which;
        int ay;
        int flag4 = 0;
        int elespeed = 15;
        int flag10 = 0;


        Timer t = new Timer();

        public Form1()
        {
            this.Load += new EventHandler(Form1_Load);
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            CreateWorld();

            //createele();


            createladder();

            

            createland();

            heroidle();


            //createenemy1();


        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {

                Lhero[0].iF = runrightframe;
                Lhero[0].X += 50;


                runrightframe++;
                if (runrightframe == 13)
                {
                    runrightframe = 8;
                }


                if (XScroll + 5 <= (Lmap[0].img[0].Width - ClientSize.Width))
                {
                    XScroll += 10;
                }

                if (Lhero[0].X >= this.ClientSize.Width)
                {
                    Lhero[0].X = 90;
                    XScroll = 0;
                    ay = this.ClientSize.Height - Lhero[0].img[0].Height;
                    flag++;
                    if (flag == 3)
                    {
                        llaser.Clear();
                    }
                }

                if (flag == 1)
                {
                    checkhite1();
                }

                if (flag == 2)
                {
                    //checkbullethit2();
                    //checkhite2();
                }

                
                
                    if (flag == 3)
                    {
                    if (flag10 == 0)
                    {
                        CreateELE();
                        flag10 = 1;
                    }
                    }
                    
                


            }

            if (e.KeyCode == Keys.A)
            {

                Lhero[0].iF = runleftframe;
                Lhero[0].X -= 15;


                runleftframe++;
                if (runleftframe == 20)
                {
                    runleftframe = 15;
                }


                if (XScroll - 5 >= 0)
                {
                    XScroll -= 10;
                }

                if (flag == 1)
                {
                    checkhite1();
                }

                if (flag == 2)
                {
                    //checkbullethit2();
                    //checkhite2();
                }

            }

            if (e.KeyCode == Keys.S)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (YScroll + 5 <= (Lmap[0].img[i].Height - ClientSize.Height))
                    {
                        YScroll += 5;
                    }
                }

            }

            if (e.KeyCode == Keys.W)
            {
                if (YScroll - 5 >= 0)
                {
                    YScroll -= 5;
                }
            }


            if (e.KeyCode == Keys.F)
            {
                Lhero[0].iF = attackframe;



                attackframe++;
                if (attackframe == 34)
                {
                    attackframe = 29;
                }

                bullet pnn = new bullet();
                pnn.x = Lhero[0].X + Lhero[0].img[21].Width;
                pnn.y = Lhero[0].Y + ((Lhero[0].img[21].Height / 2) - 20);
                pnn.img = new Bitmap("CC.bmp");
                pnn.dx = 1;

                lbullet.Add(pnn);

                AnimateBull();
                if(flag == 1)
                {
                    checkbullethit1();
                }
                

                if(flag == 2)
                {
                    checkbullethit2();
                }
            }


            if (e.KeyCode == Keys.C)
            {


                Lhero[0].iF = secondattackframe;



                secondattackframe++;
                if (secondattackframe == 40)
                {
                    secondattackframe = 35;
                }


                bullet pnn = new bullet();
                pnn.x = Lhero[0].X + Lhero[0].img[21].Width;
                pnn.y = Lhero[0].Y + ((Lhero[0].img[21].Height / 2) - 20);
                pnn.img = new Bitmap("CC.bmp");

                single = pnn;
                AnimateBullsingle();
                //checkbullethit1();

                flag2 = 1;
            }

            if (e.KeyCode == Keys.E)
            {
                Checkhitladder();



                if (donehit == 1)
                {
                    Lhero[0].Y = lladder[which].Y - Lhero[0].img[1].Height;
                    donehit = 0;

                }




            }

            if (e.KeyCode == Keys.Space)
            {
                int i = 0;
                jumpdis = Lhero[0].Y;

                //Lhero[0].X += 60;

                for (; ; )
                {
                    if (i % 5 == 0)
                    {
                        Lhero[0].iF = jumpframe;
                        Lhero[0].Y -= 10;
                        DrawDubb(this.CreateGraphics());
                        jumpframe++;


                        if (jumpframe == 28)
                        {
                            jumpframe = 21;
                            break;
                        }


                    }

                    i++;
                }

                Lhero[0].Y = jumpdis;

            }



            ModifyRects();

            DrawDubb(this.CreateGraphics());
        }



        void heroidle()
        {
            Hero pnn = new Hero();
            pnn.img = new List<Bitmap>();
            for (int i = -1; i < 40; i++)
            {
                Bitmap pimg = new Bitmap((i + 1) + ".bmp");
                pnn.img.Add(pimg);
            }
            pnn.X = this.ClientSize.Width / 30;
            pnn.Y = this.ClientSize.Height - pnn.img[0].Height;
            pnn.iF = 0;
            Lhero.Add(pnn);
        }

        void createenemy1()
        {
            Enemy pnn = new Enemy();
            pnn.img = new List<Bitmap>();
            for (int i = 40; i < 46; i++)
            {
                Bitmap pimg = new Bitmap((i + 1) + ".bmp");
                pnn.img.Add(pimg);
            }
            pnn.X = this.ClientSize.Width;
            pnn.Y = this.ClientSize.Height - pnn.img[0].Height;
            pnn.iF = 0;
            Lenemy.Add(pnn);
        }

        void animateenemy1()
        {
            for (int i = 0; i < Lenemy.Count; i++)
            {
                Lenemy[i].X -= 15;
                Lenemy[i].iF = enemyoneframeattack;
                enemyoneframeattack++;
                if (enemyoneframeattack == 6)
                {
                    enemyoneframeattack = 0;
                }
            }
        }

        void checkhite1()
        {

            for (int i = 0; i < Lenemy.Count; i++)
            {
                int xH = Lhero[0].X;
                int yH = Lhero[0].Y;

                int xB = Lenemy[i].X;
                int yB = Lenemy[i].Y;
                int xB2 = Lenemy[i].X + Lenemy[i].img[1].Width;

                if (xH >= xB && xH <= xB2 && yH >= yB)
                {
                    over = 1;
                    this.Hide();
                    Form2 f2 = new Form2();
                    f2.ShowDialog();
                    this.Close();
                    break;
                }

            }


        }

        void checkbullethit1()
        {

            for (int i = 0; i < Lenemy.Count; i++)
            {

                for (int k = 0; k < lbullet.Count; k++)
                {
                    int xH = lbullet[k].x;
                    int yH = lbullet[k].y;

                    int xB = Lenemy[i].X;
                    int yB = Lenemy[i].Y;
                    int xB2 = Lenemy[i].X + Lenemy[i].img[1].Width;

                    if (xH >= xB && xH <= xB2 && yH >= yB)
                    {
                        //animation of him die
                        Lenemy[i].X = -600;
                        lbullet[k].x = this.ClientSize.Width + 600;
                    }
                }


            }

        }

        

        void checkbullethitsingle1()
        {

            for (int i = 0; i < Lenemy.Count; i++)
            {

                //for (int k = 0; k < single.Count; k++)
                //{
                    int xH = single.x;
                    int yH = single.y;

                    int xB = Lenemy[i].X;
                    int yB = Lenemy[i].Y;
                    int xB2 = Lenemy[i].X + Lenemy[i].img[1].Width;

                    if (xH >= xB && xH <= xB2 && yH >= yB)
                    {
                       
                        Lenemy[i].X = -600;
                        single.x = this.ClientSize.Width + 600;
                    }
                //}


            }

        }

        void createenemy2()
        {

            Enemy pnn = new Enemy();
            pnn.img = new List<Bitmap>();
            for (int i = 46; i < 58; i++)
            {
                Bitmap pimg = new Bitmap((i + 1) + ".bmp");
                pnn.img.Add(pimg);
            }
            pnn.X = this.ClientSize.Width;
            pnn.Y = ay;
            pnn.iF = 0;
            Lenemy2.Add(pnn);
            if (flag4 == 1)
            {
                ay = ((lland[0].Y - Lenemy2[0].img[0].Height) / 2 ) + 20;
            }
            if (flag4 == 0)
            {
                
                ay = lland[0].Y - Lenemy2[0].img[0].Height;

                flag4 = 1;
            }
           
            
        }

        void animateenemyrun2()
        {
            for (int i = 0; i < Lenemy2.Count; i++)
            {
                enemydis2 = Lenemy2[i].X;


                if (Lenemy2[i].X > Lhero[0].X + 600)
                {
                    Lenemy2[i].done = 1;
                    
                    Lenemy2[i].X -= 15;
                    Lenemy2[i].iF = enemytwoframerun;
                    enemytwoframerun++;
                    if (enemytwoframerun == 12)
                    {
                        enemytwoframerun = 6;
                    }


                }



            }
        }

        void animateenemyattack2() // create el bullet bta3t abo 7osayba
        {
            for (int i = 0; i < Lenemy2.Count; i++)
            {


                //for (int i = 0; i < Lenemy2.Count; i++)
                //{
                //    if (flag == 2 && cttick % 80 == 0 && Lenemy2[i].done == 1)
                //    {
                //        bullet pnn = new bullet();
                //        pnn.x = Lenemy2[0].X + Lenemy2[0].img[47].Width;
                //        pnn.y = Lenemy2[0].Y + ((Lenemy2[0].img[47].Height / 2) - 20);
                //        pnn.img = new Bitmap("CCC.bmp");
                //        pnn.dx = 1;

                //        lbullet2.Add(pnn);
                //    }
                //}


                if (Lenemy2[i].done == 1)
                {
                    bullet pnn = new bullet();
                    pnn.x = Lenemy2[0].X - Lenemy2[i].img[0].Width;
                    pnn.y = Lenemy2[0].Y - ((Lenemy2[i].img[0].Height / 2) - 20);
                    pnn.img = new Bitmap("CCC.bmp");
                    pnn.dx = 1;

                    lbullet2.Add(pnn);

                    Lenemy2[i].iF = enemytwoattackframe2;



                    enemytwoattackframe2++;
                    if (enemytwoattackframe2 == 6)
                    {
                        enemytwoattackframe2 = 0;
                    }


                }



            }
        }

        void checkhite2() // check bullet mn el emnemy 3la el hero
        {

            for (int i = 0; i < Lhero.Count; i++)
            {

                for (int k = 0; k < lbullet2.Count; k++)
                {
                    int xH = lbullet2[k].x;
                    int yH = lbullet2[k].y;

                    int xB = Lhero[i].X;
                    int yB = Lhero[i].Y;
                    int xB2 = Lhero[i].X + Lhero[i].img[1].Width;

                    if (xH >= xB && xH <= xB2 && yH >= yB)
                    {


                        over = 1;
                        this.Hide();
                        Form2 f2 = new Form2();
                        f2.ShowDialog();
                        this.Close();
                        break;



                    }
                }


            }


        }

        void checkbullethit2() // mn elhero ll enemy 
        {

            for (int i = 0; i < Lenemy2.Count; i++)
            {

                for (int k = 0; k < lbullet.Count; k++)
                {
                    int xH = lbullet[k].x;
                    int yH = lbullet[k].y;

                    int xB = Lenemy2[i].X;
                    int yB = Lenemy2[i].Y;
                    int xB2 = Lenemy2[i].X + Lenemy2[i].img[1].Width;

                    if (xH >= xB && xH <= xB2 && yH >= yB)
                    {
                        
                        Lenemy2[i].X = -600;
                        lbullet[k].x = this.ClientSize.Width + 600;
                    }
                }


            }

        }


        void checkbullethitsingle2()// BE2A SHIF BTNADY FEN
        {

            for (int i = 0; i < Lenemy2.Count; i++)
            {

                
                int xH = single.x;
                int yH = single.y;

                int xB = Lenemy2[i].X;
                int yB = Lenemy2[i].Y;
                int xB2 = Lenemy2[i].X + Lenemy2[i].img[1].Width;

                if (xH >= xB && xH <= xB2 && yH >= yB)
                {

                    Lenemy2[i].X = -600;
                    single.x = this.ClientSize.Width + 600;
                }
                


            }

        }



        ////void createele()
        ////{
        ////    ele pnn = new ele();
        ////    pnn.img = new Bitmap("A.bmp");
        ////    pnn.X = this.ClientSize.Width / 2;
        ////    pnn.Y = this.ClientSize.Height / 2;

        ////    lele.Add(pnn);


        ////    pnn = new ele();
        ////    pnn.img = new Bitmap("B.bmp");
        ////    pnn.X = ((this.ClientSize.Width / 2) - 500);
        ////    pnn.Y = this.ClientSize.Height / 2;

        ////    lele.Add(pnn);


        ////    pnn = new ele();
        ////    pnn.img = new Bitmap("C.bmp");
        ////    pnn.X = ((this.ClientSize.Width / 2) + 500);
        ////    pnn.Y = this.ClientSize.Height / 2;

        ////    lele.Add(pnn);
        ////}

        //////void elemove()
        //////{
        //////    for (int i = 0; i < 3; i++)
        //////    {
        //////        lele[i].Y += 10;

        //////        if (lele[i].Y >= 800)
        //////        {
        //////            lele[i].Y -= 10;
        //////        }
        //////    }
        //////}

        void createladder()
        {
            ele pnn = new ele();
            pnn.img = new Bitmap("L.bmp");
            pnn.X = (this.ClientSize.Width / 2) - 900;
            pnn.Y = (this.ClientSize.Height / 2) + 260;

            lladder.Add(pnn);

            pnn = new ele();
            pnn.img = new Bitmap("L.bmp");
            pnn.X = (this.ClientSize.Width / 4) + 200;
            pnn.Y = (this.ClientSize.Height / 2) - 5;

            lladder.Add(pnn);


            pnn = new ele();
            pnn.img = new Bitmap("L.bmp");
            pnn.X = this.ClientSize.Width - 200;
            pnn.Y = (this.ClientSize.Height / 2) - 290;

            lladder.Add(pnn);
        }

        void Checkhitladder()
        {
            for (int i = 0; i < lladder.Count; i++)
            {
                int xH = Lhero[0].X;
                int yH = Lhero[0].Y;

                int xB = lladder[i].X;
                int yB = lladder[i].Y;
                int xB2 = lladder[i].X + lladder[i].img.Width;

                if (xH >= xB && xH <= xB2)
                {
                    which = i;
                    donehit = 1;
                }

            }

        }

        void createlaser()
        {
            Random RR = new Random();
            Hero pnn = new Hero();
            pnn.X = RR.Next(0, this.ClientSize.Width);
            pnn.Y = 0;
            pnn.img = new List<Bitmap>();
            for (int i = 0; i < 4; i++)
            {
                Bitmap im = new Bitmap((i + 59) + ".bmp");
                pnn.img.Add(im);
            }

            llaser.Add(pnn);
        }

        void animatelaser()
        {
            for (int i = 0; i < llaser.Count; i++)
            {

                llaser[i].Y += 25;

                llaser[i].iF++;

                if (llaser[i].iF >= llaser[i].img.Count)
                {
                    llaser[i].iF = 0;
                }
            }
        }

        void checklaserhit()
        {
            for (int i = 0; i < llaser.Count; i++)
            {
                int xH = Lhero[0].X;
                int yH = Lhero[0].Y;
                int xH2 = Lhero[0].X + Lhero[0].img[0].Width;
                int yH2 = Lhero[0].Y + Lhero[0].img[0].Height;

                int xB = llaser[i].X;
                int yB = llaser[i].Y;
                int xB2 = llaser[i].X + llaser[i].img[0].Width;
                int yB2 = llaser[i].Y + llaser[i].img[0].Height;

                if (xB >= xH && yB >= yH && xB2 < xH2 && yB2 < yH2)
                {
                    laserishit = true;
                }

            }
        }

        void createland()
        {

            int afs = 0;
            for (int i = 0; i < 360; i++)
            {
                ele pnn = new ele();
                pnn.img = new Bitmap("BBB.bmp");

                if (i == 120)
                {
                    afs = 0;
                }

                if (i == 240)
                {
                    afs = 0;
                }

                pnn.X = afs;

                if (i < 120)
                {
                    pnn.Y = (this.ClientSize.Height / 2) + 250;
                }

                if (i < 240 && i > 120)
                {
                    pnn.Y = (this.ClientSize.Height / 2) - 30;
                }

                if (i < 360 && i > 240)
                {
                    pnn.Y = (this.ClientSize.Height / 2) - 315;
                }


                lland.Add(pnn);

                afs += boxwidth;

            }



        }


        void AnimateBull()
        {
            for (int i = 0; i < lbullet.Count; i++)
            {
                bullet ptrav = lbullet[i];
                ptrav.x += ptrav.dx * 30;
            }

        }

        void AnimateBull2()
        {
            for (int i = 0; i < lbullet2.Count; i++)
            {
                bullet ptrav = lbullet2[i];
                ptrav.x += ptrav.dx * 20;
            }

        }

        void AnimateBullsingle()
        {
            for (int i = 0; i < 1; i++)
            {

                single.x -= single.dx * 30;
            }

        }


        void CreateELE()
        {
            ele pnn = new ele();
            pnn.img = new Bitmap("AAAA.bmp");
            pnn.X = this.ClientSize.Width - pnn.img.Width;
            pnn.Y = 10;

            lele.Add(pnn);
        }

        void AnimateElevator()
        {
            for (int i = 0; i < lele.Count; i++)
            {

                lele[i].Y += elespeed;

                if (lele[i].Y >= this.ClientSize.Height - lele[0].img.Height)
                {
                    elespeed = -15;
                }
                if (lele[i].Y <= 0)
                {
                    elespeed = 15;
                }

            }
        }
        void CheckHitEle()
        {


            int xH = Lhero[0].X;
            int yH = Lhero[0].Y;
            int xH2 = Lhero[0].X + Lhero[0].img[0].Width;
            int yH2 = Lhero[0].Y + Lhero[0].img[0].Height;

            int xB = lele[0].X;
            int yB = lele[0].Y;
            int xB2 = lele[0].X + lele[0].img.Width;
            int yB2 = lele[0].Y + lele[0].img.Height;

            if (xH >= xB && xH2 <= xB2 && yH2 <= yB2)
            {
                

                Lhero[0].Y = lele[0].Y - lele[0].img.Height - Lhero[0].img[0].Height;
            }


        }

        void t_Tick(object sender, EventArgs e)
        {
            Lhero[0].iF = idleframe;
            idleframe++;
            if (Lhero[0].iF == 7)
            {
                idleframe = 0;
            }


            if (cttick % 5 == 0)
            {
                //elemove();
            }

            if (flag == 1 && cttick % 80 == 0)
            {
                createenemy1();
            }


            if (flag == 2 && cttick % 80 == 0 && count < 3)
            {
                createenemy2();
                count++;
            }


            if (cttick % 20 == 0)
            {
                createlaser();




            }
            checklaserhit();
            animatelaser();


            

            if (laserishit)
            {
                if (close == 0)
                {
                    close = 1;
                    over = 1;
                    this.Hide();
                    Form2 f2 = new Form2();
                    f2.ShowDialog();
                    this.Close();

                }
            }


           

            if (over == 0)
            {
                checkhite1();
            }

            checkbullethit1();

            checkbullethit2();

            checkhite2();

            AnimateBull();

            AnimateBull2();

            AnimateBullsingle();

            AnimateElevator();

            checkbullethitsingle1();

            checkbullethitsingle2();

            animateenemy1();

            animateenemyattack2();

            animateenemyrun2();

            cttick++;

            DrawDubb(this.CreateGraphics());
        }


        void ModifyRects()
        {
            Lmap[0].rcSrc = new Rectangle(XScroll, YScroll, ClientSize.Width, ClientSize.Height);
        }

        void CreateWorld()
        {
            Map pnn = new Map();
            pnn.img = new List<Bitmap>();
            Bitmap pimgl = new Bitmap("BG" + (1) + ".bmp");
            pnn.img.Add(pimgl);
            Bitmap pimgl2 = new Bitmap("BG" + (2) + ".bmp");
            pnn.img.Add(pimgl2);
            Bitmap pimgl3 = new Bitmap("BG" + (3) + ".bmp");
            pnn.img.Add(pimgl3);
            pnn.rcDst = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            pnn.rcSrc = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

            Lmap.Add(pnn);
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }

        void DrawScene(Graphics g)
        {
            if (flag == 1)
            {
                g.Clear(Color.LightBlue);

                for (int i = 0; i < Lmap.Count; i++)
                {
                    g.DrawImage(Lmap[i].img[0], Lmap[i].rcDst, Lmap[i].rcSrc, GraphicsUnit.Pixel);
                }

                for (int i = 0; i < Lenemy.Count; i++)
                {
                    g.DrawImage(Lenemy[i].img[Lenemy[i].iF], Lenemy[i].X, Lenemy[i].Y);
                }

                //for (int i = 0; i < lladder.Count; i++)
                //{
                //    g.DrawImage(lladder[i].img, lladder[i].X, lladder[i].Y);
                //}

                for (int i = 0; i < Lhero.Count; i++)
                {
                    g.DrawImage(Lhero[i].img[Lhero[i].iF], Lhero[i].X, Lhero[i].Y);
                }

                for (int i = 0; i < lbullet.Count; i++)
                {
                    bullet ptrav = lbullet[i];
                    g.DrawImage(ptrav.img, ptrav.x, ptrav.y);
                }

                for (int i = 0; i < llaser.Count; i++)
                {
                    g.DrawImage(llaser[i].img[llaser[i].iF], llaser[i].X, llaser[i].Y);
                }

                if (flag2 == 1)
                {
                    for (int i = 0; i < 1; i++)
                    {

                        g.DrawImage(single.img, single.x, single.y);
                    }
                }

            }

            if (flag == 2)
            {
                g.Clear(Color.LightBlue);

                for (int i = 0; i < Lmap.Count; i++)
                {
                    g.DrawImage(Lmap[i].img[1], Lmap[i].rcDst, Lmap[i].rcSrc, GraphicsUnit.Pixel);
                }

                for (int i = 0; i < lele.Count; i++)
                {
                    g.DrawImage(lele[i].img, lele[i].X, lele[i].Y);
                }

                for (int i = 0; i < llaser.Count; i++)
                {
                    g.DrawImage(llaser[i].img[llaser[i].iF], llaser[i].X, llaser[i].Y);
                }



                for (int i = 0; i < lland.Count; i++)
                {
                    g.DrawImage(lland[i].img, lland[i].X, lland[i].Y);
                }




                for (int i = 0; i < lladder.Count; i++)
                {
                    g.DrawImage(lladder[i].img, lladder[i].X, lladder[i].Y);
                }






                for (int i = 0; i < Lenemy2.Count; i++)
                {
                    g.DrawImage(Lenemy2[i].img[Lenemy2[i].iF], Lenemy2[i].X, Lenemy2[i].Y);
                }

                for (int i = 0; i < Lhero.Count; i++)
                {
                    g.DrawImage(Lhero[i].img[Lhero[i].iF], Lhero[i].X, Lhero[i].Y);
                }

                for (int i = 0; i < lbullet.Count; i++)
                {
                    bullet ptrav = lbullet[i];
                    g.DrawImage(ptrav.img, ptrav.x, ptrav.y);
                }

                if (flag2 == 1)
                {
                    for (int i = 0; i < 1; i++)
                    {

                        g.DrawImage(single.img, single.x, single.y);
                    }
                }
            }


            if (flag == 3)
            {
                g.Clear(Color.LightBlue);

                for (int i = 0; i < Lmap.Count; i++)
                {
                    g.DrawImage(Lmap[i].img[2], Lmap[i].rcDst, Lmap[i].rcSrc, GraphicsUnit.Pixel);
                }


                for (int i = 0; i < Lhero.Count; i++)
                {
                    g.DrawImage(Lhero[i].img[Lhero[i].iF], Lhero[i].X, Lhero[i].Y);
                }


                for (int i = 0; i < lele.Count; i++)
                {
                    g.DrawImage(lele[i].img, lele[i].X, lele[i].Y);
                }

                for (int i = 0; i < lbullet.Count; i++)
                {
                    bullet ptrav = lbullet[i];
                    g.DrawImage(ptrav.img, ptrav.x, ptrav.y);
                }

            }


        }

        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
