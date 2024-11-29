using System.Collections.Generic;
using UnityEngine;

namespace Src.Logic.Player
{
    public class Players : MonoBehaviour
    {
        public static Players Instance;

        public static readonly List<PlayerControl> List = new();
        public static PlayerControl Local;

        private void Start()
        {
            Instance = this;
        }
    }
}