using UnityEngine;

namespace Src.Logic.Cameras
{
    public class CameraTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = Vector3.up;

        private void Update()
        {
            if (!target) return;

            var targetPosition = target.position + offset;
            var distance = ((targetPosition) - transform.position).magnitude;
            var magnitude = Time.deltaTime * 4;
            if (distance >= 1f)
                magnitude = Time.deltaTime * distance * 2;
            transform.position = Vector3.Slerp(transform.position, targetPosition, magnitude);
        }
    }
}