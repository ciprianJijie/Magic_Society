using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

namespace MS.Model
{
    public class ResourceAmount : ModelElement
    {
        public Resource             Resource;
        public int                  Amount;
        public ModelElement         Source;

        public ResourceAmount(Resource resource, int amount, ModelElement source)
        {
            Resource    =   resource;
            Amount      =   amount;
            Source      =   source;
        }

        public override void FromJSON(JSONNode json)
        {
            throw new NotImplementedException();
        }

        public override JSONNode ToJSON()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string sign;

            sign = Amount > 0 ? "+" : "-";

            return string.Format("{0}{1} {2} from {3}", sign, Amount, Resource.Name, Source.Name);
        }
    }
}
