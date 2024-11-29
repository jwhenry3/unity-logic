using UnityEngine;
using UnityEngine.AI;

namespace Src.Logic.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movable : MonoBehaviour
    {
        private NavMeshAgent _agent;
        public float moveSpeed = 5.0f;

        public bool canMove = true;

        private bool _isMoving;

        protected virtual void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = moveSpeed;
        }

        public void Move(Vector3 direction)
        {
            if (!canMove) return;
            if (direction == Vector3.zero) return;

            _agent.isStopped = true;
            _agent.Move(direction.normalized * (moveSpeed * Time.deltaTime));
        }

        public void MoveTo(Vector3 location)
        {
            if (!canMove) return;
            _agent.isStopped = false;
            _agent.SetDestination(location);
        }

        public void Stop()
        {
            _agent.isStopped = true;
            _agent.SetDestination(transform.position);
        }

        protected virtual void Update()
        {
            ValidateStopped();
            _isMoving = _agent.velocity.magnitude > 0.15f;
        }

        private void ValidateStopped()
        {
            if (!canMove)
                _agent.isStopped = true;
        }

        public bool IsMoving() => _isMoving;
    }
}