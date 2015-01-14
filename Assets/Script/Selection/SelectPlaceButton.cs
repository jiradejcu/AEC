using UnityEngine;
using System.Collections;

public class SelectPlaceButton : CommonButton
{
		public string storyName;
		public string storyDisplayName;
		int originalSiblingIndex;
		bool isSelected;
	
		public override void OnMouseDown ()
		{
				isSelected = true;
				base.OnMouseDown ();
				PlaceCamera placeCamera = Camera.main.GetComponent<PlaceCamera> ();
				placeCamera.SetTargetPosition (gameObject);
				StartCoroutine (DoOnMouseDown ());
		}

		IEnumerator DoOnMouseDown ()
		{
				yield return new WaitForSeconds (PlaceCamera.flyTime);
				Main.selectedStory = storyName;
				Application.LoadLevel ("Main");
		}

		public override void OnMouseOver ()
		{
				originalSiblingIndex = transform.parent.transform.parent.GetSiblingIndex ();
				transform.parent.transform.parent.SetAsLastSibling ();
		}
	
		public override void OnMouseExit ()
		{
				if (!isSelected)
						transform.parent.transform.parent.SetSiblingIndex (originalSiblingIndex);
		}
}
