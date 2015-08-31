using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MS.Managers.UI
{
    public class TileInformationManager : MonoBehaviour
    {
        public GameInputManager InputManager;

        public MS.Controllers.UI.RepeatableIcon FoodIcons;

        public void UpdateInfo(int x, int y)
        {
            int     food;

            food = GameController.Instance.Game.Resources.CalculateFoodProduction(x, y);

            FoodIcons.UpdateIcons(food);

            Show();
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        protected void Start()
        {
            InputManager.OnTileHover        +=  UpdateInfo;
            InputManager.OnTileHoverEnds    +=  Hide;

            Hide();
        }

        protected void OnDestroy()
        {
            InputManager.OnTileHover        -=  UpdateInfo;
            InputManager.OnTileHoverEnds    -=  Hide;
        }
    }    
}
