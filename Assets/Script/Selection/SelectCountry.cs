using UnityEngine;
using System;
using System.Collections;

public class SelectCountry : CommonSelect
{
		static GameObject[] buttonObjectList;
	
		public delegate void Callback ();
	
		public static event Callback FadeOutCompleted;

		void Start ()
		{
				columnCount = new int[]{3, 4, 3};
				if (string.IsNullOrEmpty (Main.selectedCountry))
						Logo.FadeOutCompleted += CreateSelectCountryButton;
		}

		void CreateSelectCountryButton ()
		{
				string[] countryCodeList = Enum.GetNames (typeof(CommonConfig.COUNTRY_CODE));
				buttonObjectList = new GameObject[countryCodeList.Length];
				for (int i = 0; i< buttonObjectList.Length; i++) {
						GameObject buttonObject = CreateSelectButton ("SelectCountryButton", i, countryCodeList.Length);
						buttonObjectList [i] = buttonObject;
						buttonObject.GetComponent<SelectCountryButton> ().countryCode = ((CommonConfig.COUNTRY_CODE)(i + 1)).ToString ();
						buttonObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Image/Button/" + ((CommonConfig.COUNTRY_CODE)(i + 1)).ToString ());
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
