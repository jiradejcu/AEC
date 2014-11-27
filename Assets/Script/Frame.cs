using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Frame : MonoBehaviour
{
		private Image image;
		private Text text;
		public QuestionPanel qp;
		public GameObject imageOnlyLayout;
		public GameObject imageWithTextLayout;
		public GameObject textOnlyLayout;

		public enum Layout
		{
				IMAGE_ONLY = 0,
				IMAGE_WITH_TEXT = 1,
				TEXT_ONLY = 2,
				QUESTION = 3
		}

		public void SetImage (string countryCode, string imageName, List<ContentText> contentTextList)
		{
				if (Main.ContainText (contentTextList)) {
						if (string.IsNullOrEmpty (imageName)) {
								SetLayout ((int)Layout.TEXT_ONLY);
						} else {
								SetLayout ((int)Layout.IMAGE_WITH_TEXT);
						}
				} else
						SetLayout ((int)Layout.IMAGE_ONLY);

				image = GetComponentInChildren<Image> ();
				if (image != null && !string.IsNullOrEmpty (imageName)) {
						image.sprite = Resources.Load<Sprite> ("Image/Country/" + countryCode + "/" + imageName);
				}

				text = GetComponentInChildren<Text> ();
				if (text != null) {
						text.text = Main.ConcatText (contentTextList);
				}
		}

		public void SetLayout (int layout)
		{
				switch (layout) {
				case (int)Layout.IMAGE_ONLY:
						imageOnlyLayout.SetActive (true);
						imageWithTextLayout.SetActive (false);
						textOnlyLayout.SetActive (false);
						qp.gameObject.SetActive (false);
						break;
				case (int)Layout.IMAGE_WITH_TEXT:
						imageOnlyLayout.SetActive (false);
						imageWithTextLayout.SetActive (true);
						textOnlyLayout.SetActive (false);
						qp.gameObject.SetActive (false);
						break;
				case (int)Layout.TEXT_ONLY:
						imageOnlyLayout.SetActive (false);
						imageWithTextLayout.SetActive (false);
						textOnlyLayout.SetActive (true);
						qp.gameObject.SetActive (false);
						break;
				case (int)Layout.QUESTION:
						imageOnlyLayout.SetActive (false);
						imageWithTextLayout.SetActive (false);
						textOnlyLayout.SetActive (false);
						qp.gameObject.SetActive (true);
						break;
				}
		}
}
