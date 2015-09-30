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

        public override void UpdateView(Personality element)
        {
            NameLabel.text          =   element.Name;
            AgeLabel.text           =   element.Age.ToString();
            StrengthLabel.text      =   element.Strength.ToString();
            DexterityLabel.text     =   element.Dexterity.ToString();
            ConstitutionLabel.text  =   element.Constitution.ToString();
            IntelligenceLabel.text  =   element.Intelligence.ToString();
            WisdomLabel.text        =   element.Wisdom.ToString();
            CharismaLabel.text      =   element.Charisma.ToString();

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
            UnityEngine.Debug.Log("Loading sprite from " + Model.Portrait.ImagePath);
            StartCoroutine(LoadPortrait(Model.Portrait.ImagePath));
        }

        protected IEnumerator LoadPortrait(string filePath)
        {
            string  finalPath;
            WWW     localFile;
            Texture texture;
            Sprite  sprite;

            finalPath = "file://" + Path.ToPortrait(filePath);
            localFile = new WWW(finalPath);

            UnityEngine.Debug.Log("Loading image file " + finalPath);

            yield return localFile;

            UnityEngine.Debug.Log("Done");

            texture = localFile.texture;
            sprite = Sprite.Create(texture as Texture2D, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            PortraitImage.sprite = sprite;
        }
    }
}
