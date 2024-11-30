using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Src.Logic.AI
{
    [Serializable]
    public class DialogueOption
    {
        public string id;
        public string text;
        public bool willProgress;
        public bool isCancel;
        public bool isBack;
    }
    [Serializable]
    public class Dialogue
    {
        public string text = "";
        public List<DialogueOption> options = new();
    }

    public class DialogueHolder : MonoBehaviour
    {
        public List<Dialogue> dialogues = new();
    }
}