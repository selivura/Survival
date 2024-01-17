using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
namespace Selivura.Tests
{
    public class PrimitiveMovementTest
    {
        [UnityTest]
        public IEnumerator MovementTest()
        {
            RigidbodyMovement testObject = new GameObject("movement").AddComponent<RigidbodyMovement>();
            testObject.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            testObject.Move(Vector2.right, 1);

            yield return new WaitForFixedUpdate();
            Assert.AreEqual(Vector3.right * Time.fixedDeltaTime, testObject.transform.position);
        }
    }
}
