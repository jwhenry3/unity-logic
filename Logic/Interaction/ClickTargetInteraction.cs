using Src.Logic.AI;
using Src.Logic.Player;
using UnityEngine;

namespace Src.Logic.Interaction
{
    [RequireComponent(typeof(PlayerControl))]
    [RequireComponent(typeof(TargetContainer))]
    public class ClickTargetInteraction : MonoBehaviour
    {
        private TargetContainer _targetContainer;
        private PlayerControl _player;
        private Camera _mainCamera;
        public bool clickOffRemovesTarget = false;

        private void Start()
        {
            _mainCamera = Camera.main;

            _targetContainer = GetComponent<TargetContainer>();
            _player = GetComponent<PlayerControl>();
        }

        private void LateUpdate()
        {
            if (!_player.hasInput) return;
            // invalid state, do nothing
            if (!_mainCamera || !Input.GetMouseButtonDown(0))
                return;

            // check the mouse position on click
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            // invalid hits, do nothing
            if (!Physics.Raycast(ray, out var hit)) return;
            var target = hit.transform.GetComponent<TargetReceiver>();
            if (target)
                _targetContainer.SetTarget(target);
            else if (clickOffRemovesTarget)
                _targetContainer.target = null;
        }
    }
}