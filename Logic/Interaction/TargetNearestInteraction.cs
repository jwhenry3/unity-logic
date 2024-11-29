using Src.Logic.AI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Src.Logic.Interaction
{
    public class TargetNearestInteraction : MonoBehaviour
    {
        public TargetContainer targetContainer;

        private void Start()
        {
            if (!targetContainer)
                targetContainer = GetComponent<TargetContainer>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (targetContainer.target != null) return;
            targetContainer.SetTarget(other.gameObject);
        }

        public void OnTriggerExit(Collider other)
        {
            targetContainer.UnsetTarget(other.gameObject);
        }
    }
}