using SimpleJson;
using System;

namespace ChromaNoodleConverter
{
    internal class ChromaConverter
    {
        private static int rgbOffset = 2000000000;
        private JSONNode map;
        private JSONNode[] currentColor = new JSONNode[5] { null, null, null, null, null };

        public ChromaConverter(JSONNode inputMap)
        {
            this.map = inputMap;
        }

        public JSONNode start()
        {
            JSONNode newEventArray = new JSONArray();
            foreach (JSONObject mapEvent in map["_events"])
            { 
                if (mapEvent["_type"] == 5 || mapEvent["_type"] == 6 || mapEvent["_type"] == 7 || mapEvent["_type"] == 10 || mapEvent["_type"] == 11) { continue; }
                if (mapEvent["_type"] <= 4 && mapEvent["_value"] >= rgbOffset)
                {
                    currentColor[mapEvent["_type"]] = getRGBColor(mapEvent["_value"]);
                }
                else if (mapEvent["_type"] <= 4 && mapEvent["_value"] > 0)
                {
                    if (currentColor[mapEvent["_type"]] != null)
                    {
                        JSONNode temp = new JSONObject();

                        if (mapEvent["_customData"] != null)
                            temp = mapEvent["_customData"];

                        temp["_color"] = currentColor[mapEvent["_type"]];
                        mapEvent["_customData"] = temp;
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

        private JSONNode getRGBColor(int value)
        {
            int decColor = value - rgbOffset;
            Color color = new Color((double)((decColor >> 16) & 0x0ff) / 255, (double)((decColor >> 8) & 0x0ff) / 255, (double)((decColor) & 0x0ff) / 255);

            return color.ToJSONNode();
        }
    }
}