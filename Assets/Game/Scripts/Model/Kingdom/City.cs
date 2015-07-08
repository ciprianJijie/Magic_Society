using SimpleJSON;

namespace MS
{
    public class City : ModelElement
    {
        public City()
        {
            Name        =   "No name";
            Citizens    =   0;
            CitizensMax =   0;
        }

        public override void FromJSON(JSONNode json)
        {
            throw new System.NotImplementedException();
        }

        public override JSONNode ToJSON()
        {
            throw new System.NotImplementedException();
        }

        public string   Name;
        public int      Citizens;

        protected int   CitizensMax;
    }    
}
