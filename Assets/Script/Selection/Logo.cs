using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour
{
		public delegate void Callback ();

		public static event Callback FadeOutCompleted;

		void Start ()
		{
				iTween.FadeFrom (gameObject, iTween.Hash ("alpha", 0f, "time", 0.3f, "delay", 0.5f));
				iTween.FadeTo (gameObject, iTween.Hash ("alpha", 0f, "time", 0.2f, "delay", 2f, "oncomplete", "LogoFadeOut"));
		}

		void LogoFadeOut ()
		{
				FadeOutCompleted.Invoke ();
		}
}
