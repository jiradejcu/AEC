using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollableImage : MonoBehaviour
{
		ScrollRect scrollRect;
		public Image image;

		void Start ()
		{
				scrollRect = GetComponent<ScrollRect> ();
//				image.transform.localScale = new Vector3 (2f, 2f);
//				scrollRect.horizontalScrollbar.value = 0;
		}

		void Update ()
		{
	
		}
}
