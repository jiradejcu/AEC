using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class StoryData : Singleton<StoryData>
{
		public static string[] countryCodeList = {
			"bn",
			"kh",
			"id",
			"la",
			"my",
			"mm",
			"ph",
			"sg",
			"th",
			"vn"
		};
		public static Dictionary<string, string> countryNameList = new Dictionary<string, string> ();
		public delegate void Callback ();

		public Callback callback;
		public static string defaultAnimation = "idle";
		public static Dictionary<string, Dictionary<string, StorySet>> storyData = new Dictionary<string, Dictionary<string, StorySet>> ();
		public static Dictionary<string, Dictionary<string, List<Question>>> questionData = new Dictionary<string, Dictionary<string, List<Question>>> ();
	
		public StoryData ()
		{
				countryNameList.Add (countryCodeList [0], "Brunei");
				countryNameList.Add (countryCodeList [1], "Cambodia");
				countryNameList.Add (countryCodeList [2], "Indonesia");
				countryNameList.Add (countryCodeList [3], "Lao");
				countryNameList.Add (countryCodeList [4], "Malaysia");
				countryNameList.Add (countryCodeList [5], "Myanmar");
				countryNameList.Add (countryCodeList [6], "Philipines");
				countryNameList.Add (countryCodeList [7], "Singapore");
				countryNameList.Add (countryCodeList [8], "Thai");
				countryNameList.Add (countryCodeList [9], "Vietnam");
		}

		public void RetrieveData (Callback callback = null)
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
												animationData.character = animationDataNode ["character"].Value;
												animationData.animationName = animationDataNode ["animation_name"].Value;
												animationData.animationLength = animationDataNode ["animation_length"].AsFloat;
												animationData.animationDelay = animationDataNode ["animation_delay"].AsFloat;
												if (!string.IsNullOrEmpty (animationDataNode ["image_name"].Value))
														animationData.imageName = animationDataNode ["image_name"].Value;
												if (!string.IsNullOrEmpty (animationDataNode ["sound"].Value))
														animationData.sound = animationDataNode ["sound"].Value;
												JSONArray textArray = animationDataNode ["text"].AsArray;
												animationData.text = new List<SubtitleText> ();
												foreach (JSONNode textNode in textArray) {
														SubtitleText subtitleText = new SubtitleText ();
														subtitleText.time = textNode ["time"].AsFloat;
														subtitleText.text = textNode ["text"].Value;
														animationData.text.Add (subtitleText);
												}
												if (animationDataNode ["position_x"].Value != "null")
														animationData.positionX = animationDataNode ["position_x"].AsFloat;
												if (animationDataNode ["scale_x"].Value != "null")
														animationData.scaleX = animationDataNode ["scale_x"].AsFloat;
												animationData.autoProceed = animationDataNode ["auto_proceed"].AsInt;
												animationDataList.Add (animationData);
										}
										storySet.animationDataList = animationDataList;
										storySet.displayName = storyDataNode ["display_name"].Value;
										storySet.bgm = storyDataNode ["bgm"].Value;
										storyData [countryCode] [storyDataNode ["name"].Value] = storySet;
								}
						}
				}

				if (callback != null)
						callback ();
		}
	
		public void RetrieveQuestion (Callback callback = null)
		{
				WWWForm form = new WWWForm ();
				string url = CommonConfig.API_URL + "route=api/aec/questions";
				this.callback = callback;
		
				StartCoroutine (ServerEngine.PostData (url, form, new ServerEngine.Callback (RetrieveQuestionCallback)));
		}

		void RetrieveQuestionCallback (JSONNode data)
		{
				foreach (string countryCode in countryCodeList) {
						if (data ["results"] [countryCode] != null) {
								JSONArray storyDataArray = data ["results"] [countryCode].AsArray;
								questionData [countryCode] = new Dictionary<string, List<Question>> ();
		
								foreach (JSONNode storyDataNode in storyDataArray) {
										List<Question> questionList = new List<Question> ();

										JSONArray questionArray = storyDataNode ["questions"].AsArray;
										foreach (JSONNode questionNode in questionArray) {
												Question question = new Question ();
												question.text = questionNode ["text"].Value;
												question.answerList = new List<Answer> ();

												foreach (JSONNode answerNode in questionNode["answers"].AsArray) {
														Answer answer = new Answer ();
														answer.text = answerNode ["text"].Value;
														answer.isCorrect = (answerNode ["is_correct"].AsInt == 1);
														question.answerList.Add (answer);
												}
												questionList.Add (question);
										}
										questionData [countryCode] [storyDataNode ["name"].Value] = questionList;
								}
						}
				}
			
				if (callback != null)
						callback ();
		}
}

public class AnimationData
{
		public string character;
		public string animationName;
		public float animationLength;
		public float animationDelay;
		public float? positionX;
		public float? scaleX;
		public string imageName;
		public string sound;
		public List<SubtitleText> text;
		public int autoProceed;
}

public class StorySet
{
		public List<AnimationData> animationDataList;
		public string displayName;
		public string bgm;
}

public class SubtitleText
{
		public float time;
		public string text;
}

public class Question
{
		public string text;
		public string sound;
		public List<Answer> answerList;
}

public class Answer
{
		public string text;
		public string sound;
		public bool isCorrect;
}