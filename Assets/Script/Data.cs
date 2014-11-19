using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class StoryData : Singleton<StoryData>
{
		public static string[] countryCodeList = {"th", "sg", "ab", "cd", "ef", "gh", "ij", "kl", "mn", "op"};
		public delegate void Callback ();

		public Callback callback;
		public static string defaultAnimation = "idle";
		public static Dictionary<string, Dictionary<string, StorySet>> storyData = new Dictionary<string, Dictionary<string, StorySet>> ();

		public void RetrieveData (Callback callback)
		{
				WWWForm form = new WWWForm ();
				string url = CommonConfig.API_URL + "route=api/aec";
				this.callback = callback;
		
				StartCoroutine (ServerEngine.PostData (url, form, new ServerEngine.Callback (RetrieveDataCallback)));
		}

		void RetrieveDataCallback (JSONNode data)
		{
				AnimationData animationData;

				foreach (string countryCode in countryCodeList) {
						if (data ["results"] [countryCode] != null) {
								JSONArray storyDataArray = data ["results"] [countryCode].AsArray;
								storyData [countryCode] = new Dictionary<string, StorySet> ();

								foreach (JSONNode storyDataNode in storyDataArray) {
										StorySet storySet = new StorySet ();
										List<AnimationData> animationDataList = new List<AnimationData> ();
										foreach (JSONNode animationDataNode in storyDataNode["contents"].AsArray) {
												animationData = new AnimationData ();
												animationData.animationName = animationDataNode ["animation_name"].Value;
												animationData.animationLength = animationDataNode ["animation_length"].AsFloat;
												animationData.animationDelay = animationDataNode ["animation_delay"].AsFloat;
												if (animationDataNode ["image_name"].Value != "null")
														animationData.imageName = animationDataNode ["image_name"].Value;
												if (animationDataNode ["sound"].Value != "null")
														animationData.sound = animationDataNode ["sound"].Value;
												if (animationDataNode ["text"].Value != "null")
														animationData.text = animationDataNode ["text"].Value;
												if (animationDataNode ["position_x"].Value != "null")
														animationData.positionX = animationDataNode ["position_x"].AsFloat;
												if (animationDataNode ["scale_x"].Value != "null")
														animationData.scaleX = animationDataNode ["scale_x"].AsFloat;
												animationData.autoProceed = animationDataNode ["auto_proceed"].AsBool;
												animationDataList.Add (animationData);
										}
										storySet.animationDataList = animationDataList;
										storySet.bgm = storyDataNode ["bgm"];
										storyData [countryCode] [storyDataNode ["name"]] = storySet;
								}
						}
				}

				callback ();
		}
}

public class AnimationData
{
		public string animationName;
		public float animationLength;
		public float animationDelay;
		public float? positionX;
		public float? scaleX;
		public string imageName;
		public string sound;
		public string text;
		public bool autoProceed;
}

public class StorySet
{
		public List<AnimationData> animationDataList;
		public string bgm;
}