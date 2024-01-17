using Selivura.BehaviorTrees;
using System.Collections.Generic;

namespace Selivura
{
    public class ScriptNode : Node
    {
        public ScriptNode(List<Node> children) : base(children) { }
        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Succes:
                        continue;
                    case NodeState.Running:
                        state = NodeState.Running;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.Succes;
            return state;
        }
    }
}
