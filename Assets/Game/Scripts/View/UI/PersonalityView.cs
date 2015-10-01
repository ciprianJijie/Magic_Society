using System;
using System.Collections;
using System.Collections.Generic;
using MS.Model;
using UnityEngine;
using UnityEngine.UI;

namespace MS.Views.UI
{
    public class PersonalityView : View<Model.Personality>
    {
        public Text     NameLabel;
        public Text     AgeLabel;
        public Image    MaleIcon;
        public Image    FemaleIcon;
        public Text     StrengthLabel;
        public Text     DexterityLabel;
        public Text     ConstitutionLabel;
        public Text     IntelligenceLabel;
        public Text     WisdomLabel;
        public Text     CharismaLabel;
        public Image    PortraitImage;
        public Sprite BabySprite;
        public Sprite DeathSprite;

        public override void UpdateView(Personality element)
        {
            NameLabel.text          =   element.Name;
            AgeLabel.text           =   element.Age.ToString();
            StrengthLabel.text      =   element.BaseStrength.ToString();
            DexterityLabel.text     =   element.BaseDexterity.ToString();
            ConstitutionLabel.text  =   element.BaseConstitution.ToString();
            IntelligenceLabel.text  =   element.BaseIntelligence.ToString();
            WisdomLabel.text        =   element.BaseWisdom.ToString();
            CharismaLabel.text      =   element.BaseCharisma.ToString();

            switch (element.Gender)
            {
                case Personality.EGender.Female:
                    MaleIcon.gameObject.SetActive(false);
                    FemaleIcon.gameObject.SetActive(true);
                    break;
                case Personality.EGender.Male:
                    MaleIcon.gameObject.SetActive(true);
                    FemaleIcon.gameObject.SetActive(false);
                    break;
                case Personality.EGender.Unknown:
                    MaleIcon.gameObject.SetActive(false);
                    FemaleIcon.gameObject.SetActive(false);
                    break;
            }
            
            if (element.Alive == false)
            {
                PortraitImage.sprite = DeathSprite;                
            }
            else if (element.Age < 16)
            {
                PortraitImage.sprite = BabySprite;
            }
            else
            {
                StartCoroutine(LoadPortrait(Model.Portrait.ImagePath));
            }
        }

        protected IEnumerator LoadPortrait(string filePath)
        {
            string  finalPath;
            WWW     localFile;
            Texture texture;
            Sprite  sprite;

            finalPath = "file://" + Path.ToPortrait(filePath);
            localFile = new WWW(finalPath);

            yield return localFile;

            texture = localFile.texture;
            sprite = Sprite.Create(texture as Texture2D, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            PortraitImage.sprite = sprite;
        }
    }
}
