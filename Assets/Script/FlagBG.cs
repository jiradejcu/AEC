using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlagBG : MonoBehaviour
{
		float nextShow = float.MaxValue;
		Animator anim;
		public string country;

		void Start ()
		{
				anim = GetComponentInChildren<Animator> ();
				Image image = GetComponentInChildren<Image> ();
				image.sprite = Resources.Load<Sprite> ("Image/Country/" + country + "/flag");
		
				if (SelectTopic.isSelectingCountry) {
						Image map = GetComponentInParent<Image> ();
						map.gameObject.SetActive (false);
				}
				Logo.FadeOutCompleted += SetFirstShow;
		}

		void Update ()
		{
				if (!SelectTopic.isSelectingCountry) {
						if (nextShow < 0) {
								SetNextShow (20f);
								anim.SetTrigger ("active");
						} else
								nextShow -= Time.deltaTime;
				}
		}

		void SetFirstShow ()
		{
				SetNextShow (3f);
				Logo.FadeOutCompleted -= SetFirstShow;
		}

		void SetNextShow (float max)
		{
				float min = 3f;
				if (max <= min)
						min = 0f;
				nextShow = Random.Range (min, max);
		}
}
