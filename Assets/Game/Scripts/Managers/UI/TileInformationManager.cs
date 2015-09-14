using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MS.Managers.UI
{
    public class TileInformationManager : MonoBehaviour
    {
        public GameInputManager InputManager;

        public Text                             NameLabel;

        public MS.Controllers.UI.RepeatableIcon FoodIcons;
        public MS.Controllers.UI.RepeatableIcon ProductionIcons;
        public MS.Controllers.UI.RepeatableIcon GoldIcons;
        public MS.Controllers.UI.RepeatableIcon ResearchIcons;

        private float width;
        private float height;

        public void UpdateInfo(int x, int y)
        {
            int                     food;
            int                     production;
            int                     gold;
            int                     research;
            string                  name;
            MS.Model.MapElement     element;

            food        =   GameController.Instance.Game.Resources.CalculateFoodGeneration(x, y);
            production  =   GameController.Instance.Game.Resources.CalculateProductionGeneration(x, y);
            gold        =   GameController.Instance.Game.Resources.CalculateGoldGeneration(x, y);
            research    =   GameController.Instance.Game.Resources.CalculateResearchGeneration(x, y);
            element     =   GameController.Instance.Game.Map.Grid.GetElement(x, y);
            name        =   GameController.Instance.Game.Map.Grid.GetTile(x, y).TerrainType.ToString();

            if (element != null)
            {
                name += " + " + element.Name;
            }

            FoodIcons.UpdateIcons(food);
            ProductionIcons.UpdateIcons(production);
            GoldIcons.UpdateIcons(gold);
            ResearchIcons.UpdateIcons(research);

            NameLabel.text = name;

            Show();
        }

        public void Show()
        {
            this.gameObject.SetActive(true);

            Vector2 mousePosition;
            Vector2 offset;
            RectTransform rect;

            mousePosition   =   Input.mousePosition;
            rect            =   this.gameObject.GetComponent<RectTransform>();
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

            rect.anchoredPosition = new Vector2(mousePosition.x + offset.x, mousePosition.y + offset.y);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
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
