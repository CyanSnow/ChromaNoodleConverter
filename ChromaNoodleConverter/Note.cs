using SimpleJson;

namespace ChromaNoodleConverter
{
    public struct Note
    {
        private double _time;
        private double _lineIndex;
        private double _lineLayer;
        private double _type;
        private double _cutDirection;
        private JSONArray NE_position;
        private double? NE_cutDirection;

        public Note(JSONNode note, JSONArray NE_position, double? NE_cutDirection)
        {
            this._time = note["_time"];
            this._lineIndex = note["_lineIndex"];
            this._lineLayer = note["_lineLayer"];
            this._type = note["_type"];
            this._cutDirection = note["_cutDirection"];
            this.NE_position = NE_position;
            this.NE_cutDirection = NE_cutDirection;
        }

        public JSONNode ToJSONNode()
        {
            JSONObject obj = new JSONObject();
            obj.Add("_time", _time);
            obj.Add("_lineIndex", _lineIndex);
            obj.Add("_lineLayer", _lineLayer);
            obj.Add("_type", _type);
            obj.Add("_cutDirection", _cutDirection);

            JSONObject customData = new JSONObject();
            customData.Add("_position", NE_position);
            if (NE_cutDirection != null)
                customData.Add("_cutDirection", NE_cutDirection);
            obj.Add("_customData", customData);

            return obj;
        }
    }
}

//{ "_time":17.5,"_lineIndex":-5883,"_lineLayer":13398,"_type":0,"_cutDirection":666,"_customData":{ "_position":[-1,0],"_cutDirection":0}}