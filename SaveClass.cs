using g = SFML.Graphics;
using s = SFML.System;
using sus = System;
using c = Cards;
namespace Save
{
    [sus.Serializable]
    internal class SaveClass
    {
        public g.Sprite sprite { get; set; }
        public int player { get; set; }

        public bool way1 = false, way2 = false, way3 = false, way4 = false;
        public int level { get; set; } = 0;
        public int code { get; set; }
        public SaveClass()
        {
            code = 0;
        }
        public void save_card(c.Card card)
        {
            if(card as c.Doroga!=null)
            {
                c.Doroga gay = card as c.Doroga;
                sprite = gay.sprite;
                player = gay.player;
                way1 = gay.way1;
                way2 = gay.way2;
                way3 = gay.way3;
                way4 = gay.way4;
                code = 1;
            }
            else if(card as c.GasStation!=null)
            {
                c.GasStation gay = card as c.GasStation;
                sprite = gay.sprite;
                player = gay.player;
                level = gay.level;
                code = 2;
            }
            else if(card as c.Trafic!=null)
            {
                c.Trafic trafic = card as c.Trafic;
                sprite = trafic.sprite;
                player = trafic.player;
                code = 3;
            }
        }
    }
}
