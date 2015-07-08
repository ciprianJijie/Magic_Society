using UnityEngine;
using System;
using SimpleJSON;

public static class ExtensionMethods
{
	public static void FromJSON(this Vector2 vector, JSONNode json)
	{
		vector.x = json["x"].AsFloat;
		vector.y = json["y"].AsFloat;
	}
}

