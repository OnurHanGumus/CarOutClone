using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class CarData
    {
        public float Speed = 5;
        public int InitializePosX, InitializePosY;
        public float CarMoveTreshold = 0.8f;
    }
}