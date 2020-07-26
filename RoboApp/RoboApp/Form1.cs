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
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
namespace RoboApp
{//Największym problemem w programie jest część jazdy bezkolizyjnej do celu, testując go robot albo jechał do celu po slamomie albo ten punkt ignorował albo jechał wręcz w przeciwnym kieurnku od niego. Fragment, w którym rozpoczyna się współpaca zaczyna się od linijki 1261.
 //Znajduje się on w BGWorkerze Jazda_do_celu. Na wizualizacji zostawiłem textboxy pokazujące współrzędne wybranego robota w układzie stołu oraz w układzie PP, po lewej stronie jeszcze textboxy z otrzymywaną prękością obrotową oraz liniową z funkcji controller.
 //Innym małym problemem jest to, że na wizualizacji wyświetlane okręgi wokół robotów mocno mrygają.
 //Rysowanie ścieżki jest jeszcze umieszczone w BGWorkerze, więc od razu zniknie z obrazu, gdyż obraz jest co chwilę czyszczony, by nie występował efekt smugi, będzie trzeba to przenieść.
 //Trzeba też dodać na wszelki wypadek event, który wypuszcza robota jeśli ten jest przywłaszczany w trakcie zamykania aplikacji
 //Sterowanie manualne działa, ma dwa tryby Symulacyjny i Arcadowy, program startuje w trybie Symulacyjnym, lecz przy starcie program startuje w trybie sterowania automatycznego i panel maualny znika, a klawisze WSAD nie reagują, reaguje spacja, by mogła zatrzymać robota w trakcie jazdy do celu
 //Tryb Symulacyjny działa w następujący sposób:
 //Guzikami lub przyciskami WSAD  stopniowe zwiększamy  wartości "Zwrot" ,"Kierunek_prawo" lub "Kierunek_lewo", a robot na ich podstawie nadaje kołom odpowiednią prędkość, prękości te zostają nawet przy puszczeniu guzików, spacją lub guzikiem Stop resetujemy te wartości przez co robot staje
 //Tryb Arcadowy działa następująco:
 //Guzikami "w przód' i 'w tył' oraz W i S stopnio przyspieszamy bądź hamujemy robotem. Robot sam z siebie powoli zwalnia gdy żaden z guzików nie jest wciśnięty. Guzikami 'w prawo' i 'w lewo' oraz A i D możemy skręcać robotem w prawo lub w lewo. Prędkość skrętu zależy tylko od prędkości liniowej robota, czyli od zmiennej "Zwrot"
 //Przełączenie sterowania na manulane ma zatrzymać robota, jeśli ten by wykonywał jazdę do celu, odkrywa także panel sterowania manualnego i aktywuje WSAD'a
    public partial class Form1 : Form
    {
        int Zwrot = 0, //Zmienna determinująca kierunek jazdy(przód,tył), przyjmuje wartości od -10 do 10, wykorzystywana jest do pobierania wartości prędkości z wektora "Prędkości"
            W_przód = 0, //Zmienna sprawdzająca, czy przycisk w przód(guzik na wizualizacji, albo klawisz W) jest wciśnięty(używane do drugiego trybu sterowania)
            W_tył = 0, //Zmienna sprawdzająca, czy przycisk w tył(guzik na wizualizacji, albo klawisz S) jest wciśnięty(używane do drugiego trybu sterowania)
            Znak_minusa, //Zmienna determinująca czy robot ma jechać w przód czy w tył(używane do drugiego trybu sterowania)
            W_lewo = 0,//Zmienna sprawdzająca, czy przycisk w lewo(guzik na wizualizacji, albo klawisz A) jest wciśnięty(używane do drugiego trybu sterowania)
            W_prawo = 0, //Zmienna sprawdzająca, czy przycisk w prawo(guzik na wizualizacji, albo klawisz D) jest wciśnięty(używane do drugiego trybu sterowania)
            Kolejność = 0, //Zmienna, z którą porządkujemy informacje o robotach w kolejności rosnącej wartości ID
            Nr_wektora_Porządkowanie = 0, //Zmienna używana do stworzenia nowych wektorów i macierzy z poukładanymi informacjami o robotach
            Nr_wektora_Info_rob = 0, //Zmienna używana do stworzenia wektorów i macierzy z informacjami o robotach wziętych prosto z funkcji GetTableRobots
            Nr_wektora_Rysowanie_okręgów_rob = 0, //Zmienna używana do pobierania wartości z macierzy ze współrzędnymi robotów do rysowania okręgów wokół przywłaszczonych robotów
            Nr_wektora_Stan_rob = 0, //Zmienna używana do wypełniania wektora Lista Stanu
            Kierunek_lewo = 5, //Zmienna odpowiedzialna za skręcanie w lewo, przy wartości 5 robot nie skręca w ogóle, przy 10 skręca maksymalnie(używane do pierwszego trybu sterowania)
            Kierunek_prawo = 5, //Zmienna odpowiedzialna za skręcanie w prawo(podobnie jak w lewo)
            Różnica_prawo_lewo = 0, ////Zmienna, która oddaje różnicę między Kierunek_lewo i Kierunek_prawo determinującą kierunek jazdy robota(używane do pierwszego trybu sterowania)
            Wybor_robota = 0, //Zmienna, która używana jest m.in w case'ie, pokazującym, który robot jest wybrany przez nas, pozwala wyciągnąć te wartości z wektorów i macierzy, które są przypisane do wybranego robota
            Nr_wektora_Rys_rob = 0, //Zmienna wykorzystywana do rysowania robotów na planszy
            Czy_robot_naciśnięty = 2; //Zmienna sprawdzająca, czy punkt na planszy, który został wciśnięty myszką znajduje się w obrębie robota, jeśli tak, to ten robot jest przywłaszczany, jeśli jest wolny; 
                                      //jeśli w tym punkcie nie ma robota, to na planszy rysowany krzyżyk oznaczający punkt, do którego robot ma jechać
          
        double Tick_timer = 0, //Zmienna, która będzie oznaczać czas jazdy robota
               Prędkość_liniowa_PP = 0, //Zmienna wzięta z matlaba z funkcji controller Pure Pursuita, oznaczająca prędkość liniową robota
               Prędkość_kątowa_PP = 0, //Zmienna wzięta z matlaba z funkcji controller Pure Pursuita, oznaczająca prędkość obrotową robota
               Współ_X_Stół =0, //Rzeczywista współrzędna X robota(prosto z kamery)
               Współ_Y_Stół = 0, //Rzeczywista współrzędna Y robota(prosto z kamery)
               Orientacja_Stół = 0, //Rzeczywista orientacja robota(prosto z kamery)
               Współ_X_PP = 0, //Współrzędna X robota przeskalowana na układ Pure Pursuita
               Współ_Y_PP = 0,  //Współrzędna Y robota przeskalowana na układ  Pure Pursuita
               Orientacja_PP = 0,  //Orientacja robota przeskalowana na układ Pure Pursuita
               Współ_Y_Myszka_Przeskalowane_Układ_Stołu = 0, //Współrzędna X punktu końcowego przeskalowana na układ stołu
               Współ_X_Myszka_Przeskalowane_Układ_Stołu = 0, //Współrzędna Y punktu końcowego przeskalowana na układ stołu
               Współ_X_Punkt_Końcowy_PP =0, //Współrzędna X punktu końcowego przeskalowana na układ Pure Pursuit
               Współ_Y_Punkt_Końońcowy_PP = 0;//Współrzędna Y punktu końcowego przeskalowana na układ Pure Pursuit

        float Współ_X_Rysowanie_okręgów, //Zmienna która przypisuje sobie współrzędną X z macierzy współrzędnych robotów, by narysować okrąg wokół robota przywłaszczonego
              Współ_Y_Rysowanie_okręgów, //Zmienna, która przypisuje sobie współrzędną Y z macierzy współrzędnych robotów, by narysować okrąg wokół robota przywłaszczonego
              Punkt_X_Przeszkody, //Zmienna, która przypisuje sobie wspołrzędną X z GetObstacles
              Punkt_Y_Przeszkody,//Zmienna, która przypisuje sobie wspołrzędną Y z GetObstacles
              Współ_X_Myszka_Układ_PictureBox, ////Zmienna, która przypisuje sobie wspołrzędną X pictureBoxa miejsca naciśnięcia na pictureBox u układzie PictureBoxa
              Współ_Y_Myszka_Układ_PictureBox;////Zmienna, która przypisuje sobie wspołrzędną Y pictureBoxa miejsca naciśnięcia na pictureBox u układzie PictureBoxa


        Graphics f;
        Bitmap Draw;
        
        //Pisaki
        Pen BLU = new Pen(Color.Blue);
        Pen Czarny = new Pen(Color.Black);
       
        List<PointF[]> tablicapunktow = new List<PointF[]>();//Lista do której będą wpisywane punkty

        //Wypełnienie do przeszkód
        SolidBrush blueBrush = new SolidBrush(Color.Blue);

        EndpointAddress actionEndpoint = new EndpointAddress("http://192.168.2.20:8733/Design_Time_Addresses/ZUT.MobileRobots.PRM.Services.StatusService/PlatformAction/");
        EndpointAddress controlEndpoint = new EndpointAddress("http://192.168.2.20:8733/Design_Time_Addresses/ZUT.MobileRobots.PRM.Services.StatusService/RobotControl/");
        EndpointAddress checkObject = new EndpointAddress("http://192.168.2.20:8733/Design_Time_Addresses/ZUT.MobileRobots.PRM.Services.StatusService/PlatformStatus/");
        EndpointAddress Obiekty = new EndpointAddress("http://192.168.2.20:8733/Design_Time_Addresses/ZUT.MobileRobots.PRM.Services.Common/Obstacle/");

        Thread Rysowanie_Planszy,Status,Manul,Lista, Różnica_skrętu;
       
        int[] Prędkości = new int[21];//Wektor z prędkościami
        int[]  Nr_ID = new int[8];//W tym wektorze zapisywane są numery ID robotów wzięte z GetTableRobots
        int[] Nr_ID_Uporządkowane = new int[8];//W tym wektorze zapisywane są numery ID robotów wzięte z GetTableRobots, ułożone w kolejności od najniższego nr ID do najwyższego
        int[] Lista_Stanu = new int[8];// Ten wektor odpowiada za stan robota, jest wykorzystywany w graficznym przedstawieniu stanów robotów gdzie wartość 1 oznacza, że robot jest zajęty, 2, że tego robota nie ma(w sytuacji, kiedy nie wykorzytujemy wszystkich robotów) i 0, kiedy robot jest wolny i można go przywłaszczyć
        int[] Theta = new int[8];// W tym wektorze zapisywane są kąt orientacji robotów

        int[] Theta_Uporządkowane = new int[8];// W tym wektorze zapisywane są kąt orientacji robotów ułożone w kolejności ID
        string[] Tekst_stan = new string[8];// W tym wektorze zapisane są stany robotów wzięte z GetTableRobots
        string[] Tekst_stan_Uporządkowane = new string[8];// W tym wektorze zapisane są stany robotów wzięte z GetTableRobots, ułożone do odpowiadających sobie numerom ID
        string[,] Współrzędne_Robota_Przetransferowane = new string[8, 2];// W tym wektorze zapisywane są współrzędne punktów robotów zeskalowane do rozmiaru wizualizacji
        string[,] Współrzędne_Robota_Przetransferowane_Uporządkowane = new string[8, 2];// W tym wektorze zapisywane są współrzędne punktów robotów zeskalowane do rozmiaru wizualizacji, ułożone w kolejności ID
        string[,] Współrzędne_Rzeczywiste_Robota = new string[8, 2];// W tym wektorze zapisywane są rzeczywiste współrzędne punktów robotów
        string[,] Współrzędne_Rzeczywiste_Robota_Uporządkowane = new string[8, 2];// W tym wektorze zapisywane są rzeczywiste współrzędne punktów robotów ułożone w kolejności
       
