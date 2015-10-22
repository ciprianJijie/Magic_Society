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

        public Personality CreateRandom(NobleHouse house)
        {
            string                  name;
            Personality.EGender     gender;
            Personality             personality;
            int                     traitsCount;

            gender      =   Tools.DiceBag.Roll(1, 20, 0) <= 10 ? Personality.EGender.Male : Personality.EGender.Female;
            name        =   gender == Personality.EGender.Male ? Generators.NameGenerator.RandomMaleName() : Generators.NameGenerator.RandomFemaleName();
            personality =   new Personality();

            personality.Name                    =   name;
            personality.Gender                  =   gender;
            personality.Age                     =   UnityEngine.Random.Range(16, 45);
            personality.Portrait                =   RandomPortrait(personality.Gender);
            personality.Owner                   =   house.Owner;
            personality.BaseVigor.Score         =   Tools.DiceBag.Roll(3, 6, 0);
            personality.BaseManagement.Score    =   Tools.DiceBag.Roll(3, 6, 0);
            personality.BaseIntrigue.Score      =   Tools.DiceBag.Roll(3, 6, 0);
            personality.BaseCharisma.Score      =   Tools.DiceBag.Roll(3, 6, 0);
            personality.BaseMorality.Score      =   Tools.DiceBag.Roll(3, 6, 0);
            traitsCount                         =   UnityEngine.Mathf.FloorToInt(personality.Age / 10.0f) + 2;

            for (int i = 0; i < traitsCount; i++)
            {
                personality.AddPersonalityTrait(RandomTrait());
            }

            personality.ChiefHouse = house;

            m_Personalities.Add(personality);
            
            return personality;
        }

        public Personality Brew(Personality father, Personality mother, NobleHouse house)
        {
            string              name;
            Personality.EGender gender;
            Personality         personality;
            int                 vigorModifier;
            int                 managementModifier;
            int                 intrigueModifier;
            int                 charismaModifier;
            int                 moralityModifier;
            
            gender      =   Tools.DiceBag.Roll(1, 20, 0) <= 10 ? Personality.EGender.Male : Personality.EGender.Female;
            name        =   gender == Personality.EGender.Male ? Generators.NameGenerator.RandomMaleName() : Generators.NameGenerator.RandomFemaleName();
            personality =   new Personality();
            
            if (Tools.DiceBag.Roll(1, 10, 0) > 5)
            {
                vigorModifier = Ability.CalculateModifier(father.Vigor);
            }
            else
            {
                vigorModifier = Ability.CalculateModifier(mother.Vigor);
            }

            if (Tools.DiceBag.Roll(1, 10, 0) > 5)
            {
                managementModifier = Ability.CalculateModifier(father.Management);
            }
            else
            {
                managementModifier = Ability.CalculateModifier(mother.Management);
            }

            if (Tools.DiceBag.Roll(1, 10, 0) > 5)
            {
                intrigueModifier = Ability.CalculateModifier(father.Intrigue);
            }
            else
            {
                intrigueModifier = Ability.CalculateModifier(mother.Intrigue);
            }

            if (Tools.DiceBag.Roll(1, 10, 0) > 5)
            {
                charismaModifier = Ability.CalculateModifier(father.Charisma);
            }
            else
            {
                charismaModifier = Ability.CalculateModifier(mother.Charisma);
            }

            if (Tools.DiceBag.Roll(1, 10, 0) > 5)
            {
                moralityModifier = Ability.CalculateModifier(father.Morality);
            }
            else
            {
                moralityModifier = Ability.CalculateModifier(mother.Morality);
            }

            personality.Name                    =   name;
            personality.Gender                  =   gender;
            personality.Age                     =   0;
            personality.Portrait                =   RandomPortrait(personality.Gender);
            personality.Owner                   =   house.Owner;
            personality.BaseVigor.Score         =   Tools.DiceBag.Roll(3, 6, vigorModifier);
            personality.BaseManagement.Score    =   Tools.DiceBag.Roll(3, 6, managementModifier);
            personality.BaseIntrigue.Score      =   Tools.DiceBag.Roll(3, 6, intrigueModifier);
            personality.BaseCharisma.Score      =   Tools.DiceBag.Roll(3, 6, charismaModifier);
            personality.BaseMorality.Score      =   Tools.DiceBag.Roll(3, 6, moralityModifier);

            personality.AddPersonalityTrait(father.GetRandomTrait());
            personality.AddPersonalityTrait(mother.GetRandomTrait());

            personality.Father      =   father;
            personality.Mother      =   mother;
            personality.ChiefHouse  =   house;

            m_Personalities.Add(personality);

            return personality;
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
