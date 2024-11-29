using Src.Logic.AI;
using Src.Logic.Movement;
using Src.Logic.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Src.Logic.Interaction
{
    [RequireComponent(typeof(PlayerControl))]
    [RequireComponent(typeof(TargetContainer))]
    public class ClickToMoveInteraction : BaseTargetClickInteraction
    {
        public ClickToMoveTarget destinationTarget;

        private bool _clickedATarget;

        protected override void Update()
        {
            // invalid state, do nothing
            if (!MainCamera || !Input.GetMouseButtonDown(0))
            {
                MoveToTarget();
                return;
            }

            _clickedATarget = false;
            base.Update();
        }

        protected override void InteractWithTarget(TargetReceiver targetReceiver)
        {
            _clickedATarget = true;
            var target = targetReceiver.GetComponent<ClickToMoveTarget>();
            if (target && target.enabled)
                destinationTarget = target;
        }

        protected override void InteractWithHit(RaycastHit hit)
        {
            if (_clickedATarget) return;
            if (!hit.transform.CompareTag("Walkable")) return;
            Player.MoveTo(hit.point);
        }

        private void MoveToTarget()
        {
            if (!destinationTarget) return;

            var distance = Vector3.Distance(Player.transform.position, destinationTarget.transform.position);
            if (distance < destinationTarget.stopRadius)
                Player.Stop();
            else
                Player.MoveTo(destinationTarget.transform.position);
        }
    }
}