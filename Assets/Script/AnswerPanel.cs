using UnityEngine;
using System.Collections;

public class AnswerPanel : MonoBehaviour
{
		private TextMesh text;
		Answer answer;
		QuestionPanel qp;

		void InitComponent ()
		{
				this.text = transform.parent.GetComponentInChildren<TextMesh> ();
				this.qp = transform.parent.transform.parent.GetComponent<QuestionPanel> ();
		}

		public void SetAnswer (Answer answer)
		{
				InitComponent ();
				this.answer = answer;
				this.text.text = answer.text;
		}

		public void SetActive (bool active)
		{
				transform.parent.gameObject.SetActive (active);
		}

		void OnMouseDown ()
		{
				if (answer.isCorrect)
						qp.PlayCorrectAnimation ();
				else
						qp.PlayWrongAnimation ();
		}
}
