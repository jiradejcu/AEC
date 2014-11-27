using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour
{
		public delegate void Callback ();

		public static event Callback FadeOutCompleted;

		public static bool isPlayed = false;

		void Start ()
		{
				if (!isPlayed) {
						iTween.FadeFrom (gameObject, iTween.Hash ("alpha", 0f, "time", 0.3f, "delay", 0.5f));
						iTween.FadeTo (gameObject, iTween.Hash ("alpha", 0f, "time", 0.2f, "delay", 2f, "oncomplete", "LogoFadeOut"));
						StoryData.Instance.RetrieveData ();
						StoryData.Instance.RetrieveQuestion ();
						isPlayed = true;
				} else {
						foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
								sr.renderer.enabled = false;
						StartCoroutine (LogoFadeOut ());
				}
		}

		IEnumerator LogoFadeOut ()
		{
				yield return new WaitForEndOfFrame ();
				FadeOutCompleted.Invoke ();
		}
}
