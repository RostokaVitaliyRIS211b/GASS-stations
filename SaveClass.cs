using System.Xml.Serialization;
using c = Cards;
using System.Collections.Generic;
namespace Save
{
    public class SaveWindow
    {
        public SaveCard catching;
        public float x, y;
        public int count_of_players, size_of_colod , player , count_of_cards;
        [XmlArrayItem]
        public List<SaveCard> map;
        [XmlArrayItem]
        public List<List<SaveCard>> colods;
        [XmlArrayItem]
        public List<SaveCard> way;
        public SaveWindow()
        {
            way = new();
            colods = new();
            map = new();
            x = 0;
            y = 0;
            catching = new();
            count_of_players = 0;
            size_of_colod = 0;
            player = 0;
            count_of_cards = 0;
        }
    }
    public class SaveCard
    {
        public float x, y;
        public int player { get; set; }

        public bool way1 = false, way2 = false, way3 = false, way4 = false;
        public int level { get; set; }
        public int color { get; set; }
        public int code { get; set; }
        public SaveCard()
        {
            level = 0;
            x = 0;
            y = 0;
            code = 0;
        }
        public void save_card(c.Card card)
        {
            if(card as c.Doroga!=null)
            {
                c.Doroga gay = card as c.Doroga;
                x = gay.sprite.Position.X;
                y = gay.sprite.Position.Y;
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
                x = gay.sprite.Position.X;
                y = gay.sprite.Position.Y;
                player = gay.player;
                level = gay.level;
                code = 2;
            }
            else if(card as c.Trafic!=null)
            {
                c.Trafic gay = card as c.Trafic;
                x = gay.sprite.Position.X;
                y = gay.sprite.Position.Y;
                player = gay.player;
                code = 3;
            }
        }
    }
}

