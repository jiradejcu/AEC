using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Frame : MonoBehaviour
{
		private MultipleText multipleText;
		public QuestionPanel qp;
		public GameObject imageOnlyLayout;
		public GameObject imageWithTextLayout;
		public GameObject textOnlyLayout;
		ImageContent imageContent;
		ScrollableImage scrollableImage;

		public enum Layout
		{
				IMAGE_ONLY = 0,
				IMAGE_WITH_TEXT = 1,
				TEXT_ONLY = 2,
				QUESTION = 3
		}

		public void SetImage (string countryCode, string imageName, List<ContentText> contentTextList, int scroll)
		{
				if (Main.ContainText (contentTextList)) {
						if (string.IsNullOrEmpty (imageName)) {
								SetLayout ((int)Layout.TEXT_ONLY);
						} else {
								SetLayout ((int)Layout.IMAGE_WITH_TEXT);
						}
				} else
						SetLayout ((int)Layout.IMAGE_ONLY);

				imageContent = GetComponentInChildren<ImageContent> ();
				if (imageContent != null && !string.IsNullOrEmpty (imageName)) {
						imageContent.SetSprite (LoadCountryImage (countryCode, imageName));
						scrollableImage = GetComponentInChildren<ScrollableImage> ();
						if (scrollableImage != null) {
								scrollableImage.Reset ();
						}
						AnimationEngine.Instance.animateImage (imageContent.gameObject, 0);
						List<ContentText> cloneContentTextList = ContentText.CloneImage (contentTextList);
						SubImageContent[] subImageContentList = GetComponentsInChildren<SubImageContent> ();

						foreach (SubImageContent subImageContent in subImageContentList) {
								subImageContent.ClearSprite ();
						}
			
						int i = 0;
						foreach (ContentText contentText in cloneContentTextList) {
								if (i < subImageContentList.Length)
										subImageContentList [i++].SetSprite (LoadCountryImage (countryCode, contentText.image), contentText.time);
						}
				}

				if (scrollableImage != null) {
						if (scroll == (int)AnimationData.SCROLL.PAN)
								scrollableImage.Zoom ();
						else if (scroll == (int)AnimationData.SCROLL.NORMAL)
								scrollableImage.KenBurns ();
				}

				multipleText = GetComponentInChildren<MultipleText> ();
				if (multipleText != null) {
						MultipleText.previousTime = 0f;
						multipleText.Text = ContentText.CloneText (contentTextList);
				}
		}

		Sprite LoadCountryImage (string countryCode, string imageName)
		{
				return Resources.Load<Sprite> ("Image/Country/" + countryCode + "/" + imageName);
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
