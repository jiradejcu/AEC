using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
		public static GameObject sound;
		string selectedCountry = "th";
		string selectedScene = "intro";
		int currentSceneNo;
		float? countdown = null;
		bool? isInit = null;
		Frame frame;
		Character[] characterList;
		AnimationData animationData = null;
		public static TextMesh subtitle;

		void Awake ()
		{
				sound = Resources.Load ("Prefabs/Sound") as GameObject;
		}

		void Start ()
		{
				StoryData.Instance.RetrieveData (new StoryData.Callback (OnDataReady));
		}
	
		void Update ()
		{
				if (isInit.HasValue && (!isInit.Value || (Input.GetKeyDown (KeyCode.Space) && !countdown.HasValue))) {
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
		
				GameObject frameObject = GameObject.FindGameObjectWithTag ("Frame");
				frame = frameObject.GetComponent<Frame> ();
		
				GameObject subtitleObject = GameObject.FindGameObjectWithTag ("Subtitle");
				subtitle = subtitleObject.GetComponent<TextMesh> ();
		
				GameObject[] characterObjectList = GameObject.FindGameObjectsWithTag ("Character");
				characterList = new Character[characterObjectList.Length];
				int i = 0;
				foreach (GameObject characterObject in characterObjectList) {
						Character character = characterObject.GetComponent<Character> ();
						characterList [i++] = character;
				}

				AudioSource bgmSource = (GameObject.Instantiate (sound) as GameObject).GetComponent<AudioSource> ();
				AudioClip bgmClip = Resources.Load ("Sound/BGM/" + StoryData.storyData [selectedCountry] [selectedScene].bgm) as AudioClip;
				bgmSource.clip = bgmClip;
				bgmSource.loop = true;
				bgmSource.volume = 0.2f;
				bgmSource.Play ();

				isInit = false;
		}

		void playAllCharacterAnimation ()
		{
				if (StoryData.storyData [selectedCountry] [selectedScene].animationDataList.Count > currentSceneNo) {
						animationData = StoryData.storyData [selectedCountry] [selectedScene].animationDataList [currentSceneNo];
						frame.SetImage (animationData.imageName);
						countdown = animationData.animationDelay + animationData.animationLength;
						foreach (Character character in characterList) {
								character.PlayAnimation (StoryData.storyData [selectedCountry] [selectedScene] .animationDataList [currentSceneNo]);
						}
						currentSceneNo++;
				}
		}
}
