﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectStory : CommonSelect
{
		static GameObject[] buttonObjectList;
		public GameObject placeButton;
	
		void Start ()
		{
				if (Main.selectedCountry == StoryData.aecName)
						columnCount = new int[]{1, 3};
				else
						columnCount = new int[]{2, 2};
				scaleHeightCoeff = 1.8f;
				if (SelectTopic.isSelectingCountry && !string.IsNullOrEmpty (Main.selectedCountry))
						Logo.FadeOutCompleted += CreateSelectStoryButton;
		}
	
		public void CreateSelectStoryButton ()
		{
				if (StoryData.storyData.ContainsKey (Main.selectedCountry)) {
						Dictionary<string, StorySet> storyDictionary = StoryData.storyData [Main.selectedCountry];
						buttonObjectList = new GameObject[storyDictionary.Count];
						int i = 0;
						placeButton.SetActive (false);
						foreach (string storyName in storyDictionary.Keys) {
								if (!storyDictionary [storyName].lat.HasValue || !storyDictionary [storyName].lon.HasValue) {
										if (!CommonConfig.ASEAN_TOPIC_LIST.Contains (storyName)) {
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
												AnimationEngine.Instance.addButtonShadow (buttonObject, i, width, height);
												i++;
										}
								} else {
										placeButton.SetActive (true);
								}
						}
				}
				Logo.FadeOutCompleted -= CreateSelectStoryButton;
		}
	
		public static void ClearSelectStoryButton ()
		{
				foreach (GameObject buttonObject in buttonObjectList)
						Destroy (buttonObject);
				Application.LoadLevel ("Main");
		}
}
