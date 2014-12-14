using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CommonSelect : MonoBehaviour
{
		protected int[] columnCount;
		protected static float width = 1.8f;
		protected static float height = 1.2f;
		protected float scaleWidthCoeff = 1.4f;
		protected float scaleHeightCoeff = 1.8f;
	
		protected GameObject CreateSelectButton (string buttonName, int index, int length)
		{
				Object buttonPrefab = Resources.Load ("Prefabs/" + buttonName);
				GameObject buttonObject = GameObject.Instantiate (buttonPrefab,
		                                                  new Vector3 (GetX (index) * width * scaleWidthCoeff, - GetY (index) * height * scaleHeightCoeff)
			                                                  , transform.rotation) as GameObject;
				AnimationEngine.Instance.animateButton (buttonObject, index);
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
