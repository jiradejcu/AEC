using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryData
{
		public static string defaultAnimation = "idle";
		public static Dictionary<string, Dictionary<string, StorySet>> storyData = new Dictionary<string, Dictionary<string, StorySet>> ();

		public static void InitData ()
		{
				List<AnimationData> animationDataList = new List<AnimationData> ();
				AnimationData animationData;

				animationData = new AnimationData ();
				animationData.animationName = "walk";
				animationData.animationLength = 3.0f;
				animationData.positionX = -5.3f;
				animationData.imageName = "island";
				animationDataList.Add (animationData);
		
				animationData = new AnimationData ();
				animationData.animationName = "respect";
				animationData.animationLength = 3.0f;
				animationData.imageName = "flag";
				animationData.sound = "th_sawasdee";
				animationData.text = "Hello";
				animationDataList.Add (animationData);
		
				animationData = new AnimationData ();
				animationData.animationName = "respect";
				animationData.animationLength = 3.0f;
				animationData.imageName = "night";
				animationData.sound = "th_thankyou";
				animationData.text = "Thank you";
				animationData.autoProceed = true;
				animationDataList.Add (animationData);
		
				animationData = new AnimationData ();
				animationData.animationName = "walk";
				animationData.imageName = "night";
				animationData.animationDelay = 0.5f;
				animationData.animationLength = 3.0f;
				animationData.positionX = -9f;
				animationData.scaleX = -1f;
				animationDataList.Add (animationData);

				StorySet storySet = new StorySet ();
				storySet.animationDataList = animationDataList;
				storySet.bgm = "ค้างคาวกินกล้วย";

				storyData ["th"] = new Dictionary<string, StorySet> ();
				storyData ["th"] ["intro"] = storySet;
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