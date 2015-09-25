using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MS.Model
{
    public class ResourceAdvancedAmount : ModelElement, IEnumerable<ResourceAmount>
    {
        protected List<ResourceAmount> m_Amounts;

        public ResourceAdvancedAmount()
        {
            Name = "RESOURCE_ADVANCED_AMOUNT";
            m_Amounts = new List<ResourceAmount>();
        }

        public void AddAmount(Resource resource, int amount, ModelElement source)
        {
            AddAmount(new ResourceAmount(resource, amount, source));
        }

        public void AddAmount(ResourceAmount resourceAmount)
        {
            m_Amounts.Add(resourceAmount);
        }

        public void Clear()
        {
            m_Amounts.Clear();
        }

        public int GetTotalAmount()
        {
            int amount;

            amount = 0;

            foreach (ResourceAmount res in m_Amounts)
            {
                amount += res.Amount;
            }

            return amount;
        }

        public void SortByAmount()
        {
            m_Amounts.Sort(
                delegate(ResourceAmount a, ResourceAmount b)
                {
                    return a.Amount.CompareTo(b.Amount);
                }
                );
        }

        public ResourceAdvancedAmount GetNegative()
        {
            ResourceAdvancedAmount amount = new ResourceAdvancedAmount();

            foreach (ResourceAmount single in m_Amounts)
            {
                if (single.Amount < 0)
                {
                    amount.AddAmount(single);
                }
            }

            return amount;
        }

        public ResourceAdvancedAmount GetPositive()
        {
            ResourceAdvancedAmount amount = new ResourceAdvancedAmount();

            foreach (ResourceAmount single in m_Amounts)
            {
                if (single.Amount > 0)
                {
                    amount.AddAmount(single);
                }
            }
            return amount;
        }

        public void Split(ref ResourceAdvancedAmount positive, ref ResourceAdvancedAmount negative)
        {
            foreach (ResourceAmount amount in m_Amounts)
            {
                if (amount.Amount > 0)
                {
                    positive.AddAmount(amount);
                }
                else
                {
                    negative.AddAmount(amount);
                }
                
            }
        }

        public IEnumerator<ResourceAmount> GetEnumerator()
        {
            return m_Amounts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Amounts.GetEnumerator();
        }
    }
}
