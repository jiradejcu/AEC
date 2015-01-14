using UnityEngine;
using System.Collections;

public class SelectStoryButton : CommonButton
{
		public string storyName;
		public string storyDisplayName;

		void Start ()
		{
				TextMesh text = GetComponentInChildren<TextMesh> ();
				text.text = storyDisplayName;
		}
	
		public override void OnMouseDown ()
		{
				base.OnMouseDown ();
				Debug.Log (storyName);
				Main.selectedStory = storyName;
				SelectStory.ClearSelectStoryButton ();
		}
}
