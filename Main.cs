using sus = System;
using s = SFML.System;
using g = SFML.Graphics;
using w = SFML.Window;
using c = Cards;
using scg = System.Collections.Generic;
namespace Main
{
    class ProgramMain
    {
        public static int Main()
        {
            MainWindow Game = new MainWindow();
            Game.GameLoop();
            return 0;
        }
    }
    class MainWindow
    {
        sus.Random rand = new sus.Random();
        public int width_screen = 700, height_screen = 700,count_of_players = 1,size_of_colod=3,player=1;
        g.RenderWindow Window;
        scg.List<scg.List<c.Card>> colods = new scg.List<scg.List<c.Card>>();
        scg.List<c.Card> map = new scg.List<c.Card>();
        string [] jpgs = { "images/road_1_2_3_4.png", "images/road_1.png", "images/road_2.png", "images/road_3.png", "images/road_4.png", "images/road_1_2_3.png", "images/road_4_1_2.png", "images/road_1_2.png", "images/road_1_3.png", "images/road_2_3_4.png", "images/road_2_3.png", "images/road_2_4.png", "images/road_3_4.png", "images/road_4_1.png", "images/road_3_4_1.png", "images/car_stream.png" };
        string [] gasstations = { "images/gas_station_1_red.png", "images/gas_station_2_red.png", "images/gas_station_3_red.png", "images/gas_station_1_blue.png", "images/gas_station_2_blue.png", "images/gas_station_3_blue.png", "images/gas_station_1_green.png", "images/gas_station_2_green.png", "images/gas_station_3_green.png", "images/gas_station_1_orange.png", "images/gas_station_2_orange.png", "images/gas_station_3_orange.png" };
        public MainWindow()
        {
            Window = new g.RenderWindow(new w.VideoMode((uint)width_screen,(uint)height_screen),"KING OF GAS STATIONS");
            Window.MouseMoved += Window_MouseMoved;
        }
        public void GameLoop()
        {
            while(Window.IsOpen)
            {
                Window.Clear(g.Color.White);
                foreach (c.Card card in colods[player - 1])
                    Window.Draw(card);
                foreach (c.Card card in map)
                    Window.Draw(card);
                Window.DispatchEvents();
                Window.Display();
            }
        }

        void initialize_colods()
        {
            for(int i=0;i<count_of_players;++i)
            {
                scg.List<c.Card> coloda = new scg.List<c.Card>();
                for (int j = 0; j < size_of_colod; ++j)
                {
                    string name = jpgs[rand.Next(jpgs.Length)];
                    if(name.Contains("gas"))
                    {
                        c.Doroga doroga = new c.Doroga{ player = i+1, sprite = new g.Sprite(new g.Texture(new g.Image(name)))};
                        coloda.Add(doroga);
                    }
                    else
                    {
                        c.Trafic trafic = new c.Trafic { player = i + 1, sprite = new g.Sprite(new g.Texture(new g.Image(name))) };
                        coloda.Add(trafic);
                    }
                }
                for(int j = 4*i; j<4*i+4;++j)
                {
                    string name = gasstations[j];
                    c.GasStation gas = new c.GasStation { player = i + 1, sprite = new g.Sprite(new g.Texture(new g.Image(name))) };
                }

            }
        }
        void Window_MouseMoved(object sender,w.MouseMoveEventArgs e)
        {

        }

    }
}