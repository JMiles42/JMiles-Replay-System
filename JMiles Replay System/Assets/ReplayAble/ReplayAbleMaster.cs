using System.Collections;
using System.Collections.Generic;
using JMiles42.Attributes;
using JMiles42.Extensions;
using JMiles42.Generics;
using UnityEngine;

namespace ReplayAble {
	public class ReplayAbleMaster: Singleton<ReplayAbleMaster> {
		[NoFoldout] public List<KeyframeBase> KeyFrames = new List<KeyframeBase>();

		public string Replay;

		public float StartTimeDisplay;
		public static float StartTime;

		public bool Recording = true;

		void Start() {
			StartTime = StartTimeDisplay = Time.timeSinceLevelLoad;
			if (!Replay.IsNullOrEmpty()) {
				Recording = false;
				KeyframeSavedData data;
				JMiles42.IO.Generic.SavingLoading.LoadGameDataFilepath(Replay, out data, new KeyframeSavedData(), false);
				StartCoroutine(PlayBack(data));
			}
		}

		public static void AddKeyFrame(KeyframeBase key) {
			if (!Instance.Recording)
				return;
			key.Time = Time.timeSinceLevelLoad - StartTime;
			Instance.KeyFrames.Add(key);
		}

		public IEnumerator PlayBack(KeyframeSavedData data) {
			if (data.IsNull())
				yield break;
			int i = 0;
			StartTime = StartTimeDisplay = Time.timeSinceLevelLoad;
			while (true) {
				if (data.Keyframes.InRange(i)) {
					yield return new WaitForSeconds(StartTime - data.Keyframes[i].Time);

					Debug.Log(data.Keyframes[i].ToString());

					data.Keyframes[i].Replay();
					++i;
				}
				else {
					yield break;
				}
			}
		}
	}
}