using SimpleJson;

namespace ChromaNoodleConverter
{
    public struct Wall
    {
        private double _time;
        private double _lineIndex;
        private double _lineLayer;
        private double _type;
        private double _width;
        private double _duration;

        private double NE_positionX;
        private double NE_positionY;
        private double NE_scaleX;
        private double NE_scaleY;

        public Wall(JSONNode wall, double NE_positionX, double NE_positionY, double NE_scaleX, double NE_scaleY)
        {
            this._time = wall["_time"];
            this._lineIndex = wall["_lineIndex"];
            this._lineLayer = wall["_lineLayer"];
            this._type = wall["_type"];
            this._width = wall["_width"];
            this._duration = wall["_duration"];

            this.NE_positionX = NE_positionX;
            this.NE_positionY = NE_positionY;
            this.NE_scaleX = NE_scaleX;
            this.NE_scaleY = NE_scaleY;
        }

        public JSONNode ToJSONNode()
        {
            JSONObject obj = new JSONObject();
            obj.Add("_time", _time);
            obj.Add("_lineIndex", _lineIndex);
            obj.Add("_lineLayer", _lineLayer);
            obj.Add("_type", _type);
            obj.Add("_width", _width);
            obj.Add("_duration", _duration);

            JSONArray _position = new JSONArray();
            _position[0] = NE_positionX;
            _position[1] = NE_positionY;

            JSONArray _scale = new JSONArray();
            _scale[0] = NE_scaleX;
            _scale[1] = NE_scaleY;

            JSONObject _customData = new JSONObject();
            _customData.Add("_position", _position);
            _customData.Add("_scale", _scale);

            obj.Add("_customData", _customData);

            return obj;
        }
    }
}

//{ "_time":51.5,"_lineIndex":0,"_type":0,"_duration":0.75,"_width":0,
//"_customData":{ 
//"_position":[14,-0.25],"_scale":[0.1,0.2],
//"_localRotation":[0,0,0],
//"_rotation":341,
//"_color":[1,0,0,1]}},