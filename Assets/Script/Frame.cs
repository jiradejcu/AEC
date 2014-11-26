using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Frame : MonoBehaviour
{
		private SpriteRenderer sr;
		private SmartTextMesh textMesh;
		public QuestionPanel qp;
		public GameObject imageOnlyLayout;
		public GameObject imageWithTextLayout;
		public GameObject textOnlyLayout;
		float width;
		float height;
		static float scale = 0.95f;

		public enum Layout
		{
				IMAGE_ONLY = 0,
				IMAGE_WITH_TEXT = 1,
				TEXT_ONLY = 2,
				QUESTION = 3
		}

		void Start ()
		{
				SpriteRenderer parent_sr = transform.parent.GetComponent<SpriteRenderer> ();
				width = parent_sr.bounds.size.x * scale;
				height = parent_sr.bounds.size.y * scale;
		}

		public void SetImage (string countryCode, string imageName, List<ContentText> contentTextList)
		{
				float localWidth = width;
				float localHeight = height;

				if (Main.ContainText (contentTextList)) {
						if (string.IsNullOrEmpty (imageName)) {
								SetLayout ((int)Layout.TEXT_ONLY);
						} else {
								SetLayout ((int)Layout.IMAGE_WITH_TEXT);
								localWidth /= 2;
								localHeight /= 2;
						}
				} else
						SetLayout ((int)Layout.IMAGE_ONLY);

				sr = GetComponentInChildren<SpriteRenderer> ();
				if (sr != null && !string.IsNullOrEmpty (imageName)) {
						sr.transform.localScale = new Vector3 (1f, 1f);
						sr.sprite = Resources.Load<Sprite> ("Image/Country/" + countryCode + "/" + imageName);
						if (sr.sprite != null)
								sr.transform.localScale = new Vector3 (localWidth / sr.bounds.size.x, localHeight / sr.bounds.size.y);
				}

				textMesh = GetComponentInChildren<SmartTextMesh> ();
				if (textMesh != null) {
						textMesh.UnwrappedText = Main.ConcatText (contentTextList);
						textMesh.NeedsLayout = true;
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
