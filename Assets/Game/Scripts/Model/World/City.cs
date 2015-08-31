using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;

namespace MS.Model
{
	public class City : OwnableMapElement
	{
        public string RealName;

        public City()
        {

        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
            RealName = json["real_name"];
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;

            root = base.ToJSON();
            root.Add("real_name", new JSONData(RealName));

            return root;
        }
	}
}
