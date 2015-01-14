using UnityEngine;
using System.Collections;

public class CommonButton : MonoBehaviour
{
		public static AudioSource clickSource;

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
				transform.localScale = new Vector3 (1.2f, 1.2f);
		}
	
		public virtual void OnMouseExit ()
		{
				transform.localScale = new Vector3 (1f, 1f);
		}

		public virtual void OnMouseDown ()
		{
				clickSource.Play ();
		}
}
