using UnityEngine;
using System.Collections.Generic;

namespace MS.Controllers.UI
{
    public class RepeatableIcon : MonoBehaviour
    {
        public GameObject IconPrefab;

        protected List<GameObject> m_Icons;

        public void UpdateIcons(int iconCount)
        {
            if (iconCount > 0)
            {
                this.gameObject.SetActive(true);
            }
            else
            {
                this.gameObject.SetActive(false);
            }

            if (iconCount <= m_Icons.Count)
            {
                int numToRemove;

                numToRemove = m_Icons.Count - iconCount;

                while (numToRemove > 0)
                {
                    Destroy(m_Icons[m_Icons.Count - 1]);
                    m_Icons.RemoveAt(m_Icons.Count - 1);

                    numToRemove--;
                }
            }
            else
            {
                int         numToInsert;
                GameObject  icon;

                numToInsert = iconCount - m_Icons.Count;

                while (numToInsert > 0)
                {
                    icon = Utils.Instantiate(IconPrefab, this.transform, this.transform.position, this.transform.rotation);

                    m_Icons.Add(icon);

                    numToInsert--;
                }
            }
        }

        protected void Start()
        {
            m_Icons = new List<GameObject>();
        }
    }
}

