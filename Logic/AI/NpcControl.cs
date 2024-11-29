using Src.Logic.Movement;
using UnityEngine;

namespace Src.Logic.AI
{
    public class NpcControl : Movable
    {
        private TargetContainer _targetContainer;

        public bool canChase = true;
        public float chaseDistance = 5.0f;
        public float stopChaseDistance = 1.5f;
        public Transform chaseTarget;

        public bool canWander = true;
        public float wanderDistance = 5.0f;
        public float wanderInterval = 5.0f;
        [SerializeField] private float wanderTimer;

        private Vector3 _startingPosition;

        private bool _isChasing;

        protected override void Start()
        {
            base.Start();

            _targetContainer = GetComponent<TargetContainer>();

            _startingPosition = transform.position;
        }

        protected override void Update()
        {
            base.Update();

            UpdateChaseTarget();
            UpdateChase();
            UpdateWander();
        }

        private void UpdateChaseTarget()
        {
            // we should clear the chase target
            if (!canChase || !_targetContainer.target)
            {
                chaseTarget = null;
                return;
            }

            // chasing a target, no change
            if (chaseTarget) return;
            // set a new chase target
            chaseTarget = _targetContainer.target.transform;
        }

        private void UpdateChase()
        {
            // invalid state, do not chase
            if (!canMove) return;
            if (!canChase) return;
            if (!chaseTarget) return;

            // calculate distances and determine if we chase or stop
            var distanceFromStart = Vector3.Distance(_startingPosition, chaseTarget.position);
            var distanceFromTarget = Vector3.Distance(transform.position, chaseTarget.position);

            var isOutsideWanderDistance = distanceFromStart > wanderDistance;
            var hasTargetEscaped = distanceFromTarget > chaseDistance;

            var shouldStopChasing = isOutsideWanderDistance && hasTargetEscaped;
            var hasReachedTarget = distanceFromTarget <= stopChaseDistance;

            // stop chasing
            if (shouldStopChasing)
            {
                chaseTarget = null;
                _isChasing = false;
            }

            // stop the agent so it does not move
            if (hasReachedTarget || shouldStopChasing)
            {
                Stop();
                return;
            }

            // move toward the chase target
            _isChasing = true;
            wanderTimer = 0;
            MoveTo(chaseTarget.position);
        }

        private void UpdateWander()
        {
            // invalid state, do not wander
            if (!canMove) return;
            if (_isChasing) return;
            if (!canWander) return;
            if (wanderInterval <= Time.deltaTime) return;
            // increment wander timer if not moving
            if (!IsMoving())
                wanderTimer += Time.deltaTime;
            // do nothing until the timer has met the interval
            if (wanderTimer < wanderInterval) return;

            // do the wandering
            wanderTimer = 0;
            Wander();
        }

        private void Wander()
        {
            // wander to a random direction within the wander radius
            var distance = Random.Range(0, wanderDistance);
            var angle = Random.Range(0, 360);
            var destination = _startingPosition + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;

            MoveTo(destination);
        }
    }
}