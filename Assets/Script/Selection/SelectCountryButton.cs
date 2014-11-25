using UnityEngine;
using System.Collections;

public class SelectCountryButton : MonoBehaviour
{
		public string countryCode;
	
		void Start ()
		{
				TextMesh text = GetComponentInChildren<TextMesh> ();
				text.text = StoryData.countryNameList [countryCode];
		}

		void OnMouseDown ()
		{
				Debug.Log (countryCode);
				Main.selectedCountry = countryCode;
				SelectCountry.ClearSelectCountryButton ();
		}
}
