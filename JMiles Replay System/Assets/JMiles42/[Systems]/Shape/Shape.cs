using System;
using System.Collections.Generic;
using UnityEngine;

namespace JMiles42.Systems.Shapes
{
	[Serializable]
	public class Shape
	{
		public List<Vector3> Points = new List<Vector3>();

		public List<Triangle> Triangles
		{
			get { return GenerateTriangles(); }
		}

		public List<Triangle> GenerateTriangles() { return GenerateTriangles(Points); }

		public static List<Triangle> GenerateTriangles(List<Vector3> points)
		{
			if (points.Count == 3)
			{
				return new List<Triangle> {new Triangle(points[0], points[1], points[2])};
			}
			if (points.Count <= 2)
			{
				return new List<Triangle>();
			}

			var temp = new List<int>();

			var triangles = new List<Triangle>(points.Count - 2);

			for (int i = 0; i < points.Count - 1; i++)
			{
				if (temp.Contains(i))
					continue;
				if (i.IsEven())
				{
					triangles.Add(new Triangle(points[i], points[i + 1], points[points.Count - 1]));
					temp.Add(i);
				}
				else
				{
					triangles.Add(new Triangle(points[i], points[i + 1], points[points.Count - 1]));
					temp.Add(temp.Count - 1);
				}
			}

			return triangles;
		}
	}
}