using UnityEngine;
using UnityEngine.UI;
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
		static int mode;
		Frame frame;
		Dictionary<string, Character> characterList;
		List<Question> questionList;
		static AnimationData animationData = null;
		public static Subtitle subtitle;
		public static Text title;
		public static int score;
		public static int fullScore;
		public static int questionNo;
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
				score = 0;
				fullScore = 0;
				questionNo = 0;

				foreach (AnimationData animationData in StoryData.storyData [selectedCountry] [selectedStory].animationDataList) {
						if (animationData.autoProceed == (int)Mode.QUESTION)
								fullScore++;
				}

				questionList = new List<Question> ();
				if (StoryData.questionData.ContainsKey (selectedCountry)
						&& StoryData.questionData [selectedCountry].ContainsKey (selectedStory))
						foreach (Question question in StoryData.questionData [selectedCountry] [selectedStory]) {
								questionList.Add (question);
						}
		
				GameObject frameObject = GameObject.FindGameObjectWithTag ("Frame");
				frame = frameObject.GetComponent<Frame> ();
				frame.qp.main = this;
		
				GameObject subtitleObject = GameObject.FindGameObjectWithTag ("Subtitle");
				subtitle = subtitleObject.GetComponent<Subtitle> ();
		
				GameObject labelObject = GameObject.FindGameObjectWithTag ("Title");
				title = labelObject.GetComponent<Text> ();

				title.text = StoryData.storyData [selectedCountry] [selectedStory].displayName;
		
				characterContainer = new GameObject[2];
				characterContainer [0] = GameObject.Find ("CharacterContainer1");
				characterContainer [1] = GameObject.Find ("CharacterContainer2");
		
				if (StoryData.storyData [selectedCountry] [selectedStory].bgm != null) {
						AudioSource bgmSource = (GameObject.Instantiate (sound) as GameObject).GetComponent<AudioSource> ();
						AudioClip bgmClip = Resources.Load ("Sound/BGM/" + StoryData.storyData [selectedCountry] [selectedStory].bgm) as AudioClip;
						bgmSource.clip = bgmClip;
						bgmSource.loop = true;
						bgmSource.volume = 0.15f;
						bgmSource.Play ();
				}
		}
	
		void Update ()
		{
				if (!isInit || IsNext) {
						isInit = true;
						PlayAnimation ();
				}

				if (countdown.HasValue) {
						countdown = countdown.Value - Time.deltaTime;
						if (countdown < 0) {
								countdown = null;
								if (mode == (int)Mode.AUTO)
										PlayAnimation ();
						}
				}
		}

		public void PlayAnimation ()
		{
				if (!IsFinished) {
						animationData = StoryData.storyData [selectedCountry] [selectedStory].animationDataList [currentSceneNo];
						mode = animationData.autoProceed;

						string characterName = Character.GetCharacterName (selectedCountry, animationData.character);
						if (!characterList.ContainsKey (characterName)) {
								GameObject characterObject = GameObject.Instantiate (Resources.Load ("Prefabs/" + characterName)) as GameObject;
								Character character = characterObject.GetComponent<Character> ();
								character.containerNo = currentCharacter++;
								characterObject.transform.parent = characterContainer [character.containerNo].transform;
								characterObject.transform.localPosition = Vector3.zero;
								characterList.Add (characterName, character);
						}

						if (mode == (int)Mode.QUESTION) {
								if (questionList.Count > 0) {
										int index = Random.Range (0, questionList.Count);
										Question question = questionList [index];
										questionList.Remove (question);
										subtitle.TextList = new List<ContentText> ();
										frame.SetLayout ((int)Frame.Layout.QUESTION);
										frame.qp.SetQuestion (question, characterList [characterName]);
										characterList [characterName].PlayAnimation (animationData);
								} else
										countdown = 0;
						} else {
								frame.SetImage (selectedCountry, animationData.imageName, animationData.text);
								countdown = animationData.animationDelay + animationData.animationLength;

								characterList [characterName].PlayAnimation (animationData);
								subtitle.TextList = animationData.text;
						} 

						currentSceneNo++;

				} else if (fullScore > 0) {
						mode = (int)Mode.QUESTION;
						subtitle.TextList = new List<ContentText> ();
						frame.SetLayout ((int)Frame.Layout.QUESTION);
						frame.qp.SetShowScore ();
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
				return Main.mode == (int)mode;
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
	
		public static string ConcatText (List<ContentText> contentTextList, bool withNewLine = false)
		{
				string result = "";
				foreach (ContentText contentText in contentTextList) {
						if (!string.IsNullOrEmpty (contentText.text)) {
								result += contentText.text;
								if (withNewLine)
										result += "\n";
						}
				}
				return result;
		}

		public static bool ContainText (List<ContentText> contentTextList)
		{
				return !string.IsNullOrEmpty (ConcatText (contentTextList));
		}
	
		public static void SetNormalMode ()
		{
				fullScore = 0;
				mode = (int)Mode.NORMAL;
		}
}
