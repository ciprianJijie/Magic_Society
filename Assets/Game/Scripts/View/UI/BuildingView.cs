using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MS.Model.Kingdom;
using UnityEngine;
using UnityEngine.UI;

namespace MS.Views.UI
{
    public class BuildingView : View<Model.Kingdom.Building>
    {
        public Image Image;

        public Sprite FarmSprite;
        public Sprite TownHallSprite;

        public override void UpdateView(Building element)
        {
            BindTo(element);

            Image.sprite = SelectImage(element);
        }

        public Sprite SelectImage(Building element)
        {
            if (element is Farm)
            {
                return FarmSprite;
            }
            else if (element is TownHall)
            {
                return TownHallSprite;
            }

            return null;
        }
    }
}
