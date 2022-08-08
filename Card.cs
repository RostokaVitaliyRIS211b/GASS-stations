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

