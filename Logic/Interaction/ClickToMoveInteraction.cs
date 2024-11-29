using Src.Logic.AI;
using Src.Logic.Movement;
using Src.Logic.Player;
using UnityEngine;

namespace Src.Logic.Interaction
{
    [RequireComponent(typeof(PlayerControl))]
    [RequireComponent(typeof(TargetContainer))]
    public class ClickToMoveInteraction : MonoBehaviour
    {
        private PlayerControl _player;
        private Camera _mainCamera;
        private TargetContainer _targetContainer;
        private ClickToMoveTarget _destinationTarget;

        private void Start()
        {
            _mainCamera = Camera.main;
            _targetContainer = GetComponent<TargetContainer>();
            _player = GetComponent<PlayerControl>();
        }

        private void Update()
        {
            // invalid state, do nothing
            if (!_mainCamera || !Input.GetMouseButtonDown(0))
            {
                MoveToTarget();
                return;
            }

            // check the mouse position on click
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            // invalid hits, do nothing
            if (!Physics.Raycast(ray, out var hit)) return;
            _destinationTarget = null;
            var target = hit.transform.GetComponent<ClickToMoveTarget>();
            var targetReceiver = hit.transform.GetComponent<TargetReceiver>();
            if (targetReceiver && !targetReceiver.targetedBy.Contains(_targetContainer))
                return;
            if (target)
                _destinationTarget = target;
            if (!hit.transform.CompareTag("Walkable"))
                return;

            // move to location of click
            _player.MoveTo(hit.point);
        }

        private void MoveToTarget()
        {
            if (!_destinationTarget) return;

            var distance = Vector3.Distance(_player.transform.position, _destinationTarget.transform.position);
            if (distance < _destinationTarget.stopRadius)
                _player.Stop();
            else
                _player.MoveTo(_destinationTarget.transform.position);
        }
    }
}