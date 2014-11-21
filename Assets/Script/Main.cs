using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
		public static GameObject sound;
		public static string selectedCountry = "th";
		public static string selectedScene = "intro";
		GameObject[] characterContainer;
		int currentSceneNo;
		int currentCharacter;
		float? countdown = null;
		bool? isInit = null;
		Frame frame;
		Dictionary<string, Character> characterList;
		AnimationData animationData = null;
		public static TextMesh subtitle;

		void Awake ()
		{
				Input.simulateMouseWithTouches = true;
				sound = Resources.Load ("Prefabs/Sound") as GameObject;
		}

		void Start ()
		{
				characterList = new Dictionary<string, Character> ();
				StoryData.Instance.RetrieveData (new StoryData.Callback (OnDataReady));
		}
	
		void Update ()
		{
				if (isInit.HasValue && (!isInit.Value || ((Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space)) && !countdown.HasValue))) {
						isInit = true;
						playAllCharacterAnimation ();
				}

				if (countdown.HasValue) {
						countdown = countdown.Value - Time.deltaTime;
						if (countdown < 0) {
								countdown = null;
								if (animationData.autoProceed)
										playAllCharacterAnimation ();
						}
				}
		}

		void OnDataReady ()
		{
				currentSceneNo = 0;
				currentCharacter = 0;
		
				GameObject frameObject = GameObject.FindGameObjectWithTag ("Frame");
				frame = frameObject.GetComponent<Frame> ();
		
				GameObject subtitleObject = GameObject.FindGameObjectWithTag ("Subtitle");
				subtitle = subtitleObject.GetComponent<TextMesh> ();

				characterContainer = new GameObject[2];
				characterContainer [0] = GameObject.Find ("CharacterContainer1");
				characterContainer [1] = GameObject.Find ("CharacterContainer2");

				if (StoryData.storyData [selectedCountry] [selectedScene].bgm != null) {
						AudioSource bgmSource = (GameObject.Instantiate (sound) as GameObject).GetComponent<AudioSource> ();
						AudioClip bgmClip = Resources.Load ("Sound/BGM/" + StoryData.storyData [selectedCountry] [selectedScene].bgm) as AudioClip;
						bgmSource.clip = bgmClip;
						bgmSource.loop = true;
						bgmSource.volume = 0.2f;
						bgmSource.Play ();
				}

				isInit = false;
		}

		void playAllCharacterAnimation ()
		{
				if (StoryData.storyData [selectedCountry] [selectedScene].animationDataList.Count > currentSceneNo) {
						animationData = StoryData.storyData [selectedCountry] [selectedScene].animationDataList [currentSceneNo];
						frame.SetImage (selectedCountry, animationData.imageName);
						countdown = animationData.animationDelay + animationData.animationLength;

						string characterName = Character.GetCharacterName (selectedCountry, animationData.character);
						if (!characterList.ContainsKey (characterName)) {
								GameObject characterObject = GameObject.Instantiate (Resources.Load ("Prefabs/" + characterName)) as GameObject;
								characterObject.transform.parent = characterContainer [currentCharacter++].transform;
								characterObject.transform.localPosition = Vector3.zero;
								characterList.Add (characterName, characterObject.GetComponent<Character> ());
						}

						characterList [characterName].PlayAnimation (animationData);

						if (animationData.text != "null")
								subtitle.text = animationData.text;

						currentSceneNo++;
				}
		}
}
