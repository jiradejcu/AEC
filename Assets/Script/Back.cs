using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class Back : MonoBehaviour
{
		Button button;
		Text text;

		void Start ()
		{
				button = GetComponent<Button> ();
				text = GetComponentInChildren<Text> ();
		}

		void Update ()
		{
				if (Application.loadedLevelName.Equals ("SelectStory") && !Logo.isPlayed) {
						button.enabled = false;
						button.image.enabled = false;
						text.enabled = false;
				} else {
						button.enabled = true;
						button.image.enabled = true;
						text.enabled = true;
						if (Application.loadedLevelName.Equals ("SelectStory") && string.IsNullOrEmpty (Main.selectedCountry) && !SelectTopic.isSelectingCountry)
								text.text = "Quit";
						else
								text.text = "Back";
				}
		}

		public void OnClick ()
		{
				if (Application.loadedLevelName.Equals ("Main") || Application.loadedLevelName.Equals ("SelectPlace")) {

						if (CommonConfig.ASEAN_TOPIC_LIST.Contains (Main.selectedStory)) {
								Main.selectedCountry = "";
								SelectTopic.isSelectingCountry = false;
						}
						Application.LoadLevel ("SelectStory");

				} else if (Application.loadedLevelName.Equals ("SelectStory")) {
			
						if (Main.selectedCountry == StoryData.aecName)
								Main.selectedCountry = "";

						if (!string.IsNullOrEmpty (Main.selectedCountry)) {
								Main.selectedCountry = "";
								Application.LoadLevel ("SelectStory");
						} else {
								if (SelectTopic.isSelectingCountry) {
										SelectTopic.isSelectingCountry = false;
										Application.LoadLevel ("SelectStory");
								} else
										Application.Quit ();
						}
				}
		}
}
