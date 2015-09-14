using SimpleJSON;

namespace MS
{
    public abstract class ModelElement : IParseable
    {
        public string Name;

        public virtual void FromJSON(JSONNode json)
        {
            Name = json["name"];
        }

        public virtual JSONNode ToJSON()
        {
            JSONClass root;

            root = new JSONClass();

            root.Add("name", Name);

            return root;
        }

        public static T FromJSON<T>(JSONNode json) where T: ModelElement, new()
        {
            var element = new T();

            element.FromJSON(json);

            return element;
        }
    }
}