        List<Pen> Kolory = new List<Pen>();//Lista pisaków
        List<Color> Kolory2 = new List<Color>();//Lista kolorów
        MLApp.MLApp matlab ;


   

        public Form1()
        {
            InitializeComponent();
            matlab= new MLApp.MLApp();
            matlab.Execute(@"cd C:\Users\Worior\Desktop\apki");//folder z matlabem(trzeba zmienić)
            DoubleBuffered = true;     
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            actionService.Reidentify();

            //Lista kolorów do rysowania kółek wokół robotów

            Pen CZ = new Pen(Color.Red);
            Pen ŻÓ = new Pen(Color.Yellow);
            Pen NIE = new Pen(Color.LightBlue);
            Pen ZIEL = new Pen(Color.Green);
            Pen POM = new Pen(Color.Orange);
            Pen BRO = new Pen(Color.Brown);
            Pen FIO = new Pen(Color.Violet);
            Pen GRA = new Pen(Color.DarkBlue);
            Kolory.Add(CZ);
            Kolory.Add(ŻÓ);
            Kolory.Add(NIE);
            Kolory.Add(ZIEL);
            Kolory.Add(POM);
            Kolory.Add(BRO);
            Kolory.Add(FIO);
            Kolory.Add(GRA);
           
            //Lista kolorów, które wypełnią kwadrat kolorem wybranego robota

            Color CZ1 = (Color.Red);
            Color ŻÓ1 = (Color.Yellow);
            Color NIE1 = (Color.LightBlue);
            Color ZIEL1 = (Color.Green);
            Color POM1 = (Color.Orange);
            Color BRO1 = (Color.Brown);
            Color FIO1 = (Color.Violet);
            Color GRA1 = (Color.DarkBlue);
            Kolory2.Add(CZ1);
            Kolory2.Add(ŻÓ1);
            Kolory2.Add(NIE1);
            Kolory2.Add(ZIEL1);
            Kolory2.Add(POM1);
            Kolory2.Add(BRO1);
            Kolory2.Add(FIO1);
            Kolory2.Add(GRA1);

            //Wektor z prędkościami

            Prędkości[0] = -40;
            Prędkości[1] = -38;
            Prędkości[2] = -36;
            Prędkości[3] = -34;
            Prędkości[4] = -32;
            Prędkości[5] = -30;
            Prędkości[6] = -28;
            Prędkości[7] = -26;
            Prędkości[8] = -24;
            Prędkości[9] = -22;
            Prędkości[10] = 0;
            Prędkości[11] = 22;
            Prędkości[12] = 24;
            Prędkości[13] = 26;
            Prędkości[14] = 28;
            Prędkości[15] = 30;
            Prędkości[16] = 32;
            Prędkości[17] = 34;
            Prędkości[18] = 36;
            Prędkości[19] = 38;
            Prędkości[20] = 40;

            Sterowanie.Visible = false; //group box z kontrolkami do sterowania manualnego
           
            //W textboxie pokazuje wartości, które determinują z jaką prędkością jedzie robot i jak szybko skręca i w którą stronę, dla "Zwrot" wartości są w zakresie -10;10 a "Kierunek_lewo" i "Kierunek_prawo" od 0 do 10 

            War_lewo.Text = Convert.ToString(Kierunek_lewo);
            War_prawo.Text = Convert.ToString(Kierunek_prawo);
            War_kierunek.Text = Convert.ToString(Zwrot);

            //Textbox przygotowany na timer

            Tajmer.Text = String.Format("{0:0.00}", Tick_timer);

            //Zaznaczenie początkowego trybu sterowania
            Auto.Checked = true;
            Sterowanie_manualne.Checked = false;
            Sim.Checked = true;
            Arcade.Checked = false;
            
            //Ustawienie by BGWorkery mogły być przerywane

            W_górę.WorkerSupportsCancellation = true; //Bgworker, który zwiększa wartość 'Zwrot' kiedy guzik, lub klawisz 'w przód' jest wciśnięty
            Wprawo.WorkerSupportsCancellation = true;//Bgworker, który zwiększa wartość 'Kierunek_prawo', zmniejszając wartość 'Kierunek_lewo' kiedy guzik, lub klawisz 'w prawo' jest wciśnięty
            Wdół.WorkerSupportsCancellation = true;//Bgworker, który zmniejsza  wartość 'Zwrot', kiedy guzik, lub klawisz 'w tył' jest wciśnięty
            Wlewo.WorkerSupportsCancellation = true;//Bgworker, który zwiększa wartość 'Kierunek_lewo', zmniejszając wartość 'Kierunek_prawo' kiedy guzik, lub klawisz 'w Kierunek_lewo' jest wciśnięty                                                   
            Zwalnianie_przy_jeździe_w_przód.WorkerSupportsCancellation = true;//Bgworker,zmniejsza wartość 'Zwrot' kiedy puszczany jest guzik 'w przód' w drugim trybie jazdy, gdy Zwrot>0
            Zwalnianie_przy_jeździe_w_tył.WorkerSupportsCancellation = true;//Bgworker,większa wartość 'Zwrot' kiedy puszczany jest guzik 'w tył' w drugim trybie jazdy, gdy Zwrot<0
            Jazda_do_celu.WorkerSupportsCancellation = true;//Bgworker,który odpowiada za generowanie ścieżki oraz odpala algorytm PurePursuit
        }
     
        private void Form1_Load(object sender, EventArgs e)
        {// Forma zajmująca się sprawdzaniem ilości robotów na stole i próbie zidentyfikowania każdego z nich

            Form2 czekaj = new Form2();
            czekaj.ShowDialog();

            // Uprzednie uznanie wszystkich robotów za nieobecne na stole

            Lista_Stanu[0] = 2;
            Lista_Stanu[1] = 2;
            Lista_Stanu[2] = 2;
            Lista_Stanu[3] = 2;
            Lista_Stanu[4] = 2;
            Lista_Stanu[5] = 2;
            Lista_Stanu[6] = 2;
            Lista_Stanu[7] = 2;

            // Wątek, który pobiera wszystkie potrzebne informacje o robotach i układa je w kolejności ID, do tego podpisuje roboty umieszczone na wizualizacji

            Lista = new Thread(Lista_rob);
            Lista.Start();

            TextBoxy.RunWorkerAsync(); //BgWorker, który sprawia, że wybrane textboxy aktualizują się w czasie rzeczywistym
            Przywłaszczenie.RunWorkerAsync();//BgWorker, na "panelu" z robotami pokazuje, którego wzięliśmy, tam jest też zmienna rob, która jest wykorzystywana do wybierania odpowiednich zmiennych z macierzy ze współrzędnymi
            Stany_robotów_String.RunWorkerAsync();//BgWorker, który sprawia, stan przywłaszczenia robotów i daje mi odpowiednie do tego wartości
            Stany_Robotów_Wizu.RunWorkerAsync();//BgWorker, który odpowiada, za wizualne przedstawienie stanu robota przez odpowiedni obrazek, np czerwony X

            //Przygotowanie pictureBoxa
            Draw = new Bitmap(Obraz.Size.Width, Obraz.Size.Height);
            Obraz.Image = Draw;
            f = Graphics.FromImage(Draw);
           
            Różnica_skrętu = new Thread(Różnica);//ten wątek liczy różnicę pomiędzy 'Kierunek_prawo' a 'Kierunek_lewo' co determinuje jak będzie skręcał robot podczas pierwszego z trybów jazdy
            Różnica_skrętu.Start();
            Status= new Thread(Kolor);// ten wątek będzie kolorował mi kwadrat na kolor wybranego robota
            Status.Start();
            RYS();//ta funkcja jednorazowo pobiera informacje o przeszkodach i zapisuje je na liście
            Rysowanie_Planszy= new Thread(Rysowanie);// ten wątek rysuje wszystko, co jest widoczne na planszy
            Rysowanie_Planszy.Start();
            
            //Ten Region służy tylko do nałożeniu obrazków stanu na siebie
            #region Status_robotów
            OK5.Parent = Rob5;
            OK5.Location =
                new Point(
                    OK5.Location.X
                    - Rob5.Location.X,
                    OK5.Location.Y
                    - Rob5.Location.Y);
            X6.Parent =Rob6;
            X6.Location =
                new Point(
                    X6.Location.X
                    - Rob6.Location.X,
                    X6.Location.Y
                    - Rob6.Location.Y);
            OK6.Parent = Rob6;
            OK6.Location =
                new Point(
                    OK6.Location.X
                    - Rob6.Location.X,
                    OK6.Location.Y
                    - Rob6.Location.Y);
            X7.Parent = Robo7;
            X7.Location =
                new Point(
                    X7.Location.X
                    - Robo7.Location.X,
                    X7.Location.Y
                    - Robo7.Location.Y);
            OK7.Parent = Robo7;
            OK7.Location =
                new Point(
                    OK7.Location.X
                    - Robo7.Location.X,
                    OK7.Location.Y
                    - Robo7.Location.Y);
            X8.Parent = Robo7;
            X8.Location =
                new Point(
                    X8.Location.X
                    - Robo8.Location.X,
                    X8.Location.Y
                    - Robo8.Location.Y);
            OK8.Parent = Robo8;
            OK8.Location =
                new Point(
                    OK8.Location.X
                    - Robo8.Location.X,
                    OK8.Location.Y
                    - Robo8.Location.Y);
            X5.Parent = Rob5;
            X5.Location =
                new Point(
                    X5.Location.X
                    - Rob5.Location.X,
                    X5.Location.Y
                    - Rob5.Location.Y);
        
            X1.Parent = Robo1;
            X1.Location =
                new Point(
                    X1.Location.X
                    - Robo1.Location.X,
                    X1.Location.Y
                    - Robo1.Location.Y);
            OK1.Parent = Robo1;
            OK1.Location =
                new Point(
                    OK1.Location.X
                    - Robo1.Location.X,
                    OK1.Location.Y
                    - Robo1.Location.Y);
            X2.Parent = Robo2;
            X2.Location =
                new Point(
                    X2.Location.X
                    - Robo2.Location.X,
                    X2.Location.Y
                    - Robo2.Location.Y);
            OK2.Parent = Robo2;
            OK2.Location =
                new Point(
                    OK2.Location.X
                    - Robo2.Location.X,
                    OK2.Location.Y
                    - Robo2.Location.Y);

            X3.Parent = Robo3;
            X3.Location =
                new Point(
                    X3.Location.X
                    - Robo3.Location.X,
                    X3.Location.Y
                    - Robo3.Location.Y);
            OK3.Parent = Robo3;
            OK3.Location =
                new Point(
                    OK3.Location.X
                    - Robo3.Location.X,
                    OK3.Location.Y
                    - Robo3.Location.Y);
            X4.Parent = Robo4;
            X4.Location =
                new Point(
                    X4.Location.X
                    - Robo4.Location.X,
                    X4.Location.Y
                    - Robo4.Location.Y);
            OK4.Parent = Robo4;
            OK4.Location =
                new Point(
                    OK4.Location.X
                    - Robo4.Location.X,
                    OK4.Location.Y
                    - Robo4.Location.Y);
            X5.Visible = false;
            X3.Visible = false;
            X2.Visible = false;
            X1.Visible = false;
            X4.Visible = false;
            OK2.Visible = false;
            OK3.Visible = false;
            OK1.Visible = false;
            OK5.Visible = false;
            OK4.Visible = false;
            OK6.Visible = false;
            OK7.Visible = false;
            OK8.Visible = false;
            X6.Visible = false;
            X7.Visible = false;
            X8.Visible = false;
            Nie1.Visible = false;
            Nie2.Visible = false;
            Nie3.Visible = false;
            Nie4.Visible = false;
            Nie5.Visible = false;
            Nie6.Visible = false;
            Nie7.Visible = false;
            Nie8.Visible = false;

            #endregion 
        }

