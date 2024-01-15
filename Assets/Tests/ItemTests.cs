using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace Selivura.Tests
{
    public class ItemTests
    {
        [UnityTest]
        public IEnumerator RegenItemTest()
        {
            TestUtils.CreateInjector();
            var player = TestUtils.CreatePlayer();
            player.TakeDamage(5);
            int playerHealthBefor = player.CurrentHealth;
            var item = Resources.Load<GameObject>("Items/RegenItem").GetComponent<RegenItem>();
            Debug.Log(item);
            player.AddItem(item);
            yield return new WaitForSeconds(1);
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(playerHealthBefor + 1, player.CurrentHealth);
        }
    }
}
