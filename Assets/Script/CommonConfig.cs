using UnityEngine;
using System.Collections;

public class CommonConfig
{
		//		public static string API_URL = "http://localhost:10088/AEC/index.php?";
		//		public static string API_URL = "http://203.172.250.148/AEC/Server/index.php?";
		public static string API_URL = "http://192.168.0.10/AEC/Server/index.php?";
		public static string PRE_QUESTION_SOUND = "pre_question";
		public static string POST_QUESTION_SOUND = "post_question";
		public static string[] ANSWER_SOUND = {
				"answer_a",
				"answer_b",
				"answer_c",
				"answer_d"
		};
		public static string ANSWER_CORRECT = "answer_correct";
		public static string ANSWER_WRONG = "answer_wrong";
		public static string SCORE_SUMMARY = "score_summary";
}