        //Ta funkcja koloruje kwadrat na kolor przywłaszczonego przez nas robota
        private void Kolor()
        { while (true)
            { if (Wybor_robota != 0)
                {
                    Kol_Stan.BackColor = Kolory2[Wybor_robota - 1];// Bierze z listy kolor i koloruje kwadrat
                }
                else Kol_Stan.BackColor = Color.White; //Jeśli nie mamy żadnego robota to kwadrat będzie biały
                Thread.Sleep(500);
            }

        }
        //Ten wątek cyklicznie pobiera informacje o robotach i porządkuje je, podpisuje też roboty na panelu
        private void Lista_rob()
        {
          
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            var cFactoryControl = new ChannelFactory<PRMServices.IRobotControl>(binding, controlEndpoint);
            var cFactoryCheck = new ChannelFactory<PRMServices.IPlatformStatus>(binding, checkObject);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            PRMServices.IRobotControl controlService = cFactoryControl.CreateChannel();
            PRMServices.IPlatformStatus platformStatus = cFactoryCheck.CreateChannel();
            while (true)
            {
                var roboty = platformStatus.GetTableRobots();
                foreach (var robo in roboty)
                {
                    //Zebranie informacji o robotach

                    if (robo.ID != null)
                    Nr_ID[Nr_wektora_Info_rob] = (int)robo.ID; //Pobieramy i zapisujemy nr ID robota
                    Tekst_stan[Nr_wektora_Info_rob] = Convert.ToString(robo.LeaseState);//Pobieramy i zapisujemy stan przywłaszczenia robota
                    Współrzędne_Robota_Przetransferowane[Nr_wektora_Info_rob, 0] = Convert.ToString(760-(robo.X* 0.4f));//Pobieramy, zapisujemy, skalujemy i transformujemy współrzędną X robota
                    Współrzędne_Robota_Przetransferowane[Nr_wektora_Info_rob, 1] = Convert.ToString(760-(robo.Y * 0.4f));//Pobieramy, zapisujemy, skalujemy i transformujemy współrzędną Y robota
                    Współrzędne_Rzeczywiste_Robota[Nr_wektora_Info_rob, 0] = Convert.ToString((robo.X));//Pobieramy i zapisujemy rzeczywistą współrzędną X robota
                    Współrzędne_Rzeczywiste_Robota[Nr_wektora_Info_rob, 1] = Convert.ToString((robo.Y));//Pobieramy i zapisujemy rzeczywistą współrzędną Y robota
                    Theta[Nr_wektora_Info_rob] = Convert.ToInt16(robo.Theta);//Pobieramy i zapisujemy rzeczywistą orientacje robota
                    Nr_wektora_Info_rob++;

                }
                Nr_wektora_Info_rob = 0;
                
                //Poukładnie ich w kolejności ID

                foreach (var ID in Nr_ID)
                {
                    int Lista_Stanu = ID;
                    if (Lista_Stanu == 0)
                        continue; //Jeżeli wybrana komórka nie ma przypisanej wartości ID to funkcja przeskakuje dalej
                    if (Nr_ID[0] == 0)
                        goto Koniec_listy;
                    if (Lista_Stanu > Nr_ID[0])
                        Kolejność = Kolejność + 1;
                    if (Nr_ID[1] == 0)
                        goto Koniec_listy;//Jeśli ta komórka wektora nie ma przypisanego ID to kończy się porządkowanie dla tej wartości
                    if (Lista_Stanu > Nr_ID[1])
                        Kolejność = Kolejność + 1;
                    if (Nr_ID[2] == 0)
                        goto Koniec_listy;//Jeśli ta komórka wektora nie ma przypisanego ID to kończy się porządkowanie dla tej wartości
                    if (Lista_Stanu > Nr_ID[2])
                        Kolejność = Kolejność + 1;
                    if (Nr_ID[3] == 0)
                        goto Koniec_listy;//Jeśli ta komórka wektora nie ma przypisanego ID to kończy się porządkowanie dla tej wartości
                    if (Lista_Stanu > Nr_ID[3])
                        Kolejność = Kolejność + 1;
                    if (Nr_ID[4] == 0)
                        goto Koniec_listy;//Jeśli ta komórka wektora nie ma przypisanego ID to kończy się porządkowanie dla tej wartości
                    if (Lista_Stanu > Nr_ID[4])
                        Kolejność = Kolejność + 1;
                    if (Nr_ID[5] == 0)
                        goto Koniec_listy;//Jeśli ta komórka wektora nie ma przypisanego ID to kończy się porządkowanie dla tej wartości
                    if (Lista_Stanu > Nr_ID[5])
                        Kolejność = Kolejność + 1;
                    if (Nr_ID[6] == 0)
                        goto Koniec_listy;//Jeśli ta komórka wektora nie ma przypisanego ID to kończy się porządkowanie dla tej wartości
                    if (Lista_Stanu > Nr_ID[6])
                        Kolejność = Kolejność + 1;
                    if (Nr_ID[7] == 0)
                        goto Koniec_listy;//Jeśli ta komórka wektora nie ma przypisanego ID to kończy się porządkowanie dla tej wartości
                    if (Lista_Stanu > Nr_ID[7])
                        Kolejność = Kolejność + 1;
                    Koniec_listy: //Funkcja idzie tutaj, jeśli ilość robotów jest mniejsza niż ich maksymalna ilość

                    Nr_ID_Uporządkowane[0 + Kolejność] = Lista_Stanu;
                    Tekst_stan_Uporządkowane[0 + Kolejność] = Tekst_stan[Nr_wektora_Porządkowanie];
                    Współrzędne_Robota_Przetransferowane_Uporządkowane[0 + Kolejność, 0] = Współrzędne_Robota_Przetransferowane[Nr_wektora_Porządkowanie, 0];
                    Współrzędne_Robota_Przetransferowane_Uporządkowane[0 + Kolejność, 1] = Współrzędne_Robota_Przetransferowane[Nr_wektora_Porządkowanie, 1];
                    Współrzędne_Rzeczywiste_Robota_Uporządkowane[0 + Kolejność, 0] = Współrzędne_Rzeczywiste_Robota[Nr_wektora_Porządkowanie, 0];
                    Współrzędne_Rzeczywiste_Robota_Uporządkowane[0 + Kolejność, 1] = Współrzędne_Rzeczywiste_Robota[Nr_wektora_Porządkowanie, 1];
                    Theta_Uporządkowane[0 + Kolejność] = Theta[Nr_wektora_Porządkowanie];
                    Kolejność = 0;
                    Nr_wektora_Porządkowanie++;

                }
                Nr_wektora_Porządkowanie = 0;

                //Podpisanie robotów
                    
 #region        Podpisy_robotów
                if (Nr_ID_Uporządkowane[0] != 0)
                {
                    label4.Invoke((MethodInvoker)(() => label1.Text = "Robot nr " + Nr_ID_Uporządkowane[0]));
                    Lista_Stanu[0] = 0;
                }
                else
                {
                    label1.Invoke((MethodInvoker)(() => label1.Text = "Brak Robota"));

                }
                if (Nr_ID_Uporządkowane[1] != 0)
                {
                    label2.Invoke((MethodInvoker)(() => label2.Text = "Robot nr " + Nr_ID_Uporządkowane[1]));
                    Lista_Stanu[1] = 0;
                }
                else
                {
                    label2.Invoke((MethodInvoker)(() => label2.Text = "Brak Robota"));

                }
                if (Nr_ID_Uporządkowane[2] != 0)
                {
                    label3.Invoke((MethodInvoker)(() => label3.Text = "Robot nr " + Nr_ID_Uporządkowane[2]));
                    Lista_Stanu[2] = 0;

                }
                else
                {
                    label3.Invoke((MethodInvoker)(() => label3.Text = "Brak Robota"));

                }
                if (Nr_ID_Uporządkowane[3] != 0)
                {
                    label4.Invoke((MethodInvoker)(() => label4.Text = "Robot nr " + Nr_ID_Uporządkowane[3]));
                    Lista_Stanu[3] = 0;
                }
                else
                {
                    label4.Invoke((MethodInvoker)(() => label4.Text = "Brak Robota"));

                }
                if (Nr_ID_Uporządkowane[4] != 0)
                {
                    label5.Invoke((MethodInvoker)(() => label5.Text = "Robot nr " + Nr_ID_Uporządkowane[4]));
                    Lista_Stanu[4] = 0;
                }
                else
                {
                    label5.Invoke((MethodInvoker)(() => label5.Text = "Brak Robota"));

                }
                if (Nr_ID_Uporządkowane[5] != 0)
                {
                    label6.Invoke((MethodInvoker)(() => label6.Text = "Robot nr " + Nr_ID_Uporządkowane[5]));
                    Lista_Stanu[5] = 0;
                }
                else
                {
                    label6.Invoke((MethodInvoker)(() => label6.Text = "Brak Robota"));

                }
                if (Nr_ID_Uporządkowane[6] != 0)
                {
                    label7.Invoke((MethodInvoker)(() => label7.Text = "Robot nr " + Nr_ID_Uporządkowane[6]));
                    Lista_Stanu[6] = 0;
                }
                else
                {
                    label7.Invoke((MethodInvoker)(() => label7.Text = "Brak Robota"));

                }
                if (Nr_ID_Uporządkowane[7] != 0)
                {
                    label8.Invoke((MethodInvoker)(() => label8.Text = "Robot nr " + Nr_ID_Uporządkowane[7]));
                    Lista_Stanu[7] = 0;
                }
                else
                {
                    label8.Invoke((MethodInvoker)(() => label8.Text = "Brak Robota"));

                }
#endregion
                Thread.Sleep(75);
            }
        }
        
        //Funkcja, która zapisuje punkty przeszkód do listy
        private void RYS()
        {
            tablicapunktow.Clear();
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            var cFactoryControl = new ChannelFactory<PRMServices.IRobotControl>(binding, controlEndpoint);
            var cFactoryCheck = new ChannelFactory<PRMServices.IPlatformStatus>(binding, checkObject);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            PRMServices.IRobotControl controlService = cFactoryControl.CreateChannel();
            PRMServices.IPlatformStatus platformStatus = cFactoryCheck.CreateChannel();

            //Pobieranie przeszkód
            var przeszkody = platformStatus.GetObstacles();

            //Spisywanie punktów
            foreach (var przeszkoda in przeszkody)
            {
                PointF[] punkty = new PointF[przeszkoda.Points.Count];
               
                for (int Współrzędne_Robota_Przetransferowane = 0; Współrzędne_Robota_Przetransferowane < przeszkoda.Points.Count; Współrzędne_Robota_Przetransferowane++)
                {
                    Punkt_X_Przeszkody = Convert.ToSingle(przeszkoda.Points[Współrzędne_Robota_Przetransferowane].X);
                    Punkt_Y_Przeszkody = Convert.ToSingle(przeszkoda.Points[Współrzędne_Robota_Przetransferowane].Y);
                    Punkt_X_Przeszkody = Punkt_X_Przeszkody * 0.4f;
                    Punkt_Y_Przeszkody = Punkt_Y_Przeszkody * 0.4f;
                    Punkt_X_Przeszkody = 760 - Punkt_X_Przeszkody;
                    Punkt_Y_Przeszkody = 760 - Punkt_Y_Przeszkody;
                    punkty[Współrzędne_Robota_Przetransferowane] = (new PointF(Punkt_X_Przeszkody, Punkt_Y_Przeszkody));
                  
                }
              
                tablicapunktow.Add(punkty);
 
            }

        }     
       //Wątek, który liczy różnicę między 'Kierunek_prawo' a 'Kierunek_lewo'
        private void Różnica()
        {while (true)
            {
                if (Sim.Checked)
                {
                   Różnica_prawo_lewo = Kierunek_prawo - Kierunek_lewo;
                    
                    
                }
            }
        }  
        #region Guziki_kierunkowe
        private void Prosto_MouseDown(object sender, MouseEventArgs e)
        {
            if (W_górę.IsBusy != true)
            {

                W_górę.RunWorkerAsync();

            }
            if (Arcade.Checked)
            {// Przerywamy BGworkery od zwalniania
                if (Zwalnianie_przy_jeździe_w_przód.IsBusy)
                    Zwalnianie_przy_jeździe_w_przód.CancelAsync();
                if (Zwalnianie_przy_jeździe_w_tył.IsBusy)
                    Zwalnianie_przy_jeździe_w_tył.CancelAsync();
                W_przód = 1; //Zaznaczamy że guzik jest wciśnięty
            }
         
        }

