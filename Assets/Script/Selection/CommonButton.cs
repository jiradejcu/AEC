using UnityEngine;
using System.Collections;

public class CommonButton : MonoBehaviour
{
		public void OnMouseOver ()
		{
				transform.localScale = new Vector3 (1.2f, 1.2f);
		}
	
		public void OnMouseExit ()
		{
				transform.localScale = new Vector3 (1f, 1f);
		}
}
