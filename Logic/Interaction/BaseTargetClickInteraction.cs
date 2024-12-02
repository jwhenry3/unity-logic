using System.Collections.Generic;
using System.Linq;
using Src.Logic.AI;
using Src.Logic.Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Src.Logic.Interaction
{
    [RequireComponent(typeof(PlayerControl))]
    [RequireComponent(typeof(TargetContainer))]
    public abstract class BaseTargetClickInteraction : MonoBehaviour
    {
        protected PlayerControl Player;
        protected Camera MainCamera;
        protected TargetContainer TargetContainer;


        protected virtual void Start()
        {
            MainCamera = Camera.main;
            TargetContainer = GetComponent<TargetContainer>();
            Player = GetComponent<PlayerControl>();
        }

        protected virtual void Update()
        {
            if (!Player.hasInput) return;
            // invalid state, do nothing
            if (!MainCamera || !Input.GetMouseButtonDown(0))
                return;

            // check the mouse position on click
            PointerEventData pointerData = new(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> hitList = new();
            EventSystem.current?.RaycastAll(pointerData, hitList);
            
            // invalid hits, do nothing
            if (hitList.Count > 0) return;

            var ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            var targetReceiver = hit.transform.GetComponent<TargetReceiver>();
            if (targetReceiver && targetReceiver.targetedBy.Contains(TargetContainer))
                InteractWithTarget(targetReceiver);
            InteractWithHit(hit);
        }

        protected abstract void InteractWithTarget(TargetReceiver targetReceiver);

        protected virtual void InteractWithHit(RaycastHit hit)
        {
        }
    }
}