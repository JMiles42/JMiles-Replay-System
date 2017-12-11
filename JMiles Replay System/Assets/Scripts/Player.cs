using JMiles42.Components;
using JMiles42.UnityInterfaces;
using UnityEngine;

public class Player: JMilesBehavior, IEventListening
{
	public float speed = 0.5f;

	public void OnEnable()
	{
		ReplayAble.ReplayAbleInputSystem.Jump.OnKeyDown += OnKeyDown;
		ReplayAble.ReplayAbleInputSystem.Horizontal.OnKey += OnHorizontal;
		ReplayAble.ReplayAbleInputSystem.Vertical.OnKey += OnVertical;
	}

	public void OnDisable()
	{
		ReplayAble.ReplayAbleInputSystem.Jump.OnKeyDown -= OnKeyDown;
		ReplayAble.ReplayAbleInputSystem.Horizontal.OnKey -= OnHorizontal;
		ReplayAble.ReplayAbleInputSystem.Vertical.OnKey -= OnVertical;
	}

	private void OnKeyDown()
	{
		Position += Vector3.up;
	}

	private void OnHorizontal(float val)
	{
		Position += Vector3.left * val * speed;
	}

	private void OnVertical(float val)
	{
		Position += Vector3.back * val * speed;
	}
}