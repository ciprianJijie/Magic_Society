using UnityEngine;
using SimpleJSON;
using System.IO;

namespace MS.Generators
{
    public static class NameGenerator
    {
        public static string RandomMaleName()
        {
            return ComposeName("", "", "Data/Names/male");
        }

        public static string RandomFemaleName()
        {
            return ComposeName("", "", "Data/Names/female");
        }

        public static string RandomDistrictName()
        {
            string prefix;
            string suffix;

            prefix = "";
            suffix = "";

            if (Random.Range(0, 100) > 50)
            {
                prefix = RandomPrefix();
            }
            else
            {
                suffix = RandomSuffix();
            }

            return ComposeName(prefix, suffix, "Data/Names/districts");
        }

        public static string RandomHouseName()
        {
            return ComposeName("", "", "Data/Names/houses");
        }

        private static string ComposeName(string prefix, string suffix, string filePathWithNames)
        {
            string      text;
            JSONNode    json;
            JSONArray   array;
            int         randomIndex;
            string      name;

            text            =   Resources.Load<TextAsset>(filePathWithNames).text;
            json            =   JSON.Parse(text);
            array           =   json["names"].AsArray;
            randomIndex     =   Random.Range(0, array.Count);
            name            =   prefix + array[randomIndex]["name"] + suffix;

            return name;
        }

        private static string RandomPrefix()
        {
            string      text;
            JSONNode    json;
            JSONArray   array;
            int         randomIndex;

            text            =   Resources.Load<TextAsset>("Data/Names/prefixes").text;
            json            =   JSON.Parse(text);
            array           =   json["prefixes"].AsArray;
            randomIndex     =   Random.Range(0, array.Count);

            return array[randomIndex]["prefix"].ToString();
        }

        private static string RandomSuffix()
        {
            string      text;
            JSONNode    json;
            JSONArray   array;
            int         randomIndex;
            
            text            =   Resources.Load<TextAsset>("Data/Names/suffixes").text;
            json            =   JSON.Parse(text);
            array           =   json["suffixes"].AsArray;
            randomIndex     =   Random.Range(0, array.Count);
            
            return array[randomIndex]["suffix"].ToString();
        }
    }
}

