using Src.Logic.AI;
using Src.Logic.Movement;
using Src.Logic.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Src.Logic.Interaction
{
    [RequireComponent(typeof(PlayerControl))]
    [RequireComponent(typeof(TargetContainer))]
    public class ClickToOpenDoorInteraction : BaseTargetClickInteraction
    {
        public ClickToMoveInteraction clickToMoveInteraction;

        protected override void InteractWithTarget(TargetReceiver targetReceiver)
        {
            var door = targetReceiver.GetComponent<Door>();
            if (!door) return;

            var distance = Vector3.Distance(targetReceiver.transform.position, TargetContainer.transform.position);
            if (distance < door.clickToMoveTarget.stopRadius)
            {
                door.Open();
                if (clickToMoveInteraction && clickToMoveInteraction.destinationTarget == door.clickToMoveTarget)
                    clickToMoveInteraction.destinationTarget = null;
            }
            else if (clickToMoveInteraction) clickToMoveInteraction.destinationTarget = door.clickToMoveTarget;
        }
    }
}