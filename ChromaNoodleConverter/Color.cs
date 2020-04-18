using SimpleJson;
using System;

namespace ChromaNoodleConverter
{
    public struct Color
    {
        private double r;
        private double g;
        private double b;

        public Color(double r, double g, double b)
        {
            this.r = Math.Round(r, 3);
            this.g = Math.Round(g, 3);
            this.b = Math.Round(b, 3);
        }

        public JSONNode ToJSONNode()
        {
            JSONArray obj = new JSONArray();
            obj.Add(r);
            obj.Add(g);
            obj.Add(b);
            return obj;
        }
    }
}