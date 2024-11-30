using System;
using Src.Logic.Player;
using UnityEngine;

namespace Src.Logic.AI
{
    public class Roof : MonoBehaviour
    {
        public GameObject visualObject;

        public void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerControl>();
            if (player && player.hasInput)
                visualObject.SetActive(false);
        }

        public void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<PlayerControl>();
            if (player && player.hasInput)
                visualObject.SetActive(true);
        }
    }
}