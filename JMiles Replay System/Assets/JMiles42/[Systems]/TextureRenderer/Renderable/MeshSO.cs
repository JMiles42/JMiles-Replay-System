using System.Collections.Generic;
using JMiles42.ScriptableObjects;
using UnityEngine;

namespace JMiles42.Systems.TextureRenderer
{
    [CreateAssetMenu(fileName = "New Mesh", menuName = "SO/Mesh", order = 0)]
    public class MeshSO: JMilesScriptableObject
    {
        public List<Vertex> Vertices = new List<Vertex>
                                       {
                                           //Top
                                           new Vertex(-0.5f, 0.5f, 0.5f),
                                           new Vertex(0.5f, 0.5f, 0.5f),
                                           new Vertex(0.5f, -0.5f, 0.5f),
                                           new Vertex(-0.5f, -0.5f, 0.5f),
                                           //Bottom
                                           new Vertex(-0.5f, 0.5f, -0.5f),
                                           new Vertex(0.5f, 0.5f, -0.5f),
                                           new Vertex(0.5f, -0.5f, -0.5f),
                                           new Vertex(-0.5f, -0.5f, -0.5f)
                                       };
    }
}