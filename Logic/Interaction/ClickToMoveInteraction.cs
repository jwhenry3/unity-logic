using Src.Logic.Player;
using UnityEngine;

namespace Src.Logic.Interaction
{
    [RequireComponent(typeof(PlayerControl))]
    public class ClickToMoveInteraction : MonoBehaviour
    {
        private PlayerControl _player;
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
            _player = GetComponent<PlayerControl>();
        }

        private void Update()
        {
            // invalid state, do nothing
            if (!_mainCamera || !Input.GetMouseButtonDown(0)) return;
            // check the mouse position on click
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            // invalid hits, do nothing
            if (!Physics.Raycast(ray, out var hit)) return;
            if (!hit.transform.CompareTag("Walkable")) return;

            // move to location of click
            _player.MoveTo(hit.point);
        }
    }
}