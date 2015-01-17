using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollableImage : MonoBehaviour
{
		ScrollRect scrollRect;
		public Image image;
		static float speedCoeff = 0.00006f;
		static float kenBurnsSpeed = 0.001f;
		static float zoomTime = 2.5f;
		public static float animationLength;
		float speed;
		bool vertical = true;
		bool scroll;

		void Start ()
		{
				scrollRect = GetComponent<ScrollRect> ();
		}

		void Update ()
		{
				if (scroll) {
						if (vertical)
								scrollRect.verticalNormalizedPosition -= speed;
						else
								scrollRect.horizontalNormalizedPosition += speed;
				}
		}

		public void Reset ()
		{
				scroll = false;

				if (vertical) {
						image.rectTransform.pivot = new Vector2 (0.5f, 1f);
				} else {
						image.rectTransform.pivot = new Vector2 (0f, 0.5f);
				}
				image.rectTransform.anchoredPosition = Vector2.zero;
				image.rectTransform.localScale = new Vector2 (1f, 1f);
		
				scrollRect.horizontalNormalizedPosition = image.rectTransform.pivot.x;
				scrollRect.verticalNormalizedPosition = image.rectTransform.pivot.y;
		}

		public void Zoom ()
		{
				if (vertical)
						speed = image.rectTransform.rect.height / (animationLength - zoomTime);
				else
						speed = image.rectTransform.rect.width / (animationLength - zoomTime);

				speed *= speedCoeff;

				iTween.ScaleTo (image.rectTransform.gameObject, iTween.Hash ("scale", new Vector3 (2f, 2f), "time", zoomTime, "oncomplete", "StartScroll", "oncompletetarget", gameObject));
		}
	
		public void KenBurns ()
		{
				speed = kenBurnsSpeed;
				iTween.ScaleTo (image.rectTransform.gameObject, iTween.Hash ("scale", new Vector3 (1.5f, 1.5f), "time", animationLength, "easetype", iTween.EaseType.linear));
				StartScroll ();
		}

		private void StartScroll ()
		{
				scroll = true;
		}
}
