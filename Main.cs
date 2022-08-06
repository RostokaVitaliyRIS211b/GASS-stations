using sus = System;
using s = SFML.System;
using g = SFML.Graphics;
using w = SFML.Window;
using gas = Cards;
namespace Main
{
    class ProgramMain
    {
        public static int Main()
        {
            MainWindow Game= new MainWindow();
            Game.GameLoop();
            return 0;
        }
    }
    class MainWindow
    {
        int width_screen = 700, height_screen = 700;
        g.RenderWindow Window;
        public MainWindow()
        {
            Window = new g.RenderWindow(new w.VideoMode((uint)width_screen,(uint)height_screen),"KING OF GAS STATIONS");
        }
        public void GameLoop()
        {
            while(Window.IsOpen)
            {
                Window.Clear(g.Color.White);
                Window.DispatchEvents();
                Window.Display();
            }
        }
    }
}