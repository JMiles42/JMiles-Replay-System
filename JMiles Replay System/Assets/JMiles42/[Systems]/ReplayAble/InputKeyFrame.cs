using System;
using JMiles42.Systems.InputManager;

namespace ReplayAble
{
	[Serializable]
	public class InputButtonKeyFrame: KeyframeBase
	{
		public string AxisName;
		public bool Pressed;

		public override void Replay()
		{
			ReplayAbleInputSystem.Instance[AxisName].KeyPos = Pressed? KeyPosition.Down : KeyPosition.Up;
			ReplayAbleInputSystem.Instance[AxisName].DoInput();
		}

		public override string ToString()
		{
			return string.Format("{0} was {1} At {2}", AxisName, Pressed? "Pressed" : "Released", Time);
		}
	}

	[Serializable]
	public class InputAxisKeyFrame: KeyframeBase
	{
		public string AxisName;
		public float Strength;

		public override void Replay()
		{
			ReplayAbleInputSystem.Instance[AxisName].KeyPos = KeyPosition.Held;
			ReplayAbleInputSystem.Instance[AxisName].Value = Strength;
			ReplayAbleInputSystem.Instance[AxisName].DoInput();
		}

		public override string ToString()
		{
			return string.Format("{0} was moved with a strength of{1} At {2}", AxisName, Strength, Time);
		}
	}
}