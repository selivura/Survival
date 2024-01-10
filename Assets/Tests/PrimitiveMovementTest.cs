using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Selivura.Player;
using UnityEngine;
using UnityEngine.TestTools;
namespace Selivura.Tests
{
    public class PrimitiveMovementTest
    {
        [UnityTest]
        public IEnumerator MovementTest()
        {
            PrimitiveMovement testObject = new GameObject("movement").AddComponent<PrimitiveMovement>();
            testObject.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            testObject.Move(Vector2.right, 1);

            yield return new WaitForFixedUpdate();
            Assert.AreEqual(Vector3.right * Time.fixedDeltaTime, testObject.transform.position);
        }
    }
}