        private void Prosto_MouseUp(object sender, MouseEventArgs e)
        {
                if (W_górę.WorkerSupportsCancellation == true)
                {

                    W_górę.CancelAsync();
                }
          
        if(Arcade.Checked)
            {// Odpalamy BGworker od zwalniania
                if (Zwalnianie_przy_jeździe_w_przód.IsBusy != true)
                {

                    Zwalnianie_przy_jeździe_w_przód.RunWorkerAsync();
                }
                W_przód = 0;//Zaznaczamy że guzik jest odciśnięty
            }
        }

        private void Lewo_MouseDown(object sender, MouseEventArgs e)
        {
            if (Wlewo.IsBusy != true && Sim.Checked)
            {

                Wlewo.RunWorkerAsync();
            }
            if (Arcade.Checked)
            {//Przerywamy bgworkery od zwalniania
                if (Zwalnianie_przy_jeździe_w_przód.IsBusy)
                    Zwalnianie_przy_jeździe_w_przód.CancelAsync();
                if (Zwalnianie_przy_jeździe_w_tył.IsBusy)
                    Zwalnianie_przy_jeździe_w_tył.CancelAsync();
                W_lewo = 1; //Zaznaczamy że guzik jest wciśnięty

            }

        }
        private void Lewo_MouseUp(object sender, MouseEventArgs e)
        {


            if (Wlewo.WorkerSupportsCancellation == true && Sim.Checked)
            {

                Wlewo.CancelAsync();
            }
            if (Arcade.Checked)
            {//Odnawiamy bgworkery od zwalniania
                if (Zwalnianie_przy_jeździe_w_przód.IsBusy != true)
                    Zwalnianie_przy_jeździe_w_przód.RunWorkerAsync();
                if (Zwalnianie_przy_jeździe_w_tył.IsBusy != true)
                    Zwalnianie_przy_jeździe_w_tył.RunWorkerAsync();
                W_lewo = 0;// Zaznaczamy że guzik został odciśnięty
            }
        }
        private void Prawo_MouseDown(object sender, MouseEventArgs e)
        {

            if (Wprawo.IsBusy != true && Sim.Checked)
            {

                Wprawo.RunWorkerAsync();
            }
            if (Arcade.Checked)
            {//Przerywamy bgworkery od zwalniania
                if (Zwalnianie_przy_jeździe_w_przód.IsBusy)
                    Zwalnianie_przy_jeździe_w_przód.CancelAsync();
                if (Zwalnianie_przy_jeździe_w_tył.IsBusy)
                    Zwalnianie_przy_jeździe_w_tył.CancelAsync();
                W_prawo = 1;//Zaznaczamy że guzik jest wciśnięty

            }

        }
        private void Prawo_MouseUp(object sender, MouseEventArgs e)
        {

            if (Wprawo.WorkerSupportsCancellation == true && Sim.Checked)
            {

                Wprawo.CancelAsync();
            }
            if (Arcade.Checked)
            {
                //Odnawiamy bgworkery od zwalniania
                if (Zwalnianie_przy_jeździe_w_przód.IsBusy != true)
                    Zwalnianie_przy_jeździe_w_przód.RunWorkerAsync();
                if (Zwalnianie_przy_jeździe_w_tył.IsBusy != true)
                    Zwalnianie_przy_jeździe_w_tył.RunWorkerAsync();
                W_prawo = 0;// Zaznaczamy że guzik został odciśnięty
            }

        }
        private void Tył_MouseDown(object sender, MouseEventArgs e)
        {if (Sim.Checked)
            {
                if (Wdół.IsBusy != true)
                {

                    Wdół.RunWorkerAsync();
                }
                if (Arcade.Checked)
                {
                    //Przerywamy bgworkery od zwalniania
                    if (Zwalnianie_przy_jeździe_w_przód.IsBusy)
                        Zwalnianie_przy_jeździe_w_przód.CancelAsync();
                    if (Zwalnianie_przy_jeździe_w_tył.IsBusy)
                        Zwalnianie_przy_jeździe_w_tył.CancelAsync();
                    W_tył = 1;//Zaznaczamy że guzik jest wciśnięty
                }
            }
        }
        private void Tył_MouseUp(object sender, MouseEventArgs e)
        {

            if (Wdół.WorkerSupportsCancellation == true)
            {

                Wdół.CancelAsync();
            }
            if (Arcade.Checked)
            {//Odnawiamy bgworkery od zwalniania
                if (Zwalnianie_przy_jeździe_w_przód.IsBusy != true)
                    Zwalnianie_przy_jeździe_w_przód.RunWorkerAsync();
                if (Zwalnianie_przy_jeździe_w_tył.IsBusy != true)
                    Zwalnianie_przy_jeździe_w_tył.RunWorkerAsync();
                W_tył = 0;// Zaznaczamy że guzik został odciśnięty
            }
        }
        #endregion
        
        //Lista klawiszy klawiatury na których naciśnięcie będzie reagował program i wykonywał dane polecenia
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {if (Sterowanie_manualne.Checked)
            {
                if (e.KeyCode == Keys.A)
                {

                    if (Wlewo.WorkerSupportsCancellation == true && Sim.Checked)
                    {

                        Wlewo.CancelAsync();
                    }
                    if (Arcade.Checked)
                    {//Odnawiamy bgworkery od zwalniania
                        if (Zwalnianie_przy_jeździe_w_przód.IsBusy != true)
                            Zwalnianie_przy_jeździe_w_przód.RunWorkerAsync();
                        if (Zwalnianie_przy_jeździe_w_tył.IsBusy != true)
                            Zwalnianie_przy_jeździe_w_tył.RunWorkerAsync();
                        W_lewo = 0;// Zaznaczamy że guzik został odciśnięty

                    }

                }
                if (e.KeyCode == Keys.D)
                {

                    if (Wprawo.WorkerSupportsCancellation == true && Sim.Checked)
                    {

                        Wprawo.CancelAsync();
                    }
                    if (Arcade.Checked)
                    {//Odnawiamy bgworkery od zwalniania
                        if (Zwalnianie_przy_jeździe_w_przód.IsBusy != true)
                            Zwalnianie_przy_jeździe_w_przód.RunWorkerAsync();
                        if (Zwalnianie_przy_jeździe_w_tył.IsBusy != true)
                            Zwalnianie_przy_jeździe_w_tył.RunWorkerAsync();
                        W_prawo = 0;// Zaznaczamy że guzik został odciśnięty

                    }

                }
                if (e.KeyCode == Keys.S)
                {

                    if (Wdół.WorkerSupportsCancellation == true)
                    {

                        Wdół.CancelAsync();
                    }
                    if (Arcade.Checked)
                    {
                       
                                  //Odnawiamy bgworkera od zwalniania
                        if (Zwalnianie_przy_jeździe_w_tył.IsBusy != true)
                        {

                            Zwalnianie_przy_jeździe_w_tył.RunWorkerAsync();
                        }
                        W_tył = 0;// Zaznaczamy że guzik został odciśnięty
                    }

                }
                if (e.KeyCode == Keys.W)
                {

                    if (W_górę.WorkerSupportsCancellation == true)
                    {

                        W_górę.CancelAsync();
                    }
                    if (Arcade.Checked)
                    {//Odnawiamy bgworkera od zwalniania
                        
                        if (Zwalnianie_przy_jeździe_w_przód.IsBusy != true)
                        {
                            Zwalnianie_przy_jeździe_w_przód.RunWorkerAsync();
                        }
                        W_przód = 0; // Zaznaczamy że guzik został odciśnięty
                    }

                }
            }
        }
       
        //Lista klawiszy klawiatury na których puszczenie będzie reagował program i wykonywał dane polecenia
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {if (Sterowanie_manualne.Checked)
            {
                if (e.KeyCode == Keys.A)
                {
                    if (Sim.Checked)
                    {
                        if (Wlewo.IsBusy != true)
                        {

                            Wlewo.RunWorkerAsync();
                        }
                    }
                    if (Arcade.Checked)
                    {//Zatrzymujemy bgworkery od zwalniania robota
                        if (Zwalnianie_przy_jeździe_w_przód.IsBusy == true)
                            Zwalnianie_przy_jeździe_w_przód.CancelAsync();
                        if (Zwalnianie_przy_jeździe_w_tył.IsBusy == true)
                            Zwalnianie_przy_jeździe_w_tył.CancelAsync();
                        W_lewo = 1;//Zaznaczamy że ten guzik jest wciśnięty
                    }

                }
                if (e.KeyCode == Keys.D)
                {
                    if (Sim.Checked)
                    {
                        if (Wprawo.IsBusy != true)
                        {

                            Wprawo.RunWorkerAsync();
                        }
                    }
                    if (Arcade.Checked)
                    {//Zatrzymujemy bgworkery od zwalniania robota
                        if (Zwalnianie_przy_jeździe_w_przód.IsBusy == true)
                            Zwalnianie_przy_jeździe_w_przód.CancelAsync();
                        if (Zwalnianie_przy_jeździe_w_tył.IsBusy == true)
                            Zwalnianie_przy_jeździe_w_tył.CancelAsync();
                        W_prawo = 1;//Zaznaczamy że ten guzik jest wciśnięty
                    }

                }
                if (e.KeyCode == Keys.S)
                {
                    if (Wdół.IsBusy != true)
                    {

                        Wdół.RunWorkerAsync();
                    }
                    W_tył = 1;//Zaznaczamy że ten guzik jest wciśnięty
                              //Zatrzymujemy bgworkera od zwalniania robota
                    Zwalnianie_przy_jeździe_w_przód.CancelAsync();
                }
                if (e.KeyCode == Keys.W)
                {
                    if (W_górę.IsBusy != true)
                    {

                        W_górę.RunWorkerAsync();
                    }                  
                    //Zatrzymujemy bgworkera od zwalniania robota
                    Zwalnianie_przy_jeździe_w_tył.CancelAsync();
                    W_przód = 1; //Zaznaczamy że ten guzik jest wciśnięty
                }
            }
            if(e.KeyCode==Keys.Space)
            {
                Zwrot = 0;
                if (Sim.Checked)
                {
                    Kierunek_prawo = 5;
                    Kierunek_lewo = 5;
                }
                if (Jazda_do_celu.IsBusy)
                {//Zatrzymujemy bgworkera odpowiedzialnego za jazdę automatyczną
                    Jazda_do_celu.CancelAsync();
                }
                var binding = new BasicHttpBinding();
                var cFactoryControl = new ChannelFactory<PRMServices.IRobotControl>(binding, controlEndpoint);
                PRMServices.IRobotControl controlService = cFactoryControl.CreateChannel();
                var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
                PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
                controlService.Stop();
                // Zatrzymujemy BKWorkery
                if(W_górę.IsBusy)
                W_górę.CancelAsync();
                if(Wdół.IsBusy)
                Wdół.CancelAsync();
                if(Zwalnianie_przy_jeździe_w_przód.IsBusy)
                Zwalnianie_przy_jeździe_w_przód.CancelAsync();
                if (Zwalnianie_przy_jeździe_w_tył.IsBusy)
                Zwalnianie_przy_jeździe_w_tył.CancelAsync();
                
            }


        }

