using System.Collections.Generic;
using Src.Logic.Player;
using UnityEngine;

namespace Src.Logic.AI
{
    public class TargetContainer : MonoBehaviour
    {
        public GameObject target;

        public List<string> targetTags = new List<string>();

        public NpcControl GetNpcControl()
        {
            return target.GetComponent<NpcControl>();
        }

        public PlayerControl GetPlayerControl()
        {
            return target.GetComponent<PlayerControl>();
        }

        public void SetTarget(GameObject targetEntity)
        {
            if (!targetEntity) return;
            if (targetTags.Contains(targetEntity.tag))
                target = targetEntity;
        }

        public void UnsetTarget(GameObject targetEntity)
        {
            if (target == targetEntity)
                target = null;
        }

        public GameObject GetTargetEntity(Component cmp)
        {
            Debug.Log(cmp.transform);
            var receiver = cmp.GetComponent<TargetReceiver>();
            return !receiver ? null : receiver.entity;
        }
    }
}