using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectPlaceButton : CommonButton
{
		static float fadeIn = 0.02f;
		public float delay;
		public string storyName;
		public string storyDisplayName;
		public GameObject image;
		int originalSiblingIndex;
		bool isSelected;
		CanvasGroup cg;

		void Start ()
		{
				Text text = GetComponentInChildren<Text> ();
				text.text = storyDisplayName;
				cg = GetComponent<CanvasGroup> ();
				cg.alpha = 0;
		}
	
		void Update ()
		{
				if (delay >= 0) {
						delay -= Time.deltaTime;
				} else if (cg.alpha < 1) {
						cg.alpha += fadeIn;
				}
		}
	
		public override void OnMouseDown ()
		{
				isSelected = true;
				base.OnMouseDown ();
				PlaceCamera placeCamera = Camera.main.GetComponent<PlaceCamera> ();
				placeCamera.SetTargetPosition (image);
				StartCoroutine (DoOnMouseDown ());
		}

		IEnumerator DoOnMouseDown ()
		{
				yield return new WaitForSeconds (PlaceCamera.flyTime * 0.8f);
				Main.selectedStory = storyName;
				Application.LoadLevel ("Main");
		}

		public override void OnMouseOver ()
		{
				base.OnMouseOver ();
				originalSiblingIndex = transform.GetSiblingIndex ();
				transform.SetAsLastSibling ();
		}
	
		public override void OnMouseExit ()
		{
				if (!isSelected) {
						base.OnMouseExit ();
						transform.SetSiblingIndex (originalSiblingIndex);
				}
		}
}
