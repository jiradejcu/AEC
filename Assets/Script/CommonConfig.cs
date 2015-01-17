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
				CENTRAL_COORDINATE.Add (COUNTRY_CODE.th.ToString (), new Vector2 (13.141246f, 101.520955f));
				MAP_SCALE.Add (COUNTRY_CODE.th.ToString (), 4.5f);
		}
}
