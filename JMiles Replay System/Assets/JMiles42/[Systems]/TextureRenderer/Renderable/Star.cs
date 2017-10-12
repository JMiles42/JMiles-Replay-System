using System;
using JMiles42.Types;
using UnityEngine;

namespace JMiles42.Systems.TextureRenderer
{
    [Serializable]
    public class Star
    {
        public Vector3 Position;
        public Colour Colour;

        public Star()
        {
            Position = Vector3.zero;
            Colour = Colour.White;
        }

        public Star(Vector3 pos)
        {
            Position = pos;
            Colour = Colour.White;
        }

        public Star(Vector3 pos, Colour colour)
        {
            Position = pos;
            Colour = colour;
        }
    }
}