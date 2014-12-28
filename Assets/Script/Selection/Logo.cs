using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour
{
		public delegate void Callback ();

		public static event Callback FadeOutCompleted;

		public static AudioSource bgmSource;
		public static bool isPlayed = false;
		public GameObject logoPanel;
		public GameObject objectivePanel;

		void Start ()
		{
				Input.simulateMouseWithTouches = true;
				if (Main.selectedCountry.Equals (StoryData.aecName))
						Main.selectedCountry = "";

				if (bgmSource == null) {
						GameObject sound = GameObject.Instantiate (Resources.Load ("Prefabs/Sound")) as GameObject;
						GameObject.DontDestroyOnLoad (sound);
						bgmSource = sound.GetComponent<AudioSource> ();
				}

				if (!isPlayed && !CommonConfig.TEST_MODE) {
						bgmSource.clip = Resources.Load ("Sound/BGM/asean_way1") as AudioClip;
						bgmSource.volume = 0.5f;
						bgmSource.Play ();
						StartCoroutine (PlayDelayBGM ());
						iTween.FadeFrom (logoPanel, iTween.Hash ("alpha", 0f, "time", 0.8f, "delay", 1f));
						iTween.FadeTo (logoPanel, iTween.Hash ("alpha", 0f, "time", 0.2f, "delay", 3.5f));
						iTween.FadeFrom (objectivePanel, iTween.Hash ("alpha", 0f, "time", 0.3f, "delay", 4f));
						iTween.FadeTo (objectivePanel, iTween.Hash ("alpha", 0f, "time", 0.2f, "delay", 7.5f, "oncomplete", "LogoFadeOut", "oncompletetarget", gameObject));
				} else {
						foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
								sr.renderer.enabled = false;
						StartCoroutine (LogoFadeOut ());
						StartCoroutine (PlayBGM ());
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
	
		IEnumerator PlayDelayBGM ()
		{
				yield return new WaitForSeconds (bgmSource.clip.length);
				StartCoroutine (PlayBGM ());
		}

		IEnumerator PlayBGM ()
		{
				yield return null;
				if (!bgmSource.isPlaying) {
						bgmSource.clip = Resources.Load ("Sound/BGM/asean_way2") as AudioClip;
						bgmSource.volume = 0.5f;
						bgmSource.loop = true;
						bgmSource.Play ();
				}
		}
}
