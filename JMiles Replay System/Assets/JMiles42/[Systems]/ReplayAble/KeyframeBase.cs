using System;

namespace ReplayAble {
	[Serializable]
	public class KeyframeBase {
		public float Time;

		public override string ToString() { return Time.ToString(); }

		public virtual void Replay() {}
	}
}