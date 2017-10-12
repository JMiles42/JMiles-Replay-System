using JMiles42.Components;
using JMiles42.UnityInterfaces;
using UnityEngine;

public class Player: JMilesBehavior, IEventListening {
	public void OnEnable() { ReplayAble.ReplayAbleInputSystem.Jump.onKeyDown += OnKeyDown; }

	public void OnDisable() { ReplayAble.ReplayAbleInputSystem.Jump.onKeyDown -= OnKeyDown; }

	private void OnKeyDown() { Position += Vector3.up; }
}