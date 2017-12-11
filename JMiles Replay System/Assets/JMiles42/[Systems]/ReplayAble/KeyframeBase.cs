using System;

namespace ReplayAble
{
	[Serializable]
	public class KeyframeBase : IReplayAble
	{
		public float Time;

		public override string ToString()
		{
			return Time.ToString();
		}

		/// <summary>
		/// Calls the replay logic in the child classes
		/// </summary>
		public virtual void Replay()
		{ }
	}
}