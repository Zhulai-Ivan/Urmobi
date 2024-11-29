using Data;
using UnityEngine;

namespace View
{
    public class ProducerObjectView : ObjectView
    {
        [SerializeField] private string _producedObjectId;
        public string ProducedObjectId => _producedObjectId;

        public void Init(int id, ObjectType type, string producedObjectId)
        {
            base.Init(type, id);
            _producedObjectId = producedObjectId;
        }
    }
}