        private void Auto_CheckedChanged(object sender, EventArgs e)
        {
            if (Auto.Checked)
            {
                Auto.Checked = true;
               Sterowanie_manualne.Checked = false;
                Sterowanie.Visible = false;
                var binding = new BasicHttpBinding();
                var cFactoryControl = new ChannelFactory<PRMServices.IRobotControl>(binding, controlEndpoint);
                PRMServices.IRobotControl controlService = cFactoryControl.CreateChannel();
                var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
                PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
                //Zatrzymuje robota i zatrzymuje wszystkie BGWorkery odpowiedzialne za jazdę
                controlService.Stop();
                Kierunek_lewo = 5;
                Kierunek_prawo = 5;
                Zwrot = 0;
                
            }
            else Sterowanie_manualne.Checked = true;
           
        }

        private void Sterowanie_manualne_CheckedChanged(object sender, EventArgs e)
        {
            if (Sterowanie_manualne.Checked)
            {
                Sterowanie_manualne.Checked = true;
                Auto.Checked = false;
                //Przerywamy BGWorkera odpowiadającego za automatyczną jazdę
                Jazda_do_celu.CancelAsync();
                Sterowanie.Visible = true;
            }
            else Auto.Checked = true;
           
        }
        private void Sim_CheckedChanged(object sender, EventArgs e)
        {
           
            if (Sim.Checked)
            {
                Sim.Checked = true;
                Arcade.Checked = false;
                //Przerywamy BGWorkery od Arcade'da
                if (W_górę.IsBusy)
                    W_górę.CancelAsync();
                if (Wdół.IsBusy)
                    Wdół.CancelAsync();
                if (Zwalnianie_przy_jeździe_w_przód.IsBusy)
                    Zwalnianie_przy_jeździe_w_przód.CancelAsync();
                if (Zwalnianie_przy_jeździe_w_tył.IsBusy)
                    Zwalnianie_przy_jeździe_w_tył.CancelAsync();
                //Resetujemy zmienną
                Zwrot = 0;

            }
            else
            {
                Arcade.Checked = true;
            }

        }
        private void Arcade_CheckedChanged(object sender, EventArgs e)
        {
            if (Arcade.Checked)
            {
                Arcade.Checked = true;
               Sim.Checked = false;
                //Resetujemy wartości
                Zwrot = 0;
                Kierunek_lewo = 5;
                Kierunek_prawo = 5;
            }
            else Sim.Checked = true;
        }
        //BGworker, który ikrementuje zmienną 'Zwrot' co 500 ms od momentu wciśnięcia klawisza, stoi dłużej kiedy wartość osiągnie 0
        private void wgórę_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)

            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                   return;
                }
                if (Zwrot == 0)
                {
                    Thread.Sleep(1000);
                }
                if (Zwrot < 10)
                {
                    Zwrot = Zwrot + 1;
                  
                    Thread.Sleep(500);
                }
               



                }
        }
           
        //Tajmer
        private void timer1_Tick(object sender, EventArgs e)
        {
                Tick_timer = Tick_timer + 0.1;
                Tajmer.Text = String.Format("{0:0.0}", Tick_timer);
        }
        
        //Guzik, który oddaje przywłaszczonego robota
        private void Oddaj_Click(object sender, EventArgs e)
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            if (Wybor_robota != 0)
            {
                actionService.ReleaseRobot();
                MessageBox.Show("Robot oddany");
                Wybor_robota = 0;
                Thread Manul = new Thread(Manualnie);
                if (Manul.IsAlive)
                {
                    Manul.Abort();
                }



            }
        }

        //Guzik ponawiający procedure szukania robotów, która odbywa się przy odpaleniu aplikacji
        private void Szukaj_Click(object sender, EventArgs e)
        {
            Form2 czekaj = new Form2();
            czekaj.ShowDialog();
        }

        //Wątek zmieniający wygląd robota w zależności od jest stanu na 'panelu'
        private void Stany_Robotów_Wizu_DoWork(object sender, DoWorkEventArgs e)
        {//Możliwe wartości w "Lista_stanu
         // 0 - robot wolny
         // 1 - robot zajęty
         // 2 - brak robota

            while (true)
            {// Ikonki symbolizujące zajętego robota
                if (Lista_Stanu[3] == 1 && Wybor_robota != 4)
                    X4.Invoke((MethodInvoker)(() => X4.Visible = true));
                else if (Lista_Stanu[3] == 0)
                    X4.Invoke((MethodInvoker)(() => X4.Visible = false));
                if (Lista_Stanu[4] == 1 && Wybor_robota != 5)
                    X5.Invoke((MethodInvoker)(() => X5.Visible = true));
                else if (Lista_Stanu[4] == 0)
                    X5.Invoke((MethodInvoker)(() => X5.Visible = false));
                if (Lista_Stanu[2] == 1 && Wybor_robota != 3)
                    X3.Invoke((MethodInvoker)(() => X3.Visible = true));
                else if (Lista_Stanu[2] == 0)
                    X3.Invoke((MethodInvoker)(() => X3.Visible = false));
                if (Lista_Stanu[1] == 1 && Wybor_robota != 2)
                    X2.Invoke((MethodInvoker)(() => X2.Visible = true));
                else if (Lista_Stanu[1] == 0)
                    X2.Invoke((MethodInvoker)(() => X2.Visible = false));
                if (Lista_Stanu[0] == 1 && Wybor_robota != 1)
                    X1.Invoke((MethodInvoker)(() => X1.Visible = true));
                else if (Lista_Stanu[0] == 0)
                    X1.Invoke((MethodInvoker)(() => X1.Visible = false));
                if (Lista_Stanu[5] == 1 && Wybor_robota != 6)
                    X6.Invoke((MethodInvoker)(() => X6.Visible = true));
                else if (Lista_Stanu[5] == 0)
                    X6.Invoke((MethodInvoker)(() => X6.Visible = false));
                if (Lista_Stanu[6] == 1 && Wybor_robota != 7)
                    X7.Invoke((MethodInvoker)(() => X7.Visible = true));
                else if (Lista_Stanu[6] == 0)
                    X7.Invoke((MethodInvoker)(() => X7.Visible = false));
                if (Lista_Stanu[7] == 1 && Wybor_robota != 8)
                    X8.Invoke((MethodInvoker)(() => X8.Visible = true));
                else if (Lista_Stanu[7] == 0)
                    X8.Invoke((MethodInvoker)(() => X8.Visible = false));
                // Ikonki symbolizujące brak robota
                if (Lista_Stanu[3] == 2)
                    Nie4.Invoke((MethodInvoker)(() => Nie4.Visible = true));
                else
                    Nie4.Invoke((MethodInvoker)(() => Nie4.Visible = false));
                if (Lista_Stanu[4] == 2)
                    Nie5.Invoke((MethodInvoker)(() => Nie5.Visible = true));
                else
                    Nie5.Invoke((MethodInvoker)(() => Nie5.Visible = false));
                if (Lista_Stanu[2] == 2)
                    Nie3.Invoke((MethodInvoker)(() => Nie3.Visible = true));
                else
                    Nie3.Invoke((MethodInvoker)(() => Nie3.Visible = false));
                if (Lista_Stanu[1] == 2)
                    Nie2.Invoke((MethodInvoker)(() => Nie2.Visible = true));
                else
                    Nie2.Invoke((MethodInvoker)(() => Nie2.Visible = false));
                if (Lista_Stanu[0] == 2)
                    Nie1.Invoke((MethodInvoker)(() => Nie1.Visible = true));
                else
                    Nie1.Invoke((MethodInvoker)(() => Nie1.Visible = false));
                if (Lista_Stanu[5] == 2)
                    Nie6.Invoke((MethodInvoker)(() => Nie6.Visible = true));
                else
                    Nie6.Invoke((MethodInvoker)(() => Nie6.Visible = false));
                if (Lista_Stanu[6] == 2)
                    Nie7.Invoke((MethodInvoker)(() => Nie7.Visible = true));
                else
                    Nie7.Invoke((MethodInvoker)(() => Nie7.Visible = false));
                if (Lista_Stanu[7] == 2)
                    Nie8.Invoke((MethodInvoker)(() => Nie8.Visible = true));
                else
                    Nie8.Invoke((MethodInvoker)(() => Nie8.Visible = false));
                Thread.Sleep(2000);
            }
            }

        //BGWorker pokazujący aktualny stan każdego robota na panelu
        private void Stany_robotów_String_DoWork(object sender, DoWorkEventArgs e)
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            var cFactoryControl = new ChannelFactory<PRMServices.IRobotControl>(binding, controlEndpoint);
            var cFactoryCheck = new ChannelFactory<PRMServices.IPlatformStatus>(binding, checkObject);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            PRMServices.IRobotControl controlService = cFactoryControl.CreateChannel();
            PRMServices.IPlatformStatus platformStatus = cFactoryCheck.CreateChannel();

            while (true)
            {



                foreach (var S in Tekst_stan_Uporządkowane)
                {
                    if (S == "LEASED")
                    {
                        Lista_Stanu[Nr_wektora_Stan_rob] = 1; //Robot jest zajęty i pojawia się X na robocie
                    }
                    else if (S == "FREE")
                    {
                        Lista_Stanu[Nr_wektora_Stan_rob] = 0; //Robot jest wolny i nie będzie niczym zasłoniętym
                    }
                    else
                    {
                        Lista_Stanu[Nr_wektora_Stan_rob] = 2;//Robota nie ma i jego miejsce na panelu jest wybielane
                    }
                    Nr_wektora_Stan_rob++;
                }
                Nr_wektora_Stan_rob = 0;

                Thread.Sleep(2000);
            }
        }

        //BGWorker, który jeśli żaden przycisk nie jest naciśnięty inkrementuje zmienną 'Zwrot' do momentu aż nie będzie ona 0
        private void Zwalnianie_przy_jeździe_w_tył_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(2000);
            while (Zwrot < 0)
            {
                if (W_lewo == 1 || W_prawo == 1 || W_przód == 1 || W_tył == 1)
                    continue;

                Thread.Sleep(1000);
                Zwrot++;

            }
        }
        
        //BGWorker, który jeśli żaden przycisk nie jest naciśnięty dekrementuje zmienną 'Zwrot' do momentu aż nie będzie ona 0
        private void Zwalnianie_przy_jeździe_w_przód_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(2000);
            while (Zwrot > 0)
            {

                if (W_lewo == 1 || W_prawo == 1 || W_przód == 1 || W_tył == 1)
                    continue;


                Thread.Sleep(1000);
                Zwrot--;

            }
        }

        //BGWorkier odpowiedzialny za generowanie ścieżki i jej realizację
        private void Jazda_do_celu_DoWork(object sender, DoWorkEventArgs e)
        {
            //Sprawdzenie czy poprzedni plik istnieje i kasuje go jeśli jest
            if (File.Exists(@"C:\Users\Worior\Desktop\apki\JK.bmp"))
            {
                File.Delete(@"C:\Users\Worior\Desktop\apki\JK.bmp");
            }
            matlab.Execute("clear");

            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            var cFactoryControl = new ChannelFactory<PRMServices.IRobotControl>(binding, controlEndpoint);
            var cFactoryCheck = new ChannelFactory<PRMServices.IPlatformStatus>(binding, checkObject);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            PRMServices.IRobotControl controlService = cFactoryControl.CreateChannel();
            PRMServices.IPlatformStatus platformStatus = cFactoryCheck.CreateChannel();

            //Ponownie pobieramy przeszkody oraz pozycję robotów oraz je rysujemy na wykresie w matlabie
            var przeszkody = platformStatus.GetObstacles();


            foreach (var przeszkoda in przeszkody)
            {

                float[] X1 = new float[przeszkoda.Points.Count + 1];
                float[] Y1 = new float[przeszkoda.Points.Count + 1];
                int o = 0;

                for (int Współrzędne_Robota_Przetransferowane = 0; Współrzędne_Robota_Przetransferowane < przeszkoda.Points.Count; Współrzędne_Robota_Przetransferowane++)
                {
                    Punkt_X_Przeszkody = Convert.ToSingle(przeszkoda.Points[Współrzędne_Robota_Przetransferowane].X);
                    Punkt_Y_Przeszkody = Convert.ToSingle(przeszkoda.Points[Współrzędne_Robota_Przetransferowane].Y);

                    X1[Współrzędne_Robota_Przetransferowane] = Punkt_X_Przeszkody;
                    Y1[Współrzędne_Robota_Przetransferowane] = Punkt_Y_Przeszkody;
                    //Dodajemy pierwszy punkt na koniec by zamknąć figurę
                    if (o == 0)
                    {
                        X1[X1.Length - 1] = Punkt_X_Przeszkody;
                        Y1[Y1.Length - 1] = Punkt_Y_Przeszkody;
                        o = 1;
                    }
                }

                object result3 = null;
                //Rysujemy przeszkody
                matlab.Feval("fill", 1, out result3, X1, Y1, "-k");
                matlab.Execute("hold on");
            }
            var roboty = platformStatus.GetTableRobots();
            foreach (var robo in roboty)
            {
                if (robo.ID != Nr_ID_Uporządkowane[Wybor_robota - 1])
                {
                    float[,] I = new float[1, 2];
                    I[0, 0] = Convert.ToSingle(robo.X);
                    I[0, 1] = Convert.ToSingle(robo.Y);
                    object result1 = null;
                    matlab.Feval("viscircles", 1, out result1, I, 19, "Color", "k");
                }
            }
            //Do matlaba wysyłamy zmienne X i Y, które będą punktem startowym jazdy robota, czyli będą to jego aktualne współrzędne przetranfomowane do układu obrazu po jego przetworzeniu
            matlab.Execute($"X = {Convert.ToSingle(Współrzędne_Rzeczywiste_Robota_Uporządkowane[Wybor_robota - 1, 0]).ToString().Replace(',', '.')}");
            matlab.Execute($"Y = {Convert.ToSingle(1900 - Convert.ToSingle(Współrzędne_Rzeczywiste_Robota_Uporządkowane[Wybor_robota - 1, 1])).ToString().Replace(',', '.')}");

            //Do matlaba wysyłamy zmienne Xk i Yk, które są punktem docelowym ścieżki robota, jest to punkt, który otrzymaliśmy przy wciśnięciu myszką w punkt na planszy, ten również jest tranfomowany na układ obrazu
            matlab.Execute($"Xk = {(Współ_X_Myszka_Przeskalowane_Układ_Stołu).ToString().Replace(',', '.')}");
            matlab.Execute($"Yk = {(1900 - Współ_Y_Myszka_Przeskalowane_Układ_Stołu).ToString().Replace(',', '.')}");
            //Odpalamy skrypt, który generuje trajektorię, przeskalowuje punkty ścieżki na układ PP i tworzy nam controller pure pursuit
            matlab.Execute("Generacja");
            //Ściągamy punkty ścieżki do jej narywania na planszy(z aktywnym czyszczeniem obrazu ścieżka nie zostanie na obrazie)
            double[,] Ścieżka= matlab.GetVariable("droga_wiz", "base");
            for (int t = 1; t < Ścieżka.Length / 2; t++)
            {
                float DROX1 = Convert.ToSingle(760 - (Ścieżka[t - 1, 0] * 0.4));
                float DROY1 = Convert.ToSingle((Ścieżka[t - 1, 1] * 0.4));
                float DROX2 = Convert.ToSingle(760 - (Ścieżka[t, 0] * 0.4));
                float DROY2 = Convert.ToSingle((Ścieżka[t, 1] * 0.4));
                f.DrawLine(Czarny, DROX1, DROY1, DROX2, DROY2);
            }
            //Pętla w której pobierane są prędkości, które powinien mieć robot w danej pozycji podczas pokonywania ścieżki
            //(Dopóki odległość robota od punktu jest większa od 10)
            while (Math.Sqrt(Math.Pow((Współ_X_Myszka_Przeskalowane_Układ_Stołu - Convert.ToSingle(Współrzędne_Rzeczywiste_Robota_Uporządkowane[Wybor_robota - 1, 0])), 2) + Math.Pow(((Współ_Y_Myszka_Przeskalowane_Układ_Stołu) - Convert.ToSingle(Współrzędne_Rzeczywiste_Robota_Uporządkowane[Wybor_robota - 1, 1])), 2)) >= 10)

            {
                //Tworzymy zmienne, które będziemy transformować na układ PP
                matlab.Execute($"TRU1 = {Convert.ToSingle(Współrzędne_Rzeczywiste_Robota_Uporządkowane[Wybor_robota - 1, 0]).ToString().Replace(',', '.')}");
                matlab.Execute($"TRU2 = {Convert.ToSingle(Współrzędne_Rzeczywiste_Robota_Uporządkowane[Wybor_robota - 1, 1]).ToString().Replace(',', '.')}");
                matlab.Execute($"TRU3 = {(Convert.ToSingle((Theta_Uporządkowane[Wybor_robota - 1]) * Math.PI / 180).ToString().Replace(',', '.'))}");//(w radianach)
                //Odpalamy skryp transformacyjny
                matlab.Execute("Lam");
                // Zdobyte zmienne wrzucamy do algorytmu PP, by ten zwrócił nam prędkości
                matlab.Execute("[v, omega] = controller([TRU1 TRU2 Omega]);");
                //Zdobycie zmiennych prędkościowych
                double Liniowa = matlab.GetVariable("v", "base");
                double OMEGA = matlab.GetVariable("omega", "base");
                Prędkość_liniowa_PP = Liniowa;
                Prędkość_kątowa_PP = OMEGA;
                Współ_X_Stół = Convert.ToSingle(Współrzędne_Rzeczywiste_Robota_Uporządkowane[Wybor_robota - 1, 0]);
                Współ_Y_Stół = Convert.ToSingle(Współrzędne_Rzeczywiste_Robota_Uporządkowane[Wybor_robota - 1, 1]);
                Orientacja_Stół = Convert.ToSingle((Theta_Uporządkowane[Wybor_robota - 1]));
                double M = matlab.GetVariable("TRU1", "base"); //Przetransfomowana współrzędna X
                double N = matlab.GetVariable("TRU2", "base");//Przetransfomowana współrzędna Y
                double O = matlab.GetVariable("Omega", "base");//Przetransfomowana orientacja
                Współ_X_PP = M;
                Współ_Y_PP = N;
                Orientacja_PP = O; //W radianach
                matlab.Execute("TROM=droga(2,1);");
                matlab.Execute("TROM1=droga(2,2);");
                double DX = matlab.GetVariable("TROM", "base");
                double DY = matlab.GetVariable("TROM1", "base");
                Współ_X_Punkt_Końcowy_PP = DX;
                Współ_Y_Punkt_Końońcowy_PP = DY;
                //Jazda robota
                if (Prędkość_kątowa_PP < 0)
                    controlService.Drive((int)(Prędkość_liniowa_PP + Math.Abs(Prędkość_kątowa_PP)), (int)Prędkość_liniowa_PP);
                else if (Prędkość_kątowa_PP > 0)
                    controlService.Drive((int)Prędkość_liniowa_PP, (int)(Prędkość_liniowa_PP + Math.Abs(Prędkość_kątowa_PP)));
                else
                    controlService.Drive((int)Prędkość_liniowa_PP, (int)Prędkość_liniowa_PP);



                Thread.Sleep(50);
            }

            controlService.Stop();
            //Usuwamy krzyżyk z rysunku
            Współ_X_Myszka_Układ_PictureBox = -10;
            Współ_Y_Myszka_Układ_PictureBox = -10;
        }
        
        //BGWorker podający żądane zmienne w czasie rzeczywistym
        private void TextBoxy_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                War_lewo.Invoke((MethodInvoker)(() => War_lewo.Text = Convert.ToString(Kierunek_lewo)));
                War_prawo.Invoke((MethodInvoker)(() => War_prawo.Text = Convert.ToString(Kierunek_prawo)));
                War_kierunek.Invoke((MethodInvoker)(() => War_kierunek.Text = Convert.ToString(Zwrot)));
                PR_LIN_PP.Invoke((MethodInvoker)(() => PR_LIN_PP.Text = Convert.ToString(Prędkość_liniowa_PP)));
                PR_KĄ_PP.Invoke((MethodInvoker)(() => PR_KĄ_PP.Text = Convert.ToString(Prędkość_kątowa_PP)));
                XT.Invoke((MethodInvoker)(() => XT.Text = Convert.ToString(Współ_X_Stół)));//TextBox pokazjący Współrzędną X robota w układzie stołu
                YT.Invoke((MethodInvoker)(() => YT.Text = Convert.ToString(Współ_Y_Stół)));//TextBox pokazjący Współrzędną Y robota w układzie stołu
                THT.Invoke((MethodInvoker)(() => THT.Text = Convert.ToString(Orientacja_Stół)));//TextBox pokazjący orientacje robota w układzie stołu
                XPP.Invoke((MethodInvoker)(() => XPP.Text = Convert.ToString(Współ_X_PP)));//TextBox pokazjący Współrzędną X robota w układzie PP
                YPP.Invoke((MethodInvoker)(() => YPP.Text = Convert.ToString(Współ_Y_PP)));//TextBox pokazjący Współrzędną Y robota w układzie PP
                THPP.Invoke((MethodInvoker)(() => THPP.Text = Convert.ToString(Orientacja_PP)));//TextBox pokazjący orientację robota w układzie PP
            }
        }
        //BGWorker pokazujący na panelu, którego robota wzięliśmy
        private void Przywłaszczenie_DoWork(object sender, DoWorkEventArgs e)
        {// Po wciśnięciu na wolnego robota na jego ikonce w panelu pojawia się żółty okrąg
            while (true)
            {
                while (true)
                {
                    switch (Wybor_robota)
                    {
                        case 0:


                            OK1.Invoke((MethodInvoker)(() => OK1.Visible = false));
                            OK5.Invoke((MethodInvoker)(() => OK5.Visible = false));
                            OK2.Invoke((MethodInvoker)(() => OK2.Visible = false));
                            OK3.Invoke((MethodInvoker)(() => OK3.Visible = false));
                            OK4.Invoke((MethodInvoker)(() => OK4.Visible = false));
                            OK6.Invoke((MethodInvoker)(() => OK6.Visible = false));
                            OK7.Invoke((MethodInvoker)(() => OK7.Visible = false));
                            OK8.Invoke((MethodInvoker)(() => OK8.Visible = false));

                            break;
                        case 1:

                            OK1.Invoke((MethodInvoker)(() => OK1.Visible = true));

                            break;
                        case 2:

                            OK2.Invoke((MethodInvoker)(() => OK2.Visible = true));
                            break;
                        case 3:


                            OK3.Invoke((MethodInvoker)(() => OK3.Visible = true));
                            break;
                        case 4:

                            OK4.Invoke((MethodInvoker)(() => OK4.Visible = true));
                            break;
                        case 5:

                            OK5.Invoke((MethodInvoker)(() => OK5.Visible = true));
                            break;
                        case 6:
                            OK6.Invoke((MethodInvoker)(() => OK6.Visible = true));

                            break;
                        case 7:
                            OK7.Invoke((MethodInvoker)(() => OK7.Visible = true));
                            break;
                        case 8:
                            OK8.Invoke((MethodInvoker)(() => OK8.Visible = true));
                            break;
                    }
                }

            }
        }

        //Guzik do odświeżania przeszkód
        private void Odświeżanie_Click(object sender, EventArgs e)
        {
            RYS();
        }

        //Guzik stopu
        private void Stop_Click(object sender, EventArgs e)
        {
            Zwrot = 0;
            if (Sim.Checked)
            {
                Kierunek_prawo = 5;
                Kierunek_lewo = 5;
            }
         
            var binding = new BasicHttpBinding();
            var cFactoryControl = new ChannelFactory<PRMServices.IRobotControl>(binding, controlEndpoint);
            PRMServices.IRobotControl controlService = cFactoryControl.CreateChannel();

            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            //Zatrzymuje robota i zatrzymuje wszystkie BGWorkery odpowiedzialne za jazdę
            controlService.Stop();
            W_górę.CancelAsync();
            Wdół.CancelAsync();
            Zwalnianie_przy_jeździe_w_przód.CancelAsync();
            Zwalnianie_przy_jeździe_w_tył.CancelAsync();
        }

        //Region z eventami związanymi ze wciskaniem danych obrazków sygnalizujących stan robotów
        #region Oval:
        private void ovalPictureBox9_Click(object sender, EventArgs e)
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            if (Wybor_robota== 0)
            {actionService.LeaseRobot(Nr_ID_Uporządkowane[4]);
                Wybor_robota= 5;
            }
            else MessageBox.Show("Wybrany jest robot nr" + Nr_ID_Uporządkowane[Wybor_robota- 1]);
            

        }
      private void ovalPictureBox8_Click(object sender, EventArgs e)
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            if (Wybor_robota== 0)
            {
                actionService.LeaseRobot(Nr_ID_Uporządkowane[3]);
                Wybor_robota= 4;
            }
            else MessageBox.Show("Wybrany jest robot nr" + Nr_ID_Uporządkowane[Wybor_robota- 1]);

        }
        private void ovalPictureBox7_Click(object sender, EventArgs e)
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            if (Wybor_robota== 0)
            {
               actionService.LeaseRobot(Nr_ID_Uporządkowane[2]);
                Wybor_robota= 3;
            }
            else MessageBox.Show("Wybrany jest robot nr" + Nr_ID_Uporządkowane[Wybor_robota- 1]);
        }
        private void ovalPictureBox6_Click(object sender, EventArgs e)
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            if (Wybor_robota== 0)
            {
                actionService.LeaseRobot(Nr_ID_Uporządkowane[1]);
                Wybor_robota= 2;
            }
            else MessageBox.Show("Wybrany jest robot nr" + Nr_ID_Uporządkowane[Wybor_robota- 1]);
        }       
        private void ovalPictureBox5_Click(object sender, EventArgs e)
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            
            if (Wybor_robota== 0)
            {
               actionService.LeaseRobot(Nr_ID_Uporządkowane[0]);
                Wybor_robota= 1;
            }
            else MessageBox.Show("Wybrany jest robot nr" + Nr_ID_Uporządkowane[Wybor_robota-1]);
        }
        private void ovalPictureBox14_Click(object sender, EventArgs e)
        {
            
            if (Wybor_robota==0)
            {
                MessageBox.Show("Ten robot jest zajety");
               
            }
            else MessageBox.Show("Masz już robota");
        }
        private void ovalPictureBox13_Click(object sender, EventArgs e)
        {
            if (Wybor_robota== 0)
            {
                MessageBox.Show("Ten robot jest zajety");
            }
            else MessageBox.Show("Masz już robota");
        }
        private void Rob6_Click(object sender, EventArgs e)
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            if (Wybor_robota== 0)
            {
                actionService.LeaseRobot(Nr_ID_Uporządkowane[5]);
                Wybor_robota= 6;
            }
            else MessageBox.Show("Wybrany jest robot nr" + Nr_ID_Uporządkowane[Wybor_robota- 1]);
        }
        private void Robo7_Click(object sender, EventArgs e)
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            if (Wybor_robota== 0)
            {
                actionService.LeaseRobot(Nr_ID_Uporządkowane[6]);
                Wybor_robota= 7;
            }
            else MessageBox.Show("Wybrany jest robot nr" + Nr_ID_Uporządkowane[Wybor_robota- 1]);
        }
        private void Robo8_Click(object sender, EventArgs e)
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            if (Wybor_robota== 0)
            {
                actionService.LeaseRobot(Nr_ID_Uporządkowane[7]);
                Wybor_robota= 8;
            }
            else MessageBox.Show("Wybrany jest robot nr" + Nr_ID_Uporządkowane[Wybor_robota- 1]);
        }

        private void X6_Click(object sender, EventArgs e)
        {
            if (Wybor_robota== 0)
            {
                MessageBox.Show("Ten robot jest zajety");
            }
            else MessageBox.Show("Masz już robota");
        }

        private void X7_Click(object sender, EventArgs e)
        {
            if (Wybor_robota== 0)
            {
                MessageBox.Show("Ten robot jest zajety");
            }
            else MessageBox.Show("Masz już robota");
        }

        private void X8_Click(object sender, EventArgs e)
        {
            if (Wybor_robota== 0)
            {
                MessageBox.Show("Ten robot jest zajety");
            }
            else MessageBox.Show("Masz już robota");
        }
        private void ovalPictureBox12_Click(object sender, EventArgs e)
        {
            if (Wybor_robota== 0)
            {
                MessageBox.Show("Ten robot jest zajety");
            }
            else MessageBox.Show("Masz już robota");
        }
        private void ovalPictureBox10_Click(object sender, EventArgs e)
        {
            if (Wybor_robota== 0)
            {
                MessageBox.Show("Ten robot jest zajety");
            }
            else MessageBox.Show("Masz już robota");
        }
        private void ovalPictureBox11_Click(object sender, EventArgs e)
        {
            if (Wybor_robota== 0)
            {
                MessageBox.Show("Ten robot jest zajety");
            }
            else MessageBox.Show("Masz już robota");
        }
        #endregion
       
        //Event naciśnięcia na planszę
        private void Obraz_MouseClick(object sender, MouseEventArgs e)
        {if (Auto.Checked)
            {
                var binding = new BasicHttpBinding();
                var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
                PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
               
                //Przypisane współrzędnych pictureBoxa

                Współ_X_Myszka_Układ_PictureBox = e.X;
                Współ_Y_Myszka_Układ_PictureBox = e.Y;

                //Transformacja i przeskalowanie punktów

                Współ_X_Myszka_Przeskalowane_Układ_Stołu = 1900 - e.X / 0.4;
                Współ_Y_Myszka_Przeskalowane_Układ_Stołu = 1900 - e.Y / 0.4;
                
                //Sprawdzenie, czy w tym miejscu jest robot
                for (int O = 0; O < 8; O++)
                {
                    if ((Math.Sqrt(Math.Pow((Współ_X_Myszka_Układ_PictureBox - (float)Convert.ToDecimal(Współrzędne_Robota_Przetransferowane_Uporządkowane[O, 0])), 2) + Math.Pow((Współ_X_Myszka_Układ_PictureBox - (float)Convert.ToDecimal(Współrzędne_Robota_Przetransferowane_Uporządkowane[O, 1])), 2)) <= 19))
                    {
                        if (Tekst_stan_Uporządkowane[O] == "FREE")
                        {
                            Wybor_robota = O + 1;
                            actionService.LeaseRobot(Nr_ID_Uporządkowane[O]);
                            Lista_Stanu[O] = 1;
                            Czy_robot_naciśnięty = 1;
                          
                        }
                        if (Tekst_stan_Uporządkowane[O] == "LEASED" && O == Nr_ID_Uporządkowane[Wybor_robota])
                        {
                            Czy_robot_naciśnięty = 1;
                           
                            MessageBox.Show("To twój robot");

                        }
                        if (Tekst_stan_Uporządkowane[O] == "LEASED" && O != Nr_ID_Uporządkowane[Wybor_robota])
                        {
                            Czy_robot_naciśnięty = 1;
                          
                            MessageBox.Show("Wybrany jest robot nr" + Nr_ID_Uporządkowane[Wybor_robota - 1]);

                        }

                        if (Tekst_stan_Uporządkowane[O] == "LEASED" && Wybor_robota == 0)
                        {
                            Czy_robot_naciśnięty = 1;
                           
                            MessageBox.Show("Ten robot jest zajęty");

                        }
                    }
                }
                //Jeśli go nie ma sprawdzamy czy punkt nie jest w obszarach niedostępnych dla robota i poprawiamy go jak trzeba
                if (Czy_robot_naciśnięty == 0 && Wybor_robota != 0)
                {
                    if (Współ_X_Myszka_Przeskalowane_Układ_Stołu < 47.5)
                        Współ_X_Myszka_Przeskalowane_Układ_Stołu = 47.5;
                    else if (Współ_X_Myszka_Przeskalowane_Układ_Stołu > 1900 - 47.5)
                        Współ_X_Myszka_Przeskalowane_Układ_Stołu = 1900 - 47.5;
                    if (Współ_Y_Myszka_Przeskalowane_Układ_Stołu < 47.5)
                        Współ_Y_Myszka_Przeskalowane_Układ_Stołu = 47.5;
                    else if (Współ_Y_Myszka_Przeskalowane_Układ_Stołu > 1900 - 47.5)
                        Współ_Y_Myszka_Przeskalowane_Układ_Stołu = 1900 - 47.5;
                    if (Współ_X_Myszka_Układ_PictureBox < 19)
                        Współ_X_Myszka_Układ_PictureBox = 19;
                    else if (Współ_X_Myszka_Układ_PictureBox > 760 - 19)
                        Współ_X_Myszka_Układ_PictureBox = 760 - 19;
                    if (Współ_Y_Myszka_Układ_PictureBox < 19)
                        Współ_Y_Myszka_Układ_PictureBox = 19;
                    else if (Współ_Y_Myszka_Układ_PictureBox > 760 - 19)
                        Współ_Y_Myszka_Układ_PictureBox = 760 - 19;
                   
                  
                    //Wyświetlamy wskazany punkt

                    Punkt_kon.Text = Convert.ToString(Współ_X_Myszka_Przeskalowane_Układ_Stołu) + " " + Convert.ToString(Współ_Y_Myszka_Przeskalowane_Układ_Stołu);

                    //Odpalabym BGWorkera odpowiedzialnego za generacje ścieżki i jej realizację

                    if (Jazda_do_celu.IsBusy != true)
                    {
                        Jazda_do_celu.RunWorkerAsync();
                    }
                }
                Czy_robot_naciśnięty = 0;
            }

        }  
        //Wątek rysujący aktualną sytuację na planszy
        private void Rysowanie()
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            var cFactoryControl = new ChannelFactory<PRMServices.IRobotControl>(binding, controlEndpoint);
            var cFactoryCheck = new ChannelFactory<PRMServices.IPlatformStatus>(binding, checkObject);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            PRMServices.IRobotControl controlService = cFactoryControl.CreateChannel();
            PRMServices.IPlatformStatus platformStatus = cFactoryCheck.CreateChannel();

            while (true)
            {

                //Czyszczenie obrazu
                   f.Clear(Color.Empty);
                   //Rysowanie przeszkód
                    foreach (var punkty in tablicapunktow)
                    {
                        f.DrawPolygon(Czarny, punkty);
                        f.FillPolygon(blueBrush, punkty);

                    }
              
               //Wstawianie robotów na wizualizację
                    foreach (var Ilość_robotów in Nr_ID_Uporządkowane)
                    {
                    if ((float)Convert.ToDecimal(Współrzędne_Robota_Przetransferowane_Uporządkowane[Nr_wektora_Rys_rob, 0]) != 0 && (float)Convert.ToDecimal(Współrzędne_Robota_Przetransferowane_Uporządkowane[Nr_wektora_Rys_rob, 1]) != 0) //Sprawdzenie, czy ten robot jest na planszy
                    {
                        Image nowy = RotateImage(Properties.Resources.nowy_roro, Theta_Uporządkowane[Nr_wektora_Rys_rob]); //Tworzony jest obraz, zawierający robota obróconego do jego orientacji

                        f.DrawImage(nowy, ((float)Convert.ToDecimal(Współrzędne_Robota_Przetransferowane_Uporządkowane[Nr_wektora_Rys_rob, 0]) - 25), ((float)Convert.ToDecimal(Współrzędne_Robota_Przetransferowane_Uporządkowane[Nr_wektora_Rys_rob, 1]) - 25)); //Narysowanie tego obrazka na planszy
                    }
                    Nr_wektora_Rys_rob++;
                    
                }
                Nr_wektora_Rys_rob = 0;
           //Rysowanie kolorowych kółek wokół przywłaszczonych robotów
                foreach (var Okręgi in Nr_ID_Uporządkowane)
                    {
                    Współ_X_Rysowanie_okręgów =(float)Convert.ToDecimal(Współrzędne_Robota_Przetransferowane_Uporządkowane[Nr_wektora_Rysowanie_okręgów_rob, 0]);

                    Współ_Y_Rysowanie_okręgów = (float)Convert.ToDecimal(Współrzędne_Robota_Przetransferowane_Uporządkowane[Nr_wektora_Rysowanie_okręgów_rob, 1]);

                    if (Lista_Stanu[Nr_wektora_Rysowanie_okręgów_rob]==1)
                    {
                                
                    f.DrawEllipse(Kolory[Nr_wektora_Rysowanie_okręgów_rob], (float)(Współ_X_Rysowanie_okręgów - 19), (float)(Współ_Y_Rysowanie_okręgów - 19), (float)(39), (float)(39));
                                
                    }
                    Nr_wektora_Rysowanie_okręgów_rob++;
                    }
                Nr_wektora_Rysowanie_okręgów_rob = 0;      
               
                //Rysowanie krzyżyka punktu docelowego
                if (Współ_X_Myszka_Układ_PictureBox > 0 && Współ_Y_Myszka_Układ_PictureBox > 0)
                {
                    //Tworzenie krzyżyka
                    PointF X1 = new PointF((Współ_X_Myszka_Układ_PictureBox + 15), Współ_Y_Myszka_Układ_PictureBox + 15);
                    PointF X2 = new PointF(Współ_X_Myszka_Układ_PictureBox - 15, Współ_Y_Myszka_Układ_PictureBox + 15);
                    PointF X3 = new PointF(Współ_X_Myszka_Układ_PictureBox + 15, Współ_Y_Myszka_Układ_PictureBox - 15);
                    PointF X4 = new PointF(Współ_X_Myszka_Układ_PictureBox - 15, Współ_Y_Myszka_Układ_PictureBox - 15);
                    f.DrawLine(BLU, X1, X4);
                    f.DrawLine(BLU, X2, X3);
                }
                Obraz.Invalidate();//Odświeżanie obrazu
                Thread.Sleep(75);
            }
        }

        //Guzik odpalający robota do manualnego sterowania(do zmienienia na check boxa)

        private void Guzik_Jazdy_Click(object sender, EventArgs e)
        {
            var binding = new BasicHttpBinding();
            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();

            Manul = new Thread(Manualnie);
            Manul.Start();
        }

        //Wątek do sterowania robotem manualnie
        private void Manualnie()
        {
            var binding = new BasicHttpBinding();
            var cFactoryControl = new ChannelFactory<PRMServices.IRobotControl>(binding, controlEndpoint);
            PRMServices.IRobotControl controlService = cFactoryControl.CreateChannel();

            var cFactoryAction = new ChannelFactory<PRMServices.IPlatformAction>(binding, actionEndpoint);
            PRMServices.IPlatformAction actionService = cFactoryAction.CreateChannel();
            while (true)
            {if (Sim.Checked)
                {
                    if (Zwrot == 0 && Różnica_prawo_lewo!= 0)
                    {
                       if (Różnica_prawo_lewo == -10)
                            controlService.Drive(20,40);
                        if (Różnica_prawo_lewo == -8)
                            controlService.Drive(20, 36);
                        if (Różnica_prawo_lewo == -6)
                            controlService.Drive(20, 32);
                        if (Różnica_prawo_lewo == -4)
                            controlService.Drive(20, 28);                      
                        if (Różnica_prawo_lewo == -2)
                            controlService.Drive(20, 24);
                        if (Różnica_prawo_lewo == 10)
                            controlService.Drive(40, 20);
                        if (Różnica_prawo_lewo == 8)
                            controlService.Drive(36, 20);
                        if (Różnica_prawo_lewo == 6)
                            controlService.Drive(32, 20);
                        if (Różnica_prawo_lewo == 4)
                            controlService.Drive(28, 20);
                        if (Różnica_prawo_lewo == 2)
                            controlService.Drive(24, 20);
                    }
                    else
                controlService.Drive(Prędkości[Zwrot + 10] - (3 * ((Różnica_prawo_lewo / 2) * Convert.ToInt16(Sim.Checked))), Prędkości[Zwrot + 10] + (3 * ((Różnica_prawo_lewo / 2) * Convert.ToInt16(Sim.Checked))));

                        
                }if (Zwrot != 0)
                {//Liczymy czy zwrot jest dodatni czy ujemny
                    Znak_minusa = Math.Abs(Zwrot) / Zwrot;
                }

                if (Arcade.Checked)
                {if (W_lewo==1)
                    {
                        // Zmieniamy prędkość skręcania robota bazując na aktualnej wartości "Zwrot"
                        if (Math.Abs(Zwrot)>0 && Math.Abs(Zwrot)<=2)
                            controlService.Drive(Znak_minusa*20,Znak_minusa*24);                       
                        if (Math.Abs(Zwrot) > 2 && Math.Abs(Znak_minusa*Zwrot) <= 4)
                            controlService.Drive(Znak_minusa * 20, Znak_minusa * 28);
                        if (Math.Abs(Zwrot) > 4 && Math.Abs(Zwrot) <= 6)
                            controlService.Drive(Znak_minusa * 20, Znak_minusa * 32);
                        if (Math.Abs(Zwrot) > 6 && Math.Abs(Zwrot) <= 8)
                            controlService.Drive(Znak_minusa * 20, Znak_minusa * 36);
                        if (Math.Abs(Zwrot) > 8 && Math.Abs(Zwrot) <= 10 || Math.Abs(Zwrot) == 0)
                            controlService.Drive(Znak_minusa * 20, Znak_minusa * 40);
                    }
                else if(W_prawo==1)
                    {   // Zmieniamy prędkość skręcania robota bazując na aktualnej wartości "Zwrot"
                        if (Math.Abs(Zwrot) > 0 && Math.Abs(Zwrot) <= 2)
                            controlService.Drive(Znak_minusa * 24, Znak_minusa * 20);
                        if (Math.Abs(Zwrot) > 2 && Math.Abs(Zwrot) <= 4)
                            controlService.Drive(Znak_minusa * 28, Znak_minusa * 20);
                        if (Math.Abs(Zwrot) > 4 && Math.Abs(Zwrot) <= 6)
                            controlService.Drive(Znak_minusa * 32, Znak_minusa * 20);
                        if (Math.Abs(Zwrot) > 6 && Math.Abs(Zwrot) <= 8)
                            controlService.Drive(Znak_minusa * 36, Znak_minusa * 20);
                        if (Math.Abs(Zwrot) > 8 && Math.Abs(Zwrot) <= 10 || Math.Abs(Zwrot)==0)
                            controlService.Drive(Znak_minusa * 40, Znak_minusa * 20);

                    }
                    else controlService.Drive(Prędkości[Zwrot + 10], Prędkości[Zwrot + 10]);
                }
                Thread.Sleep(50);
                
            }
        }
        
        //BGWorker, który dekrementuje wartość Zwrot co 500 ms, przy osiągnięciu 0 czeka dłużej
        private void wdół_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker1 = sender as BackgroundWorker;
            while (true)
            {
                if (worker1.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
                if(Zwrot==0)
                {
                    Thread.Sleep(1000);
                }
                if (Zwrot > -10)
                {
                    Zwrot = Zwrot - 1;
                    
                    Thread.Sleep(500);
                }
               
            }
        }
       
        //BGWorker, który inkrementuje zmienną 'Kierunek_lewo' i dekrementuje zmienną 'Kierunek_prawo'
        private void wprawo_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker2 = sender as BackgroundWorker;
           
            while (true)
            { if (worker2.CancellationPending == true)
            {
                e.Cancel = true;
               return;
            }
                if (Kierunek_prawo < 10)
                {
                    Kierunek_prawo = Kierunek_prawo + 1;
                    Kierunek_lewo = Kierunek_lewo - 1;

                    Thread.Sleep(500);
                }
                
            }
        }

        //BGWorker, który inkrementuje zmienną 'Kierunek_prawo' i dekrementuje zmienną 'Kierunek_lewo'
        private void Wlewo_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker3 = sender as BackgroundWorker;
            
            while (true)
            {if (worker3.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

                if (Kierunek_lewo < 10)
                {
                    Kierunek_prawo = Kierunek_prawo - 1;
                    Kierunek_lewo = Kierunek_lewo + 1;
                    Thread.Sleep(500);

                }
              
            }
        }
        
        //Funkcja do obracania obrazka robota wizualizacji
        private Bitmap RotateImage(Bitmap bmp, float angle)
        {
            float height = bmp.Height;
            float width = bmp.Width;
            int hypotenuse = System.Convert.ToInt32(System.Math.Floor(Math.Sqrt(height * height + width * width)));
            Bitmap rotatedImage = new Bitmap(hypotenuse, hypotenuse);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform((float)rotatedImage.Width / 2, (float)rotatedImage.Height / 2); //set the rotation point as the center into the matrix
                g.RotateTransform(angle-90); //rotate
                g.TranslateTransform(-(float)rotatedImage.Width / 2, -(float)rotatedImage.Height / 2); //restore rotation point into the matrix
                g.DrawImage(bmp, (hypotenuse - width) / 2, (hypotenuse - height) / 2, width, height);
            }
            return rotatedImage;
        }  
    }
}
