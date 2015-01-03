using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageContent : MonoBehaviour
{
		protected Image image;

		void Start ()
		{
				image = GetComponent<Image> ();
		}

		public void SetSprite (Sprite sprite)
		{
				image.sprite = sprite;
		}
}
