using System;
using JMiles42.Extensions;
using UnityEngine;

namespace JMiles42.Systems.InputManager {
	[Serializable]
	public class InputAxis {
		/// <summary>
		/// The Axis that the coder uses as referance
		/// </summary>
		public string Axis;

		/// <summary>
		/// The Axis that is sent to Unity's input manager
		/// </summary>
		public string UnityAxis;

		public bool OnlyButton = false;

		public float Value {
			get { return ValueInverted? m_Value : -m_Value; }
			set { m_Value = ValueInverted? value : -value; }
		}

		[SerializeField] private float m_Value;
		public bool ValueInverted;
		public bool KeyDown = false;
		public KeyPosition KeyPos = KeyPosition.Up;
		public Action onKeyDown;
		public Action onKeyUp;
		public Action onKeyPositiveDown;
		public Action onKeyPositiveUp;
		public Action onKeyNegativeDown;
		public Action onKeyNegativeUp;
		public Action onKeyNoValue;
		public Action<float> onKey;

		public enum KeyPosition {
			Up,
			Down,
			Held,
			None
		}

		public InputAxis(string axis, bool invert = false) {
			Axis = UnityAxis = axis;
			ValueInverted = invert;
			InitActions();
		}

		public InputAxis(string axis, string uAxis, bool invert = false) {
			Axis = axis;
			UnityAxis = uAxis;
			ValueInverted = invert;
			InitActions();
		}

		private void InitActions() {
			onKeyDown += () => KeyDown = true;
			onKeyUp += () => KeyDown = false;
		}

		public void UpdateData() {
			m_Value = Input.GetAxis(UnityAxis);
			if (Input.GetButtonUp(Axis)) {
				KeyPos = KeyPosition.Up;
			}
			else if (Input.GetButtonDown(Axis)) {
				KeyPos = KeyPosition.Down;
			}
			else if (Input.GetButton(Axis)) {
				KeyPos = KeyPosition.Held;
			}
		}

		public static implicit operator float(InputAxis fp) { return fp.Value; }

		public static implicit operator string(InputAxis fp) { return fp.UnityAxis; }

		public static implicit operator bool(InputAxis fp) { return fp.ValueInverted; }

		public static implicit operator InputAxis(string fp) { return new InputAxis(fp); }

		public static implicit operator InputAxis(PlayerInputValues fp) { return new InputAxis(fp.ToString()); }

		public void DoInput(float deadZone = 0.05f) { DoInput(this, deadZone); }
		public bool InputInDeadZone(float deadZone = 0.05f) { return Math.Abs(Value) > deadZone; }

		public static void DoInput(InputAxis key, float deadZone = 0.05f) {
			if (key.KeyPos == KeyPosition.Up) {
				key.onKeyUp.Trigger();

				if (key.Value > 0)
					key.onKeyPositiveUp.Trigger();
				else if (key.Value < 0)
					key.onKeyNegativeUp.Trigger();
			}
			else if (key.KeyPos == KeyPosition.Down) {
				key.onKeyDown.Trigger();

				if (key.Value > 0)
					key.onKeyPositiveDown.Trigger();
				else if (key.Value < 0)
					key.onKeyNegativeDown.Trigger();
			}
			else if (Math.Abs(key.Value) > deadZone && !key.OnlyButton) {
				key.onKey.Trigger(key.Value);
				key.onKeyNoValue.Trigger();
				return;
			}
			key.KeyPos = KeyPosition.None;
		}
	}
}