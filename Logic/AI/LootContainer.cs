using System.Collections.Generic;
using Src.Logic.Inventory;
using UnityEngine;

namespace Src.Logic.AI
{
    public class LootContainer : Storage
    {
        public int[] chances;
        public int[] baseAmounts;

        protected override void Start()
        {
            base.Start();
            chances = new int[size];
            baseAmounts = new int[size];
            ID = "loot";
        }

        public void GetLoot(out string[] lootItemIds, out int[] lootAmounts)
        {
            var itemIdList = new List<string>();
            var amountList = new List<int>();
            var currentIndex = 0;
            var rand = new System.Random();

            for (var i = 0; i < size; i++)
            {
                if (itemIds[i] == null) continue;
                if (amounts[i] == 0) continue;
                var chance = chances[i] > 0 ? chances[i] : 100;
                if (!(rand.NextDouble() >= chance)) continue;
                var amount = rand.Next(0, amounts[i]);
                itemIdList[currentIndex] = itemIds[i];
                amountList[currentIndex] = baseAmounts[i] + amount;
                currentIndex++;
            }

            lootItemIds = itemIdList.ToArray();
            lootAmounts = amountList.ToArray();
        }
    }
}