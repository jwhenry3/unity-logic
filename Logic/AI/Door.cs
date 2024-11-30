using Src.Logic.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Src.Logic.AI
{
    public class Door : MonoBehaviour
    {
        private float _doorOpenTime;
        public GameObject visualObject;
        public NavMeshObstacle navMeshObstacle;
        public Collider doorCollider;
        private bool _isOpen;

        public ClickToMoveTarget clickToMoveTarget;

        private void Update()
        {
            if (clickToMoveTarget)
                clickToMoveTarget.enabled = false;
            if (!_isOpen) return;
            _doorOpenTime += Time.deltaTime;

            if (_doorOpenTime < 0.5f) return;

            visualObject.SetActive(true);
            if (navMeshObstacle)
                navMeshObstacle.enabled = true;
            if (navMeshObstacle)
                navMeshObstacle.carving = true;
            if (doorCollider)
                doorCollider.enabled = true;
            _doorOpenTime = 0f;
            _isOpen = false;
        }

        public void Open()
        {
            if (doorCollider)
                doorCollider.enabled = false;
            visualObject.SetActive(false);
            _doorOpenTime = 0f;
            if (navMeshObstacle)
                navMeshObstacle.enabled = false;
            if (navMeshObstacle)
                navMeshObstacle.carving = false;
            _isOpen = true;
        }
    }
}