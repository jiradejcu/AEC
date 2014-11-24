﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
		public static GameObject sound;
		public static string selectedCountry = "";
		public static string selectedStory = "";
		public static float? countdown;
		GameObject[] characterContainer;
		static int currentSceneNo;
		static int currentCharacter;
		bool isInit = false;
		Frame frame;
		Dictionary<string, Character> characterList;
		static AnimationData animationData = null;
		public static Subtitle subtitle;
		public static TextMesh label;
		public enum Mode
		{
				NORMAL = 0,
				AUTO = 1,
				QUESTION = 2
		}

		void Awake ()
		{
				countdown = null;
				Input.simulateMouseWithTouches = true;
				sound = Resources.Load ("Prefabs/Sound") as GameObject;
		}

		void Start ()
		{
				characterList = new Dictionary<string, Character> ();
				currentSceneNo = 0;
				currentCharacter = 0;
		
				GameObject frameObject = GameObject.FindGameObjectWithTag ("Frame");
				frame = frameObject.GetComponent<Frame> ();
		
				GameObject subtitleObject = GameObject.FindGameObjectWithTag ("Subtitle");
				subtitle = subtitleObject.GetComponent<Subtitle> ();
		
				GameObject labelObject = GameObject.FindGameObjectWithTag ("Label");
				label = labelObject.GetComponent<TextMesh> ();

				label.text = StoryData.storyData [selectedCountry] [selectedStory].displayName;
		
				characterContainer = new GameObject[2];
				characterContainer [0] = GameObject.Find ("CharacterContainer1");
				characterContainer [1] = GameObject.Find ("CharacterContainer2");
		
				if (StoryData.storyData [selectedCountry] [selectedStory].bgm != null) {
						AudioSource bgmSource = (GameObject.Instantiate (sound) as GameObject).GetComponent<AudioSource> ();
						AudioClip bgmClip = Resources.Load ("Sound/BGM/" + StoryData.storyData [selectedCountry] [selectedStory].bgm) as AudioClip;
						bgmSource.clip = bgmClip;
						bgmSource.loop = true;
						bgmSource.volume = 0.1f;
						bgmSource.Play ();
				}
		}
	
		void Update ()
		{
				if (!isInit || IsNext) {
						isInit = true;
						playAnimation ();
				}

				if (countdown.HasValue) {
						countdown = countdown.Value - Time.deltaTime;
						if (countdown < 0) {
								countdown = null;
								if (animationData.autoProceed == (int)Mode.AUTO)
										playAnimation ();
						}
				}
		}

		void playAnimation ()
		{
				if (!IsFinished) {
						animationData = StoryData.storyData [selectedCountry] [selectedStory].animationDataList [currentSceneNo];
			
						string characterName = Character.GetCharacterName (selectedCountry, animationData.character);
						if (!characterList.ContainsKey (characterName)) {
								GameObject characterObject = GameObject.Instantiate (Resources.Load ("Prefabs/" + characterName)) as GameObject;
								characterObject.transform.parent = characterContainer [currentCharacter++].transform;
								characterObject.transform.localPosition = Vector3.zero;
								characterList.Add (characterName, characterObject.GetComponent<Character> ());
						}

						if (animationData.autoProceed == (int)Mode.QUESTION) {
								if (StoryData.questionData [selectedCountry] [selectedStory].Count > 0)
										frame.SetQuestion (StoryData.questionData [selectedCountry] [selectedStory] [0]);
						} else {
								frame.SetImage (selectedCountry, animationData.imageName);
								countdown = animationData.animationDelay + animationData.animationLength;

								characterList [characterName].PlayAnimation (animationData);
								subtitle.TextList = animationData.text;
						} 

						currentSceneNo++;
				} else
						Application.LoadLevel ("SelectStory");
		}

		bool IsNext {
				get {
						return (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space)) && HasNext;
				}
		}

		static bool IsMode (Mode mode)
		{
				return animationData.autoProceed == (int)mode;
		}
	
		static bool IsFinished {
				get {
						return  currentSceneNo >= StoryData.storyData [selectedCountry] [selectedStory].animationDataList.Count;
				}
		}

		public static bool HasNext {
				get {
						if (animationData != null)
								return !countdown.HasValue && IsMode (Mode.NORMAL);
						else
								return false;
				}
		}
}
