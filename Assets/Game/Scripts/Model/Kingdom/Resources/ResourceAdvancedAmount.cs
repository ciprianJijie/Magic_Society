using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MS.Model
{
    public class ResourceAdvancedAmount : ModelElement
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
    }
}
