using SimpleJson;
using System;

namespace ChromaNoodleConverter
{
    internal class ChromaConverter
    {
        private static int rgbOffset = 2000000000;
        private JSONNode map;
        private double[][] currentColor = new double[][] { new double[3] { -1, -1, -1 }, new double[3] { -1, -1, -1 }, new double[3] { -1, -1, -1 }, new double[3] { -1, -1, -1 }, new double[3] { -1, -1, -1 } };

        public ChromaConverter(JSONNode inputMap)
        {
            this.map = inputMap;
        }

        public JSONNode start()
        {
            JSONNode newEventArray = new JSONArray();
            foreach (JSONObject mapEvent in map["_events"])
            {
                if (mapEvent["_value"] >= rgbOffset)
                {
                    currentColor[mapEvent["_type"]] = getRGBColor(mapEvent);
                }
                else if (mapEvent["_type"] <= 4 && mapEvent["_value"] > 0)
                {
                    JSONNode tempObject = new JSONObject();
                    if (currentColor[mapEvent["_type"]][0] != -1)
                    {
                        tempObject.Add("_color", JSONNode.Parse("[" + currentColor[mapEvent["_type"]][0] + "," + currentColor[mapEvent["_type"]][1] + "," + currentColor[mapEvent["_type"]][2] + "]"));
                        mapEvent.Add("_customData", tempObject);
                    }
                    newEventArray.Add(mapEvent);
                }
                else
                {
                    newEventArray.Add(mapEvent);
                }
            }
            map["_events"] = newEventArray;
            return map;
        }

        private double[] getRGBColor(JSONObject mapEvent)
        {
            double decColor = mapEvent["_value"].AsDouble - rgbOffset,
                r = Math.Floor(decColor / (256 * 256)) / 255,
                g = (Math.Floor(decColor / 256) % 256) / 255,
                b = (decColor % 256) / 255;

            double[] color = new double[3] { r, g, b };

            return color;
        }
    }
}