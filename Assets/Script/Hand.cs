using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{
		static Vector3 smallScale = new Vector3 (0.6f, 0.6f);
		static Vector3 moveBy = new Vector3 (-0.1f, 0.1f);
		static float fadeFrom = 0.5f;
		static float time = 0.5f;
		bool previousCountdown;
		bool previousRenderer;

		void Start ()
		{
				previousCountdown = Main.countdown.HasValue;
				previousRenderer = renderer.enabled;
				iTween.ScaleTo (gameObject, iTween.Hash ("scale", smallScale, "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
				iTween.MoveBy (gameObject, iTween.Hash ("amount", moveBy, "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
				iTween.FadeFrom (gameObject, iTween.Hash ("amount", fadeFrom, "time", time, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong));
		}

		void Update ()
		{
				if (previousCountdown != Main.countdown.HasValue || previousRenderer != Main.HasNext) {
						previousCountdown = Main.countdown.HasValue;

						renderer.enabled = Main.HasNext;
						previousRenderer = renderer.enabled;

						if (renderer.enabled)
								iTween.Resume (gameObject);
						else
								iTween.Pause (gameObject);
				}
		}
}
