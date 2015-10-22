using System;
using System.Collections;
using System.Collections.Generic;

namespace MS.Model
{
    public class NobleHouse : ModelElement, IOwnable, IHouseOwneable, IEnumerable<Personality>, IRandomizable
    {
        public      Heraldry.Shield     Shield;
        protected   Player              m_Owner;
        protected   NobleHouse          m_ChiefHouse;
        protected   List<Personality>   m_FamilyMembers;

        public NobleHouse()
        {
            m_FamilyMembers =   new List<Personality>();
            Shield          =   new Heraldry.Shield();
        }

        public NobleHouse ChiefHouse
        {
            get
            {
                return m_ChiefHouse;
            }

            set
            {
                m_ChiefHouse = value;
            }
        }

        public Player Owner
        {
            get
            {
                return m_Owner;
            }

            set
            {
                m_Owner = value;
            }
        }

        public bool IsVassalOf(NobleHouse house)
        {
            return m_ChiefHouse == house;
        }

        public bool IsChiefOf(NobleHouse house)
        {
            return house.ChiefHouse == this;
        }

        public bool IsMajorHouse()
        {
            return m_ChiefHouse == null;
        }

        public void AddMember(Personality personality)
        {
            personality.ChiefHouse = this;
            m_FamilyMembers.Add(personality);
        }

        public IEnumerator<Personality> GetEnumerator()
        {
            return m_FamilyMembers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_FamilyMembers.GetEnumerator();
        }

        public void Randomize()
        {
            Shield.Randomize();
        }
    }
}
