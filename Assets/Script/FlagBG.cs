using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlagBG : MonoBehaviour
{
		float nextShow;
		Animator anim;
		public string country;

		void Start ()
		{
				SetNextShow (5f);
				anim = GetComponentInChildren<Animator> ();
				Image image = GetComponentInChildren<Image> ();
				image.sprite = Resources.Load<Sprite> ("Image/Country/" + country + "/flag");
		
				if (SelectTopic.isSelectingCountry) {
						Image map = GetComponentInParent<Image> ();
						map.gameObject.SetActive (false);
				}
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

		void SetNextShow (float max)
		{
				nextShow = Random.Range (3f, max);
		}
}
