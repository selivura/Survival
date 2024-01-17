using Selivura.BehaviorTrees;

namespace Selivura
{
    public class IfNode : Node
    {
        private Node _condition;
        private Node _succes;
        private Node _failure;
        public IfNode(Node condition, Node succes, Node failure)
        {
            _condition = condition;
            _succes = succes;
            _failure = failure;
        }
        public override NodeState Evaluate()
        {
            switch (_condition.Evaluate())
            {
                case NodeState.Failure:
                    state = _failure.Evaluate();
                    return state;
                case NodeState.Succes:
                    state = _succes.Evaluate();
                    return state;
                case NodeState.Running:
                    break;
                default:
                    break;
            }
            state = NodeState.Running;
            return state;
        }
    }
}
