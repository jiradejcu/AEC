using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollableImage : MonoBehaviour
{
		ScrollRect scrollRect;
		public Image image;
		float speed = 0.001f;
		bool vertical = true;

		void Start ()
		{
				scrollRect = GetComponent<ScrollRect> ();
				Reset ();
		}

		void Update ()
		{
				if (vertical)
						scrollRect.verticalNormalizedPosition -= speed;
				else
						scrollRect.horizontalNormalizedPosition += speed;
		}

		public void Reset ()
		{
				if (vertical) {
						image.rectTransform.pivot = new Vector2 (0.5f, 1f);
				} else {
						image.rectTransform.pivot = new Vector2 (0f, 0.5f);
				}
				image.rectTransform.anchoredPosition = Vector2.zero;
				image.rectTransform.localScale = new Vector3 (2f, 2f);
		
				scrollRect.horizontalNormalizedPosition = image.rectTransform.pivot.x;
				scrollRect.verticalNormalizedPosition = image.rectTransform.pivot.y;
		}
}
