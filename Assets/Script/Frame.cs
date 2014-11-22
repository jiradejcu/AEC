using UnityEngine;
using System.Collections;

public class Frame : MonoBehaviour
{
		private SpriteRenderer sr;
		float width;
		float height;
		static float scale = 0.95f;

		void Start ()
		{
				sr = GetComponent<SpriteRenderer> ();
				SpriteRenderer parent_sr = transform.parent.GetComponent<SpriteRenderer> ();
				width = parent_sr.bounds.size.x * scale;
				height = parent_sr.bounds.size.y * scale;
		}

		public void SetImage (string countryCode, string imageName)
		{
				if (!string.IsNullOrEmpty (imageName)) {
						sr.transform.localScale = new Vector3 (1f, 1f);
						sr.sprite = Resources.Load<Sprite> ("Country/" + countryCode + "/" + imageName);
						sr.transform.localScale = new Vector3 (width / sr.bounds.size.x, height / sr.bounds.size.y);
				}
		}
}
