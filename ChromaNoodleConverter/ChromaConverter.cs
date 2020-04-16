using SimpleJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChromaNoodleConverter
{
    class ChromaConverter
    {
        private static int rgbOffset = 2000000000;
        private JSONNode map;

        public ChromaConverter(JSONNode inputMap)
        {
            this.map = inputMap;
        }


        public JSONNode start()
        {
            JSONNode newEventObject = new JSONArray();
            foreach (JSONObject mapEvent in map["_events"])
            {
                if (mapEvent["_value"] >= rgbOffset)
                {
                    newEventObject.Add(convertRGBLight(mapEvent));
                }
                else
                {
                    newEventObject.Add(mapEvent);
                }
            }

            map["_events"] = newEventObject;

            return map;
        }


        private JSONObject convertRGBLight(JSONObject mapEvent)
        {
            double decColor = mapEvent["_value"].AsDouble - rgbOffset,
                r = Math.Floor(decColor / (256 * 256)) / 255,
                g = (Math.Floor(decColor / 256) % 256) / 255,
                b = (decColor % 256) / 255;
            if (mapEvent["_customData"] == null)
            {
                JSONNode tempObject = new JSONObject();
                tempObject.Add("_color", r + "," + g + "," + b);
                mapEvent.Add("_customData", tempObject);
            }
            else
            {
                mapEvent["_customData"].Add( "_color", r + "," + g + "," + b);
            }

            return mapEvent;
        }


    }
}
