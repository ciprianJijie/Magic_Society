using System;
using SimpleJSON;

namespace MS.Model
{
    public abstract class ModelElement : MS.IParseable
    {
        public abstract void FromJSON(JSONNode node);
        public abstract JSONNode ToJSON();
    }
}

