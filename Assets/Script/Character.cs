using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
		private Animator animator;

		void Start ()
		{
				animator = GetComponent<Animator> ();
		}

		void Update ()
		{
				float length = animator.GetFloat ("length");
				if (length >= 0) {
						Debug.Log (length - Time.deltaTime);
						animator.SetFloat ("length", length - Time.deltaTime);
				}
		}

		public void PlayAnimation (AnimationData animationData)
		{
				AnimatorStateInfo current = animator.GetCurrentAnimatorStateInfo (0);
				if (!current.IsName (animationData.animationName)) {
						animator.SetTrigger (animationData.animationName);
						animator.SetFloat ("length", animationData.animationLength);
				}
				if (animationData.positionX.HasValue)
						iTween.MoveTo (gameObject.transform.parent.gameObject, iTween.Hash ("x", animationData.positionX.Value, "time", animationData.animationLength, "easetype", iTween.EaseType.linear));
				if (animationData.scaleX.HasValue)
						transform.parent.localScale = new Vector3 (transform.parent.localScale.x * animationData.scaleX.Value, transform.parent.localScale.y);
		}
}
