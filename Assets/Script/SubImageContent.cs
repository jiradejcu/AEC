using UnityEngine;
using System.Collections;

public class SubImageContent : ImageContent
{
		public void SetSprite (Sprite sprite, float delay)
		{
				image.enabled = false;
				StartCoroutine (doSetSprite (sprite, delay));
		}

		IEnumerator doSetSprite (Sprite sprite, float delay)
		{
				yield return new WaitForSeconds (delay);
				image.enabled = true;
				image.sprite = sprite;
				AnimationEngine.Instance.animateSubImage (gameObject);
		}

		public void ClearSprite ()
		{
				image.enabled = false;
				image.sprite = null;
		}
}
