using UnityEngine;
using System.Collections;

public class SelectTopicCharacter : MonoBehaviour
{
		GameObject[] characterList;

		void Start ()
		{
				characterList = GameObject.FindGameObjectsWithTag ("Character");
				StartCoroutine (CharacterRespect ());
		}

		IEnumerator CharacterRespect ()
		{
				yield return new WaitForSeconds (2f);
				AnimationData animationData = new AnimationData ();
				animationData.animationName = "respect";
				foreach (GameObject character in characterList) {
						character.GetComponent<Character> ().PlayAnimation (animationData);
						yield return new WaitForSeconds (0.5f);
				}
		}
}
