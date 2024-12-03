using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Src.Logic.Inventory
{
    public class Storage : MonoBehaviour
    {
        public string id;

        public string owner;

        public int size = 16;

        public string[] itemIds;
        public int[] amounts;


        private void Start()
        {
            itemIds = new string[size];
            amounts = new int[size];
        }

        public bool AddItem(string itemId, int amount, int index)
        {
            if (itemIds[index] != itemId && amounts[index] != 0) return false;
            itemIds[index] = itemId;
            amounts[index] += amount;
            return true;
        }

        public bool AddItem(string itemId, int amount)
        {
            return CanAddItem(itemId, out var indexToAddTo) && AddItem(itemId, amount, indexToAddTo);
        }

        public bool RemoveItem(string itemId, int amount, int index)
        {
            if (itemIds[index] != itemId || amounts[index] < amount) return false;
            amounts[index] -= amount;
            if (amounts[index] <= 0)
                itemIds[index] = null;
            return true;
        }

        public bool RemoveItem(string itemId, int amount)
        {
            if (!CanRemoveItem(itemId, amount, out var indexesToAdjust)) return false;
            foreach (var kvp in indexesToAdjust)
                RemoveItem(itemId, kvp.Value, kvp.Key);

            return true;
        }

        public bool CanAddItem(string itemId, out int indexToAddTo)
        {
            indexToAddTo = -1;
            for (var i = 0; i < size; i++)
            {
                if (itemIds[i] == id || itemIds[i] == null)
                {
                    indexToAddTo = i;
                    break;
                }
            }

            return indexToAddTo > -1;
        }

        public bool CanRemoveItem(string itemId, int amount, out Dictionary<int, int> indexesToAdjust)
        {
            var remaining = amount;
            indexesToAdjust = new Dictionary<int, int>();
            // Iterate backwards to remove items from the end first
            for (var i = size - 1; i > -1; i++)
            {
                if (itemIds[i] != itemId) continue;
                if (amounts[i] <= remaining)
                    indexesToAdjust.Add(i, amounts[i]);
                else
                {
                    indexesToAdjust.Add(i, amounts[i] - remaining);
                    remaining = 0;
                    break;
                }

                remaining -= amounts[i];
            }

            return remaining == 0;
        }
    }
}