using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CurrentCountry : MonoBehaviour
{
		Image image;
		static bool isShow = false;
		Sprite currentCountrySprite;
		Sprite logoSprite;
		string selectedStory;
		public SelectStory selectStory;

		void Start ()
		{
				Logo.FadeOutCompleted += SetShow;
				image = GetComponent<Image> ();
				logoSprite = Resources.Load<Sprite> ("Image/Logo/icon_asean_small");
		}

		void Update ()
		{
				if (isShow) {
						image.enabled = true;
						if (!string.IsNullOrEmpty (Main.selectedCountry) && Main.selectedCountry != StoryData.aecName) {
								if (currentCountrySprite == null || currentCountrySprite.name != Main.selectedCountry) {
										currentCountrySprite = Resources.Load<Sprite> ("Image/Button/" + Main.selectedCountry);
										image.sprite = currentCountrySprite;
								}
						} else {
								image.sprite = logoSprite;
						}
				} else
						image.enabled = false;
		}
	
		void SetShow ()
		{
				isShow = true;
		}
}
