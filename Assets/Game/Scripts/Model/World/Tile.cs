using UnityEngine;
using SimpleJSON;

namespace MS
{
	public class Tile : ModelElement
	{
		public Tile()
		{
            Status 		= 	EStatus.Available;
            Visibility 	= 	EVisibility.Visible;
            Type 		= 	ETerrain.Fertile;
            Surface 	= 	ESurface.Prairie;
            Height 		= 	0;
        }

		public override void FromJSON(JSONNode json)
        {
            Status 		= 	EnumUtils.ParseEnum<EStatus>(json["status"]);
            Visibility 	= 	EnumUtils.ParseEnum<EVisibility>(json["visibility"]);
            Type 		= 	EnumUtils.ParseEnum<ETerrain>(json["type"]);
            Surface 	= 	EnumUtils.ParseEnum<ESurface>(json["surface"]);
			Height 		=	json["height"].AsInt;
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = JSON.Parse(Resources.Load<TextAsset>("Data/JSON/Templates/Tile").text);

            json["status"] 			= 	Status.ToString();
            json["visibility"] 		= 	Visibility.ToString();
            json["type"] 			= 	Type.ToString();
            json["surface"] 		= 	Surface.ToString();
            json["height"].AsInt 	= 	Height;

            return json;
        }

        [System.Serializable]
        public enum EStatus
        {
            Available,      // Player can interact with it normally
            Disabled,       // Can't be interacted with
            Blocked         // Some effect prevents some interactions
        }

        [System.Serializable]
        public enum EVisibility
        {
            Visible,        // The tile is visible under normal circunstances
            Invisible,      // The tile is not visible under normal circunstances
            Blocking        // The tile is visible, but blocks further vision
        }

        [System.Serializable]
        public enum ETerrain
        {
            Fertile,
            Barren,
            Desert,
            Frozen,
            Volcanic
        }

        [System.Serializable]
        public enum ESurface
        {
            Prairie,
            Forest,
            Mountain,
            Water
        }

        public EStatus      Status;
        public EVisibility  Visibility;
        public ETerrain     Type;
        public ESurface     Surface;
        public int 			Height;
    }
}
