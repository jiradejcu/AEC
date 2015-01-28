using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CountryMap : MonoBehaviour
{
		public static float animationDelay = 0.5f;
		public static float animationTime = 1.5f;
		public static float placeInterval = 0.15f;

		void Start ()
		{
				bool isAnimate = false;
				Hashtable ht = iTween.Hash ("time", animationTime, "delay", animationDelay, "easetype", iTween.EaseType.easeInQuad);
				if (CommonConfig.MAP_OFFSET_SCALE.ContainsKey (Main.selectedCountry)) {
						ht.Add ("scale", new Vector3 (CommonConfig.MAP_OFFSET_SCALE [Main.selectedCountry], CommonConfig.MAP_OFFSET_SCALE [Main.selectedCountry]));
						iTween.ScaleTo (gameObject, ht);
						isAnimate = true;
				}
		
				if (CommonConfig.CENTRAL_COORDINATE_OFFSET.ContainsKey (Main.selectedCountry)) {
						ht.Remove ("scale");
						ht.Add ("position", new Vector3 (CommonConfig.CENTRAL_COORDINATE_OFFSET [Main.selectedCountry].y, 0f, CommonConfig.CENTRAL_COORDINATE_OFFSET [Main.selectedCountry].x));
						ht.Add ("islocal", true);
						iTween.MoveTo (gameObject, ht);
						isAnimate = true;
				}

				if (StoryData.storyData.ContainsKey (Main.selectedCountry)) {
						Dictionary<string, StorySet> storyDictionary = StoryData.storyData [Main.selectedCountry];
						int i = 0;
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

										float delay = animationDelay + i * placeInterval;
										if (isAnimate)
												delay += animationTime;
										selectPlaceButton.delay = delay;

										foreach (AnimationData animationData in storyDictionary[storyName].animationDataList) {
												if (!string.IsNullOrEmpty (animationData.imageName)) {
														Image image = selectPlaceButton.image.GetComponent<Image> ();
														image.sprite = Resources.Load<Sprite> ("Image/Country/" + Main.selectedCountry + "/" + animationData.imageName);
														break;
												}
										}
										i++;
								}
						}
				}
		}

		public static Vector2 ConvertCoordinate (Vector2 original, string countryCode)
		{
				Vector2 relativeCoordinate = CommonConfig.CENTRAL_COORDINATE [countryCode];
				float scale = CommonConfig.MAP_SCALE [countryCode];
				Vector2 result = new Vector2 ((original.x - relativeCoordinate.x) * scale, (original.y - relativeCoordinate.y) * scale);

				if (CommonConfig.MAP_OFFSET_SCALE.ContainsKey (countryCode)) {
						result.Scale (new Vector2 (CommonConfig.MAP_OFFSET_SCALE [countryCode], CommonConfig.MAP_OFFSET_SCALE [countryCode]));
				}

				if (CommonConfig.CENTRAL_COORDINATE_OFFSET.ContainsKey (countryCode)) {
						result += CommonConfig.CENTRAL_COORDINATE_OFFSET [countryCode];
				}

				return result;
		}
}
