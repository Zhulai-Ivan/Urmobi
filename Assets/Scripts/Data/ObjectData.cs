using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct ObjectData
    {
        public int id;
        public string name;
        public ObjectType type;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public string layer;
        public bool isActive;
        public string producedObjectId;
    }
}