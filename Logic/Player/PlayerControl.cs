using System.Collections.Generic;
using System.Linq;
using Src.Logic.AI;
using Src.Logic.Cameras;
using Src.Logic.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace Src.Logic.Player
{
    [RequireComponent(typeof(TargetContainer))]
    public class PlayerControl : Movable
    {
        private TargetContainer _targetContainer;


        protected override void Start()
        {
            base.Start();

            _targetContainer = GetComponent<TargetContainer>();

            Players.Local = this;
            Players.List.Add(this);

            var cameraTarget = FindFirstObjectByType<CameraTarget>();
            cameraTarget.target = transform;
        }


        protected override void Update()
        {
            base.Update();

            if (!Cursor.visible)
                Cursor.visible = true;
            if (Cursor.lockState != CursorLockMode.None)
                Cursor.lockState = CursorLockMode.None;
        }
    }
}