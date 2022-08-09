﻿using sus = System;
using s = SFML.System;
using g = SFML.Graphics;
using w = SFML.Window;
using c = Cards;
using scg = System.Collections.Generic;
using l = Lines;
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
        scg.List<l.Line> grid = new scg.List<l.Line>();
        string [] jpgs = { "images/road_1_2_3_4.png", "images/road_1.png", "images/road_2.png", "images/road_3.png", "images/road_4.png", "images/road_1_2_3.png", "images/road_4_1_2.png", "images/road_1_2.png", "images/road_1_3.png", "images/road_2_3_4.png", "images/road_2_3.png", "images/road_2_4.png", "images/road_3_4.png", "images/road_4_1.png", "images/road_3_4_1.png", "images/car_stream.png" };
        string [] gasstations = { "images/gas_station_1_red.png", "images/gas_station_2_red.png", "images/gas_station_3_red.png", "images/gas_station_1_blue.png", "images/gas_station_2_blue.png", "images/gas_station_3_blue.png", "images/gas_station_1_green.png", "images/gas_station_2_green.png", "images/gas_station_3_green.png", "images/gas_station_1_orange.png", "images/gas_station_2_orange.png", "images/gas_station_3_orange.png" };
        s.Vector2i mouse_pos1 = new s.Vector2i();
        public MainWindow()
        {
            Window = new g.RenderWindow(new w.VideoMode((uint)width_screen,(uint)height_screen),"KING OF GAS STATIONS");
            Window.MouseMoved += Window_MouseMoved;
            Window.MouseButtonPressed +=Window_MouseButtonPressed;
            Window.KeyPressed += Window_KeyPressed;
            Window.MouseWheelScrolled += Window_MouseWheel;
        }
        public void GameLoop()
        {
            initialize_colods();
            foreach (scg.List<c.Card> list in colods)
                set_coords_colod(list);
            c.Doroga start = new c.Doroga { player = 0, sprite = new g.Sprite(new g.Texture(new g.Image("images/road_1.png"))) };
            start.sprite.Origin = new s.Vector2f(50, 50);
            start.sprite.Position = new s.Vector2f(width_screen / 2, height_screen / 2);
            start.Parse("1");
            map.Add(start);
            create_coords_grid();
            while(Window.IsOpen)
            {
                Window.Clear(g.Color.White);
                foreach (c.Card card in map)
                    Window.Draw(card);
                foreach (l.Line line in grid)
                    Window.Draw(line);
                foreach (c.Card card in colods[player - 1])
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
                        doroga.Parse(name);
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
                    gas.Parse(name);
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
        void Window_MouseWheel(object sender,w.MouseWheelScrollEventArgs e)
        {
            s.Vector2f scale = new s.Vector2f(0.25f * e.Delta, 0.25f * e.Delta);
            scg.List<c.Card> map2 = map.FindAll(x => !x.Equals(map[0]));
            foreach (c.Card card in map2)
                correct_coords_scale(scale, card);
            foreach (c.Card card in map)
                card.sprite.Scale += scale;
            create_coords_grid();
            if (catchicng != null)
                catchicng.sprite.Scale += scale;
          
        }
        void Window_MouseMoved(object sender,w.MouseMoveEventArgs e)
        {
            if(catchicng==null & w.Mouse.IsButtonPressed(w.Mouse.Button.Left))
            {
                s.Vector2f movement = new s.Vector2f(e.X-mouse_pos1.X,e.Y-mouse_pos1.Y);
                foreach (c.Card card in map)
                    card.sprite.Position += movement;
                create_coords_grid();
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
                    double side = 100 * map[0].sprite.Scale.X;
                    catchicng.sprite.Position = convert_coords(catchicng);
                    scg.List<c.Card> prov = map.FindAll(card1 => (card1.sprite.Position.X - catchicng.sprite.Position.X == 0 | card1.sprite.Position.Y - catchicng.sprite.Position.Y == 0)& dlina(catchicng,card1)<=side);
                    bool flag = true;
                    foreach (c.Card card in prov)
                    {
                        sus.Console.WriteLine("count\n");
                        var XYI = card as c.Doroga;
                        var XYI1 = card as c.GasStation;
                        if (XYI == null)
                            flag = XYI1.can_i_set(catchicng);
                        else
                            flag = XYI.can_i_set(catchicng);
                        if (!flag)
                            break;
                    }
                    if(flag)
                    {
                        //sus.Console.WriteLine("can i  way\n");
                        flag = false;
                        scg.List<c.Card> prov2 = map.FindAll(card1 => (card1.sprite.Position.X - catchicng.sprite.Position.X == 0 | card1.sprite.Position.Y - catchicng.sprite.Position.Y == 0) & card1 as c.Doroga!=null & dlina(catchicng, card1) <= side);
                        foreach(c.Card card in prov2)
                        {
                            var dorg = card as c.Doroga;
                            flag = dorg.can_i_way(catchicng as c.Doroga);
                            if (flag)
                                break;
                        }
                    }
                    if(!flag)
                        catchicng.sprite.Position = catching_pos;
                    else 
                    {
                        map.Add(catchicng);
                        colods[player-1].Remove(catchicng);
                        set_coords_colod(colods[player - 1]);
                    }
                }
                if (catchicng as c.GasStation != null)
                {
                    double side = 100 * map[0].sprite.Scale.X;
                    catchicng.sprite.Position = convert_coords(catchicng);
                    scg.List<c.Card> prov = map.FindAll(card1 => (card1.sprite.Position.X - catchicng.sprite.Position.X == 0 | card1.sprite.Position.Y - catchicng.sprite.Position.Y == 0) & (sus.Math.Abs(card1.sprite.Position.X - catchicng.sprite.Position.X) <= side & sus.Math.Abs(card1.sprite.Position.Y - catchicng.sprite.Position.Y) <= side));
                    bool flag = true;
                    foreach(c.Card card in prov)
                    {
                        var XYI = card as c.Doroga;
                        var XYI1 = card as c.GasStation;
                        if (XYI == null)
                            flag = XYI1.can_i_set(catchicng);
                        else
                            flag = XYI.can_i_set(catchicng);
                        if (!flag)
                            break;
                    }
                    if(!flag | prov.Count==0)
                        catchicng.sprite.Position = catching_pos;
                    else
                    {
                        map.Add(catchicng);
                        colods[player - 1].Remove(catchicng);
                        set_coords_colod(colods[player - 1]);
                    }
                }

                catchicng = null;
                //sus.Console.WriteLine("coords = {0}", convert_coords(catchicng));


            }
            

        }
        void Window_KeyPressed(object sender,w.KeyEventArgs e)
        {
            var window = (g.RenderWindow)sender;
            if (e.Code == w.Keyboard.Key.Escape)
                window.Close();
            //sus.Console.WriteLine("keypr");
            if (catchicng as c.Doroga!=null & e.Code==w.Keyboard.Key.R)
            {
                catchicng.sprite.Rotation += 90;
                if (catchicng.sprite.Rotation == 360)
                    catchicng.sprite.Rotation = 0;
                c.Doroga dor = catchicng as c.Doroga;
                dor.rotate();
            }
            if (e.Code == w.Keyboard.Key.A)
                add_cards_to_colod(1, colods[player - 1], player);
            if(e.Code ==w.Keyboard.Key.T)
            {
                var tr = new c.Trafic();
                tr.sprite = new g.Sprite(new g.Texture(new g.Image("images/car_stream.png")));
                tr.player = player;
                colods[player-1].Add(tr);
            }
            
        }
        void create_coords_grid()
        {
            grid.Clear();
            float x = map[0].sprite.Position.X, y = map[0].sprite.Position.Y, side=100f * map[0].sprite.Scale.X;
            g.Color color = new g.Color(0, 255, 255);
            x -= side / 2;
            y -= side / 2;
            while (x >= side)
                x -= side;
            while (y >= side)
                y -= side;
            for(float x1=x; x1<width_screen;x1+=side)
            {
                l.Line line = new l.Line();
                line.point_one = new s.Vector2f(x1, 0);
                line.point_two = new s.Vector2f(x1, height_screen);
                line.color = new g.Color(0, 255, 255);
                grid.Add(line);
            }
            for (float y1 = y; y1 < height_screen; y1 += side)
            {
                l.Line line = new l.Line();
                line.point_one = new s.Vector2f(0, y1);
                line.point_two = new s.Vector2f(width_screen, y1);
                line.color = new g.Color(0, 255, 255);
                grid.Add(line);
            }
        }
        void add_cards_to_colod(int count_of_cards,scg.List<c.Card> coloda,int player)
        {
            for(int i=0;i<count_of_cards;++i)
            {
                string name = jpgs[rand.Next(jpgs.Length)];
                if(name.Contains("road"))
                {
                    var dorg = new c.Doroga();
                    dorg.sprite = new g.Sprite(new g.Texture(new g.Image(name)));
                    dorg.player = player;
                    dorg.Parse(name);
                    coloda.Add(dorg);
                }
                else
                {
                    var tr = new c.Trafic();
                    tr.sprite = new g.Sprite(new g.Texture(new g.Image(name)));
                    tr.player = player;
                    coloda.Add(tr);
                }
            }
            set_coords_colod(coloda);
        }
        double dlina(c.Card card1 , c.Card card2)
        {
            double dlina = 0;
            dlina = sus.Math.Sqrt(sus.Math.Pow((card1.sprite.Position.X-card2.sprite.Position.X),2)+ sus.Math.Pow((card1.sprite.Position.Y - card2.sprite.Position.Y), 2));
            //sus.Console.WriteLine("dlina = {0}",dlina);
            return dlina;
        }
        s.Vector2f convert_coords(c.Card card)
        {
            float side = 100 * map[0].sprite.Scale.X;
            s.Vector2f pos = new s.Vector2f();
            foreach(l.Line line in grid)
                if(card.sprite.Position.X - line.point_one.X<0)
                {
                    pos.X = line.point_one.X-side/2;
                    break;
                }
            foreach (l.Line line in grid)
                if (card.sprite.Position.Y - line.point_one.Y < 0)
                {
                    pos.Y = line.point_one.Y - side/2;
                    //sus.Console.WriteLine("line {0}", line.point_one);
                    break;
                }
            //sus.Console.WriteLine("coords = {0}}", pos);
            return pos;
        }
        void correct_coords_scale(s.Vector2f scale, c.Card card )
        {
            float side = 100 * map[0].sprite.Scale.X;
            s.Vector2f dif = new s.Vector2f(card.sprite.Position.X - map[0].sprite.Position.X, card.sprite.Position.Y - map[0].sprite.Position.Y);
            s.Vector2i mnoz = new s.Vector2i((int)sus.Math.Round(dif.X / side,sus.MidpointRounding.AwayFromZero), (int)sus.Math.Round(dif.Y / side, sus.MidpointRounding.AwayFromZero));
            s.Vector2f plus = new s.Vector2f(100 * scale.X * mnoz.X, 100 * scale.Y * mnoz.Y);
            card.sprite.Position += plus;
        }
    }
}