using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour
{
		public delegate void Callback ();

		public static event Callback FadeOutCompleted;

		void Start ()
		{
				iTween.FadeTo (gameObject, iTween.Hash ("alpha", 0f, "time", 1f, "delay", 2f, "oncomplete", "LogoFadeOut"));
		}

		void LogoFadeOut ()
		{
				FadeOutCompleted.Invoke ();
		}
}
