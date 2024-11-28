using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Src.Logic.AI
{
    public class NpcControl : MonoBehaviour
    {
        public TargetContainer targetContainer;

        public float moveSpeed = 5.0f;
        public bool canMove = true;
        public bool canChase = true;
        public float chaseDistance = 5.0f;
        public float stopChaseDistance = 1.5f;
        public Transform chaseTarget;

        public bool canWander = true;
        public float wanderDistance = 5.0f;
        public float wanderInterval = 5.0f;
        [SerializeField] private float wanderTimer;

        public NavMeshAgent agent;
        private Vector3 _startingPosition;

        private bool isMoving;

        private void Start()
        {
            if (agent == null)
                agent = GetComponent<NavMeshAgent>();
            if (targetContainer == null)
                targetContainer = GetComponent<TargetContainer>();
            agent.speed = moveSpeed;
            _startingPosition = transform.position;
        }

        private void Update()
        {
            isMoving = agent.velocity.magnitude > 0.15f;
            if (!canMove || !canWander)
            {
                wanderTimer = 0;
                return;
            }

            if (canChase)
            {
                if (!chaseTarget && targetContainer.target)
                    chaseTarget = targetContainer.target.transform;

                if (chaseTarget)
                {
                    wanderTimer = 0;
                    UpdateChase();
                    return;
                }
            }

            UpdateWander();
        }

        private void UpdateChase()
        {
            float distanceFromStart = Vector3.Distance(_startingPosition, chaseTarget.position);
            float distanceFromTarget = Vector3.Distance(transform.position, chaseTarget.position);

            if ((distanceFromStart > chaseDistance) && (distanceFromTarget > chaseDistance))
            {
                chaseTarget = null;
                agent.isStopped = true;
                return;
            }

            if (distanceFromTarget <= stopChaseDistance)
            {
                agent.isStopped = true;
                return;
            }

            MoveTo(chaseTarget.position);
        }

        private void UpdateWander()
        {
            if (!isMoving)
                wanderTimer += Time.deltaTime;
            if (!(wanderTimer >= wanderInterval) || wanderInterval <= Time.deltaTime) return;

            wanderTimer = 0;
            Wander();
        }

        private void MoveTo(Vector3 location)
        {
            agent.isStopped = false;
            agent.SetDestination(location);
        }

        private void Wander()
        {
            float distance = Random.Range(0, wanderDistance);
            float angle = Random.Range(0, 360);
            Vector3 destination = _startingPosition + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;

            MoveTo(destination);
        }
    }
}