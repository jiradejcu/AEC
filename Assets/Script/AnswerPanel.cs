using UnityEngine;
using System.Collections;

public class AnswerPanel : MonoBehaviour
{
		SpriteRenderer bg;
		TextMesh text;
		int index;
		Answer answer;
		QuestionPanel qp;

		public enum State
		{
				NORMAL = 0,
				SELECTED = 1,
				ANSWER = 2
		}

		void InitComponent ()
		{
				this.bg = GetComponent<SpriteRenderer> ();
				this.text = transform.parent.GetComponentInChildren<TextMesh> ();
				this.qp = transform.parent.transform.parent.GetComponent<QuestionPanel> ();
		}

		public void SetAnswer (Answer answer, int index)
		{
				InitComponent ();
				this.index = index;
				this.answer = answer;
				this.text.text = answer.text;
				SetState ((int)State.NORMAL);
		}

		public void SetActive (bool active)
		{
				transform.parent.gameObject.SetActive (active);
		}

		public void SetState (int state)
		{
				switch (state) {
				case (int)State.NORMAL:
						this.bg.color = new Color (0, 0, 0, 0.5f);
						break;
				case (int)State.SELECTED:
						this.bg.color = new Color (0, 0, 0, 0f);
						break;
				case (int)State.ANSWER:
						this.bg.color = new Color (0, 0, 0, 1f);
						break;
				}
		}

		void OnMouseDown ()
		{
				if (QuestionPanel.isEnabled) {
						SetState ((int)State.SELECTED);
						if (answer.isCorrect)
								qp.PlayCorrectAnimation (answer, index);
						else
								qp.PlayWrongAnimation (answer, index);
				}
		}
}
