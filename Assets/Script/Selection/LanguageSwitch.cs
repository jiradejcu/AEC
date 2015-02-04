using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LanguageSwitch : MonoBehaviour
{
		CanvasGroup cg;
		Toggle[] toggles;

		void Start ()
		{
				cg = GetComponent<CanvasGroup> ();
				toggles = GetComponentsInChildren<Toggle> ();
				string lang = PlayerPrefs.GetString ("language");
				foreach (Toggle toggle in toggles)
						toggle.isOn = toggle.name.Equals (lang);
		}

		void Update ()
		{
				if (SelectTopic.isSelectingCountry) {
						cg.alpha = 1f;
				} else
						cg.alpha = 0f;

				cg.interactable = !Application.loadedLevelName.Equals ("Main");
		}

		public void SetLanguage (string lang)
		{
				LanguageSetting.SetLanguage (lang);
		}
}
