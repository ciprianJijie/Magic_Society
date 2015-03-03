using UnityEngine;

namespace MS.View
{
    public class ResourceProducerView : MapElementView
    {
        public Sprite WoodSprite;
        public Sprite FoodSprite;
        public Sprite StoneSprite;

        public Sprite MissingSprite;

        public override void UpdateView()
        {
            MS.Model.ResourceProducer producer;

            producer = this.m_model as MS.Model.ResourceProducer;

            if (producer != null)
            {
                Sprite sprite;
                string name;

                sprite = MissingSprite;
                name = "Missing";

                if (producer.Resource.Name == "Wood")
                {
                    sprite = WoodSprite;
                    name = "Wood";
                }
                else if (producer.Resource.Name == "Stone")
                {
                    sprite = StoneSprite;
                    name = "Stone";
                }
                else if (producer.Resource.Name == "Food")
                {
                    sprite = FoodSprite;
                    name = "Food";
                }

                GetComponent<SpriteRenderer>().sprite = sprite;

                this.gameObject.name = name + " Producer @ " + this.m_model.Location;
            }
        }
    }
}
