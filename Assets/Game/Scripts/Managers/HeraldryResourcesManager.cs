using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

namespace MS.Managers.Heraldry
{
    public class HeraldryResourcesManager : Singleton<HeraldryResourcesManager>
    {
        public      string          FieldsFolder;
        public      string          EmblemsFolder;
        protected   List<Sprite>    m_PrimaryFields;
        protected   List<Sprite>    m_SecondaryFields;
        protected   List<Sprite>    m_Emblems;
        protected   List<Color>     m_Colors;

        protected   bool            m_IsLoading;
        protected   Sprite          m_LoadedSprite;

        public Sprite FindPrimaryField(string name)
        {
            return m_PrimaryFields.Find(i => i.name == name);
        }

        public Sprite FindSecondaryField(string name)
        {
            return m_SecondaryFields.Find(i => i.name == name);
        }

        public void GetRandomField(out Sprite primary, out Sprite secondary)
        {
            int index;
            
            index       =   Random.Range(0, m_PrimaryFields.Count);
            primary     =   m_PrimaryFields[index];
            secondary   =   m_SecondaryFields[index];
        }

        public Color GetRandomColor()
        {
            return m_Colors[Random.Range(0, m_Colors.Count)];
        }

        public Sprite GetRandomEmblem()
        {
            return m_Emblems[Random.Range(0, m_Emblems.Count)];
        }

        protected override void Awake()
        {
            base.Awake();
            JSONNode root;

            m_PrimaryFields     =   new List<Sprite>();
            m_SecondaryFields   =   new List<Sprite>();
            m_Emblems           =   new List<Sprite>();
            m_Colors            =   new List<Color>();
            root                =   JSON.Parse(Resources.Load<TextAsset>("Heraldry/Heraldry").text);

            LoadColors(root["colors"].AsArray);
            LoadFields(root["fields"].AsArray);
            LoadEmblems(root["emblems"].AsArray);
        }

        protected void LoadColors(JSONArray colors)
        {
            foreach (JSONNode color in colors)
            {
                m_Colors.Add(Utils.HexToRGB(color["color"]));
            }
        }

        protected void LoadFields(JSONArray fields)
        {
            foreach (JSONNode field in fields)
            {
                LoadField(field["primary"], field["secondary"]);
            }
        }

        protected void LoadField(string primaryName, string secondaryName)
        {
            Sprite primary;
            Sprite secondary;

            primary = Resources.Load<Sprite>(FieldsFolder + "/" + primaryName);
            secondary = Resources.Load<Sprite>(FieldsFolder + "/" + secondaryName);

            if (primary != null && secondary != null)
            {
                m_PrimaryFields.Add(primary);
                m_SecondaryFields.Add(secondary);
            }
            else
            {
                UnityEngine.Debug.LogError("Field images " + primaryName + " and " + secondaryName + " not found.");
            }
        }

        public void LoadEmblems(JSONArray emblems)
        {
            Sprite sprite;

            foreach (JSONNode emblem in emblems)
            {
                sprite = Resources.Load<Sprite>(EmblemsFolder + "/" + emblem);

                if (sprite != null)
                {
                    m_Emblems.Add(sprite);
                }
                else
                {
                    UnityEngine.Debug.LogError("Sprite " + emblem + " not found");
                }                
            }
        }
    }
}
