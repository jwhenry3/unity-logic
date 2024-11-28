using System.Collections.Generic;
using UnityEngine;

namespace Src.Logic.Player
{
    public class Players : MonoBehaviour
    {
        public static Players Instance;

        public List<PlayerControl> list = new();
        public PlayerControl local;

        private void Start()
        {
            Instance = this;
        }

        public static PlayerControl GetLocalPlayer()
        {
            return Instance?.local;
        }
    }
}