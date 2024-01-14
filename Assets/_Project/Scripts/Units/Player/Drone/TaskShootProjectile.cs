using Selivura.BehaviorTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
