using JMiles42.Editor;
using UnityEditor;
using UnityEngine;

namespace ReplayAble {
	[CustomEditor(typeof (ReplayAbleMaster))]
	public class ReplayAbleMasterEditor: CustomEditorBase {
		private const string EXTENSION = "rply";

		public override void DrawGUI() {
			if (JMilesGUILayoutEvents.Button("Save List")) {
				SaveData();
			}
		}

		private void SaveData() {
			var master = (ReplayAbleMaster) target;

			var path = EditorUtility.SaveFilePanel("Save Replay", Application.dataPath, System.DateTime.Now.ToLongDateString(), EXTENSION);

			JMiles42.IO.Generic.SavingLoading.SaveGameDataFilepath(path, new KeyframeSavedData {Keyframes = master.KeyFrames});
		}
	}
}