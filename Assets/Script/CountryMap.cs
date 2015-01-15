using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CountryMap : MonoBehaviour
{
		void Start ()
		{
				if (StoryData.storyData.ContainsKey (Main.selectedCountry)) {
						Dictionary<string, StorySet> storyDictionary = StoryData.storyData [Main.selectedCountry];
						foreach (string storyName in storyDictionary.Keys) {
								if (storyDictionary [storyName].lat.HasValue && storyDictionary [storyName].lon.HasValue) {
										GameObject placeObject = GameObject.Instantiate (Resources.Load ("Prefabs/Place")) as GameObject;
										placeObject.transform.SetParent(transform.parent);
										RectTransform rt = placeObject.GetComponent<RectTransform> ();
										rt.anchoredPosition3D = new Vector3 (ConvertCoordinate (storyDictionary [storyName].lon.Value, 100), 0f, ConvertCoordinate (storyDictionary [storyName].lat.Value, 13));
										rt.anchoredPosition = new Vector2 (ConvertCoordinate (storyDictionary [storyName].lon.Value, 100), 0f);
										SelectPlaceButton selectPlaceButton = placeObject.GetComponentInChildren<SelectPlaceButton> ();
										selectPlaceButton.storyName = storyName;
										selectPlaceButton.storyDisplayName = storyDictionary [storyName].displayName;
										foreach (AnimationData animationData in storyDictionary[storyName].animationDataList) {
												if (!string.IsNullOrEmpty (animationData.imageName)) {
														Image image = selectPlaceButton.image.GetComponent<Image> ();
														image.sprite = Resources.Load<Sprite> ("Image/Country/" + Main.selectedCountry + "/" + animationData.imageName);
														break;
												}
										}
								}
						}
				}
		}

		public static float ConvertCoordinate (float original, float relativePosition)
		{
				return original - relativePosition;
		}
}
