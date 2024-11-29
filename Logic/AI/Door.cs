using Src.Logic.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Src.Logic.AI
{
    [RequireComponent(typeof(ClickToMoveTarget))]
    public class Door : MonoBehaviour
    {
        private float _doorOpenTime;
        public GameObject visualObject;
        public NavMeshObstacle navMeshObstacle;
        public Collider doorCollider;

        public ClickToMoveTarget clickToMoveTarget;
        private void Update()
        {
            clickToMoveTarget.enabled = false;
            if (navMeshObstacle.enabled) return;
            _doorOpenTime += Time.deltaTime;

            if (_doorOpenTime < 1f) return;

            visualObject.SetActive(true);
            navMeshObstacle.enabled = true;
            doorCollider.enabled = true;
            _doorOpenTime = 0f;
            navMeshObstacle.carving = true;
        }

        public void Open()
        {
            navMeshObstacle.enabled = false;
            doorCollider.enabled = false;
            visualObject.SetActive(false);
            _doorOpenTime = 0f;
            navMeshObstacle.carving = false;
        }
    }
}