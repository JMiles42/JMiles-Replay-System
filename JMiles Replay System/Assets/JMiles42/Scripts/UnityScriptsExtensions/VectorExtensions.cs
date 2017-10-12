using UnityEngine;

namespace JMiles42
{
	public static class VectorExtensions
	{
		public static Vector3 Randomize(this Vector3 vec, float min = -100, float max = 100)
		{
			return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
		}

		public static Vector3 ToXZ(this Vector3 vec) { return new Vector2(vec.x, vec.z); }

		public static Vector2 NegX(this Vector2 vec) { return new Vector2(-vec.x, vec.y); }
		public static Vector2 NegY(this Vector2 vec) { return new Vector2(vec.x, -vec.y); }

		public static Vector3 NegX(this Vector3 vec) { return new Vector3(-vec.x, vec.y, vec.z); }
		public static Vector3 NegY(this Vector3 vec) { return new Vector3(vec.x, -vec.y, vec.z); }
		public static Vector3 NegZ(this Vector3 vec) { return new Vector3(vec.x, vec.y, -vec.z); }
	}
}