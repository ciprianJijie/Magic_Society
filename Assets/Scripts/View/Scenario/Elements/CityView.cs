using UnityEngine;
using System.Collections;

namespace MS.View
{
    public class CityView : MapElementView
    {
        /// <summary>
        /// Sprites used to represent each level of the city.
        /// </summary>
        public Sprite[]     LevelSprite;

        public override void UpdateView()
        {
            int level;
            Sprite spriteForLevel;
            Model.City city;

            city            =   this.m_model as Model.City;
            level           =   city.Population;
            level           =   Mathf.Min(level, LevelSprite.Length - 1);
            spriteForLevel  =   LevelSprite[level];

            GetComponent<SpriteRenderer>().sprite = spriteForLevel;
        }

        public override string ToString()
        {
            return string.Format("City @ {0}", this.m_model.Location);
        }
    }
}
