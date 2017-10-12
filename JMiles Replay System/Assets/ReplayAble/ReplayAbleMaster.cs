using System.Collections.Generic;
using JMiles42.Attributes;
using JMiles42.Generics;
using UnityEngine;

namespace ReplayAble {
	public class ReplayAbleMaster: Singleton<ReplayAbleMaster> {
		[NoFoldout] public List<KeyframeBase> KeyFrames = new List<KeyframeBase>();
		public static float StartTime;
		void Start() { StartTime = Time.timeSinceLevelLoad; }

		public static void AddKeyFrame(KeyframeBase key) {
			key.Time = Time.timeSinceLevelLoad - StartTime;
			Instance.KeyFrames.Add(key);
		}
	}
}