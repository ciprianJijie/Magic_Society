using UnityEngine;
using MS.ExtensionMethods;

namespace MS.Managers
{
    public class RegionMarkerManager : MonoBehaviour, IEventListener
    {
        public GameInputManager InputManager;
        public Transform        RegionMarker;
        public float            HexagonSize;

		public void OnRegionHover(MS.Model.World.Region region)
		{
			Vector3 worldPosition;

			worldPosition = Hexagon.CubeToWorld(region.CubePosition, HexagonSize).SwappedYZ();

			RegionMarker.position = worldPosition;

			RegionMarker.gameObject.SetActive(true);
		}

		public void OnRegionHoverEnds(MS.Model.World.Region region)
		{
			RegionMarker.gameObject.SetActive(false);
		}

        public void SubscribeToEvents()
        {
            InputManager.OnRegionHover        +=  OnRegionHover;
            InputManager.OnRegionHoverEnds    +=  OnRegionHoverEnds;
        }

        public void UnsubscribeToEvents()
        {
            InputManager.OnRegionHover        -=  OnRegionHover;
            InputManager.OnRegionHoverEnds    -=  OnRegionHoverEnds;
        }

        protected void Start()
        {
            SubscribeToEvents();

            RegionMarker.gameObject.SetActive(false);
        }

        protected void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
