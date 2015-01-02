using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationEngine : Singleton<AnimationEngine>
{
		protected static float imageTransLateOffsetY = -1f;
		protected static float imageTransLateOffsetZ = -5f;
		protected static float delayInterval = 0.1f;
		public delegate void Callback ();

		public void animateImage (GameObject go, int index, Callback callback = null)
		{
				Vector3 fromPosition = new Vector3 (go.transform.position.x, go.transform.position.y + imageTransLateOffsetY, go.transform.position.z + imageTransLateOffsetZ);
				iTween.MoveFrom (go, iTween.Hash ("position", fromPosition, "time", 1f, "delay", index * delayInterval));
				Animator animator = go.GetComponent<Animator> ();
				if (animator != null) {
						animator.enabled = true;
						animator.SetTrigger ("hide");
						StartCoroutine (fadeIn (animator, index, callback));
				} else {
						iTween.FadeFrom (go, iTween.Hash ("alpha", 0, "time", 1f, "delay", index * delayInterval));
				}
		}

		IEnumerator fadeIn (Animator animator, int index, Callback callback = null)
		{
				yield return new WaitForSeconds (index * delayInterval);
				animator.enabled = true;
				animator.SetTrigger ("fadeIn");
				yield return new WaitForSeconds (animator.GetCurrentAnimatorStateInfo (0).length);
				if (callback != null) {
						callback ();
				}
		}

		public void addButtonShadow (GameObject go, int index, float width, float height)
		{
				GameObject shadowObject = GameObject.Instantiate (Resources.Load ("Prefabs/Shadow")) as GameObject;
				SpriteRenderer shadow = shadowObject.GetComponent<SpriteRenderer> ();
				shadowObject.transform.parent = go.transform;
				shadowObject.transform.localPosition = new Vector3 (0.05f, -0.05f);
				shadowObject.transform.localScale = new Vector3 (width / shadow.bounds.size.x, height / shadow.bounds.size.y);
				iTween.FadeFrom (shadowObject, iTween.Hash ("alpha", 0, "time", 0.5f, "delay", index * delayInterval + 0.5f));
		}
}
