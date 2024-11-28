using Src.Logic.Player;
using UnityEngine;

namespace Src.Logic.Interaction
{
    public class NavigateInteraction : MonoBehaviour
    {
        public Camera mainCamera;

        private void Awake()
        {
            if (!mainCamera)
                mainCamera = Camera.main;
        }

        private void Update()
        {
            if (mainCamera && Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out var hit)) return;
                if (!hit.transform.CompareTag("Walkable")) return;
                
                Players.GetLocalPlayer().MoveTo(hit.point);
            }
        }
    }
}