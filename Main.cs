using sus = System;
using s = SFML.System;
using g = SFML.Graphics;
using w = SFML.Window;
using c = Cards;
using scg = System.Collections.Generic;
using l = Lines;
using t = Text;
using st = System.Threading;
using p = Polzet;
namespace Main
{
    class ProgramMain
    {
        public static int Main()
        {
            Menu menu = new Menu();
            MainWindow Game = new MainWindow();
            while(!menu.closed)
            {
                menu.MenuLoop();
                if(!menu.closed)
                {
                    MainWindow.count_of_cards = Menu.count_of_cards;
                    MainWindow.count_of_players = Menu.count_of_players;
                    MainWindow.size_of_colod = Menu.count_of_start_cards;
                    Game.GameLoop();
                }
                  
            }
            return 0;
        }
    }
    class Menu
    {   
        g.RenderWindow Window;
        static public int width_screen = 1280, height_screen = 720,count_of_cards=50,count_of_start_cards=3,count_of_players=2;
        g.Text nazvanie = new g.Text { CharacterSize=48,Font=new g.Font("ofont.ru_Impact.ttf"),DisplayedString="KINGS OF GAS STATIONS",FillColor=g.Color.Black };
        scg.List<t.Textbox> textboxes = new scg.List<t.Textbox>();
        t.Textbox exit_st = new t.Textbox();
        scg.List<p.HPolzynok> polzynoks = new scg.List<p.HPolzynok>();
        static g.Font font = new g.Font("ofont.ru_Impact.ttf");
        g.Text text = new g.Text { Font = font,CharacterSize = 28,FillColor = g.Color.Black};
        public bool closed = false,settings_fl = false,load_save=false;
        g.Text message = new g.Text { DisplayedString = "Must be count cards > count players * count start cards",CharacterSize=36,Font=font,FillColor=g.Color.Black,Position=new s.Vector2f(40,40)};
        s.Clock clock = new s.Clock();
        public Menu()
        {
           
        }
        public void MenuLoop()
        {
            p.HPolzynok polzynok1 = new p.HPolzynok();
            polzynok1.set_Fillcolor_act(g.Color.White);
            polzynok1.set_outline_color_act(new g.Color(0, 113, 188));
            polzynok1.set_Fill_color_rect(new g.Color(0, 113, 188));
            polzynok1.set_size_rect(400, 20);
            polzynok1.set_rad_act(20);
            polzynok1.set_Outline_thickness_act(15);
            polzynok1.set_pos_polz(width_screen / 2, height_screen / 2 - 150);

            p.HPolzynok polzynok2 = new p.HPolzynok(ref polzynok1);
            polzynok2.set_pos_polz(width_screen / 2, height_screen / 2);
            p.HPolzynok polzynok3 = new p.HPolzynok(ref polzynok1);
            polzynok3.set_pos_polz(width_screen / 2, height_screen / 2 + 150);

            polzynok1.key = "1";
            polzynok2.key = "2";
            polzynok3.key = "3";

            polzynok1.change_val_func(count_of_cards_func);
            polzynok2.change_val_func(count_players_val);
            polzynok3.change_val_func(count_of_starts_cards);

            polzynok1.change_start_func(co_of__ca_start);
            polzynok2.change_start_func(co_of_p_start);
            polzynok3.change_start_func(co_of_st_ca_start);

            polzynok1.set_start_pos(count_of_cards);
            polzynok2.set_start_pos(count_of_players);
            polzynok3.set_start_pos(count_of_start_cards);

            //polzynok1.move(count_of_cards + (width_screen / 2f) - 200f);
            //polzynok2.move(100f * count_of_players + width_screen / 2f - 200f);
            //polzynok3.move(count_of_start_cards + (width_screen / 2f) - 200f);

            polzynoks.Add(polzynok1);
            polzynoks.Add(polzynok2);
            polzynoks.Add(polzynok3);

            nazvanie.Position = new s.Vector2f(width_screen/2-nazvanie.GetGlobalBounds().Width/2,50);
            t.Textbox textbox1 = new t.Textbox();
            textbox1.set_Fill_color_rect(new g.Color(0, 255, 255));
            textbox1.set_outline_thickness_rect(2);
            textbox1.set_outline_color_rect(g.Color.Black);
            textbox1.set_size_rect(150, 70);
            textbox1.set_size_character_text(32);
            textbox1.set_color_text(g.Color.Black);
            textbox1.set_string("START");
            textbox1.set_pos(new s.Vector2f(width_screen / 2, height_screen / 2 - 150));
            t.Textbox textbox2 = new t.Textbox( ref textbox1);
            textbox2.set_string("SETTINGS");
            textbox2.set_pos(new s.Vector2f(width_screen / 2, height_screen / 2-50));
            t.Textbox textbox3 = new t.Textbox(ref textbox1);
            textbox3.set_string("EXIT");
            textbox3.set_pos(new s.Vector2f(width_screen / 2, height_screen / 2+50));
            exit_st.Copy(ref textbox3);
            exit_st.set_pos(new s.Vector2f(width_screen / 2, height_screen / 2 + 250));
            textboxes.Add(textbox1);
            textboxes.Add(textbox2);
            textboxes.Add(textbox3);
            Window = new g.RenderWindow(new w.VideoMode((uint)width_screen, (uint)height_screen), "Kings of gas stations");
            Window.MouseButtonPressed += Window_MouseButton_Pressed;
            Window.KeyPressed += Window_KeyPressed;
            Window.MouseMoved += Window_MouseMoved;
            while (Window.IsOpen)
            {
                s.Vector2i mouse_pos = w.Mouse.GetPosition(Window);
                Window.Clear(g.Color.White);
                if(!settings_fl)
                {
                    foreach (t.Textbox textbox in textboxes)
                    {
                        if (textbox.contains(mouse_pos.X, mouse_pos.Y))
                            textbox.set_Fill_color_rect(g.Color.Magenta);
                        else
                            textbox.set_Fill_color_rect(new g.Color(0, 255, 255));
                        Window.Draw(textbox);
                    }
                    Window.Draw(nazvanie);
                }
                else
                    settings();
                Window.DispatchEvents();
                Window.Display();
            }
            Window = null;
            polzynoks.Clear();
            textboxes.Clear();
        }
        void Window_MouseButton_Pressed(object sender,w.MouseButtonEventArgs e)
        {
            if (textboxes[2].contains(e.X, e.Y) && !settings_fl)
            {
                Window.Close();
                closed = true;
            }
            else if (textboxes[1].contains(e.X, e.Y) && !settings_fl)
                settings_fl = true;
            else if (textboxes[0].contains(e.X, e.Y) && !settings_fl)
                Window.Close();
            if (settings_fl && exit_st.contains(e.X, e.Y))
                settings_fl = !settings_fl;
        }
        void Window_MouseMoved(object sender,w.MouseMoveEventArgs e)
        {
            if(settings_fl)
            {
                foreach(p.HPolzynok polzynok in polzynoks)
                {
                    if (polzynok.contains(e.X, e.Y))
                        polzynok.set_Fillcolor_act(g.Color.Magenta);
                    else
                        polzynok.set_Fillcolor_act(g.Color.White);
                    if (w.Mouse.IsButtonPressed(w.Mouse.Button.Left) && polzynok.contains(e.X, e.Y))
                        polzynok.move(e.X);
                }
                if(exit_st.contains(e.X,e.Y))
                    exit_st.set_Fill_color_rect(g.Color.Magenta);
                else
                    exit_st.set_Fill_color_rect(new g.Color(0, 255, 255));
            }
        }
        void Window_KeyPressed(object sender,w.KeyEventArgs e)
        {
            if(e.Code==w.Keyboard.Key.Escape && !settings_fl)
            {
                Window.Close();
                closed = true;
            }
            if (e.Code == w.Keyboard.Key.Escape && settings_fl)
                settings_fl = false;
        }
        void settings()
        {
            text.DisplayedString = "COUNT OF CARDS = " + polzynoks[0].get_value().ToString();
            text.Origin = new s.Vector2f(text.GetGlobalBounds().Width / 2f, text.GetGlobalBounds().Height / 2f + text.CharacterSize / 6f);// магические числа на
            text.Position=new s.Vector2f(width_screen/2,height_screen/2-200);
            count_of_cards = polzynoks[0].get_value();
            Window.Draw(text);
            text.DisplayedString = "COUNT OF PLAYERS = " + polzynoks[1].get_value().ToString();
            text.Origin = new s.Vector2f(text.GetGlobalBounds().Width / 2f, text.GetGlobalBounds().Height / 2f + text.CharacterSize / 6f);// магические числа на
            text.Position = new s.Vector2f(width_screen / 2, height_screen/2 - 50);
            Window.Draw(text);
            count_of_players = polzynoks[1].get_value();
            text.DisplayedString = "COUNT OF START CARDS = " + polzynoks[2].get_value().ToString();
            text.Origin = new s.Vector2f(text.GetGlobalBounds().Width / 2f, text.GetGlobalBounds().Height / 2f + text.CharacterSize / 6f);// магические числа на
            text.Position = new s.Vector2f(width_screen / 2, height_screen/2+100);
            count_of_start_cards = polzynoks[2].get_value();
            Window.Draw(text);
            foreach (p.HPolzynok polzynok in polzynoks)
                Window.Draw(polzynok);
            //if (clock.ElapsedTime.AsSeconds() < 1.5f)
            //    Window.Draw(message);
            Window.Draw(exit_st);
        }
        int count_players_val(float x)
        {
            int value = 0;
            value = x >= 0  ? 1 : value;
            value = x >= 100 ? 2 : value;
            value = x >= 200 ? 3 : value;
            value = x >= 300 ? 4 : value;
            return value;
        }
        int count_of_starts_cards(float x)
        {
            int value = 0;
            value = (int)x / 2;
            return value;
        }
        int count_of_cards_func(float x)
        {
            int value = 1;
            value += (int)x;
            return value;
        }
        float co_of_p_start(int value)
        {
            float x = value;
            return x*100f+width_screen/2-200f;
        }
        float co_of_st_ca_start(int value)
        {
            float x = value;
            return x * 2f + width_screen / 2 - 200f;
        }
        float co_of__ca_start(int value)
        {
            float x = value;
            return x + width_screen / 2 - 200f;
        }
        bool can_i_move(p.HPolzynok polzynok)
        {
            bool flag = true;
            //flag = ((polzynoks[0].get_value() - polzynoks[1].get_value() * polzynoks[2].get_value()) > 0) ? false : flag;
            //if (!flag)
            //    clock.Restart();
            return flag;
        }
    }
    class MainWindow
    {
        c.Card catchicng = null;
        s.Vector2f catching_pos = new s.Vector2f();
        sus.Random rand = new sus.Random();
        static public int width_screen = 1280, height_screen = 720,count_of_players = 4,size_of_colod=3,player=1,count_of_cards=20;
        g.RenderWindow Window;
        scg.List<scg.List<c.Card>> colods = new scg.List<scg.List<c.Card>>();
        scg.List<c.Card> map = new scg.List<c.Card>();
        scg.List<c.Doroga> way = new scg.List<c.Doroga>();
        scg.List<l.Line> grid = new scg.List<l.Line>();
        t.Textbox textbox1 = new t.Textbox();
        s.Clock clock = new s.Clock();
        g.Text message = new g.Text { CharacterSize=32,FillColor=g.Color.Black,Font=new g.Font("ofont.ru_Impact.ttf"),DisplayedString="You cannot choose this card to way",Position=new s.Vector2f(width_screen/2,height_screen/10) };
        g.Sprite billy = new g.Sprite { Texture = new g.Texture(new g.Image("images/card zadnik.png")), Origin = new s.Vector2f(50, 50),Position =new s.Vector2f(60,120) };
        g.Text text = new g.Text();
        g.Text count = new g.Text();
        g.Sprite zadnik = new g.Sprite(new g.Texture(new g.Image("images/zadnik.png")));
        bool draw_grid = true;
        string [] jpgs = { "images/road_1_2_3_4.png", "images/road_1.png", "images/road_2.png", "images/road_3.png", "images/road_4.png", "images/road_1_2_3.png", "images/road_1_2_4.png", "images/road_1_2.png", "images/road_1_3.png", "images/road_2_3_4.png", "images/road_2_3.png", "images/road_2_4.png", "images/road_3_4.png", "images/road_1_4.png", "images/road_1_3_4.png", "images/car_stream.png" };
        string [] gasstations = { "images/gas_station_1_red.png", "images/gas_station_2_red.png", "images/gas_station_3_red.png", "images/gas_station_1_blue.png", "images/gas_station_2_blue.png", "images/gas_station_3_blue.png", "images/gas_station_1_green.png", "images/gas_station_2_green.png", "images/gas_station_3_green.png", "images/gas_station_1_orange.png", "images/gas_station_2_orange.png", "images/gas_station_3_orange.png" };
        s.Vector2i mouse_pos1 = new s.Vector2i();
        public MainWindow()
        {

        }
        public void GameLoop()
        {
            Window = new g.RenderWindow(new w.VideoMode((uint)width_screen, (uint)height_screen), "KING OF GAS STATIONS");
            Window.MouseMoved += Window_MouseMoved;
            Window.MouseButtonPressed += Window_MouseButtonPressed;
            Window.KeyPressed += Window_KeyPressed;
            Window.MouseWheelScrolled += Window_MouseWheel;
            message.Position -= new s.Vector2f(message.GetGlobalBounds().Width/2, 0);
            initialize_colods();
            foreach (scg.List<c.Card> list in colods)
                set_coords_colod(list);
            c.Doroga start = new c.Doroga { player = 0, sprite = new g.Sprite(new g.Texture(new g.Image("images/road_1.png"))) };
            start.sprite.Origin = new s.Vector2f(50, 50);
            start.sprite.Position = new s.Vector2f(width_screen / 2, height_screen / 2);
            start.Parse("1");
            map.Add(start);
            create_coords_grid();
            textbox1.set_string("GO!");
            textbox1.set_color_text(new g.Color(255,128,0));
            textbox1.set_Fill_color_rect(new g.Color(0,255,255));
            textbox1.set_size_rect(100, 50);
            textbox1.set_size_character_text(20);
            textbox1.set_outline_color_rect(g.Color.Blue);
            textbox1.set_outline_thickness_rect(2);
            textbox1.set_pos(width_screen - 150,100);
            text.CharacterSize=20;
            text.FillColor = new g.Color(g.Color.Red);
            text.Font = new g.Font("ofont.ru_Impact.ttf");
            text.Position = new s.Vector2f(30,40);
            text.DisplayedString = "PLAYER" + player.ToString();
            count.CharacterSize = 20;
            count.FillColor = g.Color.Black;
            count.Font = new g.Font("ofont.ru_Impact.ttf");
            count.Position = new s.Vector2f(50, 180);
            count.DisplayedString = count_of_cards.ToString();
            while (Window.IsOpen)
            {
                Window.Clear(g.Color.White);
                Window.Draw(zadnik);
                foreach (c.Card card in map)
                    Window.Draw(card);
                if(draw_grid)
                    foreach (l.Line line in grid)
                        Window.Draw(line);
                foreach (c.Card card in colods[player - 1])
                    Window.Draw(card);
                if (clock.ElapsedTime.AsSeconds() < 1 && catchicng!=null)
                    Window.Draw(message);
                if (way.Count > 1 && is_first(way[way.Count - 1]))
                    Window.Draw(textbox1);
                Window.Draw(billy);
                Window.Draw(text);
                Window.Draw(count);
                Window.DispatchEvents();
                if (count_of_cards == 0)
                    win();
                mouse_pos1 = w.Mouse.GetPosition(Window);
                Window.Display();
                if (count_of_cards == 0)
                {
                    st.Thread.Sleep(2000);
                    Window.Close();
                }
            }
            Window = null;
            way.Clear();
            map.Clear();
            foreach (scg.List<c.Card> list in colods)
                list.Clear();
            colods.Clear();
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
                    --count_of_cards;
                }
                for(int j = 3*i; j<3*i+3;++j)
                {
                    string name = gasstations[j];
                    c.GasStation gas = new c.GasStation { player = i + 1, sprite = new g.Sprite(new g.Texture(new g.Image(name))) };
                    gas.sprite.Origin = new s.Vector2f(50, 50);
                    gas.Parse(name);
                    coloda.Add(gas);
                }
                colods.Add(coloda);

            }
            count.DisplayedString = count_of_cards.ToString();
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
            if (map[0].sprite.Scale.X>=0.5 & e.Delta<0 | e.Delta>0)
            {
                foreach (c.Card card in map2)
                    correct_coords_scale(scale, card);
                foreach (c.Card card in map)
                    card.sprite.Scale += scale;
                create_coords_grid();
                if (catchicng != null)
                    catchicng.sprite.Scale += scale;
            }
           
          
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
            if (catchicng == null && e.Button == w.Mouse.Button.Left)
            {
                foreach (c.Card card in colods[player - 1])
                    if (card.sprite.GetGlobalBounds().Contains(e.X, e.Y))
                    {
                        catchicng = card;
                        catchicng.sprite.Scale = map[0].sprite.Scale;
                        catching_pos = card.sprite.Position;
                        if (catchicng as c.Trafic != null)
                        {
                            catchicng.sprite.Scale = new s.Vector2f(1, 1);
                            c.Trafic tr = catchicng as c.Trafic;
                            tr.set_detec_image();
                        }
                        break;
                    }
                if (catchicng == null && billy.GetGlobalBounds().Contains(e.X, e.Y))
                {
                    add_cards_to_colod(1, colods[player - 1], player);
                    increase_player();
                    update_dispayeld_pl_text();
                    --count_of_cards;
                    count.DisplayedString = count_of_cards.ToString();
                }
                    
            }
            else if (e.Button == w.Mouse.Button.Left && catchicng != null)
            {
                double side = 100 * map[0].sprite.Scale.X;
                if (catchicng as c.Doroga != null)
                {
                    catchicng.sprite.Position = convert_coords(catchicng);
                    scg.List<c.Card> prov = map.FindAll(card1 => (card1.sprite.Position.X - catchicng.sprite.Position.X == 0 | card1.sprite.Position.Y - catchicng.sprite.Position.Y == 0) & dlina(catchicng, card1) <= side);
                    bool flag = true;
                    foreach (c.Card card in prov)
                    {
                        //sus.Console.WriteLine("count\n");
                        var XYI = card as c.Doroga;
                        var XYI1 = card as c.GasStation;
                        if (XYI == null)
                            flag = XYI1.can_i_set(catchicng);
                        else
                            flag = XYI.can_i_set(catchicng);
                        if (!flag)
                            break;
                    }
                    if (flag)
                    {
                        //sus.Console.WriteLine("can i  way\n");
                        flag = false;
                        scg.List<c.Card> prov2 = map.FindAll(card1 => (card1.sprite.Position.X - catchicng.sprite.Position.X == 0 | card1.sprite.Position.Y - catchicng.sprite.Position.Y == 0) & card1 as c.Doroga != null & dlina(catchicng, card1) <= side);
                        foreach (c.Card card in prov2)
                        {
                            var dorg = card as c.Doroga;
                            flag = dorg.can_i_way(catchicng as c.Doroga);
                            if (flag)
                                break;
                        }
                    }
                    if (!flag)
                    {
                        catchicng.sprite.Position = catching_pos;
                        catchicng.sprite.Scale = new s.Vector2f(1, 1);
                    }

                    else
                    {
                        map.Add(catchicng);
                        colods[player - 1].Remove(catchicng);
                        set_coords_colod(colods[player - 1]);
                        increase_player();
                        update_dispayeld_pl_text();
                    }
                    catchicng = null;
                }
                if (catchicng as c.GasStation != null)
                {
                    catchicng.sprite.Position = convert_coords(catchicng);
                    scg.List<c.Card> prov = map.FindAll(card1 => (card1.sprite.Position.X - catchicng.sprite.Position.X == 0 | card1.sprite.Position.Y - catchicng.sprite.Position.Y == 0) & (sus.Math.Abs(card1.sprite.Position.X - catchicng.sprite.Position.X) <= side & sus.Math.Abs(card1.sprite.Position.Y - catchicng.sprite.Position.Y) <= side));
                    bool flag = true;
                    foreach (c.Card card in prov)
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
                    if (!flag | prov.Count == 0)
                    {
                        catchicng.sprite.Position = catching_pos;
                        catchicng.sprite.Scale = new s.Vector2f(1, 1);
                    }
                    else
                    {
                        map.Add(catchicng);
                        colods[player - 1].Remove(catchicng);
                        set_coords_colod(colods[player - 1]);
                        increase_player();
                        update_dispayeld_pl_text();
                    }
                    catchicng = null;
                }
                if (catchicng as c.Trafic != null)
                {
                    if (catchicng.sprite.GetGlobalBounds().Contains(e.X, e.Y))
                    {
                        foreach (c.Doroga card in way)
                            card.set_usual_image();
                        way.Clear();
                        c.Trafic tr = catchicng as c.Trafic;
                        tr.set_usual_image();
                        catchicng = null;
                    }
                    else
                    {
                        c.Doroga card = map.Find(card1 => card1.sprite.GetGlobalBounds().Contains(e.X, e.Y)) as c.Doroga;
                        if (way.Contains(card))
                        {
                            int index = way.IndexOf(card);
                            //sus.Console.WriteLine("index = {0}", index);
                            scg.List<c.Doroga> way2 = way.GetRange(index, way.Count - index);
                            foreach (c.Doroga doroga in way2)
                                doroga.set_usual_image();
                            way.RemoveRange(index, way.Count - index);
                        }
                        else if (card != null && is_first(card) && way.Count == 0 && card.is_this_typic())
                        {
                            card.set_detec_image();
                            way.Add(card);
                        }
                        else if (card != null && way.Count > 0 && dlina(card, way[way.Count - 1]) <= side && way[way.Count - 1].can_i_way(card) && card.is_this_typic())
                        {
                            card.set_detec_image();
                            way.Add(card);
                        }
                        else if(textbox1.contains(e.X,e.Y) && way.Count > 1 && is_first(way[way.Count - 1]))
                        {
                            go_the_way();
                            foreach (c.Doroga card1 in way)
                                card1.set_usual_image();
                            way.Clear();
                            colods[player - 1].Remove(catchicng);
                            catchicng = null;
                            increase_player();
                            update_dispayeld_pl_text();
                            set_coords_colod(colods[player - 1]);
                        }
                        else if(card!=null)
                        {
                            clock.Restart();
                        }
                    }
                }
            }
            else if (e.Button == w.Mouse.Button.Right && catchicng != null)
            {
                catchicng.sprite.Rotation += 90;
                if (catchicng.sprite.Rotation == 360)
                    catchicng.sprite.Rotation = 0;
            }
        }
        void Window_KeyPressed(object sender,w.KeyEventArgs e)
        {
            var window = (g.RenderWindow)sender;
            if (e.Code == w.Keyboard.Key.Escape)
                window.Close();
            //sus.Console.WriteLine("keypr");
            else if (catchicng as c.Doroga != null & e.Code == w.Keyboard.Key.R)
            {
                catchicng.sprite.Rotation += 90;
                if (catchicng.sprite.Rotation == 360)
                    catchicng.sprite.Rotation = 0;
                c.Doroga dor = catchicng as c.Doroga;
                dor.rotate();
            }
            else if (e.Code == w.Keyboard.Key.A)
                add_cards_to_colod(1, colods[player - 1], player);
            else if (e.Code == w.Keyboard.Key.T)
            {
                var tr = new c.Trafic
                {
                    sprite = new g.Sprite(new g.Texture(new g.Image("images/car_stream.png"))),
                    player = player
                };
                tr.sprite.Origin = new s.Vector2f(50, 50);
                colods[player - 1].Add(tr);
                set_coords_colod(colods[player - 1]);
            }
            else if (e.Code == w.Keyboard.Key.P && catchicng as c.Doroga != null)
            {
                c.Doroga dorg = catchicng as c.Doroga;
                dorg.set_detec_image();
            }
            else if (e.Code == w.Keyboard.Key.O && catchicng as c.Doroga != null)
            {
                c.Doroga dorg = catchicng as c.Doroga;
                dorg.set_usual_image();
            }
            else if (e.Code == w.Keyboard.Key.V && catchicng as c.Doroga != null)
            {
                c.Doroga dorg = catchicng as c.Doroga;
                //sus.Console.WriteLine("way1 ={0},way2 ={1},way3 ={2},way4 ={3}", dorg.way1, dorg.way2, dorg.way3, dorg.way4);
            }
            else if (e.Code == w.Keyboard.Key.M)
                display_count_of_cards();
            else if (e.Code == w.Keyboard.Key.D)
                draw_grid = !draw_grid;
            else if(e.Code==w.Keyboard.Key.S)
                save();
            
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
                    dorg.sprite.Origin = new s.Vector2f(50, 50);
                    dorg.player = player;
                    dorg.Parse(name);
                    coloda.Add(dorg);
                }
                else
                {
                    var tr = new c.Trafic();
                    tr.sprite = new g.Sprite(new g.Texture(new g.Image(name)));
                    tr.sprite.Origin = new s.Vector2f(50, 50);
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
            //sus.Console.WriteLine("dlina = {0}", dlina);
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
        bool is_first(c.Doroga card)
        {
            bool flag = false;
            float side = 100 * map[0].sprite.Scale.X;
            if (card.way1 == true & map.Find(card1 => card1.sprite.Position == card.sprite.Position + new s.Vector2f(0, -side)) == null)
            {
                flag = true;
                //sus.Console.WriteLine("flag1 = {0}", flag);
            }
            if (card.way2 == true && map.Find(card1 => card1.sprite.Position == card.sprite.Position + new s.Vector2f(side, 0)) == null)
            {
                flag = true;
                //sus.Console.WriteLine("flag2 = {0}", flag);
            }
            if (card.way3 == true && map.Find(card1 => card1.sprite.Position == card.sprite.Position + new s.Vector2f(0, side)) == null)
            {
                flag = true;
                //sus.Console.WriteLine("flag3 = {0}", flag);
            }
            if (card.way4 == true & map.Find(card1 => card1.sprite.Position == card.sprite.Position + new s.Vector2f(-side, 0)) == null)
            {
                flag = true;
                //sus.Console.WriteLine("flag4 = {0}", flag);
            }
            return flag;
        }
        void go_the_way()
        {
            foreach (c.Doroga doroga in way)
                get_some_player_card(doroga);
        }
        void get_some_player_card(c.Doroga doroga)
        {
            double side = 100 * map[0].sprite.Scale.X;
            scg.List<c.GasStation> stations = new scg.List<c.GasStation>();
            foreach (c.Card card in map)
                if (card as c.GasStation != null && dlina(card, doroga) <= side * sus.Math.Sqrt(2))
                    stations.Add(card as c.GasStation);
            //sus.Console.WriteLine("count of stations = {0}", stations.Count);
            if (stations.Count > 1)
            {
                scg.List<c.GasStation> stations_level_3 = stations.FindAll(card1 => card1.level == 3);
                scg.List<c.GasStation> stations_level_2 = stations.FindAll(card1 => card1.level == 2);
                scg.List<c.GasStation> stations_level_1 = stations.FindAll(card1 => card1.level == 1);
                if(stations_level_3.Count == 1)
                    add_cards_to_colod(1, colods[stations_level_3[0].player - 1], stations_level_3[0].player);
                else if(stations_level_3.Count==0 && stations_level_2.Count==1)
                    add_cards_to_colod(1, colods[stations_level_2[0].player - 1], stations_level_2[0].player);
                else if(stations_level_3.Count == 0 && stations_level_2.Count == 0 && stations_level_1.Count==1)
                    add_cards_to_colod(1, colods[stations_level_1[0].player - 1], stations_level_1[0].player);
            }
            else if(stations.Count==1)
                add_cards_to_colod(1, colods[stations[0].player - 1], stations[0].player);
        }
        void increase_player()
        {
            ++player;
            if (player > count_of_players)
                player = 1;
        }
        void update_dispayeld_pl_text()
        {
            text.FillColor = player == 1 ? g.Color.Red : text.FillColor;
            text.FillColor = player == 2 ? g.Color.Blue : text.FillColor;
            text.FillColor = player == 3 ? g.Color.Green : text.FillColor;
            text.FillColor = player == 4 ? g.Color.Yellow : text.FillColor;
            text.DisplayedString = "PLAYER" + player.ToString();
        }
        void display_count_of_cards()
        {
            //int i = 0;
            //foreach (scg.List<c.Card> cards in colods)
            //    sus.Console.WriteLine("PLAYER {0} = {1}", ++i, cards.Count);
            //sus.Console.WriteLine("");
        }
        void win()
        {
            int max = 0, player = 1;
            for (int i = 1; i <= count_of_players; ++i)
            {
                if (colods[i - 1].Count > max)
                {
                    max = colods[i - 1].Count;
                    player = i;
                }
                else if (colods[i - 1].Count == max)
                    player = 0;
            }
            g.Text win_text = new g.Text
            {
                CharacterSize = 64,
                FillColor = new g.Color(g.Color.Red),
                Font = new g.Font("ofont.ru_Impact.ttf"),
                Position = new s.Vector2f(width_screen / 2, height_screen / 2),
                DisplayedString ="PLAYER " + player.ToString() + " WINS!"
            };
            if(player==0)
            {
                win_text.FillColor = g.Color.Black;
                win_text.DisplayedString = "DRAW";
            }
            else if(player!=0)
            {
                win_text.FillColor = player == 1 ?g.Color.Red:g.Color.White;
                win_text.FillColor = player == 2 ? g.Color.Blue : g.Color.White;
                win_text.FillColor = player == 3 ? g.Color.Green : g.Color.White;
                win_text.FillColor = player == 4 ? g.Color.Yellow : g.Color.White;
            }
            win_text.Origin = new s.Vector2f(win_text.GetGlobalBounds().Width / 2f, win_text.GetGlobalBounds().Height / 2f + win_text.CharacterSize / 6f);
            Window.Draw(win_text);
          
        }
        void save()
        {
            
        }
        void load()
        {

        }
    }
    //class Data
    //{
    //    public SaveClass catching { get; set; } = new SaveClass();
    //    float x, y;
    //    scg.List<SaveClass> map = new scg.List<SaveClass>();
    //    scg.List<scg.List<SaveClass>> colods = new scg.List<scg.List<SaveClass>>();
    //    scg.List<SaveClass> way = new scg.List<SaveClass>();
    //    public int count_of_players = 4, size_of_colod = 3, player = 1, count_of_cards = 20;
    //    public Data()
    //    {
    //        x = 0;y = 0;
    //    }
    //    public void save_catching_pos(s.Vector2f pos)
    //    {
    //        x = pos.X;
    //        y = pos.Y;
    //    }
    //    public s.Vector2f get_catching_pos()
    //    {
    //        return new s.Vector2f(x, y);
    //    }
    //    public void save_map(scg.List<c.Card> map1)
    //    {
    //        foreach(c.Card card in map1)
    //        {
    //            SaveClass hoho = new SaveClass();
    //            hoho.save_card(card);
    //            map.Add(hoho);
    //        }
    //    }
    //    public void save_colods(scg.List<scg.List<c.Card>> colods1)
    //    {
    //        foreach(scg.List<c.Card> list in colods1)
    //        {
    //            scg.List<SaveClass> hoho1 = new scg.List<SaveClass>();
    //            foreach(c.Card card in list)
    //            {
    //                SaveClass hoho = new SaveClass();
    //                hoho.save_card(card);
    //                hoho1.Add(hoho);
    //            }
    //            colods.Add(hoho1);
    //        }
    //    }
    //    public void save_way(scg.List<c.Card> way1)
    //    {
    //        foreach(c.Card card in way1)
    //        {
    //            SaveClass save = new SaveClass();
    //            save.save_card(card);
    //            way.Add(save);
    //        }
    //    }
    //    public scg.List<c.Card> get_map()
    //    {
    //        scg.List<c.Card> map1 = new scg.List<c.Card>();
    //        foreach(SaveClass save in map)
    //        {
    //            if(save.code==1)
    //            {
    //                c.Doroga dorg = new c.Doroga();
    //                dorg.load(save);
    //                map1.Add(dorg);
    //            }
    //            else if(save.code==2)
    //            {
    //                c.GasStation gas = new c.GasStation();
    //                gas.load(save);
    //                map1.Add(gas);
    //            }
    //        }
    //        return map1;
    //    }
    //    public scg.List<scg.List<c.Card>> get_colods()
    //    {
    //        scg.List<scg.List<c.Card>> colods1 = new scg.List<scg.List<c.Card>>();
    //        foreach(scg.List<SaveClass> list in colods)
    //        {
    //            scg.List<c.Card> colod = new scg.List<c.Card>();
    //            foreach (SaveClass save in list)
    //            {
    //                if (save.code == 1)
    //                {
    //                    c.Doroga dorg = new c.Doroga();
    //                    dorg.load(save);
    //                    colod.Add(dorg);
    //                }
    //                else if (save.code == 2)
    //                {
    //                    c.GasStation gas = new c.GasStation();
    //                    gas.load(save);
    //                    colod.Add(gas);
    //                }
    //                else if(save.code==3)
    //                {
    //                    c.Trafic trafic = new c.Trafic();
    //                    trafic.load(save);
    //                    colod.Add(trafic);
    //                }
    //            }
    //            colods1.Add(colod);
    //        }
    //        return colods1;
    //    }
    //    public scg.List<c.Card> get_way()
    //    {
    //        scg.List<c.Card> way1 = new scg.List<c.Card>();
    //        foreach(SaveClass save in way)
    //        {
    //            if(save.code == 1)
    //            {
    //                c.Doroga doroga = new c.Doroga();
    //                doroga.load(save);
    //                way1.Add(doroga);
    //            }
    //        }
    //        return way1;
    //    }
    //}
}