using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestionPanel : MonoBehaviour
{
		public Text qText;
		public AnswerPanel[] aTextList;
		List<AnimationData> animationDataList;
		AnimationData animationData;
		Character character;
		int correctIndex;
		Answer correctAnswer;
		public Main main;
		public static bool isEnabled;
	
		public void SetQuestion (Question question, Character character)
		{
				qText.text = question.text;
				this.character = character;
				animationDataList = new List<AnimationData> ();

				animationData = new AnimationData ();
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

								if (answer.isCorrect) {
										correctIndex = i;
										correctAnswer = answer;
								}

								aTextList [i].SetAnswer (answer, i);
						} else
								aTextList [i].SetActive (false);
			
						i++;
				}

				isEnabled = true;
				StartCoroutine (PlayAnimation ());
		}

		IEnumerator PlayAnimation (bool continueToMain = false)
		{
				AnimationData animationData = animationDataList [0];
				animationData.animationLength = Character.GetVerbalClip (animationData.sound).length;
				character.PlayAnimation (animationData);
				animationDataList.RemoveAt (0);

				if (animationData.sound == CommonConfig.ANSWER_CORRECT)
						SetAnswerState ();

				yield return new WaitForSeconds (animationData.animationLength);
		
				if (animationData.sound == CommonConfig.ANSWER_WRONG)
						SetAnswerState ();

				if (animationDataList.Count > 0)
						StartCoroutine (PlayAnimation (continueToMain));
				else if (continueToMain) {
						yield return new WaitForSeconds (1f);
						main.PlayAnimation ();
				}
		}

		public void PlayCorrectAnimation (Answer answer, int index)
		{
				Debug.Log ("correct");
				Main.score++;

				PlayAnswerAnimation (answer, index);
		
				animationData = new AnimationData ();
				animationData.sound = CommonConfig.ANSWER_CORRECT;
				animationDataList.Add (animationData);
		
				StartCoroutine (PlayAnimation (true));
		}
	
		public void PlayWrongAnimation (Answer answer, int index)
		{
				Debug.Log ("wrong");

				PlayAnswerAnimation (answer, index);
		
				animationData = new AnimationData ();
				animationData.sound = CommonConfig.ANSWER_WRONG;
				animationDataList.Add (animationData);
		
				animationData = new AnimationData ();
				animationData.sound = CommonConfig.ANSWER_SOUND [correctIndex];
				animationDataList.Add (animationData);
		
				if (!string.IsNullOrEmpty (correctAnswer.sound)) {
						animationData = new AnimationData ();
						animationData.sound = correctAnswer.sound;
						animationDataList.Add (animationData);
				}

				StartCoroutine (PlayAnimation (true));
		}

		void PlayAnswerAnimation (Answer answer, int index)
		{
				isEnabled = false;
				animationDataList = new List<AnimationData> ();
		
				animationData = new AnimationData ();
				animationData.sound = CommonConfig.ANSWER_SOUND [index];
				animationDataList.Add (animationData);
		
				if (!string.IsNullOrEmpty (answer.sound)) {
						animationData = new AnimationData ();
						animationData.sound = answer.sound;
						animationDataList.Add (animationData);
				}
		
				animationData = new AnimationData ();
				animationData.sound = CommonConfig.POST_QUESTION_SOUND;
				animationDataList.Add (animationData);
		}

		void SetAnswerState ()
		{
				aTextList [correctIndex].SetState ((int)AnswerPanel.State.ANSWER);
		}
}
