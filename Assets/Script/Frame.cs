using UnityEngine;
using System.Collections;

public class Frame : MonoBehaviour
{
		private SpriteRenderer sr;

		void Start ()
		{
				sr = GetComponent<SpriteRenderer> ();
		}

		public void SetImage (string imageName)
		{
				sr.transform.localScale = new Vector3 (1f, 1f);
				sr.sprite = Resources.Load<Sprite> ("Image/Thailand/" + imageName);
				sr.transform.localScale = new Vector3 (9.0f / sr.bounds.size.x, 6.0f / sr.bounds.size.y);
		}
}
