using UnityEngine;
using System.Collections;

public class SelectCountry : CommonSelect
{
		static GameObject[] buttonObjectList;
	
		public delegate void Callback ();
	
		public static event Callback FadeOutCompleted;

		void Start ()
		{
				if (string.IsNullOrEmpty (Main.selectedCountry))
						Logo.FadeOutCompleted += CreateSelectCountryButton;
		}

		void CreateSelectCountryButton ()
		{
				buttonObjectList = new GameObject[StoryData.countryCodeList.Length];
				for (int i = 0; i< StoryData.countryCodeList.Length; i++) {
						GameObject buttonObject = CreateSelectButton ("SelectCountryButton", i, StoryData.countryCodeList.Length);
						buttonObjectList [i] = buttonObject;
						buttonObject.GetComponent<SelectCountryButton> ().countryCode = StoryData.countryCodeList [i];
						buttonObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Image/Button/" + StoryData.countryCodeList [i]);
				}
				Logo.FadeOutCompleted -= CreateSelectCountryButton;
		}

		public static void ClearSelectCountryButton ()
		{
				foreach (GameObject buttonObject in buttonObjectList)
						Destroy (buttonObject);
				FadeOutCompleted.Invoke ();
		}
}
