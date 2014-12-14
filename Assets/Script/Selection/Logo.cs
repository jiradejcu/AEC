using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour
{
		public delegate void Callback ();

		public static event Callback FadeOutCompleted;

		public static bool isPlayed = false;
		public GameObject logoPanel;
		public GameObject objectivePanel;

		void Start ()
		{
				Input.simulateMouseWithTouches = true;
				if (Main.selectedCountry.Equals (StoryData.aecName))
						Main.selectedCountry = "";

				if (!isPlayed && !CommonConfig.TEST_MODE) {
						iTween.FadeFrom (logoPanel, iTween.Hash ("alpha", 0f, "time", 0.3f, "delay", 0.5f));
						iTween.FadeTo (logoPanel, iTween.Hash ("alpha", 0f, "time", 0.2f, "delay", 3f));
						iTween.FadeFrom (objectivePanel, iTween.Hash ("alpha", 0f, "time", 0.3f, "delay", 3.5f));
						iTween.FadeTo (objectivePanel, iTween.Hash ("alpha", 0f, "time", 0.2f, "delay", 7f, "oncomplete", "LogoFadeOut", "oncompletetarget", gameObject));
				} else {
						foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
								sr.renderer.enabled = false;
						StartCoroutine (LogoFadeOut ());
				}

				if (!isPlayed) {
						StoryData.Instance.RetrieveData ();
						StoryData.Instance.RetrieveQuestion ();
				}
		}

		IEnumerator LogoFadeOut ()
		{
				yield return new WaitForEndOfFrame ();
				FadeOutCompleted.Invoke ();
				isPlayed = true;
		}
}
