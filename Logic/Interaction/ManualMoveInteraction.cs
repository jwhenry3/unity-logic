using System.Collections.Generic;
using System.Linq;
using Src.Logic.Player;
using UnityEngine;

namespace Src.Logic.Interaction
{
    [RequireComponent(typeof(PlayerControl))]
    public class ManualMoveInteraction : MonoBehaviour
    {
        private PlayerControl _player;
        private ClickToMoveInteraction _clickToMoveInteraction;

        private readonly Dictionary<KeyCode, Vector3> _moveDirections = new()
        {
            { KeyCode.A, Vector3.left },
            { KeyCode.D, Vector3.right },
            { KeyCode.W, Vector3.forward },
            { KeyCode.S, Vector3.back }
        };

        private void Start()
        {
            _player = GetComponent<PlayerControl>();
            _clickToMoveInteraction = GetComponent<ClickToMoveInteraction>();
        }

        private void Update()
        {
            if (!_player.hasInput) return;
            // construct move direction based off of the key mapping
            var pressedDirections = _moveDirections.Where(entry => Input.GetKey(entry.Key));
            var moveDirection = pressedDirections.Aggregate(Vector3.zero, (current, entry) => current + entry.Value);

            // do nothing if we did not press anything
            if (moveDirection == Vector3.zero) return;
            if (_clickToMoveInteraction) _clickToMoveInteraction.destinationTarget = null;
            // move the player in the direction we pressed
            _player.Move(moveDirection.normalized);
        }
    }
}