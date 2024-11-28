using System;
using System.Linq;
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
            if (targetContainer == null)
                targetContainer = GetComponent<TargetContainer>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (targetContainer.target != null) return;
            TargetReceiver receiver = other.GetComponent<TargetReceiver>();
            GameObject targetEntity = receiver.entity;
            if (targetContainer.targetTags.Contains(targetEntity.tag))
                targetContainer.target = targetEntity;
        }

        public void OnTriggerExit(Collider other)
        {
            TargetReceiver receiver = other.GetComponent<TargetReceiver>();
            GameObject targetEntity = receiver.entity;
            if (targetContainer.target == targetEntity)
                targetContainer.target = null;
        }
    }
}