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
				if (Application.loadedLevelName.Equals ("SelectStory") && string.IsNullOrEmpty (Main.selectedCountry)) {
						button.enabled = false;
						button.image.enabled = false;
						text.enabled = false;
				} else {
						button.enabled = true;
						button.image.enabled = true;
						text.enabled = true;
				}
		}

		public void OnClick ()
		{
				if (Application.loadedLevelName.Equals ("Main"))
						Application.LoadLevel ("SelectStory");
				else if (Application.loadedLevelName.Equals ("SelectStory") && !string.IsNullOrEmpty (Main.selectedCountry)) {
						Main.selectedCountry = "";
						Application.LoadLevel ("SelectStory");
				}
		}
}
