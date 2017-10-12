using JMiles42.ScriptableObjects;
using UnityEngine;

namespace JMiles42.Systems.TextureRenderer
{
	[CreateAssetMenu(fileName = "New Image Data SO", menuName = "SO/ImageData", order = 0)]
	public class ImageDataScriptableObject: GenericScriptableObject<ImageData>
	{
		[ContextMenu("Clear Array")]
		void OnDisable()
		{
			Data.Buffer = new byte[0];
		}
	}
}