using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectStory : CommonSelect
{
		static GameObject[] buttonObjectList;
	
		void Start ()
		{
				scaleHeightCoeff = 1.8f;
				SelectCountry.FadeOutCompleted += CreateSelectStoryButton;
		}
	
		void CreateSelectStoryButton ()
		{
				if (StoryData.storyData.ContainsKey (Main.selectedCountry)) {
						Dictionary<string, StorySet> storyDictionary = StoryData.storyData [Main.selectedCountry];
						buttonObjectList = new GameObject[storyDictionary.Count];
						int i = 0;
						foreach (string storyName in storyDictionary.Keys) {
								GameObject buttonObject = CreateSelectButton ("SelectStoryButton", i, storyDictionary.Count);
								buttonObjectList [i] = buttonObject;
								SelectStoryButton selectStoryButton = buttonObject.GetComponent<SelectStoryButton> ();
								selectStoryButton.storyName = storyName;
								selectStoryButton.storyDisplayName = storyDictionary [storyName].displayName;
								foreach (AnimationData animationData in storyDictionary[storyName].animationDataList) {
										if (!string.IsNullOrEmpty (animationData.imageName)) {
												SpriteRenderer sr = buttonObject.GetComponentInChildren<SpriteRenderer> ();
												sr.sprite = Resources.Load<Sprite> ("Country/" + Main.selectedCountry + "/" + animationData.imageName);
												sr.transform.localScale = new Vector3 (width / sr.bounds.size.x, height / sr.bounds.size.y);
												break;
										}
								}
								i++;
						}
				}
				SelectCountry.FadeOutCompleted -= CreateSelectStoryButton;
		}
	
		public static void ClearSelectStoryButton ()
		{
				foreach (GameObject buttonObject in buttonObjectList)
						Destroy (buttonObject);
				Application.LoadLevel ("Main");
		}
}
