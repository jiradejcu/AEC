using UnityEngine;
using System.Collections;

public class SelectTopicButton : CommonButton
{
		public string storyName;

		void Start ()
		{
				scaleUp = 1.1f;
		}

		public override void OnMouseDown ()
		{
				base.OnMouseDown ();
				if (!string.IsNullOrEmpty (storyName)) {
						Main.selectedCountry = StoryData.aecName;
						if (storyName == CommonConfig.ASEAN_MAIN_TOPIC) {
								SelectTopic.isSelectingCountry = true;
								Application.LoadLevel ("SelectStory");
						} else {
								Main.selectedStory = storyName;
								Application.LoadLevel ("Main");
						}
				} else {
						SelectTopic.isSelectingCountry = true;
						Application.LoadLevel ("SelectStory");
				}
		}
}
