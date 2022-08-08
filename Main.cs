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
        c.Card catchicng = null;
        s.Vector2f catching_pos = new s.Vector2f();
        sus.Random rand = new sus.Random();
        public int width_screen = 1280, height_screen = 720,count_of_players = 1,size_of_colod=3,player=1;
        g.RenderWindow Window;
        scg.List<scg.List<c.Card>> colods = new scg.List<scg.List<c.Card>>();
        scg.List<c.Card> map = new scg.List<c.Card>();
        scg.List<c.Card> way = new scg.List<c.Card>();
        string [] jpgs = { "images/road_1_2_3_4.png", "images/road_1.png", "images/road_2.png", "images/road_3.png", "images/road_4.png", "images/road_1_2_3.png", "images/road_4_1_2.png", "images/road_1_2.png", "images/road_1_3.png", "images/road_2_3_4.png", "images/road_2_3.png", "images/road_2_4.png", "images/road_3_4.png", "images/road_4_1.png", "images/road_3_4_1.png", "images/car_stream.png" };
        string [] gasstations = { "images/gas_station_1_red.png", "images/gas_station_2_red.png", "images/gas_station_3_red.png", "images/gas_station_1_blue.png", "images/gas_station_2_blue.png", "images/gas_station_3_blue.png", "images/gas_station_1_green.png", "images/gas_station_2_green.png", "images/gas_station_3_green.png", "images/gas_station_1_orange.png", "images/gas_station_2_orange.png", "images/gas_station_3_orange.png" };
        s.Vector2i mouse_pos1 = new s.Vector2i();
        public MainWindow()
        {
            Window = new g.RenderWindow(new w.VideoMode((uint)width_screen,(uint)height_screen),"KING OF GAS STATIONS");
            Window.MouseMoved += Window_MouseMoved;
            Window.MouseButtonPressed +=Window_MouseButtonPressed;
        }
        public void GameLoop()
        {
            initialize_colods();
            foreach (scg.List<c.Card> list in colods)
                set_coords_colod(list);
            c.Doroga start = new c.Doroga { player = 0, sprite = new g.Sprite(new g.Texture(new g.Image("images/road_1.png"))) };
            start.sprite.Origin = new s.Vector2f(50, 50);
            start.sprite.Position= new s.Vector2f(width_screen/2,height_screen/2);
            map.Add(start);
            while(Window.IsOpen)
            {
                
                Window.Clear(g.Color.White);
                foreach (c.Card card in colods[player - 1])
                    Window.Draw(card);
                foreach (c.Card card in map)
                    Window.Draw(card);
                Window.DispatchEvents();
                mouse_pos1 = w.Mouse.GetPosition(Window);
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
                    if(name.Contains("road"))
                    {
                        c.Doroga doroga = new c.Doroga{ player = i+1, sprite = new g.Sprite(new g.Texture(new g.Image(name)))};
                        doroga.sprite.Origin = new s.Vector2f(50, 50);
                        coloda.Add(doroga);
                    }
                    else
                    {
                        c.Trafic trafic = new c.Trafic { player = i + 1, sprite = new g.Sprite(new g.Texture(new g.Image(name))) };
                        trafic.sprite.Origin = new s.Vector2f(50, 50);
                        coloda.Add(trafic);
                    }
                }
                for(int j = 4*i; j<4*i+3;++j)
                {
                    string name = gasstations[j];
                    c.GasStation gas = new c.GasStation { player = i + 1, sprite = new g.Sprite(new g.Texture(new g.Image(name))) };
                    gas.sprite.Origin = new s.Vector2f(50, 50);
                    coloda.Add(gas);
                }
                colods.Add(coloda);

            }
        }
        void set_coords_colod(scg.List<c.Card> coloda)
        {
            float x = width_screen/2 - 110 * coloda.Count/2,y=height_screen-75;
            for(int i=0;i<coloda.Count;++i)
                coloda[i].sprite.Position = new s.Vector2f(x + 110f * i, y);
            

        }
        void Window_MouseMoved(object sender,w.MouseMoveEventArgs e)
        {
            if(catchicng==null & w.Mouse.IsButtonPressed(w.Mouse.Button.Left))
            {
                s.Vector2f movement = new s.Vector2f(e.X-mouse_pos1.X,e.Y-mouse_pos1.Y);
                foreach (c.Card card in map)
                    card.sprite.Position += movement;
            }
            if (catchicng != null & (catchicng as c.Doroga != null | catchicng as c.GasStation != null))
                catchicng.sprite.Position = new s.Vector2f(e.X, e.Y);
        }
        void Window_MouseButtonPressed(object sender,w.MouseButtonEventArgs e)
        {
            if(catchicng == null)
            {
                foreach (c.Card card in colods[player - 1])
                    if (card.sprite.GetGlobalBounds().Contains(e.X, e.Y))
                    {
                        catchicng = card;
                        catching_pos = card.sprite.Position;
                        break;
                    }
            }
            else
            {
                if(catchicng as c.Doroga!=null)
                {
                    
                }
                if(catchicng as c.GasStation!=null)
                {
                    
                }
                catchicng.sprite.Position = catching_pos;
                catchicng = null;

            }
            

        }
        void Window_KeyPressed(object sender,w.KeyEventArgs e)
        {
            var window = (g.RenderWindow)sender;
            if (e.Code == w.Keyboard.Key.Escape)
                window.Close();
        }

    }
}