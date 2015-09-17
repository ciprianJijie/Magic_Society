using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MS.Managers.UI
{
    public class TileInformationManager : MonoBehaviour
    {
        public GameInputManager InputManager;
        public Controllers.UI.ResourceAmountController ResourceAmountController;

        public void UpdateInfo(int x, int y)
        {
            Model.Tile tile;
            Model.ResourceAdvancedAmount amount;

            tile = Game.Instance.Map.Grid.GetTile(x, y);
            amount = new MS.Model.ResourceAdvancedAmount();

            foreach (var collected in tile.Collect())
            {
                amount.AddAmount(collected);
            }
            ResourceAmountController.ClearViews();
            ResourceAmountController.Show(amount);
        }

        public void Hide()
        {
            ResourceAmountController.ClearViews();
            ResourceAmountController.Hide();
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
