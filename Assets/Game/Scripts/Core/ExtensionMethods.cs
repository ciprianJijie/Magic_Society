using UnityEngine;
using SimpleJSON;

public static class ExtensionMethods
{
	public static void FromJSON(this Vector2 vector, JSONNode json)
	{
		vector.x = json["x"].AsFloat;
		vector.y = json["y"].AsFloat;
	}

	public static string ToString(this string[] stringList, string separator)
	{
        string str;

        str = "";

        foreach (string item in stringList)
		{
			str += item + separator;
		}

        return str;
    }
}
