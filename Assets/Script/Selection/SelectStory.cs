using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectStory : CommonSelect
{
		static GameObject[] buttonObjectList;
	
		void Start ()
		{
				SelectCountry.FadeOutCompleted += CreateSelectStoryButton;
		}
	
		void CreateSelectStoryButton ()
		{
				Dictionary<string, StorySet> storyDictionary = StoryData.storyData [Main.selectedCountry];
				buttonObjectList = new GameObject[storyDictionary.Count];
				int i = 0;
				foreach (string storyName in storyDictionary.Keys) {
						GameObject buttonObject = CreateSelectButton ("SelectStoryButton", i, storyDictionary.Count);
						buttonObjectList [i] = buttonObject;
						buttonObject.GetComponent<SelectStoryButton> ().storyName = storyName;
						foreach (AnimationData animationData in storyDictionary[storyName].animationDataList) {
								if (!string.IsNullOrEmpty (animationData.imageName)) {
										SpriteRenderer sr = buttonObject.GetComponent<SpriteRenderer> ();
										BoxCollider2D bc = buttonObject.GetComponent<BoxCollider2D> ();
										sr.sprite = Resources.Load<Sprite> ("Country/" + Main.selectedCountry + "/" + animationData.imageName);
										sr.transform.localScale = new Vector3 (width / sr.bounds.size.x, height / sr.bounds.size.y);
										bc.size = new Vector2 (bc.size.x / sr.transform.localScale.x, bc.size.y / sr.transform.localScale.y);
										break;
								}
						}
						i++;
				}
		}
	
		public static void ClearSelectStoryButton ()
		{
				foreach (GameObject buttonObject in buttonObjectList)
						Destroy (buttonObject);
				Application.LoadLevel ("Main");
		}
}
