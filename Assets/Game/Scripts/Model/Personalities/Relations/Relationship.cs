
namespace MS.Model
{
    public class Relationship : ModelElement
    {
        public enum EType { Friendship, Family, None }

        public Personality Source;
        public Personality Target;

        public int Rate;
    }
}
