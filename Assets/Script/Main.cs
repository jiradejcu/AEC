using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
		string selectedScene = "intro";
		int currentSceneNo;
		GameObject[] characterList;

		void Start ()
		{
				StoryData.InitData ();
				currentSceneNo = 0;
				characterList = GameObject.FindGameObjectsWithTag ("Character");
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Space) && StoryData.storyThaiData [selectedScene].Count > currentSceneNo) {
						foreach (GameObject characterObject in characterList) {
								Character character = characterObject.GetComponent<Character> ();
								character.PlayAnimation (StoryData.storyThaiData [selectedScene] [currentSceneNo++]);
						}
				}
		}
}
