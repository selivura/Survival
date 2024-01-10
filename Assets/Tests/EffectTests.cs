using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Selivura.Player;
using UnityEngine;
using UnityEngine.TestTools;

namespace Selivura.Tests
{
    public class EffectTests
    {
        [UnityTest]
        public IEnumerator EffectDespawnTest()
        {
            TestUtils.Setup();

            Effect effect = new GameObject("effect").AddComponent<Effect>();
            effect.Despawn();
            yield return null;
            Assert.AreEqual(false, effect.gameObject.activeSelf);
        }
        [UnityTest]
        public IEnumerator EffectDespawnLifetimeTest()
        {
            TestUtils.Setup();

            Effect effect = new GameObject("effect").AddComponent<Effect>();
            float lifetime = 2;
            effect.Setup(lifetime, true);
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(lifetime);
            Assert.AreEqual(false, effect.gameObject.activeSelf);
        }
    }
    public static class TestUtils
    {
        public static void Setup()
        {
            GameObject go = new GameObject("Injector");
            go.AddComponent<Injector>();
        }
    }
}
