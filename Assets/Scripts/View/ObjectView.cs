using Data;
using UnityEngine;

namespace View
{
    public class ObjectView : MonoBehaviour
    {
        [SerializeField] protected int _id;
        [SerializeField] protected ObjectType _type;
        public ObjectType Type => _type;
        public int Id => _id;

        public void Init(ObjectType type, int id)
        {
            _id = id == 0 ? GetInstanceID() : id;
            _type = type;
        }
    }
}