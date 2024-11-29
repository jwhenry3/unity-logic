using System;
using System.Collections.Generic;
using Src.Logic.Player;
using UnityEngine;

namespace Src.Logic.AI
{
    public class TargetContainer : MonoBehaviour
    {
        public TargetReceiver target;

        public List<string> targetTags = new List<string>();

        public void SetTarget(GameObject targetEntity)
        {
            SetTarget(GetReceiver(targetEntity));
        }

        public void SetTarget(TargetReceiver targetEntity)
        {
            if (!targetEntity) return;
            if (!targetTags.Contains(targetEntity.tag)) return;
            if (target) UnsetTarget(target);

            target = targetEntity;
            if (!target.targetedBy.Contains(this))
                target.targetedBy.Add(this);
        }

        public void UnsetTarget(GameObject targetEntity)
        {
            UnsetTarget(GetReceiver(targetEntity));
        }

        public void UnsetTarget(TargetReceiver targetEntity)
        {
            if (!targetEntity) return;
            if (target != targetEntity) return;
            
            if (target.targetedBy.Contains(this))
                target.targetedBy.Remove(this);
            target = null;
        }

        private static TargetReceiver GetReceiver(GameObject obj)
        {
            return obj.GetComponent<TargetReceiver>();
        }
    }
}