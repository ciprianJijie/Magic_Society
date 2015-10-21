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
        public Text                             NameLabel;
        public Text                             AgeLabel;
        public Image                            MaleIcon;
        public Image                            FemaleIcon;
        public Text                             VigorLabel;
        public Text                             ManagementLabel;
        public Text                             IntrigueLabel;
        public Text                             CharismaLabel;
        public Text                             MoralityLabel;
        public Image                            PortraitImage;
        public Sprite                           BabySprite;
        public Sprite                           DeathSprite;
        public Controllers.UI.TraitController   TraitController;
        public Transform                        TraitsHolder;

        public override void UpdateView(Personality element)
        {
            NameLabel.text          =   element.FullName;
            AgeLabel.text           =   element.Age.ToString();
            VigorLabel.text         =   element.Vigor.ToString();
            ManagementLabel.text    =   element.Management.ToString();
            IntrigueLabel.text      =   element.Intrigue.ToString();
            CharismaLabel.text      =   element.Charisma.ToString();
            MoralityLabel.text      =   element.Morality.ToString();

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

            //TraitController.ClearViews();
            TraitController.Holder = TraitsHolder;

            foreach (Trait trait in element)
            {
                var newView = TraitController.CreateView(trait);

                UnityEngine.Debug.Log("Created trait view for " + trait.Name + " => " + newView);

                newView.UpdateView(trait);
            }

            m_Model = element;

            UnityEngine.Debug.Log("Updating view for " + element);
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
