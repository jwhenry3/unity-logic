using System.Collections.Generic;
using Src.Logic.Player;
using UnityEngine;

namespace Src.Logic.AI
{
    public class TargetContainer : MonoBehaviour
    {
        public GameObject target;
        
        public List<string> targetTags = new List<string>();

        public NpcControl GetNpcControl()
        {
            return target.GetComponent<NpcControl>();
        }

        public PlayerControl GetPlayerControl()
        {
            return target.GetComponent<PlayerControl>();
        }
    }
}