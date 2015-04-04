using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace MS
{
	public class SceneLoadingManager : MS.Core.Singleton<SceneLoadingManager>
	{
		public Image 		Fader;
		public float 		TimeBeforeFade;
		public float 		FadeDuration;

		public void LoadSceneWithFade(string sceneName)
		{
			UnityEngine.Debug.Log("Loading scene " + name);
			LoadScene(sceneName, true);
		}

		public static void LoadScene(string sceneName, bool transition)
		{
			if (transition && Instance.Fader != null)
			{
				// TODO: Fade out the fader, load the scene and the fade in
				Instance.StartCoroutine(Instance.LoadLevelWithFade(sceneName));
			}
			else
			{
				Application.LoadLevel(sceneName);
			}
		}

		private IEnumerator LoadLevelWithFade(string sceneName)
		{
			yield return new WaitForSeconds(TimeBeforeFade);

			float timeStart;
			float timeEnd;
			Color visibleColor;
			Color hiddenColor;

			timeStart 		= 	Time.time;
			timeEnd			=	timeStart + (FadeDuration / 2.0f);
			visibleColor 	= 	Fader.color;
			hiddenColor 	= 	new Color(visibleColor.r, visibleColor.g, visibleColor.b, 0.0f);

			UnityEngine.Debug.Log("Fading out!");

			while (Time.time < timeEnd)
			{
				Fader.color = Color.Lerp(visibleColor, hiddenColor, Time.time / timeEnd);
				yield return Time.deltaTime;
			}

			Application.LoadLevel(sceneName);

			timeStart 	= 	Time.time;
			timeEnd		=	timeStart + (FadeDuration / 2.0f);

			UnityEngine.Debug.Log("Fading in!");

			while (Time.time < timeEnd)
			{
				Fader.color = Color.Lerp(hiddenColor, visibleColor, Time.time / timeEnd);
				yield return Time.deltaTime;
			}
		}
	}
}
