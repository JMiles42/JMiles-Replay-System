using System;

namespace ReplayAble {
	[Serializable]
	public class InputButtonKeyFrame: KeyframeBase {
		public string AxisName;
		public bool Pressed;
	}

	[Serializable]
	public class InputAxisKeyFrame: KeyframeBase {
		public string AxisName;
		public float Strength;
	}
}