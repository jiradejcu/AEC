using UnityEngine;
using System.Collections;

public class Frame : MonoBehaviour
{
		private SpriteRenderer sr;
		public QuestionPanel qp;
		float width;
		float height;
		static float scale = 0.95f;

		void Start ()
		{
				sr = GetComponentInChildren<SpriteRenderer> ();
				SpriteRenderer parent_sr = transform.parent.GetComponent<SpriteRenderer> ();
				width = parent_sr.bounds.size.x * scale;
				height = parent_sr.bounds.size.y * scale;
		}

		public void SetImage (string countryCode, string imageName)
		{
				sr.renderer.enabled = true;
				qp.gameObject.SetActive (false);
				if (!string.IsNullOrEmpty (imageName)) {
						sr.transform.localScale = new Vector3 (1f, 1f);
						sr.sprite = Resources.Load<Sprite> ("Image/Country/" + countryCode + "/" + imageName);
						sr.transform.localScale = new Vector3 (width / sr.bounds.size.x, height / sr.bounds.size.y);
				}
		}

		public void SetQuestion (Question question)
		{
				sr.renderer.enabled = false;
				qp.gameObject.SetActive (true);
				qp.SetQuestion (question);
		}
}
