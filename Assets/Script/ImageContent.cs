using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageContent : MonoBehaviour
{
		Image image;

		void Start ()
		{
				image = GetComponent<Image> ();
		}

		public Sprite sprite {
				set {
						image.sprite = value;
				}
		}
}
