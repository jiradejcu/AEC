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
				NONE = -1,
				IMAGE_ONLY = 0,
				IMAGE_WITH_TEXT = 1,
				TEXT_ONLY = 2,
				QUESTION = 3
		}

		public void SetImage (string countryCode, string imageName, List<ContentText> contentTextList, int scrollType)
		{
				if (Main.ContainText (contentTextList)) {
						if (string.IsNullOrEmpty (imageName)) {
								SetLayout ((int)Layout.TEXT_ONLY);
						} else {
								SetLayout ((int)Layout.IMAGE_WITH_TEXT);
						}
				} else if (!string.IsNullOrEmpty (imageName))
						SetLayout ((int)Layout.IMAGE_ONLY);

				imageContent = GetComponentInChildren<ImageContent> ();

				if (imageContent != null && !string.IsNullOrEmpty (imageName)) {

						GameObject imageAnimation = LoadCountryPrefab (countryCode, imageName);
						if (imageAnimation != null) {
								imageAnimation.transform.SetParent (gameObject.transform, false);
								SetLayout ((int)Layout.NONE);
						} else {
								Destroy (imageAnimation);
								imageContent.SetSprite (LoadCountryImage (countryCode, imageName));
								scrollableImage = GetComponentInChildren<ScrollableImage> ();
								if (scrollableImage != null) {
										scrollableImage.Reset ();
								}
								AnimationEngine.Instance.animateImage (imageContent.gameObject, 0, delegate {
										ScrollImage (scrollType);
								});
						}
				}

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

				if (string.IsNullOrEmpty (imageName))
						ScrollImage (scrollType);

				multipleText = GetComponentInChildren<MultipleText> ();
				if (multipleText != null) {
						MultipleText.previousTime = 0f;
						multipleText.Text = ContentText.CloneText (contentTextList);
				}
		}

		void ScrollImage (int scrollType)
		{
				if (scrollableImage != null) {
						if (scrollType == (int)AnimationData.SCROLL.PAN)
								scrollableImage.Zoom ();
						else if (scrollType == (int)AnimationData.SCROLL.NORMAL)
								scrollableImage.KenBurns ();
				}
		}

		Sprite LoadCountryImage (string countryCode, string imageName)
		{
				return Resources.Load<Sprite> ("Image/Country/" + countryCode + "/" + imageName);
		}
	
		GameObject LoadCountryPrefab (string countryCode, string imageName)
		{
				GameObject imageAnimation = Resources.Load ("Image/Country/" + countryCode + "/" + imageName) as GameObject;
				if (imageAnimation != null)
						return GameObject.Instantiate (imageAnimation) as GameObject;
				else
						return null;
		}

		public void SetLayout (int layout)
		{
				switch (layout) {
				case (int)Layout.NONE:
						imageOnlyLayout.SetActive (false);
						imageWithTextLayout.SetActive (false);
						textOnlyLayout.SetActive (false);
						qp.gameObject.SetActive (false);
						break;
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
