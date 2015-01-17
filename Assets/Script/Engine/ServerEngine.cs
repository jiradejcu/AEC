using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class ServerEngine
{
		public delegate void Callback (JSONNode data);

		public static IEnumerator PostData (string url, WWWForm form, Callback callback)
		{
				WWW w = null;

				Debug.Log (url);
				form.AddField ("dummy", "");

				if (CommonConfig.OFFLINE_MODE) {
						string result = ReadFile (CommonConfig.URL_MAPPING [url]);
						Debug.Log ("OFFLINE Post data response : " + result);
						JSONNode data = JSON.Parse (result);
						if (callback != null) {
								callback (data);
						}
				} else {
						w = new WWW (url, form);
						yield return w;

						try {
								if (w != null) {
										if (w.error == null) {
												Debug.Log ("Post data response : " + w.text);
												JSONNode data = JSON.Parse (w.text);
												if (callback != null) {
														callback (data);
												}
												w.Dispose ();
										} else {
												Debug.Log (w.error);
										}
								}
						} catch (Exception e) {
								Debug.Log (e.Message);
						}
				}
		}

		public static string ReadFile (string path)
		{
				TextAsset data = Resources.Load (path) as TextAsset;
				return data.text;
		}
}
