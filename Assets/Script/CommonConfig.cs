using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonConfig
{
		public static string API_URL = "http://localhost:10088/AEC/index.php?";
//		public static string API_URL = "http://192.168.1.3:10088/AEC/index.php?";
//		public static string API_URL = "http://203.172.250.148/AEC/Server/index.php?";
//		public static string API_URL = "http://192.168.0.10/AEC/Server/index.php?";
		public static string PRE_QUESTION_SOUND = "pre_question";
		public static string POST_QUESTION_SOUND = "post_question";
		public static string[] ANSWER_SOUND = {
				"answer_a",
				"answer_b",
				"answer_c",
				"answer_d"
		};
		public static string[] ANSWER_PREFIX = {
				"ก",
				"ข",
				"ค",
				"ง"
		};
		public static string ANSWER_CORRECT = "answer_correct";
		public static string ANSWER_WRONG = "answer_wrong";
		public static string SCORE_SUMMARY = "score_summary";
		public static bool TEST_MODE = true;
		public static bool OFFLINE_MODE = false;
		public static Dictionary<string, Vector2> CENTRAL_COORDINATE = new Dictionary<string, Vector2> ();
		public static Dictionary<string, float> MAP_SCALE = new Dictionary<string, float> ();
		public static Dictionary<string, string> URL_MAPPING = new Dictionary<string, string> ();
	
		public enum COUNTRY_CODE
		{
				bn = 1,
				kh = 2,
				id = 3,
				la = 4,
				my = 5,
				mm = 6,
				ph = 7,
				sg = 8,
				th = 9,
				vn = 10
		}

		public static void Init ()
		{
				CENTRAL_COORDINATE.Add (COUNTRY_CODE.bn.ToString (), new Vector2 (4.561211f, 114.712760f));
				MAP_SCALE.Add (COUNTRY_CODE.bn.ToString (), 60f);

				CENTRAL_COORDINATE.Add (COUNTRY_CODE.kh.ToString (), new Vector2 (12.600536f, 104.986637f));
				MAP_SCALE.Add (COUNTRY_CODE.kh.ToString (), 14.5f);

				CENTRAL_COORDINATE.Add (COUNTRY_CODE.id.ToString (), new Vector2 (-2.461119f, 117.839082f));
				MAP_SCALE.Add (COUNTRY_CODE.id.ToString (), 1.85f);

				CENTRAL_COORDINATE.Add (COUNTRY_CODE.la.ToString (), new Vector2 (18.227167f, 103.867049f));
				MAP_SCALE.Add (COUNTRY_CODE.la.ToString (), 8.55f);

				CENTRAL_COORDINATE.Add (COUNTRY_CODE.my.ToString (), new Vector2 (3.886775f, 109.521332f));
				MAP_SCALE.Add (COUNTRY_CODE.my.ToString (), 3.83f);

				CENTRAL_COORDINATE.Add (COUNTRY_CODE.mm.ToString (), new Vector2 (19.260256f, 96.780903f));
				MAP_SCALE.Add (COUNTRY_CODE.mm.ToString (), 4.27f);

				CENTRAL_COORDINATE.Add (COUNTRY_CODE.ph.ToString (), new Vector2 (13.098078f, 121.764821f));
				MAP_SCALE.Add (COUNTRY_CODE.ph.ToString (), 5f);

				CENTRAL_COORDINATE.Add (COUNTRY_CODE.sg.ToString (), new Vector2 (1.323338f, 103.848287f));
				MAP_SCALE.Add (COUNTRY_CODE.sg.ToString (), 130f);

				CENTRAL_COORDINATE.Add (COUNTRY_CODE.th.ToString (), new Vector2 (13.141246f, 101.620955f));
				MAP_SCALE.Add (COUNTRY_CODE.th.ToString (), 4.8f);
		
				CENTRAL_COORDINATE.Add (COUNTRY_CODE.vn.ToString (), new Vector2 (16.079912f, 105.852973f));
				MAP_SCALE.Add (COUNTRY_CODE.vn.ToString (), 5f);
		}
}
