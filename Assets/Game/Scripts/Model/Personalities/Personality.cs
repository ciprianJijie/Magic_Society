﻿using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

namespace MS.Model
{
    public class Personality : OwnableElement, IEventListener, IEnumerable<Trait>
    {
        public static readonly int CHILD_AGE        =   0;
        public static readonly int ADULT_AGE        =   16;
        public static readonly int OLD_AGE          =   50;
        public static readonly int VENERABLE_AGE    =   70;
        public static readonly float YEARS_PER_TURN =   1.0f;

        public enum EGender { Male, Female, Unknown }
        public enum EAgeStage { Child, Adult, Old, Venerable, Unknown }

        public Ability Strength;
        public Ability Dexterity;
        public Ability Constitution;
        public Ability Intelligence;
        public Ability Wisdom;
        public Ability Charisma;

        public EGender Gender;
        public float Age;
        public bool Alive;
        public Portrait Portrait;

        protected List<Trait> m_Traits;
        protected List<Relationship> m_Relationships;

        public EAgeStage AgeStage
        {
            get
            {
                if (Age >= VENERABLE_AGE)
                {
                    return EAgeStage.Venerable;
                }
                else if (Age >= OLD_AGE)
                {
                    return EAgeStage.Old;
                }
                else if (Age >= ADULT_AGE)
                {
                    return EAgeStage.Adult;
                }
                else if (Age >= CHILD_AGE)
                {
                    return EAgeStage.Child;
                }
                else
                {
                    return EAgeStage.Unknown;
                }
            }
        }

        public Personality()
        {
            Strength        =   new Ability(Ability.EType.ABILITY_STRENGTH, 10);
            Dexterity       =   new Ability(Ability.EType.ABILITY_DEXTERITY, 10);
            Constitution    =   new Ability(Ability.EType.ABILITY_CONSTITUTION, 10);
            Intelligence    =   new Ability(Ability.EType.ABILITY_INTELLIGENCE, 10);
            Wisdom          =   new Ability(Ability.EType.ABILITY_WISDOM, 10);
            Charisma        =   new Ability(Ability.EType.ABILITY_CHARISMA, 10);
            Alive           =   true;
            m_Traits        =   new List<Trait>();
            m_Relationships =   new List<Relationship>();

            SubscribeToEvents();
        }

        public void AddPersonalityTrait(Trait trait)
        {
            UnityEngine.Debug.Log(Name + " is now " + trait.Name);
            m_Traits.Add(trait);
        }

        public bool Is<T>() where T: Trait
        {
            var trait = m_Traits.Find( i => i is T);

            return trait != null;
        }

        public bool Is(string traitName)
        {
            var trait = m_Traits.Find(i => i.Name == traitName);

            return trait != null;
        }

        public int RelationWith(Personality other)
        {
            Relationship    relationship;
            int             relationshipRate;

            relationshipRate    =   0;
            relationship        =   m_Relationships.Find(i => i.Target == other);

            if (relationship == null)
            {
                relationship            =   new Relationship();
                relationship.Source     =   this;
                relationship.Target     =   other;
                relationship.Rate       =   0;

                m_Relationships.Add(relationship);
            }

            relationshipRate = relationship.Rate;

            foreach (Trait trait in m_Traits)
            {
                foreach (Modifier modifier in trait)
                {
                    RelationshipModifier relationshipModifier;

                    relationshipModifier = modifier as RelationshipModifier;

                    if (relationshipModifier != null)
                    {
                        relationshipRate += relationshipModifier.Apply(relationship);
                    }
                }
            }

            return relationshipRate;
        }

        public void SubscribeToEvents()
        {
            Game.Instance.Turns.OnAllTurnsFinished += OnTurnEnd;
        }

        public void UnsubscribeToEvents()
        {
            Game.Instance.Turns.OnAllTurnsFinished -= OnTurnEnd;
        }

        protected void OnTurnEnd()
        {
            if (Alive)
            {
                Age += YEARS_PER_TURN;
    
                if (CheckNaturalDeath())
                {
                    // TODO: Trigger death event
                    UnityEngine.Debug.Log("<color=red>" + this.ToString() + " died!</color>");
                    Alive = false;
                }
            }
        }

        protected bool CheckNaturalDeath()
        {
            int checkDC;
            int dieResult;

            checkDC     =   CalculateDeathCheckDC(Mathf.FloorToInt(Age));
            dieResult   =   Tools.DiceBag.Roll(3, 6, Constitution.Modifier);

            return dieResult < checkDC;
        }

        protected int CalculateDeathCheckDC(int currentAge)
        {
            return Mathf.FloorToInt(Mathf.Pow(1.03f, currentAge));
        }

        public override string ToString()
        {
            string traits;

            traits = "";

            foreach (Trait trait in m_Traits)
            {
                traits += trait.Name;
            }

            return string.Format("{0} ({1}) [STR:{2} DEX:{3} CON:{4} INT:{5} WIS:{6} CHA:{7}] Owned by {8} [{9}]", Name, Mathf.FloorToInt(Age), Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma, Owner.Name, traits);
        }

        public IEnumerator<Trait> GetEnumerator()
        {
            return m_Traits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Traits.GetEnumerator();
        }
    }
}
