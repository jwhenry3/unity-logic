using System;
using System.Collections.Generic;
using Src.Logic.AI;
using Src.Logic.Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Src.Logic.Interaction
{
    [RequireComponent(typeof(Door))]
    public class AutomaticDoorInteraction : MonoBehaviour
    {
        private Door _door;

        public List<Movable> movables = new();

        private void Start()
        {
            _door = GetComponent<Door>();
        }

        private void Update()
        {
            if (movables.Count > 0)
                _door.Open();
        }
        

        public void OnTriggerEnter(Collider other)
        {
            var movable = other.GetComponent<Movable>();
            if (movable && !movables.Contains(movable))
                movables.Add(movable);
        }

        public void OnTriggerExit(Collider other)
        {
            var movable = other.GetComponent<Movable>();
            if (movable && movables.Contains(movable))
                movables.Remove(movable);
        }
    }
}