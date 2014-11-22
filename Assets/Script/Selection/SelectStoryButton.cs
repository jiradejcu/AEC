using UnityEngine;
using System.Collections;

public class SelectStoryButton : MonoBehaviour
{
		public string storyName;
		public string storyDisplayName;

		void Start ()
		{
				TextMesh text = GetComponentInChildren<TextMesh> ();
				text.text = storyDisplayName;
		}
	
		void OnMouseDown ()
		{
				Debug.Log (storyName);
				Main.selectedStory = storyName;
				SelectStory.ClearSelectStoryButton ();
		}
}
