
namespace MS
{
	public class Tile : ModelElement
	{
        public enum EStatus
        {
            Available,      // Player can interact with it normally
            Disabled,       // Can't be interacted with
            Blocked         // Some effect prevents some interactions
        }

        public enum EVisibility
        {
            Visible,        // The tile is visible under normal circunstances
            Invisible,      // The tile is not visible under normal circunstances
            Blocking        // The tile is visible, but blocks further vision
        }

        public enum EType
        {
            Fertile,
            Barren,
            Desert,
            Frozen,
            Volcanic
        }

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
        public      CollectableResource     Resource;
        public      Building        		Building;

		public override void FromJSON(SimpleJSON.JSONNode node)
        {
			switch (node["status"])
			{
				case "Available": this.Status = EStatus.Available; break;
				case "Disabled": this.Status = EStatus.Disabled; break;
				case "Blocked": this.Status = EStatus.Blocked; break;
			}
        }

        public override SimpleJSON.JSONNode ToJSON()
        {
            throw new System.NotImplementedException();
        }
	}
}
