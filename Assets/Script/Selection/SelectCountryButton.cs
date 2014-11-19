using UnityEngine;
using System.Collections;

public class SelectCountryButton : MonoBehaviour
{
		public string countryCode;

		void OnMouseDown ()
		{
				Debug.Log (countryCode);
				Main.selectedCountry = countryCode;
				Application.LoadLevel ("Main");
		}
}
