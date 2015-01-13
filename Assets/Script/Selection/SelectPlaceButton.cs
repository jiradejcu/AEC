using UnityEngine;
using System.Collections;

public class SelectPlaceButton : CommonButton
{
		public void OnMouseDown ()
		{
				base.OnMouseDown ();
				StartCoroutine (DoOnMouseDown ());
		}

		IEnumerator DoOnMouseDown ()
		{
				yield return new WaitForSeconds (PlaceCamera.flyTime);
				Main.selectedStory = "place1";
				Application.LoadLevel ("Main");
		}
}
