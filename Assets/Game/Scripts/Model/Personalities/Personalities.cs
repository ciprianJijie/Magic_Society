using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

namespace MS.Model
{
    public class Personalities : ModelElement, IEnumerable<Personality>
    {
        public static readonly int STARTING_PERSONALITIES = 4;

        protected List<Personality>     m_Personalities;
        protected List<Portrait>        m_Portraits;

        public Personalities()
        {
            m_Personalities =   new List<Personality>();
            m_Portraits     =   new List<Portrait>();

            JSONNode    root;
            Portrait portrait;

            root = Path.FileToJSON(Path.ToData("Portraits.json"));

            foreach (JSONNode node in root.AsArray)
            {
                portrait = new Portrait();

                portrait.FromJSON(node);
                m_Portraits.Add(portrait);
            }
        }

        public Personality CreateRandom(Player Owner)
        {
            string                  name;
            Personality.EGender     gender;
            Personality             personality;

            gender      =   Tools.DiceBag.Roll(1, 20, 0) <= 10 ? Personality.EGender.Male : Personality.EGender.Female;
            name        =   gender == Personality.EGender.Male ? Generators.NameGenerator.RandomMaleName() : Generators.NameGenerator.RandomFemaleName();
            personality =   new Personality();

            personality.Name                =   name;
            personality.Gender              =   gender;
            personality.Owner               =   Owner;
            personality.Strength.Score      =   Tools.DiceBag.RollAndDiscardLowers(3, 6, 0);
            personality.Dexterity.Score     =   Tools.DiceBag.RollAndDiscardLowers(3, 6, 0);
            personality.Constitution.Score  =   Tools.DiceBag.RollAndDiscardLowers(3, 6, 0);
            personality.Intelligence.Score  =   Tools.DiceBag.RollAndDiscardLowers(3, 6, 0);
            personality.Wisdom.Score        =   Tools.DiceBag.RollAndDiscardLowers(3, 6, 0);
            personality.Charisma.Score      =   Tools.DiceBag.RollAndDiscardLowers(3, 6, 0);

            m_Personalities.Add(personality);

            return personality;
        }

        public Personality Brew(Personality father, Personality mother, Player Owner)
        {
            throw new NotImplementedException();
        }

        public Personality Find(string name)
        {
            return m_Personalities.Find(personality => personality.Name == name);
        }

        public IEnumerator<Personality> GetEnumerator()
        {
            return m_Personalities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Personalities.GetEnumerator();
        }
    }
}
