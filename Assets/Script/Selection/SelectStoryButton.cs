using UnityEngine;
using System.Collections;

public class SelectStoryButton : MonoBehaviour
{
		public string storyName;
	
		void OnMouseDown ()
		{
				Debug.Log (storyName);
				Main.selectedStory = storyName;
				SelectStory.ClearSelectStoryButton ();
		}
}
