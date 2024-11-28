using System;
using UnityEngine;

namespace Src.Logic.Cameras
{
    public class CameraTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = Vector3.up;
        public float maxDistance = 5f;

        private void Update()
        {
            if (target)
            {
                Vector3 origin = target.position + offset;
                float distance = ((origin) - transform.position).magnitude;

                if (distance >= 0.5f)
                {
                    transform.position = Vector3.Slerp(transform.position, origin, Time.deltaTime * distance);
                }
            }
        }
    }
}