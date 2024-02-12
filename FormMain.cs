using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace crazy_buttons
{
    public partial class FormMain : Form
    {
        Thread t1;
        Thread t2;
        Thread t3;
        Thread t4;

        static Random r;

        ButtonComparable[] buttons;


        public delegate void HelpToCall(Button btn);
        HelpToCall helper;
        public FormMain()
        {
            helper = new HelpToCall(Motion);
            r = new Random();
            InitializeComponent();
            buttons = new ButtonComparable[4] { btnOne, btnTwo,btnThree, btnFour };
        }
 
// ------------------------------------------------------------------- ***
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (t1 == null)
            {
                t1 = new Thread(Movement1);
                t2 = new Thread(Movement2);
                t3 = new Thread(Movement3);
                t4 = new Thread(Movement4);
                t1.IsBackground = true;
                t2.IsBackground = true;
                t3.IsBackground = true;
                t4.IsBackground = true;
                t1.Start();
                t2.Start();
                t3.Start();
                t4.Start();

            }
            else 
            {
                t1.Resume();
                t2.Resume();
                t3.Resume();
                t4.Resume();
            }
            btnStart.Enabled = false;
            btnPause.Enabled = true;
            btnStop.Enabled = true;
            
            
        }
      

        private void btnPause_Click(object sender, EventArgs e)
        {
            if(t1 != null) 
            {
                t1.Suspend();
                t2.Suspend();
                t3.Suspend();
                t4.Suspend();
            }
            btnStart.Enabled = true;
            btnPause.Enabled = false;
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnPause_Click(sender, e);
            ResetGame();
            btnStop.Enabled = false;

        }

        private void ResetGame()
        {
            btnOne.Location = new Point(10, btnOne.Location.Y);
            btnTwo.Location = new Point(10, btnTwo.Location.Y);
            btnThree.Location = new Point(10, btnThree.Location.Y);
            btnFour.Location = new Point(10, btnFour.Location.Y);
            foreach(ButtonComparable btnComparable in buttons) 
            {
                btnComparable.BackColor = SystemColors.Control;
            }

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnPause_Click(sender, e);
        }

// ------------------------------------------------------------------- ///
        void Motion(Button btn)
        {
            btn.Location = new Point(btn.Location.X + r.Next(2, 8), btn.Location.Y);
            Lider();
            Finish(btn);

        }

        private void Lider()
        {
            Array.Sort(buttons);
            buttons[0].BackColor = Color.Yellow;
            for(int i=1; i<buttons.Length; i++) 
            {
                buttons[i].BackColor =SystemColors.Control;
            }
        }

        private void Finish(Button btn) 
        { 
            if((btn.Location.X + btn.Width)>= pictureBoxFinish.Location.X) 
            {
                btnPause_Click(new Object(), new EventArgs());
                MessageBox.Show("Выиграла кнопка: " + btn.Text);
                btnStart.Enabled = false; 
            }
        }

        void Movement1() 
        {
            while (true) 
            {
                Invoke(helper, btnOne);
                Thread.Sleep(r.Next(30, 100));
            }
        }
        void Movement2()
        {
            while (true)
            {
                Invoke(helper, btnTwo);
                Thread.Sleep(r.Next(30, 100));
            }
        }
        void Movement3()
        {
            while(true) 
            {
                Invoke(helper, btnThree);
                Thread.Sleep(r.Next(30, 100));
            }
        }
        void Movement4()
        {
            while (true) 
            { 
                Invoke(helper, btnFour);
                Thread.Sleep(r.Next(30, 100));
            }
        }


    }
}
