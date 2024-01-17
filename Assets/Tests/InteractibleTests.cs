using NUnit.Framework;
using Selivura.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Selivura.Tests
{
    public class InteractibleTest
    {
        [UnityTest]
        public IEnumerator ShopCanInteractTest()
        {
            Shop shop = new GameObject("Shop").AddComponent<Shop>();
            Item item = new GameObject("item").AddComponent<StatItem>();
            PlayerUnit playerUnit = TestUtils.CreatePlayer();

            playerUnit.ChangeMatter(25);
            item.Price = 100;

            shop.ItemPrefab = item;

            Assert.IsFalse(shop.CanInteract(playerUnit));
            yield return null;
        }



        [UnityTest]
        public IEnumerator ShopInteractBuyTest()
        {
            Shop shop = new GameObject("Shop").AddComponent<Shop>();
            StatItem item = new GameObject("item").AddComponent<StatItem>();
            item.AttackDamage.Value = 100;
            shop.ItemPrefab = item;
            PlayerUnit playerUnit = TestUtils.CreatePlayer();

            int cost = 100;

            playerUnit.ChangeMatter(cost);
            item.Price = cost;

            shop.Interact(playerUnit);

            Assert.AreEqual(0, playerUnit.MatterHarvested);
            Assert.AreEqual(item.AttackDamage.Value + playerUnit.BasePlayerStats.AttackDamage, playerUnit.PlayerStats.AttackDamage.Value);
            yield return null;
        }
    }
}
