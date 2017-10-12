using System;
using UnityEngine;

namespace JMiles42.Systems.Shapes
{
	[Serializable]
	public class Triangle
	{
		[SerializeField] private Vector3[] _points = new Vector3[3];

		public Vector3[] Points
		{
			get { return _points; }
			set { _points = value; }
		}

		public Vector3 P1
		{
			get { return _points[0]; }
			set { _points[0] = value; }
		}

		public Vector3 P2
		{
			get { return _points[1]; }
			set { _points[1] = value; }
		}

		public Vector3 P3
		{
			get { return _points[2]; }
			set { _points[2] = value; }
		}

		public Triangle(Vector3 p1, Vector3 p2, Vector3 p3)
		{
			P1 = p1;
			P2 = p2;
			P3 = p3;
		}
	}
}