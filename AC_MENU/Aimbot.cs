using System.Runtime.InteropServices;
using System.Threading;
using ezOverLay;


namespace AC_MENU
{
    public partial class Aimbot : Form
    {
        ez ez = new ez();
        methods? m;

        Entity localPlayer = new Entity();

        List<Entity> entities = new List<Entity>();

        [DllImport("user32.dll")]

        static extern short GetAsyncKeyState(Keys vKey);


        public Aimbot()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            m = new methods();

            if (m != null) // verifica se o processo é diferente de nulo ai ele inicia
            {
                ez.SetInvi(this);
                ez.DoStuff("AssaultCube", this);

                Thread thread = new Thread(Main) { IsBackground = true }; //função para criar a thead e rodar na janela
                thread.Start();
            }


        }

        void Main()
        {
            while (true)
            {
                localPlayer = m.ReadLocalPlayer(); //chama a função para ler as informações do jogador
                entities = m.ReadEntities(localPlayer); //le as entidades relacionadas com o jogador


                entities = entities.OrderBy(o => o.mag).ToList(); //ordena por usando magnitude para buscar a entidade mais proxima
                if (GetAsyncKeyState(Keys.XButton2) < 0) //se o botao do mouse for ativado ele entra no if
                {
                    if (entities.Count > 0)
                    {
                        foreach (var ent in entities)
                        {
                            if (ent.team != localPlayer.team) // se a entidade tirar um numero time diferente
                            {
                                var angles = m.CalcAngles(localPlayer, ent); //pega os angulos com a função de calculo
                                m.Aim(localPlayer, angles.X, angles.Y); //chama a função aimbot
                                break;
                            }
                        }

                    }

                }

                Aimbot f = this;
                f.Refresh();


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
                var wtsFeet = m.WorldToScreen(m.ReadMatrix(), ent.feet, this.Width, this.Height);
                var wtsHead = m.WorldToScreen(m.ReadMatrix(), ent.head, this.Width, this.Height);

                if (wtsFeet.X > 0)
                {
                    if (localPlayer.team == ent.team)
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