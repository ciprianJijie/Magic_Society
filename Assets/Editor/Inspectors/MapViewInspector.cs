using UnityEngine;
using UnityEditor;
using System.Collections;

namespace MS.Editor
{
	[CustomEditor(typeof(MS.View.MapView))]
	public class MapViewInspector : UnityEditor.Editor
	{
		public override void OnInspectorGUI ()
		{
			 DrawDefaultInspector();

			if (Application.isPlaying)
			{
				if (GUILayout.Button("Update"))
				{
					((MS.View.MapView)target).UpdateView();
				}
			}
		}
	}
}
