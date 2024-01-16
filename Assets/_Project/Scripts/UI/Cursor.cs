using DG.Tweening;
using UnityEngine;
namespace Selivura.UI
{
    public class Cursor : MonoBehaviour
    {
        [SerializeField] float _shootPunch = 1.25f;
        Camera _cam;
        private void Awake()
        {
            _cam = Camera.main;
        }
        private void OnEnable()
        {
            UnityEngine.Cursor.visible = false;
        }
        private void OnDisable()
        {
            UnityEngine.Cursor.visible = true;
        }
        private void LateUpdate()
        {
            transform.position = (Vector2)_cam.ScreenToWorldPoint(Input.mousePosition);
        }
        public void PulseCursor()
        {
            transform.DOPunchScale(new Vector3(_shootPunch, _shootPunch, _shootPunch), 0.1f).SetUpdate(true);
        }
    }
}
