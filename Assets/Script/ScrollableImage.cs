using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollableImage : MonoBehaviour
{
		ScrollRect scrollRect;
		public Image image;
		static float speedCoeff = 0.000065f;
		static float zoomTime = 2.5f;
		public static float animationLength;
		float speed;
		bool vertical = true;
		bool scroll;
		bool scrollable;

		void Start ()
		{
				scrollRect = GetComponent<ScrollRect> ();
		}

		void Update ()
		{
				if (scroll && scrollable) {
						if (vertical)
								scrollRect.verticalNormalizedPosition -= speed;
						else
								scrollRect.horizontalNormalizedPosition += speed;
				}
		}

		public void Reset ()
		{
				scroll = false;
				scrollable = false;

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
				scrollable = true;
		
				if (vertical)
						speed = image.rectTransform.rect.height / (animationLength - zoomTime);
				else
						speed = image.rectTransform.rect.width / (animationLength - zoomTime);

				speed *= speedCoeff;

				iTween.ScaleTo (image.rectTransform.gameObject, iTween.Hash ("scale", new Vector3 (2f, 2f), "time", zoomTime, "oncomplete", "StartScroll", "oncompletetarget", gameObject));
		}

		private void StartScroll ()
		{
				scroll = true;
		}
}
