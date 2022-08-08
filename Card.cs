using sus = System;
using s = SFML.System;
using g = SFML.Graphics;
using w = SFML.Window;
namespace Cards
{
    internal class Card : g.Drawable
    {
        
        public g.Sprite sprite { get; set; }
        public int player { get; set; }
        public Card()
        {
            sprite = new g.Sprite();
            player = 0;
        }
        public Card (ref Card card)
        {
            sprite = new g.Sprite(card.sprite.Texture);
            player = card.player;
        }
        virtual public void Parse(string name) { }
        public void Draw(g.RenderTarget target, g.RenderStates states)
        {
            target.Draw(sprite, states);
        }
    }
    internal class Doroga : Card
    {
        bool way1, way2, way3, way4;
        public Doroga() : base() 
        {
            way1 = false;
            way2 = false;
            way3 = false;
            way4 = false;
        }
        public override void Parse(string name)
        {
            way1 = name.Contains("1");
            way2 = name.Contains("2");
            way3 = name.Contains("3");
            way4 = name.Contains("4");
        }
        public void rotate()
        {
            bool buffway = way4 ;
            way4 = way3;
            way3 = way2;
            way2 = way1;
            way1 = buffway;
        }
    }
    internal class GasStation : Card
    {
        int level;
        public GasStation() : base()
        {
            level = 0;
        }
        public override void Parse(string name)
        {
            if (name.Contains("1"))
                level = 1;
            if (name.Contains("2"))
                level = 2;
            if (name.Contains("3"))
                level = 3;
        }
    }
    internal class Trafic : Card
    {
        public Trafic() : base()
        {

        }
        public override void Parse(string name)
        {

        }
    }
}

