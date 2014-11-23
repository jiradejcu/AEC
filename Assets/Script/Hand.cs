using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{
		static Vector3 smallScale = new Vector3 (0.6f, 0.6f);
		static Vector3 moveBy = new Vector3 (-0.1f, 0.1f);
		static float fadeFrom = 0.5f;
		static float time = 0.5f;
		bool previousValue;

		void Start ()
		{
				previousValue = Main.countdown.HasValue;
				iTween.ScaleTo (gameObject, iTween.Hash ("scale", smallScale, "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
				iTween.MoveBy (gameObject, iTween.Hash ("amount", moveBy, "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
				iTween.FadeFrom (gameObject, iTween.Hash ("amount", fadeFrom, "time", time, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong));
		}

		void Update ()
		{
				if (previousValue != Main.countdown.HasValue) {
						previousValue = Main.countdown.HasValue;
						renderer.enabled = (!Main.IsFinished || !Main.IsAutoProceed) && !Main.countdown.HasValue;
						if (renderer.enabled)
								iTween.Resume (gameObject);
						else
								iTween.Pause (gameObject);
				}
		}
}
