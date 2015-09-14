using MS.Model.Kingdom;
using UnityEngine;
using UnityEngine.UI;

namespace MS.Views.UI
{
    public class BuildingSchemeView : View<Model.Kingdom.Building>
    {
        public LocalizedText        NameLabel;
        public Text                 GoldCostLabel;
        public Text                 TurnsToBuildLabel;
        public Image                BackgroundImage;
        public DoubleClickButton    BuildButton;

        // Images for each building
        public Sprite                FarmImage;
        public Sprite                TownHallImage;

        // Events
        public Events.BuildingEvent OnBuild = Events.DefaultAction;

        public override void UpdateView(Building element)
        {
            BindTo(element);

            NameLabel.ID                =   element.Name;
            GoldCostLabel.text          =   element.GoldCost.ToString();
            TurnsToBuildLabel.text      =   GameController.Instance.SelectedCity.CalculateTurnsToProduce(element.ProductionCost).ToString();
            BackgroundImage.sprite      =   SelectImage(element);
            // TODO: Assign background image
            
        }

        protected Sprite SelectImage(Building element)
        {
            Sprite image;

            image = null;

            if (element is TownHall)
            {
                image = TownHallImage;
            }
            else if (element is Farm)
            {
                image = FarmImage;
            }

            return image;
        }

        protected void OnBuildEvent()
        {
            OnBuild(Model);
        }

        protected void Start()
        {
            BuildButton.OnDoubleClick += OnBuildEvent;
        }

        protected void OnDestroy()
        {
            BuildButton.OnDoubleClick -= OnBuildEvent;
        }
    }
}
