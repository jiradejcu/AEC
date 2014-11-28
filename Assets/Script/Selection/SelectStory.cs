using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectStory : CommonSelect
{
		static GameObject[] buttonObjectList;
	
		void Start ()
		{
				columnCount = new int[]{2, 2};
				scaleHeightCoeff = 1.8f;
				if (string.IsNullOrEmpty (Main.selectedCountry))
						SelectCountry.FadeOutCompleted += CreateSelectStoryButton;
				else
						Logo.FadeOutCompleted += CreateSelectStoryButton;
		}
	
		public void CreateSelectStoryButton ()
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
												sr.sprite = Resources.Load<Sprite> ("Image/Country/" + Main.selectedCountry + "/" + animationData.imageName);
												sr.transform.localScale = new Vector3 (width / sr.bounds.size.x, height / sr.bounds.size.y);
												break;
										}
								}
								GameObject shadowObject = GameObject.Instantiate (Resources.Load ("Prefabs/Shadow")) as GameObject;
								SpriteRenderer shadow = shadowObject.GetComponent<SpriteRenderer> ();
								shadowObject.transform.parent = buttonObject.transform;
								shadowObject.transform.localPosition = new Vector3 (0.05f, -0.05f);
								shadowObject.transform.localScale = new Vector3 (width / shadow.bounds.size.x, height / shadow.bounds.size.y);
								iTween.FadeFrom (shadowObject, iTween.Hash ("alpha", 0, "time", 0.5f, "delay", i * delayInterval + 0.5f));
								i++;
						}
				}
				SelectCountry.FadeOutCompleted -= CreateSelectStoryButton;
				Logo.FadeOutCompleted -= CreateSelectStoryButton;
		}
	
		public static void ClearSelectStoryButton ()
		{
				foreach (GameObject buttonObject in buttonObjectList)
						Destroy (buttonObject);
				Application.LoadLevel ("Main");
		}
}
