using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subtitle : MonoBehaviour
{
		private List<SubtitleText> textList;
		private TextMesh text;
		private float countup;

		public List<SubtitleText> TextList {
				set {
						countup = 0;
						textList = new List<SubtitleText> ();
						foreach (SubtitleText subtitleText in value)
								textList.Add (subtitleText);
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
										text.text = textList [0].text;
										textList.RemoveAt (0);
								}
						}
				}
		}
}
