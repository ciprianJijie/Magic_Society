using SimpleJSON;
using UnityEngine;

namespace MS.Model
{
    public class Portrait : ModelElement
    {
        public string               ImagePath;
        public Personality.EGender  Gender;

        public override void FromJSON(JSONNode json)
        {
            ImagePath   =   json["source"];
            Gender      =   EnumUtils.ParseEnum<Personality.EGender>(json["gender"]);
            Name        =   ImagePath.Substring(ImagePath.LastIndexOf("/") + 1, ImagePath.Length - ImagePath.LastIndexOf("."));
        }

        public override JSONNode ToJSON()
        {
            JSONClass root;

            root = new JSONClass();

            root.Add("source", ImagePath);
            root.Add("gender", Gender.ToString());

            return root;
        }
    }
}
