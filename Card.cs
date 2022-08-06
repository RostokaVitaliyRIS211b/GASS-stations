using sus = System;
using s = SFML.System;
using g = SFML.Graphics;
using w = SFML.Window;
namespace Cards
{
    internal class Card:g.Drawable
    {
        g.Sprite sprite;
        int player;
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
        public void Draw(g.RenderTarget target, g.RenderStates states)
        {
            target.Draw(sprite, states);
        }
    }
    internal class Doroga : Card
    {

    }
    internal class GasStation : Card
    {

    }
    internal class Trafic : Card
    {

    }
}

