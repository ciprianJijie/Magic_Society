using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;

namespace MS.Model
{
	public class City : OwnableMapElement
	{
        public City()
        {

        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;

            root = base.ToJSON();

            return root;
        }
	}
}
