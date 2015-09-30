using UnityEngine;

namespace MS.Model
{
    public class Personality : OwnableElement, IEventListener
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
            Strength        =   new Ability("ABILITY_STRENGTH", 10);
            Dexterity       =   new Ability("ABILITY_DEXTERITY", 10);
            Constitution    =   new Ability("ABILITY_CONSTITUTION", 10);
            Intelligence    =   new Ability("ABILITY_INTELLIGENCE", 10);
            Wisdom          =   new Ability("ABILITY_WISDOM", 10);
            Charisma        =   new Ability("ABILITY_CHARISMA", 10);
            Alive           =   true;

            SubscribeToEvents();
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
            Age += YEARS_PER_TURN;

            if (Alive && CheckNaturalDeath())
            {
                // TODO: Trigger death event
                UnityEngine.Debug.Log("<color=red>" + this.ToString() + " died!</color>");
                Alive = false;
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
            return string.Format("{0} ({1}) [STR:{2} DEX:{3} CON:{4} INT:{5} WIS:{6} CHA:{7}] Owned by {8}", Name, Mathf.FloorToInt(Age), Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma, Owner.Name);
        }
    }
}
