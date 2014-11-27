using UnityEngine;
using System.Collections;

public class CommonSelect : MonoBehaviour
{
		static int[] columnCount = {3, 4, 3};
		protected static float width = 1.8f;
		protected static float height = 1.2f;
		protected float scaleWidthCoeff = 1.4f;
		protected float scaleHeightCoeff = 1.8f;
		static float buttonTransLateOffsetY = -1f;
		static float delayInterval = 0.1f;
	
		protected GameObject CreateSelectButton (string buttonName, int index, int length)
		{
				Object buttonPrefab = Resources.Load ("Prefabs/" + buttonName);
				GameObject buttonObject = GameObject.Instantiate (buttonPrefab,
		                                                  new Vector3 (GetX (index) * width * scaleWidthCoeff, - GetY (index) * height * scaleHeightCoeff)
			                                                  , transform.rotation) as GameObject;
				Vector3 fromPosition = new Vector3 (buttonObject.transform.position.x, buttonObject.transform.position.y + buttonTransLateOffsetY);
				iTween.MoveFrom (buttonObject, iTween.Hash ("position", fromPosition, "time", 1f, "delay", index * delayInterval));
				iTween.FadeFrom (buttonObject, iTween.Hash ("alpha", 0, "time", 1f, "delay", index * delayInterval));
				return buttonObject;
		}

		int GetRow (int index)
		{
				int row = 0;
				int sumRow = 0;
				for (int i = 0; i < columnCount.Length; i++) {
						sumRow += columnCount [i];
						if (index < sumRow) {
								row = i;
								break;
						}
				}
				return row;
		}

		float GetY (int index)
		{
				float centerRow = (columnCount.Length / 2.0f) - 0.5f;
				int row = GetRow (index);
				return row - centerRow;
		}
	
		float GetX (int index)
		{
				int row = GetRow (index);
				float centerColumn = (columnCount [row] / 2.0f) - 0.5f;
				int column = 0;

				foreach (int i in columnCount) {
						if (index < i) {
								column = index;
								break;
						}
						index -= i;
				}
				return column - centerColumn;
		}
}
