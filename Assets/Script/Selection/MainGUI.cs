using UnityEngine;
using System.Collections;

public class MainGUI : MonoBehaviour
{
		public static GameObject mainGUI;

		void Start ()
		{
				if (mainGUI == null) {
						mainGUI = gameObject;
						DontDestroyOnLoad (gameObject);
				} else
						Destroy (gameObject);
		}
}
