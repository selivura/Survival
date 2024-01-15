using UnityEngine;

namespace Selivura.BehaviorTrees
{
    public abstract class BehaviourTree : MonoBehaviour
    {
        protected Node root = null;
        protected void OnEnable()
        {
            root = SetupTree();
        }
        protected void FixedUpdate()
        {
            if (root != null)
            {
                root.Evaluate();
            }
        }
        protected abstract Node SetupTree();
    }
}
