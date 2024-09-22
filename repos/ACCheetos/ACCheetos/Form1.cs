using ezOverLay;
using System.Runtime.InteropServices;

namespace ACCheetos
{
    public partial class Form1 : Form
    {
        Methods m = new Methods();
        Entity localPlayer;
        List<Entity> entities;
        ez ez = new ez();

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKeys);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (!(m == null))
            {
                ez.SetInvi(this);
                ez.DoStuff("AssaultCube", this);
                Thread thread = new Thread(Main) { IsBackground = true };
                thread.Start();
            }
        }

        void Main()
        {
            while (true)
            {
                localPlayer = m.ReadLocalPlayer();
                entities = m.ReadEntities(localPlayer);
                entities.OrderBy(x => x.Mag).ToList();

                if (GetAsyncKeyState(Keys.XButton1) < 0)
                {
                    if (entities.Count > 0)
                    {
                        foreach (var ent in entities)
                        {
                            if (ent.Team != localPlayer.Team)
                            {
                                var angles = m.CalcAngles(localPlayer, ent);
                                m.Aim(localPlayer, angles.X, angles.Y);
                                break;
                            }
                        }
                    }


                }
                this.Refresh();
                Thread.Sleep(20);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen red = new Pen(Color.Red, 3);
            Pen green = new Pen(Color.Green, 3);

            foreach (var ent in entities.ToList())
            {
                var wtsFeet = m.WorldToScreen(m.ReadMatrix(), ent.Feet, this.Width, this.Height);
                var wtsHead = m.WorldToScreen(m.ReadMatrix(), ent.Head, this.Width, this.Height);

                if (wtsFeet.X > 0)
                {
                    if (localPlayer.Team == ent.Team)
                    {
                        g.DrawLine(green, new Point(Width / 2, Height), wtsFeet);
                        g.DrawRectangle(green, m.CalcRect(wtsFeet, wtsHead));
                    }
                    else
                    {
                        g.DrawLine(red, new Point(Width / 2, Height), wtsFeet);
                        g.DrawRectangle(red, m.CalcRect(wtsFeet, wtsHead));
                    }
                }
            }
        }
    }
}
