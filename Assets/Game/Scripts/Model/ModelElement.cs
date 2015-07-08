using System;
using SimpleJSON;


public abstract class ModelElement : IParseable
{
    public abstract void FromJSON(JSONNode node);
    public abstract JSONNode ToJSON();
}
