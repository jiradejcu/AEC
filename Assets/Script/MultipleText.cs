using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MultipleText : MonoBehaviour
{
		private Text text;
		public static float previousTime;

		void Awake ()
		{
				this.text = GetComponent<Text> ();
		}

		public List<ContentText> Text {
				set {
						ContentText contentText = value [0];
						this.text.text = contentText.text;
						value.RemoveAt (0);
						if (value.Count > 0) {
								StartCoroutine (SetNextText (value, contentText.time - previousTime));
								previousTime = contentText.time;
						}
				}
		}

		IEnumerator SetNextText (List<ContentText> contentTextList, float time)
		{
				yield return new WaitForSeconds (time);
				GameObject textObject = GameObject.Instantiate (Resources.Load ("Prefabs/Text_Prefab"), transform.position, transform.rotation) as GameObject;
				textObject.transform.SetParent (transform.parent);
				textObject.transform.localScale = Vector3.one;
				RectTransform rt = textObject.GetComponent<RectTransform> ();
				RectTransform parent_rt = GetComponent<RectTransform> ();
				rt.anchoredPosition = new Vector2 (0f, parent_rt.anchoredPosition.y - parent_rt.rect.height);
				MultipleText multipleText = textObject.GetComponent<MultipleText> ();
				multipleText.Text = contentTextList;
		}
}
