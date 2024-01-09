using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Selivura.Tests
{
    public class FriendlyBaseTest
    {
        [UnityTest]
        public IEnumerator FriendlyBaseLevelUpTest()
        {
            MainBase mainBase = CreateBase();

            MainBaseData data = ScriptableObject.CreateInstance(typeof(MainBaseData)) as MainBaseData;
            mainBase.BaseData = data;
            yield return null;

            int matterToLevelUpBefore = mainBase.MatterToLevelUp;
            mainBase.ChangeMatter(mainBase.MatterToLevelUp);
            Assert.AreEqual(2, mainBase.Level);
            Assert.AreEqual(data.BaseHealth + data.HealthPerLevel, mainBase.MaxHealth);
            Assert.AreEqual(data.BaseCombatRadius + data.CombatRadiusPerLevel, mainBase.CombatArea.CombatEnableRadius);
            Assert.AreEqual(matterToLevelUpBefore + data.MatterProgression, mainBase.MatterToLevelUp);
        }
        [UnityTest]
        public IEnumerator BaseStartHealthTest()
        {
            MainBase mainBase = CreateBase();

            MainBaseData data = ScriptableObject.CreateInstance(typeof(MainBaseData)) as MainBaseData;
            mainBase.BaseData = data;
            yield return null;
            Assert.AreEqual(data.BaseHealth, mainBase.MaxHealth);
            Assert.AreEqual(data.BaseHealth, mainBase.CurrentHealth);
        }
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator CombatAreaRangeDisplayerTest()
        {
            CombatArea area = CreateCombarArea();

            GameObject displayGO = new GameObject("Range");

            displayGO.SetActive(false);
            BaseCombatRangeDisplayer rangeDisplayer = displayGO.AddComponent<BaseCombatRangeDisplayer>();
            rangeDisplayer.Area = area;
            displayGO.SetActive(true);

            yield return null;

            area.CombatEnableRadius = 25;

            Assert.AreEqual(area.CombatEnableRadius * 2, rangeDisplayer.transform.localScale.x);
            Assert.AreEqual(area.CombatEnableRadius * 2, rangeDisplayer.transform.localScale.y);
        }
        private static CombatArea CreateCombarArea()
        {
            GameObject baseGO = new GameObject("CombatArea");
            CircleCollider2D collider = baseGO.AddComponent<CircleCollider2D>();
            CombatArea area = baseGO.AddComponent<CombatArea>();
            area.CircleCollider = collider;
            return area;
        }
        private static MainBase CreateBase()
        {
            GameObject baseGO = new GameObject("Base");
            MainBase mainBase = baseGO.AddComponent<MainBase>();
            CombatArea area = CreateCombarArea();
            mainBase.CombatArea = area;
            return mainBase;
        }
    }
}
