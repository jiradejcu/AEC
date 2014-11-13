using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
		public static GameObject sound;
		string selectedCountry = "th";
		string selectedScene = "intro";
		int currentSceneNo;
		float? countdown;
		bool isInit = false;
		Frame frame;
		Character[] characterList;
		AnimationData animationData = null;
		public static TextMesh subtitle;

		void Start ()
		{
				StoryData.InitData ();
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

				sound = Resources.Load ("Prefabs/Sound") as GameObject;
				AudioSource bgmSource = (GameObject.Instantiate (sound) as GameObject).GetComponent<AudioSource> ();
				AudioClip bgmClip = Resources.Load ("Sound/BGM/" + StoryData.storyData [selectedCountry] [selectedScene].bgm) as AudioClip;
				bgmSource.clip = bgmClip;
				bgmSource.loop = true;
				bgmSource.volume = 0.2f;
				bgmSource.Play ();
		}
	
		void Update ()
		{
				if (!isInit || (Input.GetKeyDown (KeyCode.Space) && !countdown.HasValue)) {
						isInit = true;
						countdown = null;
						if (StoryData.storyData [selectedCountry] [selectedScene].animationDataList.Count > currentSceneNo) {
								animationData = StoryData.storyData [selectedCountry] [selectedScene].animationDataList [currentSceneNo];
								frame.SetImage (animationData.imageName);
								playAllCharacterAnimation ();
								countdown = animationData.animationDelay + animationData.animationLength;
						}
				}

				if (countdown.HasValue) {
						countdown = countdown.Value - Time.deltaTime;
						Debug.Log (countdown);
						if (countdown < 0) {
								countdown = null;
								if (animationData.autoProceed)
										playAllCharacterAnimation ();
						}
				}
		}

		void playAllCharacterAnimation ()
		{
				foreach (Character character in characterList) {
						character.PlayAnimation (StoryData.storyData [selectedCountry] [selectedScene] .animationDataList [currentSceneNo]);
				}
				currentSceneNo++;
		}
}
