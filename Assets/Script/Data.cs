using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class StoryData : Singleton<StoryData>
{		
		public static string aecName = "aec";
		public static string defaultCountryCode = CommonConfig.COUNTRY_CODE.th.ToString ();
		public static Dictionary<string, string> countryNameList = new Dictionary<string, string> ();
		public delegate void Callback ();

		public Callback callback;
		public static string defaultAnimation = "idle";
		public static Dictionary<string, Dictionary<string, StorySet>> storyData = new Dictionary<string, Dictionary<string, StorySet>> ();
		public static Dictionary<string, Dictionary<string, List<Question>>> questionData = new Dictionary<string, Dictionary<string, List<Question>>> ();
	
		public StoryData ()
		{
				countryNameList.Add (CommonConfig.COUNTRY_CODE.bn.ToString (), "Brunei");
				countryNameList.Add (CommonConfig.COUNTRY_CODE.kh.ToString (), "Cambodia");
				countryNameList.Add (CommonConfig.COUNTRY_CODE.id.ToString (), "Indonesia");
				countryNameList.Add (CommonConfig.COUNTRY_CODE.la.ToString (), "Lao");
				countryNameList.Add (CommonConfig.COUNTRY_CODE.my.ToString (), "Malaysia");
				countryNameList.Add (CommonConfig.COUNTRY_CODE.mm.ToString (), "Myanmar");
				countryNameList.Add (CommonConfig.COUNTRY_CODE.ph.ToString (), "Philipines");
				countryNameList.Add (CommonConfig.COUNTRY_CODE.sg.ToString (), "Singapore");
				countryNameList.Add (CommonConfig.COUNTRY_CODE.th.ToString (), "Thai");
				countryNameList.Add (CommonConfig.COUNTRY_CODE.vn.ToString (), "Vietnam");
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
		
				string[] customCountryCodeList = GetCustomCountryCodeList ();

				foreach (string countryCode in customCountryCodeList) {
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
												if (!string.IsNullOrEmpty (animationDataNode ["sound"].Value)) {
														animationData.sound = animationDataNode ["sound"].Value;
														if (animationData.animationLength == 0)
																animationData.animationLength = Character.GetVerbalClip (animationData.sound).length;
												}
												JSONArray textArray = animationDataNode ["text"].AsArray;
												animationData.text = new List<ContentText> ();
												foreach (JSONNode textNode in textArray) {
														ContentText contentText = new ContentText ();
														contentText.time = textNode ["time"].AsFloat;
														contentText.image = textNode ["image"].Value;
														contentText.text = textNode ["text"].Value;
														contentText.subtitle = textNode ["subtitle"].Value;
														animationData.text.Add (contentText);
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
										if (storyDataNode ["lat"].Value != "null" && storyDataNode ["lon"].Value != "null") {
												storySet.lat = storyDataNode ["lat"].AsFloat;
												storySet.lon = storyDataNode ["lon"].AsFloat;
										}
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
				string[] customerCountryCodeList = GetCustomCountryCodeList ();

				foreach (string countryCode in customerCountryCodeList) {
						if (data ["results"] [countryCode] != null) {
								JSONArray storyDataArray = data ["results"] [countryCode].AsArray;
								questionData [countryCode] = new Dictionary<string, List<Question>> ();
		
								foreach (JSONNode storyDataNode in storyDataArray) {
										List<Question> questionList = new List<Question> ();

										JSONArray questionArray = storyDataNode ["questions"].AsArray;
										foreach (JSONNode questionNode in questionArray) {
												Question question = new Question ();
												question.text = questionNode ["text"].Value;
												question.sound = questionNode ["sound"].Value;
												question.answerList = new List<Answer> ();

												foreach (JSONNode answerNode in questionNode["answers"].AsArray) {
														Answer answer = new Answer ();
														answer.text = answerNode ["text"].Value;
														answer.sound = answerNode ["sound"].Value;
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

		string[] GetCustomCountryCodeList ()
		{
				string[] countryCodeList = Enum.GetNames (typeof(CommonConfig.COUNTRY_CODE));
				string[] customCountryCodeList = new string[countryCodeList.Length + 1];
				countryCodeList.CopyTo (customCountryCodeList, 0);
				customCountryCodeList [countryCodeList.Length] = aecName;
				return customCountryCodeList;
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
		public List<ContentText> text;
		public int autoProceed;
}

public class StorySet
{
		public List<AnimationData> animationDataList;
		public string displayName;
		public string bgm;
		public float? lat;
		public float? lon;
}

public class ContentText
{
		public float time;
		public string image;
		public string text;
		public string subtitle;

		enum Type
		{
				IMAGE = 1,
				TEXT = 2
		}

		public static List<ContentText> CloneText (List<ContentText> original)
		{
				return Clone (original, Type.TEXT);
		}

		public static List<ContentText> CloneImage (List<ContentText> original)
		{
				return Clone (original, Type.IMAGE);
		}

		static List<ContentText> Clone (List<ContentText> original, Type type)
		{
				List<ContentText> cloneContentTextList = new List<ContentText> ();
				foreach (ContentText contentText in original) {
						string text = null;
						switch (type) {
						case Type.IMAGE:
								text = contentText.image;
								break;
						case Type.TEXT:
								text = contentText.text;
								break;
						}
						if (!string.IsNullOrEmpty (text))
								cloneContentTextList.Add (contentText);
				}
				return cloneContentTextList;
		}
}

public class ContentSound
{
		public string text;
		public string sound;
}

public class Question : ContentSound
{
		public List<Answer> answerList;
}

public class Answer: ContentSound
{
		public bool isCorrect;
}