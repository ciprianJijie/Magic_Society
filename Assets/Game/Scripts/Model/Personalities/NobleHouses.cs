using System.Collections.Generic;

namespace MS.Model
{
    public class NobleHouses : ModelElement
    {
        protected List<NobleHouse> m_Houses;

        public NobleHouses()
        {
            m_Houses = new List<NobleHouse>();
        }

        public NobleHouse GenerateRandom(Player owner)
        {
            NobleHouse nobleHouse;

            nobleHouse = new NobleHouse();
            nobleHouse.Name = Generators.NameGenerator.RandomHouseName();
            nobleHouse.Owner = owner;

            foreach (Personality personality in GenerateFamily(nobleHouse))
            {
                nobleHouse.AddMember(personality);
            }

            m_Houses.Add(nobleHouse);

            return nobleHouse;
        }

        public IEnumerable<Personality> GenerateFamily(NobleHouse nobleHouse)
        {
            Personality father;
            Personality mother;
            Personality child;
            int         childCount;

            father              =   Game.Instance.Personalities.CreateRandom(nobleHouse);
            mother              =   Game.Instance.Personalities.CreateRandom(nobleHouse);
            father.Partner      =   mother;
            mother.Partner      =   father;

            yield return father;
            yield return mother;

            childCount = UnityEngine.Random.Range(0, 4);

            for (int i = 0; i < childCount; i++)
            {
                child = Game.Instance.Personalities.Brew(father, mother, nobleHouse);

                yield return child;
            }
        }
    }
}
