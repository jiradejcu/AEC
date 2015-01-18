using UnityEngine;
using System.Collections;

public class CommonButton : MonoBehaviour
{
		public static AudioSource clickSource;
		protected float scaleUp = 1.2f;
		static float scaleUpTime = 0.5f;

		void Awake ()
		{
				if (clickSource == null) {
						GameObject sound = GameObject.Instantiate (Resources.Load ("Prefabs/Sound")) as GameObject;
						GameObject.DontDestroyOnLoad (sound);
						clickSource = sound.GetComponent<AudioSource> ();
						clickSource.clip = Resources.Load ("Sound/click") as AudioClip;
						clickSource.volume = 0.3f;
				}
		}

		public virtual void OnMouseOver ()
		{
				iTween.ScaleTo (gameObject, new Vector3 (scaleUp, scaleUp), scaleUpTime);
		}
	
		public virtual void OnMouseExit ()
		{
				iTween.ScaleTo (gameObject, new Vector3 (1f, 1f), scaleUpTime);
		}

		public virtual void OnMouseDown ()
		{
				clickSource.Play ();
		}
}
