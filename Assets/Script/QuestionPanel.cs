using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionPanel : MonoBehaviour
{
		public TextMesh qText;
		public AnswerPanel[] aTextList;
		List<AnimationData> animationDataList;
		Character character;
		public Main main;
	
		public void SetQuestion (Question question, Character character)
		{
				qText.text = question.text;
				this.character = character;
				animationDataList = new List<AnimationData> ();

				AnimationData animationData = new AnimationData ();
				animationData.sound = CommonConfig.PRE_QUESTION_SOUND;
				animationDataList.Add (animationData);

				animationData = new AnimationData ();
				if (!string.IsNullOrEmpty (question.sound)) {
						animationData.sound = question.sound;
						animationDataList.Add (animationData);
				}

				int i = 0;
				foreach (string answerSound in CommonConfig.ANSWER_SOUND) {
						if (question.answerList.Count > i) {
								Answer answer = question.answerList [i];

								if (!string.IsNullOrEmpty (answer.sound)) {
										animationData = new AnimationData ();
										animationData.sound = answerSound;
										animationDataList.Add (animationData);

										animationData = new AnimationData ();
										animationData.sound = answer.sound;
										animationDataList.Add (animationData);
								}

								aTextList [i].SetAnswer (answer);
						} else
								aTextList [i].SetActive (false);
			
						i++;
				}

				StartCoroutine (PlayAnimation ());
		}

		IEnumerator PlayAnimation ()
		{
				AnimationData animationData = animationDataList [0];
				animationData.animationLength = Character.GetVerbalClip (animationData.sound).length;
				character.PlayAnimation (animationData);
				animationDataList.RemoveAt (0);
				yield return new WaitForSeconds (animationData.animationLength);
				if (animationDataList.Count > 0)
						StartCoroutine (PlayAnimation ());
		}

		public void PlayCorrectAnimation ()
		{
				Debug.Log ("correct");
				Main.score++;
				main.PlayAnimation ();
		}
	
		public void PlayWrongAnimation ()
		{
				Debug.Log ("wrong");
				main.PlayAnimation ();
		}
}
