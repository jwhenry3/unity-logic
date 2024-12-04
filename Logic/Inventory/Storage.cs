using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Src.Logic.Inventory
{
    public class Storage : MonoBehaviour
    {
        public string ID { get; protected set; }

        public string owner;

        public int size = 16;

        public string[] itemIds;
        public int[] amounts;

        public event Action OnUpdate;


        protected virtual void Start()
        {
            itemIds = new string[size];
            amounts = new int[size];
        }

        public bool MoveItemTo(Storage toStorage, int fromIndex)
        {
            var transaction = StorageTransaction.Create(this, toStorage);
            return transaction.MoveItem(fromIndex);
        }

        public bool AddItem(string itemId, int amount, int index)
        {
            if (itemIds[index] != itemId && amounts[index] != 0) return false;
            itemIds[index] = itemId;
            amounts[index] += amount;
            TriggerUpdate();
            return true;
        }

        public bool AddItem(string itemId, int amount, out int indexAddedTo)
        {
            return CanAddItem(itemId, out indexAddedTo) && AddItem(itemId, amount, indexAddedTo);
        }

        public bool RemoveItem(string itemId, int amount, int index)
        {
            if (itemIds[index] != itemId || amounts[index] < amount) return false;
            amounts[index] -= amount;
            if (amounts[index] <= 0)
                itemIds[index] = null;
            TriggerUpdate();
            return true;
        }

        public bool RemoveItem(string itemId, int amount)
        {
            if (!CanRemoveItem(itemId, amount, out var indexesToAdjust)) return false;
            foreach (var kvp in indexesToAdjust)
                RemoveItem(itemId, kvp.Value, kvp.Key);

            TriggerUpdate();
            return true;
        }

        public bool CanAddItem(string itemId, out int indexToAddTo)
        {
            indexToAddTo = -1;
            for (var i = 0; i < size; i++)
            {
                if (itemIds[i] == ID || itemIds[i] == null)
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

        public bool MoveItem(int fromIndex, int toIndex, bool swap = false)
        {
            var itemId1 = itemIds[fromIndex];
            var quantity1 = amounts[fromIndex];
            var itemId2 = itemIds[toIndex];
            var quantity2 = amounts[toIndex];

            if (itemId1 == itemId2)
            {
                if (quantity1 > 0 && quantity2 > 0 && !swap)
                {
                    amounts[fromIndex] = 0;
                    itemIds[fromIndex] = null;

                    amounts[toIndex] = quantity1 + quantity2;
                    TriggerUpdate();
                    return true;
                }
            }

            amounts[fromIndex] = quantity2;
            amounts[toIndex] = quantity1;

            itemIds[fromIndex] = itemId2;
            itemIds[toIndex] = itemId1;
            TriggerUpdate();
            return true;
        }

        public void TriggerUpdate()
        {
            OnUpdate?.Invoke();
        }
    }
}