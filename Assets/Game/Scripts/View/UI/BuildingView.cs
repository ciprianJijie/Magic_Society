using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MS.Model.Kingdom;
using UnityEngine.UI;

namespace MS.Views.UI
{
    public class BuildingView : View<Model.Kingdom.Building>
    {
        public Text NameLabel;
        public Text DescriptionLabel;

        public override void UpdateView(Building element)
        {
            BindTo(element);

            NameLabel.text          =   element.Name;
            DescriptionLabel.text   =   element.Description;
        }
    }
}
