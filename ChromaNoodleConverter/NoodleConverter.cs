using SimpleJson;
using System;

namespace ChromaNoodleConverter
{
    internal class NoodleConverter
    {
        private JSONNode map;

        public NoodleConverter(JSONNode inputMap)
        {
            this.map = inputMap;
        }

        public JSONNode start()
        {
            JSONNode newNoteArray = new JSONArray();
            foreach (JSONObject mapNote in map["_notes"])
            {
                newNoteArray.Add(makeNENote(mapNote));
            }
            map["_notes"] = newNoteArray;
            return map;
        }

        private JSONNode makeNENote(JSONNode note)
        {
            JSONArray pos = findNENotePos(note);
            double? dir = findNENoteDir(note);
            return new Note(note, pos, dir).ToJSONNode();
        }

        private JSONArray findNENotePos(JSONNode note)
        {
            JSONArray _pos = new JSONArray();
            if (note["_lineIndex"] >= 1000 || note["_lineIndex"] <= -1000)
            {
                _pos[0] = (note["_lineIndex"] - (note["_lineIndex"] >= 1000 ? 1000 : -1000)) / 1000 - 2;
            }
            else
            {
                _pos[0] = note["_lineIndex"] - 2;
            }

            if (note["_lineLayer"] >= 1000 || note["_lineLayer"] <= -1000)
            {
                _pos[1] = (note["_lineLayer"] - (note["_lineLayer"] >= 1000 ? 1000 : -1000)) / 1000;
            }
            else
            {
                _pos[1] = note["_lineLayer"];
            }

            if (_pos[0] >= 6 || _pos[0] <= -6 || _pos[1] >= 4 || _pos[1] <= -2)
            {
                Console.WriteLine("Possiable impossible hit detected at: " + note["_time"]);
            }
            return _pos;
        }

        //_cutDirection
        private double? findNENoteDir(JSONNode note)
        {
            if (note["_cutDirection"] >= 1000)
            {
                return 1360 - note["_cutDirection"];
            }
            else
            {
                int cutDir = note["_cutDirection"];
                switch (cutDir)
                {
                    case 0:
                        return 180;

                    case 1:
                        return 0;

                    case 2:
                        return 270;

                    case 3:
                        return 90;

                    case 4:
                        return 225;

                    case 5:
                        return 135;

                    case 6:
                        return 315;

                    case 7:
                        return 45;

                    default:
                        break;
                }
            }

            return null;
        }

        //private JSONNode makeNEWall(JSONNode wall)
        //{
        //    //{ "_time":51.5,"_lineIndex":0,"_type":0,"_duration":0.75,"_width":0,"_customData":{ "_position":[14,-0.25],"_scale":[0.1,0.2],"_localRotation":[0,0,0],"_rotation":341,"_color":[1,0,0,1]}},
        //    return
        //}
    }
}