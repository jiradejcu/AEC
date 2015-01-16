using UnityEngine;
using System.Collections;

public class SelectTopicButton : CommonButton
{	
		void Start ()
		{
				scaleUp = 1.1f;
		}

		public override void OnMouseDown ()
		{
				base.OnMouseDown ();
				if (false) {
						Main.selectedCountry = StoryData.aecName;
						Main.selectedStory = StoryData.aecName;
						Application.LoadLevel ("Main");
				} else {
						SelectTopic.isSelectingCountry = true;
						Application.LoadLevel ("SelectStory");
				}
		}
}
