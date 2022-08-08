﻿using g = SFML.Graphics;
using w = SFML.Window;
using s = SFML.System;
using sus = System;
namespace Lines
{
    internal class Line:g.Drawable
    {
        public s.Vector2f point_one { get; set; }
        public s.Vector2f point_two { get; set; }
        public g.Color color { get; set; }
        public Line()
        {

        }
        public Line(s.Vector2f point_one, s.Vector2f point_two, g.Color color)
        {
            this.point_one = point_one;
            this.point_two = point_two;
            this.color = color;
        }

        public void Draw(g.RenderTarget target, g.RenderStates states)
        {
            g.Vertex one = new g.Vertex(((s.Vector2f)point_one), color);
            g.Vertex two = new g.Vertex(((s.Vector2f)point_two), color);
            g.Vertex[] lines1 = {one,two}; 
            states = g.RenderStates.Default;
            target.Draw(lines1,g.PrimitiveType.Lines);
        }

    }
}
