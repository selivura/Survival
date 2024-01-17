using Selivura.BehaviorTrees;

namespace Selivura
{
    public class TaskShootProjectile : Node
    {
        public override NodeState Evaluate()
        {
            return NodeState.Failure;
        }
    }
}
