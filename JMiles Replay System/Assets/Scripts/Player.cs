using JMiles42.Components;
using JMiles42.UnityInterfaces;
using UnityEngine;

public class Player: JMilesBehavior, IEventListening
{
	public float speed = 0.5f;
	public void OnEnable()
	{
		ReplayAble.ReplayAbleInputSystem.Jump.onKeyDown += OnKeyDown;
		ReplayAble.ReplayAbleInputSystem.Horizontal.onKey += OnHorizontal;
		ReplayAble.ReplayAbleInputSystem.Vertical.onKey += OnVertical;
	}

	public void OnDisable()
	{
		ReplayAble.ReplayAbleInputSystem.Jump.onKeyDown -= OnKeyDown;
		ReplayAble.ReplayAbleInputSystem.Horizontal.onKey -= OnHorizontal;
		ReplayAble.ReplayAbleInputSystem.Vertical.onKey -= OnVertical;
	}

	private void OnKeyDown()
	{
		Position += Vector3.up;
	}

	private void OnHorizontal(float val)
	{
		Position += Vector3.forward * val * speed;
	}

	private void OnVertical(float val)
	{
		Position += Vector3.right * val * speed;
	}
}