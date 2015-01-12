using UnityEngine;
using System.Collections;

public class PlaceCamera : MonoBehaviour
{
		GameObject targetGameObject;
		static float flyTime = 3f;

		void Start ()
		{
				Hashtable ht = iTween.Hash ("amount", new Vector3 (0f, 0.3f), "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong);
				iTween.MoveBy (gameObject, ht); 
		}

		public void SetTargetPosition (GameObject targetGameObject)
		{
				this.targetGameObject = targetGameObject;
				iTween.LookTo (gameObject, targetGameObject.transform.position, flyTime);
				iTween.MoveTo (gameObject, targetGameObject.transform.position, flyTime);
				iTween.CameraFadeAdd (iTween.CameraTexture (Color.white));
				iTween.CameraFadeTo (iTween.Hash("amount", 1f, "delay", 0.5f, "time", flyTime - 1f));
		}
}
