using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MS.Managers.UI
{
    public class TileInformationManager : MonoBehaviour
    {
        public GameInputManager InputManager;
        public Controllers.UI.ResourceAmountController ResourceAmountController;

        private float width;
        private float height;

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

        protected Vector2 CalculatePosition()
        {
            Vector2 mousePosition;
            Vector2 offset;
            RectTransform rect;

            mousePosition   =   Input.mousePosition;
            rect            =   ResourceAmountController.Holder.gameObject.GetComponent<RectTransform>();
            width           =   Mathf.Abs(rect.rect.width);
            height          =   Mathf.Abs(rect.rect.height);

            if (mousePosition.x < Screen.width / 2f)
            {
                offset.x = - width - 6f;
            }
            else
            {
                offset.x = 6f;
            }

            if (mousePosition.y < Screen.height / 2f)
            {
                offset.y = 6f;
            }
            else
            {
                offset.y = - height - 6f;
            }

            return new Vector2(mousePosition.x + offset.x, mousePosition.y + offset.y);
        }

        protected void Start()
        {
            width   =   Mathf.Abs(this.gameObject.GetComponent<RectTransform>().rect.width);
            height  =   Mathf.Abs(this.gameObject.GetComponent<RectTransform>().rect.height);

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
