using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subtitle : MonoBehaviour
{
		private List<ContentText> textList;
		private TextMesh text;
		private float countup;

		public List<ContentText> TextList {
				set {
						countup = 0;
						textList = new List<ContentText> ();
						foreach (ContentText contentText in value)
								textList.Add (contentText);
				}
		}

		void Start ()
		{
				text = GetComponent<TextMesh> ();
		}

		void Update ()
		{
				if (textList != null) {
						if (textList.Count > 0) {
								countup += Time.deltaTime;
								if (countup > textList [0].time) {
										text.text = textList [0].subtitle;
										textList.RemoveAt (0);
								}
						}
				}
		}
}
