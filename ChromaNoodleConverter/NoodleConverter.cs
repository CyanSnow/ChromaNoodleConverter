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

            JSONNode newWallArray = new JSONArray();
            foreach (JSONObject mapWall in map["_obstacles"])
            {
                newWallArray.Add(makeNEWall(mapWall));
            }
            map["_obstacles"] = newWallArray;
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
                //fuck man idk just fucking casting everything b/c everything hates me
                _pos[0] = (double)(((double)note["_lineIndex"] - (double)((double)note["_lineIndex"] >= 1000 ? 1000d : -1000d)) / 1000d - 2d);
            }
            else
            {
                _pos[0] = note["_lineIndex"] - 2;
            }

            if (note["_lineLayer"] >= 1000 || note["_lineLayer"] <= -1000)
            {
                _pos[1] = (double)(((double)note["_lineLayer"] - (double)((double)note["_lineLayer"] >= 1000 ? 1000d : -1000d)) / 1000d);
            }
            else
            {
                _pos[1] = note["_lineLayer"];
            }

            if (note["_type"] != 3 && (_pos[0] == null || _pos[0] >= 6 || _pos[0] <= -6 || _pos[1] == null || _pos[1] >= 4 || _pos[1] <= -2))
            {
                Console.WriteLine("Possiable impossible hit detected at: " + note["_time"]);
            }
            return _pos;
        }

        private double? findNENoteDir(JSONNode note)
        {
            if (note["_cutDirection"] >= 1000)
            {
                return 1360d - note["_cutDirection"];
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

        private JSONNode makeNEWall(JSONNode wall)
        {
            double NE_positionX = findStartRow(wall["_lineIndex"]);
            double NE_positionY = findStartHeight(wall["_type"]);

            double NE_scaleX = findWidth(wall["_width"]);
            double NE_scaleY = findHeight(wall["_type"]);

            return new Wall(wall, NE_positionX, NE_positionY, NE_scaleX, NE_scaleY).ToJSONNode();
        }

        private double findHeight(double wallType)
        {
            double wallHeight = wallType;
            if (wallType < 1000)
            {
            }
            else if (wallType < 4000)
            {
                wallHeight = (wallHeight - 1000) / (5.0d / 3.0d);
            }
            else
            {
                wallHeight -= 4001d;
                wallHeight /= 1000d;
                wallHeight = wallHeight / 1000d / (1.0d / 3.0d) * (5.0d / 3.0d);
            }
            return wallHeight;
        }
        private double findStartHeight(double wallType)
        {
            double wallStartHeight = wallType % 1000d;
            wallStartHeight /= 250d;
            wallStartHeight *= (5.0d / 3.0d);
            return wallStartHeight;
        }
        private double findStartRow(double wallLineIndex)
        {
            double wallStartRow = (wallLineIndex - 1000d) / 1000d;
            if (wallLineIndex >= 1000)
            {
                return wallStartRow - 2;
            }
            else
            {
                return wallStartRow;
            }
        }
        private double findWidth(double wallWidth)
        {
            return (wallWidth - 1000) / 1000;
        }


    }
}