﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationEngine : Singleton<AnimationEngine>
{
		protected static float buttonTransLateOffsetY = -1f;
		protected static float delayInterval = 0.1f;

		public void animateButton (GameObject go, int index)
		{
				Vector3 fromPosition = new Vector3 (go.transform.position.x, go.transform.position.y + buttonTransLateOffsetY);
				iTween.MoveFrom (go, iTween.Hash ("position", fromPosition, "time", 1f, "delay", index * delayInterval));
				iTween.FadeFrom (go, iTween.Hash ("alpha", 0, "time", 1f, "delay", index * delayInterval));
				Animator animator = go.GetComponent<Animator> ();
				if (animator != null) {
						animator.enabled = true;
						animator.SetTrigger ("hide");
				}
				StartCoroutine (fadeIn (go, index));
		}

		IEnumerator fadeIn (GameObject go, int index)
		{
				yield return new WaitForSeconds (index * delayInterval);
				Animator animator = go.GetComponent<Animator> ();
				if (animator != null) {
						animator.enabled = true;
						animator.SetTrigger ("fadeIn");
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
