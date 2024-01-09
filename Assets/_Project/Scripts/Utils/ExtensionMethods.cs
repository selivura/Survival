using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Selivura
{
    public static class ExtensionMethods
    {
        public static Vector3 NormalizedDirectionTo(this Vector3 position, Vector3 target)
        {
            var pos = position.DirectionTo(target);
            pos.Normalize();
            return pos;
        }
        public static Vector3 DirectionTo(this Vector3 position, Vector3 target)
        {
            var pos = target - position;
            return pos;
        }
        public static T GetRandomElement<T>(this IEnumerable<T> array)
        {
            return array.ElementAt(array.GetRandomIndex<T>());
        }
        public static int GetRandomIndex<T>(this IEnumerable<T> array, int from = 0)
        {
            return Random.Range(from, array.Count());
        }

        public static void PlayOneShotWithParameters(this AudioSource source, AudioClip clip, SoundParameters parameters)
        {
            source.pitch = Random.Range(parameters.MinPitch, parameters.MaxPitch);
            source.volume = parameters.Volume;
            source.PlayOneShot(clip);
        }
        public static Vector2 WorldToCanvas(this Canvas canvas,
                                       Vector3 world_position,
                                       Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }

            var viewport_position = camera.WorldToViewportPoint(world_position);
            var canvas_rect = canvas.GetComponent<RectTransform>();

            return new Vector2((viewport_position.x * canvas_rect.sizeDelta.x) - (canvas_rect.sizeDelta.x * 0.5f),
                               (viewport_position.y * canvas_rect.sizeDelta.y) - (canvas_rect.sizeDelta.y * 0.5f));
        }
        public static void FlipSprite(this SpriteRenderer spriteRenderer, float direction)
        {
            if (direction < 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }
    }
    public static class Utilities
    {
        public static Vector2 RandomPositionInRangeLimited(Vector2 position, float minRange, float maxRange)
        {
            var pos = position;
            while (Vector2.Distance(position, pos) < minRange)
            {
                pos = RandomPositionInRange(pos, maxRange);
            }
            return pos;
        }
        public static Vector3 GetMouseDirection(Camera cam, Vector3 pos)
        {
            Vector3 dir = pos.NormalizedDirectionTo(cam.ScreenToWorldPoint(Input.mousePosition));
            return dir;
        }
        public static Vector2 RandomPositionInRange(Vector2 position, float range)
        {
            return position + new Vector2(Random.Range(-range, range), Random.Range(-range, range));
        }
    }
}
