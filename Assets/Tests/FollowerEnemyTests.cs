using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit;
using NUnit.Framework;
using Selivura.Player;

namespace Selivura.Tests
{
    public class FollowerEnemyTests
    {
        [UnityTest]
        public IEnumerator NoTargetTest()
        {
            (GameObject enemyGO, FollowerEnemyBT follower) = Setup();

            var posWas = enemyGO.transform.position;
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(posWas, enemyGO.transform.position);
        }
        [UnityTest]
        public IEnumerator TargetTest()
        {
            GameObject target = CreateTarget();
            target.transform.position = new Vector3(0, 10, 0);

            (GameObject enemyGO, FollowerEnemyBT follower) = Setup();


            yield return new WaitForSeconds(3);
            Debug.Log(enemyGO.transform.position);
            Assert.IsTrue(enemyGO.transform.position.y > 0);
        }

        private static GameObject CreateTarget()
        {
            GameObject target = new GameObject("Target");
            target.AddComponent<PlayerUnit>();
            target.AddComponent<Rigidbody2D>().gravityScale = 0;
            target.AddComponent<CircleCollider2D>();
            target.layer = 11;
            return target;
        }

        [UnityTest]
        public IEnumerator AttackTargetTest()
        {
            GameObject target = CreateTarget();
            var unit = target.GetComponent<PlayerUnit>();

            target.transform.position = new Vector3(0, 2, 0);

            (GameObject enemyGO, FollowerEnemyBT follower) = Setup();


            yield return new WaitForSeconds(3);

            Assert.IsTrue(unit.CurrentHealth < unit.MaxHealth);
        }
        private (GameObject, FollowerEnemyBT) Setup()
        {

            GameObject enemyGO = new GameObject("Enemy test");
            enemyGO.AddComponent<Rigidbody2D>().gravityScale = 0;
            enemyGO.AddComponent<RigidbodyMovement>();
            FollowerEnemyBT follower = enemyGO.AddComponent<FollowerEnemyBT>();

            return (enemyGO, follower);
        }
    }
}
