using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using PRMResponses = ZUT.MobileRobots.PRM.Commands.Results;
using PRMServicesCommon = ZUT.MobileRobots.PRM.Services.Common;
using PRMServices = ZUT.MobileRobots.PRM.Services.StatusService;
using System.Windows.Input;
using System.Threading;
using System.Drawing.Drawing2D;

namespace RoboApp
{
    public partial class Form2 : Form
    {
        int gotowy = 1;
        int Czekaj = 0;
        string stan;
        EndpointAddress actionEndpoint = new EndpointAddress("http://192.168.2.20:8733/Design_Time_Addresses/ZUT.MobileRobots.PRM.Services.StatusService/PlatformAction/");
        EndpointAddress controlEndpoint = new EndpointAddress("http://192.168.2.20:8733/Design_Time_Addresses/ZUT.MobileRobots.PRM.Services.StatusService/RobotControl/");
        EndpointAddress checkObject = new EndpointAddress("http://192.168.2.20:8733/Design_Time_Addresses/ZUT.MobileRobots.PRM.Services.StatusService/PlatformStatus/");
        EndpointAddress Obiekty = new EndpointAddress("http://192.168.2.20:8733/Design_Time_Addresses/ZUT.MobileRobots.PRM.Services.Common/Obstacle/");
        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_Shown(object sender, EventArgs e)
        {

            Szukanie.RunWorkerAsync();
            Kontynuacja.Enabled = false;

        }

      

        private void label1_Click(object sender, EventArgs e)
        {

        }

       

       
        private void Szukanie_DoWork(object sender, DoWorkEventArgs e)
        {
            Form Aplikacja_główna = new Form1();
            Aplikacja_główna.Enabled = false;

            int robot = 0;
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            var cFactoryControl = new ChannelFactory<PRMServices.IRobotControl>(binding, controlEndpoint);
            var cFactoryCheck = new ChannelFactory<PRMServices.IPlatformStatus>(binding, checkObject);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            PRMServices.IRobotControl controlService = cFactoryControl.CreateChannel();
            PRMServices.IPlatformStatus platformStatus = cFactoryCheck.CreateChannel();
            label1.Invoke((MethodInvoker)(() => label1.Text = "Szukanie robotów..."));
            while (gotowy != 0)
            {
                gotowy = 0;
                var roboty = platformStatus.GetTableRobots();
                robot = roboty.Count();
                foreach (var robo in roboty)
                {
                    stan = Convert.ToString(robo.ConnState);
                    if (stan == "DISCONNECTED")//sprawdzamy czy robot jest połączony, jeśli nie, inkrementujemy zmienną 'gotowy', powtarzamy całą pętlę co czasu, aż zmienna 'gotowy' będzie równa 0, co oznacza, że wszystkie roboty zostały zidentyfikowane
                     {
                        gotowy++;
                    }
                }
                if (progressBar1.Value < 99) //Wypełniamy pasek progresu co 500ms
                    progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value++)); 
                else Czekaj++; //Jeśli osiągnie 99 czekamy jeszcze dzięsięć pętli
                if (Czekaj == 10)
                    break; //po dziesięciu pętlach wychodzimy z całej pętli
                Thread.Sleep(500);
                actionService.Reidentify();
            }
            progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = 100));
            Kontynuacja.Invoke((MethodInvoker)(() => Kontynuacja.Enabled = true));
            if (Czekaj == 0)
            {
                label1.Invoke((MethodInvoker)(() => label1.Text = "Znaleziono " + robot + " robota/ty")); //Podajemy ilość robotów, które znajdują się na stole i są zidentyfikowane
            }
            else label1.Invoke((MethodInvoker)(() => label1.Text = "Nie wszystkie roboty zostały zidentyfikowane")); //Jeśli kamera widzi roboty, ale nie zindentyfikowała ich wszystkich to wyświetlamy ten komunikat
        }

        private void Kontynuacja_Click(object sender, EventArgs e)
        {
            Form Aplikacja_główna = new Form1();
            Aplikacja_główna.Enabled = true; 
            this.Close();
        }
    }
}
