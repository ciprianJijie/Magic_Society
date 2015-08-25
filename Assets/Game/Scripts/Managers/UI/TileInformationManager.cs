using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MS.Managers.UI
{
    public class TileInformationManager : MonoBehaviour
    {
        public GameInputManager InputManager;

        public MS.Controllers.UI.RepeatableIcon FoodIcons;
        public MS.Controllers.UI.RepeatableIcon WoodIcons;
        public MS.Controllers.UI.RepeatableIcon IronIcons;
        public MS.Controllers.UI.RepeatableIcon GoldIcons;
        public MS.Controllers.UI.RepeatableIcon ManaIcons;

        public void UpdateInfo(int x, int y)
        {
            int     food;
            int     wood;
            int     iron;
            int     gold;
            int     mana;

            food = GameController.Instance.Game.Resources.CalculateFoodProduction(x, y);
            wood = GameController.Instance.Game.Resources.CalculateWoodProduction(x, y);
            iron = GameController.Instance.Game.Resources.CalculateIronProduction(x, y);
            gold = GameController.Instance.Game.Resources.CalculateGoldProduction(x, y);
            mana = GameController.Instance.Game.Resources.CalculateManaProduction(x, y);

            FoodIcons.UpdateIcons(food);
            WoodIcons.UpdateIcons(wood);
            IronIcons.UpdateIcons(iron);
            GoldIcons.UpdateIcons(gold);
            ManaIcons.UpdateIcons(mana);

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
