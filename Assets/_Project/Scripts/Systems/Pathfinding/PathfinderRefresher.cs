using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class PathfinderRefresher : MonoBehaviour
    {
        [SerializeField] AstarPath _astar;

        public void Refresh()
        {
            _astar.Scan();
        }
    }
}
