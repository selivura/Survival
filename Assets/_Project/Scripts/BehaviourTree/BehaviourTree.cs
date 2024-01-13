using UnityEngine;

namespace Selivura.BehaviorTrees
{
    public abstract class BehaviourTree : MonoBehaviour
    {
        private Node _root = null;
        protected void OnEnable()
        {
            _root = SetupTree();
        }
        protected void FixedUpdate()
        {
            if (_root != null)
            {
                _root.Evaluate();
            }
        }
        protected abstract Node SetupTree();
    }
}
