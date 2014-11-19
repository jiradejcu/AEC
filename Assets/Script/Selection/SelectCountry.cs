using UnityEngine;
using System.Collections;

public class SelectCountry : MonoBehaviour
{
		static int rowCount = 2;
		static float offsetX = -5f;
		static float offsetY = 3f;
		static float width = 2.5f;
		static float height = 2f;

		void Start ()
		{
				Logo.FadeOutCompleted += CreateSelectCountryButton;
		}

		void CreateSelectCountryButton ()
		{
				for (int i = 0; i< StoryData.countryCodeList.Length; i++) {
						GameObject buttonObject = GameObject.Instantiate (Resources.Load ("Prefabs/SelectCountryButton"),
			                        new Vector3 (i % (StoryData.countryCodeList.Length / rowCount) * width + offsetX, offsetY - i / (StoryData.countryCodeList.Length / rowCount) * height)
			                                                  , transform.rotation) as GameObject;
						buttonObject.GetComponent<SelectCountryButton> ().countryCode = StoryData.countryCodeList [i];
						buttonObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Image/button/" + StoryData.countryCodeList [i]);
				}
		}
}
