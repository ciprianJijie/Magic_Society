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
        protected List<Portrait>        m_MalePortraits;
        protected List<Portrait>        m_FemalePortraits;
        protected List<Trait>           m_Traits;

        public Personalities()
        {
            m_Personalities     =   new List<Personality>();
            m_MalePortraits     =   new List<Portrait>();
            m_FemalePortraits   =   new List<Portrait>();
            m_Traits            =   new List<Trait>();

            JSONNode    root;
            Portrait    portrait;
            Trait       trait;

            root = Path.FileToJSON(Path.ToData("Portraits.json"));

            foreach (JSONNode node in root["portraits"].AsArray)
            {
                portrait = new Portrait();

                portrait.FromJSON(node);

                if (portrait.Gender == Personality.EGender.Male)
                {
                    m_MalePortraits.Add(portrait);
                }
                else
                {
                    m_FemalePortraits.Add(portrait);
                }
            }

            root = Path.FileToJSON(Path.ToData("Traits.json"));

            foreach (JSONNode node in root["traits"].AsArray)
            {
                trait = new Trait();

                trait.FromJSON(node);
                m_Traits.Add(trait);
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

            personality.Name                    =   name;
            personality.Gender                  =   gender;
            personality.Age                     =   UnityEngine.Random.Range(10, 25);
            personality.Portrait                =   RandomPortrait(personality.Gender);
            personality.Owner                   =   Owner;
            personality.BaseVigor.Score         =   Tools.DiceBag.RollAndDiscardLowers(3, 6, 0);
            personality.BaseManagement.Score    =   Tools.DiceBag.RollAndDiscardLowers(3, 6, 0);
            personality.BaseIntrigue.Score      =   Tools.DiceBag.RollAndDiscardLowers(3, 6, 0);
            personality.BaseCharisma.Score      =   Tools.DiceBag.RollAndDiscardLowers(3, 6, 0);
            personality.BaseMorality.Score      =   Tools.DiceBag.RollAndDiscardLowers(3, 6, 0);

            // TODO: Temporal for testing
            personality.AddPersonalityTrait(RandomTrait());

            m_Personalities.Add(personality);

            UnityEngine.Debug.Log("Created " + personality);

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

        public Portrait RandomPortrait(Personality.EGender gender)
        {
            int randomIndex;

            if (gender == Personality.EGender.Male)
            {
                randomIndex = UnityEngine.Random.Range(0, m_MalePortraits.Count);
                return m_MalePortraits[randomIndex];
            }
            else
            {
                randomIndex = UnityEngine.Random.Range(0, m_FemalePortraits.Count);
                return m_FemalePortraits[randomIndex];
            }
        }

        public Trait RandomTrait()
        {
            return m_Traits[UnityEngine.Random.Range(0, m_Traits.Count)];
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
