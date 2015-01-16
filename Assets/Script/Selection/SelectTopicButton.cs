using UnityEngine;
using System.Collections;

public class SelectTopicButton : CommonButton
{

		void Start ()
		{
	
		}
	
		public override void OnMouseOver ()
		{
				Animator animator = GetComponent<Animator> ();
				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
						base.OnMouseOver ();
		}
	
		public override void OnMouseDown ()
		{
				base.OnMouseDown ();
				if (false) {
						Main.selectedCountry = StoryData.aecName;
						Main.selectedStory = StoryData.aecName;
						Application.LoadLevel ("Main");
				} else {
						SelectTopic.isSelectingCountry = true;
						Application.LoadLevel ("SelectStory");
				}
		}
}
