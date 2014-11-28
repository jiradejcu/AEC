using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hand : MonoBehaviour
{
		static Vector3 smallScale = new Vector3 (0.8f, 0.8f);
		static Vector3 moveBy = new Vector3 (-0.15f, 0.15f);
		static float time = 0.5f;
		bool previousCountdown;
		bool previousRenderer;
		Image image;

		void Start ()
		{
				image = GetComponent<Image> ();
				previousCountdown = Main.countdown.HasValue;
				previousRenderer = image.enabled;
				iTween.ScaleTo (gameObject, iTween.Hash ("scale", smallScale, "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
				iTween.MoveBy (gameObject, iTween.Hash ("amount", moveBy, "time", time, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
		}

		void Update ()
		{
				if (previousCountdown != Main.countdown.HasValue || previousRenderer != Main.HasNext) {
						previousCountdown = Main.countdown.HasValue;

						image.enabled = Main.HasNext;
						previousRenderer = image.enabled;
			
						if (image.enabled)
								iTween.Resume (gameObject);
						else
								iTween.Pause (gameObject);
				}
		}
}
