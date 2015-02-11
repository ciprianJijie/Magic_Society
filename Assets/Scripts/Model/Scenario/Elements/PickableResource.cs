using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public class PickableResource : MapElement
    {
        public PickableResource(int x, int y, ResourceStorage storage)
        : base(x, y)
        {
            Storage = storage;
        }

        public PickableResource(JSONNode json)
        : base(json["location"]["x"].AsInt, json["location"]["y"].AsInt)
        {
            Storage = new ResourceStorage(json["storage"]);
        }

        public override void FromJSON(JSONNode json)
        {
            this.Location   =   new Vector2(json["location"]["x"].AsInt, json["location"]["y"].AsInt);
            this.Storage    =   new ResourceStorage(json["storage"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = new JSONNode();

            json["location"]["x"]   =   this.Location.x.ToString();
            json["location"]["y"]   =   this.Location.y.ToString();
            json["storage"]         =   Storage.ToJSON();

            return json;
        }

        public ResourceStorage Storage;
    }
}
