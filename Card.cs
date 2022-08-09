using g = SFML.Graphics;
using sus = System;
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
        virtual public bool can_i_set (Card card) { return true; }
        public void Draw(g.RenderTarget target, g.RenderStates states)
        {
            target.Draw(sprite, states);
        }
    }
    internal class Doroga : Card
    {
        public bool way1, way2, way3, way4;
        public Doroga() : base() 
        {
            way1 = false;
            way2 = false;
            way3 = false;
            way4 = false;
        }
        public override bool can_i_set(Card card)
        {
            bool flag = true;
            if(card as Doroga!=null)
            {
                var dorg = card as Doroga;
                if (dorg.sprite.Position.X - sprite.Position.X > 0 )
                {
                    flag = way2 == dorg.way4;
                    //sus.Console.WriteLine("first {0}", flag);
                }
                    
                if (dorg.sprite.Position.X - sprite.Position.X < 0 )
                {
                    flag = way4 == dorg.way2;
                    //sus.Console.WriteLine("second {0}", flag);
                }

                if (dorg.sprite.Position.Y - sprite.Position.Y > 0)
                { 
                    flag = way3 == dorg.way1;
                    //sus.Console.WriteLine("thirst {0}", flag);
                }
                if (dorg.sprite.Position.Y - sprite.Position.Y < 0 )
                {
                    flag = way1 == dorg.way3;
                    //sus.Console.WriteLine("fourust {0}", flag);
                }
                    
            }
            else
            {
                if (card.sprite.Position.X - sprite.Position.X > 0)
                    flag = !way2;
                if (card.sprite.Position.X - sprite.Position.X < 0)
                    flag = !way4;
                if (card.sprite.Position.Y - sprite.Position.Y > 0)
                    flag = !way3;
                if (card.sprite.Position.Y - sprite.Position.Y < 0)
                    flag = !way1;
            }
            if (card.sprite.Position.Equals(sprite.Position))
                flag = false;
            return flag;
        }
        public override void Parse(string name)
        {
            way1 = name.Contains("1");
            way2 = name.Contains("2");
            way3 = name.Contains("3");
            way4 = name.Contains("4");
        }
        public bool can_i_way(Doroga card)
        {
            bool flag=false;
            if (card.sprite.Position.X - sprite.Position.X > 0)
            {
                flag = way1 == true & card.way4 == true;
            }

            if (card.sprite.Position.X - sprite.Position.X < 0)
            {
                flag = way4 == true & card.way1 == true;
            }

            if (card.sprite.Position.Y - sprite.Position.Y > 0)
            {
                flag = way3 == true & card.way1 == true;
            }
            if (card.sprite.Position.Y - sprite.Position.Y < 0)
            {
                flag = way1 == true & card.way3 == true;
            }
            //sus.Console.WriteLine("fivest {0}", flag);
            return flag;
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
        public override bool can_i_set(Card card)
        {
            bool flag = true;
            if(card as Doroga!=null)
            {
                var dorg = card as Doroga;
                if (dorg.sprite.Position.X - sprite.Position.X > 0)
                {
                    flag = !dorg.way4;
                    //sus.Console.WriteLine("first {0}", flag);
                }

                if (dorg.sprite.Position.X - sprite.Position.X < 0)
                {
                    flag = !dorg.way2;
                    //sus.Console.WriteLine("second {0}", flag);
                }

                if (dorg.sprite.Position.Y - sprite.Position.Y > 0)
                {
                    flag = !dorg.way1;
                    //sus.Console.WriteLine("thirst {0}", flag);
                }
                if (dorg.sprite.Position.Y - sprite.Position.Y < 0)
                {
                    flag = !dorg.way3;
                    //sus.Console.WriteLine("fourust {0}", flag);
                }
            }
            else
                flag = true;
            if (card.sprite.Position.Equals(sprite.Position))
                flag = false;
            return flag;
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

