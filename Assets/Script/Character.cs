﻿using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
		private Animator animator;
		private AudioSource verbalSource;

		void Awake ()
		{
				verbalSource = (GameObject.Instantiate (Main.sound) as GameObject).GetComponent<AudioSource> ();
				animator = GetComponent<Animator> ();
				SpriteRenderer[] spriteList = GetComponentsInChildren<SpriteRenderer> ();
				foreach (SpriteRenderer sr in spriteList) {
						sr.sortingLayerName = "Character";
				}
		}

		void Update ()
		{
				float length = animator.GetFloat ("length");
				float delay = animator.GetFloat ("delay");
				if (length >= 0) {
						animator.SetFloat ("length", length - Time.deltaTime);
				}
				if (delay >= 0) {
						animator.SetFloat ("delay", delay - Time.deltaTime);
				}
		}

		public void PlayAnimation (AnimationData animationData)
		{
				AnimatorStateInfo current = animator.GetCurrentAnimatorStateInfo (0);
				if (!string.IsNullOrEmpty (animationData.animationName) && !current.IsName (animationData.animationName)) {
						animator.SetTrigger (animationData.animationName);
						animator.SetFloat ("length", animationData.animationLength);
						animator.SetFloat ("delay", animationData.animationDelay);
				}

				if (animationData.positionX.HasValue)
						iTween.MoveTo (gameObject.transform.parent.gameObject, iTween.Hash ("x", animationData.positionX.Value, "time", animationData.animationLength, "delay", animationData.animationDelay, "easetype", iTween.EaseType.linear));
				if (animationData.scaleX.HasValue)
						transform.parent.localScale = new Vector3 (transform.parent.localScale.x * animationData.scaleX.Value, transform.parent.localScale.y);

				if (!string.IsNullOrEmpty (animationData.sound)) {
						AudioClip verbalClip = GetVerbalClip (animationData.sound);
						verbalSource.clip = verbalClip;
						verbalSource.Play ();
				}
		}

		public static AudioClip GetVerbalClip (string name)
		{
				return Resources.Load ("Sound/Verbal/" + name) as AudioClip;
		}

		public static string GetCharacterName (string countryCode, string sex)
		{
				return "Character_" + StoryData.countryNameList [countryCode] + "_" + sex;
		}
}
