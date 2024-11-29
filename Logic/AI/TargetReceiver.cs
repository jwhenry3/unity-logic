using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Src.Logic.AI
{
    public class TargetReceiver : MonoBehaviour
    {
        public GameObject entity;
        public GameObject reticle;
        public List<TargetContainer> targetedBy = new();


        private void Update()
        {
            if (!reticle) return;
            reticle.SetActive(targetedBy.Count > 0);
        }
    }
}