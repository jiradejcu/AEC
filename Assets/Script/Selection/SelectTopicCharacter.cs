using UnityEngine;
using System.Collections;

public class SelectTopicCharacter : MonoBehaviour
{
		GameObject[] characterList;

		void Start ()
		{
				characterList = GameObject.FindGameObjectsWithTag ("Character");
				SetActive (false);
				Logo.FadeOutCompleted += CharacterRespect;
		}

		void SetActive (bool active)
		{
				foreach (GameObject character in characterList)
						character.SetActive (active);
		}

		void CharacterRespect ()
		{
				if (gameObject.activeInHierarchy) {
						SetActive (true);
						StartCoroutine (DoCharacterRespect ());
				}
				Logo.FadeOutCompleted -= CharacterRespect;
		}

		IEnumerator DoCharacterRespect ()
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
