using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnswerPanel : MonoBehaviour
{
		Button button;
		Text text;
		int index;
		Answer answer;
		QuestionPanel qp;

		public enum State
		{
				NORMAL = 0,
				SELECTED = 1,
				ANSWER = 2
		}

		void Start ()
		{
				InitComponent ();
		}

		void Update ()
		{
				if (this.button != null) {
						this.button.interactable = QuestionPanel.isEnabled;
				}
		}

		void InitComponent ()
		{
				this.button = GetComponentInChildren<Button> ();
				this.text = GetComponentInChildren<Text> ();
				this.qp = GetComponentInParent <QuestionPanel> ();
		}

		public void SetAnswer (Answer answer, int index)
		{
				this.index = index;
				this.answer = answer;
				this.text.text = CommonConfig.ANSWER_PREFIX [index] + ". " + answer.text;
				SetState ((int)State.NORMAL);
				AnimationEngine.Instance.animateImage (gameObject, index);
		}

		public void SetActive (bool active)
		{
				gameObject.SetActive (active);
		}

		public void SetState (int state)
		{
				Animator animator = GetComponent<Animator> ();
				if (animator != null) {
						animator.enabled = false;
				}
				switch (state) {
				case (int)State.NORMAL:
						this.button.image.color = this.button.colors.normalColor;
						break;
				case (int)State.SELECTED:
						this.button.image.color = this.button.colors.pressedColor;
						break;
				case (int)State.ANSWER:
						this.button.image.color = this.button.colors.highlightedColor;
						break;
				}
		}

		public void OnClick ()
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
