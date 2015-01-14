using UnityEngine;
using System.Collections;

public class SelectCountryButton : CommonButton
{
		public string countryCode;
	
		void Start ()
		{
				TextMesh text = GetComponentInChildren<TextMesh> ();
				text.text = StoryData.countryNameList [countryCode];
		}

		public override void OnMouseDown ()
		{
				base.OnMouseDown ();
				Debug.Log (countryCode);
				Main.selectedCountry = countryCode;
				SelectCountry.ClearSelectCountryButton ();
		}
}
