using Src.Logic.AI;
using UnityEngine;

namespace Src.Logic.Interaction
{
    [RequireComponent(typeof(TargetContainer))]
    public class TargetNearestInteraction : MonoBehaviour
    {
        private TargetContainer _targetContainer;

        private void Start()
        {
            _targetContainer = GetComponent<TargetContainer>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (_targetContainer.target != null) return;
            _targetContainer.SetTarget(_targetContainer.GetTargetEntity(other));
        }

        public void OnTriggerExit(Collider other)
        {
            _targetContainer.UnsetTarget(_targetContainer.GetTargetEntity(other));
        }
    }
}