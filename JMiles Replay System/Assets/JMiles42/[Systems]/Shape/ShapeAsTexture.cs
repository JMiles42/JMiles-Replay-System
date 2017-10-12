using JMiles42.Systems.TextureRenderer;
using JMiles42.Types;
using UnityEngine;

namespace JMiles42.Systems.Shapes
{
	public static class ShapeAsTexture
	{
		public static Texture2D RenderShapeToTexture(Shape shape, int width, int height, ShapeRenderArgs shapeRenderArgs = null)
		{
			var Texture = new Texture2D(width, height, TextureFormat.ARGB32, false) {filterMode = FilterMode.Point};
			var imageData = new ImageData(width, height);
			imageData.InitBuffer();
			imageData.SetAllPixels(Colour.Green);

			var scanBuffer = new ScanBuffer(imageData);

			foreach (var tri in shape.Triangles)
			{
				scanBuffer.FillTriangleH(tri.Points[0].ToXZ().NegX(), tri.Points[1].ToXZ().NegX(), tri.Points[2].ToXZ().NegX(), Colour.Black);
			}
			Texture.LoadRawTextureData(imageData.Buffer);
			Texture.Apply();
			return Texture;
		}

		public class ShapeRenderArgs
		{
			public Colour background = Colour.Grey;
			public Colour line = Colour.Blue;
			public Colour point = Colour.White;
			public float pointRadius = 0.5f;
		}
	}
}