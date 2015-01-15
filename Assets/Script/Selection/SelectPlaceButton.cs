using UnityEngine;
using System.Collections;

public class SelectPlaceButton : CommonButton
{
		public string storyName;
		public string storyDisplayName;
		public GameObject image;
		int originalSiblingIndex;
		bool isSelected;
	
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
