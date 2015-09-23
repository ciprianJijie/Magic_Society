using System;
using UnityEngine;
using UnityEngine.UI;
using MS.Managers.UI;

namespace MS.Views.UI
{
    public class BuildingQueueItemView : View<Model.Kingdom.BuildingQueueItem>
    {
        public Image                Image;
        public Text                 TurnsLeftLabel;
        public DoubleClickButton    CancelButton;

        public Sprite               AquaductImage;
        public Sprite               BarracksImage;
        public Sprite               FarmImage;
        public Sprite               TownHallImage;

        public Events.BuildingQueueItemEvent OnCancel = Events.DefaultAction;

        public override void UpdateView(MS.Model.Kingdom.BuildingQueueItem element)
        {
            float   d;
            int     turnsLeft;
            int productionPerTurn;

            Image.sprite            =   SelectSprite(element.Building);
            productionPerTurn       =   GameController.Instance.SelectedCity.CollectProduction().GetTotalAmount();
            d                       =   (float)element.ProductionUntilCompletion / (float)productionPerTurn;
            turnsLeft               =   Mathf.CeilToInt(d);
            TurnsLeftLabel.text     =   turnsLeft.ToString();

            m_Model = element;
        }

        protected Sprite SelectSprite(Model.Kingdom.Building building)
        {
            if (building is Model.Kingdom.Aquaduct)
            {
                return AquaductImage;
            }
            else if (building is Model.Kingdom.Barracks)
            {
                return BarracksImage;
            }
            else if (building is Model.Kingdom.Farm)
            {
                return FarmImage;
            }
            else if (building is Model.Kingdom.TownHall)
            {
                return TownHallImage;
            }

            return null;
        }

        protected void OnCancelEvent()
        {
            OnCancel(Model);
        }

        protected void Start()
        {
            CancelButton.OnDoubleClick += OnCancelEvent;
        }

        protected void OnDestroy()
        {
            CancelButton.OnDoubleClick -= OnCancelEvent;
        }
    }
}

