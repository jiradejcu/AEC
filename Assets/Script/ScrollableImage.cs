using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollableImage : MonoBehaviour
{
		ScrollRect scrollRect;
		public Image image;
		float speed = 0.001f;
		bool vertical = true;
		bool scroll;

		void Start ()
		{
				scrollRect = GetComponent<ScrollRect> ();
				scroll = false;
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
				if (vertical) {
						image.rectTransform.pivot = new Vector2 (0.5f, 1f);
				} else {
						image.rectTransform.pivot = new Vector2 (0f, 0.5f);
				}
				image.rectTransform.anchoredPosition = Vector2.zero;
				iTween.ScaleTo (image.rectTransform.gameObject, iTween.Hash ("scale", new Vector3 (2f, 2f), "time", 2.5f, "oncomplete", "StartScroll", "oncompletetarget", gameObject));
		
				scrollRect.horizontalNormalizedPosition = image.rectTransform.pivot.x;
				scrollRect.verticalNormalizedPosition = image.rectTransform.pivot.y;
		}

		private void StartScroll ()
		{
				scroll = true;
		}
}
