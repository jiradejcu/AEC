using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryData
{
		public static string defaultThaiAnimation = "idle";
		public static Dictionary<string, List<AnimationData>> storyThaiData = new Dictionary<string, List<AnimationData>> ();

		public static void InitData ()
		{
				List<AnimationData> introThaiData = new List<AnimationData> ();
				AnimationData animationData;

				animationData = new AnimationData ();
				animationData.animationName = "walk";
				animationData.animationLength = 3.0f;
				animationData.positionX = -5.3f;
				animationData.imageName = "island";
				introThaiData.Add (animationData);
		
				animationData = new AnimationData ();
				animationData.animationName = "respect";
				//				animationData.scaleX = -1f;
				animationData.imageName = "flag";

				introThaiData.Add (animationData);
				storyThaiData ["intro"] = introThaiData;
		}
}

public class AnimationData
{
		public string animationName;
		public float animationLength;
		public float? positionX;
		public float? scaleX;
		public string imageName;
		public string text;
}