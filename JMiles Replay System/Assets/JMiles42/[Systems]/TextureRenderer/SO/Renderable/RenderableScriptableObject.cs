using JMiles42.ScriptableObjects;

namespace JMiles42.Systems.TextureRenderer
{
    public abstract class RenderableScriptableObject: JMilesScriptableObject, IRenderable
    {
        public FOVSO FOV;
        public ImageDataScriptableObject ImageDataGlobal;
        public abstract void Render(ImageData ImageData);
    }
}