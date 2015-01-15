using UnityEngine;
using System.Collections;

public class SelectTopic : MonoBehaviour
{
		public static bool isSelectingCountry = false;
		public GameObject topicButton;

		void Start ()
		{
				if (!isSelectingCountry)
						Logo.FadeOutCompleted += CreateSelectTopicButton;
		}

		void CreateSelectTopicButton ()
		{
				topicButton.SetActive (true);
				Logo.FadeOutCompleted -= CreateSelectTopicButton;
		}

		public void SetSelectCountry ()
		{
				isSelectingCountry = true;
				Application.LoadLevel ("SelectStory");
		}
}
