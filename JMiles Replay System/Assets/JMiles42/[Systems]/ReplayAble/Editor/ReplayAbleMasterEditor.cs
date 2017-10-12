using JMiles42.Editor;
using UnityEditor;
using UnityEngine;

namespace ReplayAble {
	[CustomEditor(typeof (ReplayAbleMaster))]
	public class ReplayAbleMasterEditor: CustomEditorBase {
		private const string EXTENSION = "txt";

		public override void DrawGUI() {
			if (JMilesGUILayoutEvents.Button("Save List")) {
				SaveData();
			}
		}

		private void SaveData() {
			var master = (ReplayAbleMaster) target;

			var path = EditorUtility.SaveFilePanel("Save Replay", Application.dataPath,"Replay " +  System.DateTime.Now.ToString("yyyy-MM-dd hh-mm"), EXTENSION);

			JMiles42.IO.Generic.SavingLoading.SaveGameDataFilepath(path, new KeyframeSavedData {Keyframes = master.KeyFrames});
		}
	}
}