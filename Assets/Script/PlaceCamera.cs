using UnityEngine;
using System.Collections;

public class PlaceCamera : MonoBehaviour
{
		GameObject targetGameObject;
		static float flyTime = 2f;

		public void SetTargetPosition (GameObject targetGameObject)
		{
				this.targetGameObject = targetGameObject;
				iTween.LookTo (gameObject, targetGameObject.transform.position, flyTime);
				iTween.MoveTo (gameObject, targetGameObject.transform.position, flyTime);
		}
}
