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
										placeObject.transform.SetParent (transform.parent);
										RectTransform rt = placeObject.GetComponent<RectTransform> ();
										Vector2 convertedCoordinate = ConvertCoordinate (new Vector2 (storyDictionary [storyName].lat.Value, storyDictionary [storyName].lon.Value), Main.selectedCountry);
										rt.anchoredPosition3D = new Vector3 (convertedCoordinate.y, 0f, convertedCoordinate.x);
										rt.anchoredPosition = new Vector2 (convertedCoordinate.y, 0f);
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

		public static Vector2 ConvertCoordinate (Vector2 original, string countryCode)
		{
				Vector2 relativeCoordinate = CommonConfig.CENTRAL_COORDINATE [countryCode];
				float scale = CommonConfig.MAP_SCALE [countryCode];
				return new Vector2 ((original.x - relativeCoordinate.x) * scale, (original.y - relativeCoordinate.y) * scale);
		}
}
