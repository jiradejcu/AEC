using UnityEngine;
using System.Collections;

public class Back : MonoBehaviour
{
		void Update ()
		{
				if (Application.loadedLevelName.Equals ("SelectStory") && string.IsNullOrEmpty (Main.selectedCountry)) {
						renderer.enabled = false;
				} else
						renderer.enabled = true;
		}

		void OnMouseDown ()
		{
				if (Application.loadedLevelName.Equals ("Main"))
						Application.LoadLevel ("SelectStory");
				else if (Application.loadedLevelName.Equals ("SelectStory") && !string.IsNullOrEmpty (Main.selectedCountry)) {
						Main.selectedCountry = "";
						Application.LoadLevel ("SelectStory");
				}
		}
}
