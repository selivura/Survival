using Selivura.Player;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Selivura.Tests
{
    public static class TestUtils
    {
        public static void Setup()
        {
            GameObject go = new GameObject("Injector");
            go.AddComponent<Injector>();
        }
        public static PlayerUnit CreatePlayer()
        {
            GameObject go = new GameObject("player");
            go.layer = 11;
            go.AddComponent<Rigidbody2D>().gravityScale = 0;
            go.AddComponent<CircleCollider2D>();
            var playerUnit =  go.AddComponent<PlayerUnit>();
            playerUnit.BasePlayerStats = new BasePlayerStats();
            playerUnit.Initialize();
            return playerUnit;
        }
    }
}
