using SimpleJSON;

namespace MS
{
    public abstract class ModelElement : IParseable
    {
        public abstract void FromJSON(JSONNode json);
        public abstract JSONNode ToJSON();

        public static T FromJSON<T>(JSONNode json) where T: ModelElement, new()
        {
            var element = new T();

            element.FromJSON(json);

            return element;
        }
    }
}
