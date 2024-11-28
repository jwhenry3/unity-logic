using System.Collections.Generic;
using System.Linq;
using Src.Logic.Cameras;
using UnityEngine;
using UnityEngine.AI;

namespace Src.Logic.Player
{
    public class PlayerControl : MonoBehaviour
    {
        public float moveSpeed = 5.0f;
        public NavMeshAgent agent;

        private readonly Dictionary<KeyCode, Vector3> _moveDirections = new()
        {
            { KeyCode.A, Vector3.left },
            { KeyCode.D, Vector3.right },
            { KeyCode.W, Vector3.forward },
            { KeyCode.S, Vector3.back }
        };

        private void Start()
        {
            Players.Instance.local = this;
            Players.Instance.list.Add(this);
            if (agent == null)
                agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;
            CameraTarget cameraTarget = FindFirstObjectByType<CameraTarget>();
            cameraTarget.target = transform;
        }


        private void Move()
        {
            var pressedDirections = _moveDirections.Where(entry => Input.GetKey(entry.Key));
            Vector3 moveDirection = Vector3.zero;
            foreach (var entry in pressedDirections)
            {
                moveDirection += entry.Value * moveSpeed * Time.deltaTime;
            }

            if (moveDirection == Vector3.zero) return;
            agent.isStopped = true;
            if (agent)
            {
                agent.Move(moveDirection);
                return;
            }

            transform.Translate(moveDirection);
        }

        public void MoveTo(Vector3 location)
        {
            agent.isStopped = false;
            agent.SetDestination(location);
        }

        private void Update()
        {
            if (!Cursor.visible)
                Cursor.visible = true;
            if (Cursor.lockState != CursorLockMode.None)
                Cursor.lockState = CursorLockMode.None;
            Move();
        }
    }
}