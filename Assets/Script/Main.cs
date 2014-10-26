using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
		string selectedScene = "intro";
		int currentSceneNo;
		Character[] characterList;
		Frame frame;

		void Start ()
		{
				StoryData.InitData ();
				currentSceneNo = 0;
		
				GameObject frameObject = GameObject.FindGameObjectWithTag ("Frame");
				frame = frameObject.GetComponent<Frame> ();
				frame.SetImage (StoryData.storyThaiData [selectedScene] [currentSceneNo].imageName);
		
				GameObject[] characterObjectList = GameObject.FindGameObjectsWithTag ("Character");
				characterList = new Character[characterObjectList.Length];
				int i = 0;
				foreach (GameObject characterObject in characterObjectList) {
						Character character = characterObject.GetComponent<Character> ();
						characterList [i++] = character;
				}
				playAllCharacterAnimation ();
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Space) && StoryData.storyThaiData [selectedScene].Count > currentSceneNo) {
						frame.SetImage (StoryData.storyThaiData [selectedScene] [currentSceneNo].imageName);
						playAllCharacterAnimation ();
				}
		}

		void playAllCharacterAnimation ()
		{
				foreach (Character character in characterList) {
						character.PlayAnimation (StoryData.storyThaiData [selectedScene] [currentSceneNo++]);
				}
		}
}
