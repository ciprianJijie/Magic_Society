using SimpleJSON;

public class Resource : ModelElement
{
    public Resource()
    {

    }

    public override void FromJSON(JSONNode json)
    {
        throw new System.NotImplementedException();
    }

    public override JSONNode ToJSON()
    {
        throw new System.NotImplementedException();
    }

    public string Name;
    public string Description;
}
