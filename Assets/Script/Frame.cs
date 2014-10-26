using UnityEngine;
using System.Collections;

public class Frame : MonoBehaviour
{

		private SpriteRenderer sr;

		void Start ()
		{
				sr = GetComponent<SpriteRenderer> ();
				sr.transform.localScale = new Vector3 (9.0f / sr.bounds.size.x, 6.0f / sr.bounds.size.y);
		}

}
