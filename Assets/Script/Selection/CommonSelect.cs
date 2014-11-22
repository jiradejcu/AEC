using UnityEngine;
using System.Collections;

public class CommonSelect : MonoBehaviour
{
		static int rowCount = 2;
		static float offsetX = -5f;
		static float offsetY = 3f;
		protected static float width = 1.8f;
		protected static float height = 1.2f;
		static float scaleCoeff = 1.4f;
		static float buttonTransLateOffsetY = -1f;
		static float delayInterval = 0.1f;
	
		protected GameObject CreateSelectButton (string buttonName, int index, int length)
		{
				GameObject buttonObject = GameObject.Instantiate (Resources.Load ("Prefabs/" + buttonName),
		                                                  new Vector3 (index % (length / rowCount) * width * scaleCoeff + offsetX, offsetY - index / (length / rowCount) * height * scaleCoeff)
			                                                  , transform.rotation) as GameObject;
				Vector3 fromPosition = new Vector3 (buttonObject.transform.position.x, buttonObject.transform.position.y + buttonTransLateOffsetY);
				iTween.MoveFrom (buttonObject, iTween.Hash ("position", fromPosition, "time", 1f, "delay", index * delayInterval));
				iTween.FadeFrom (buttonObject, iTween.Hash ("alpha", 0, "time", 1f, "delay", index * delayInterval));
				return buttonObject;
		}
}
