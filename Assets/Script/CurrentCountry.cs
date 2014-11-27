using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentCountry : MonoBehaviour
{
		Image image;
		bool isShow;
		Sprite currentCountrySprite;
		Sprite logoSprite;

		void Start ()
		{
				isShow = false;
				Logo.FadeOutCompleted += SetShow;
				image = GetComponent<Image> ();
				logoSprite = Resources.Load<Sprite> ("Image/Logo/icon_asean_small");
		}

		void Update ()
		{
				if (isShow) {
						image.enabled = true;
						if (!string.IsNullOrEmpty (Main.selectedCountry)) {
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
