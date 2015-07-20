using SimpleJSON;

namespace MS
{
	public class Tile : ModelElement
	{
		public Tile()
		{
            Status 		= 	EStatus.Available;
            Visibility 	= 	EVisibility.Visible;
            Type 		= 	EType.Fertile;
            Surface 	= 	ESurface.Prairie;
        }

		public override void FromJSON(JSONNode json)
        {
            Status 		= 	EnumUtils.ParseEnum<EStatus>(json["status"]);
            Visibility 	= 	EnumUtils.ParseEnum<EVisibility>(json["visibility"]);
            Type 		= 	EnumUtils.ParseEnum<EType>(json["type"]);
            Surface 	= 	EnumUtils.ParseEnum<ESurface>(json["surface"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = JSON.Parse("{ }");

            json["status"] 		= 	Status.ToString();
            json["visibility"] 	= 	Visibility.ToString();
            json["type"] 		= 	Type.ToString();
            json["surface"] 	= 	Surface.ToString();

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
        public enum EType
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
            Water,
            Vulcan
        }

        public      EStatus         		Status;
        public      EVisibility     		Visibility;
        public      EType           		Type;
        public      ESurface        		Surface;
    }
}
