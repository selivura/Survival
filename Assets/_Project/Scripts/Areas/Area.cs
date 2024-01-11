using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Selivura
{
    public abstract class Area : MonoBehaviour
    {
        public CircleCollider2D CircleCollider;

        public UnityEvent OnAreaChanged;
        public float Radius
        {
            get
            {
                return CircleCollider.radius;
            }
            set
            {
                CircleCollider.radius = value;
                OnAreaChanged?.Invoke();
            }
        }

        protected abstract void OnTriggerStay2D(Collider2D collision);
        protected abstract void OnTriggerExit2D(Collider2D collision);
    }
}
