using UnityEngine;

namespace Selivura.UI
{
    public class UnitFloatingBarSpawner : MonoBehaviour
    {
        [SerializeField] FloatingHealthBarFactory _factory;
        [SerializeField] Unit _unit;
        UnitHPFloatingBar _current;
        Camera _cam;
        [Inject]
        OverlayWindowsContainer _overlayWindowsContainer;
        private void Awake()
        {
            _cam = Camera.main;
            FindFirstObjectByType<Injector>().Inject(this);
        }
        private void OnEnable()
        {
            _unit.OnDeinitialized.AddListener(DespawnCurrent);
            _unit.OnInitialized.AddListener(Spawn);
        }
        private void Spawn(Unit unit)
        {
            if (_current)
                return;
            _current = _factory.CreateAndSetParent(_overlayWindowsContainer.transform);
            _current.Unit = _unit;
            _current.LockTransform = transform;
            _current.Initialize();
        }
        private void OnDisable()
        {
            _unit.OnDeinitialized.RemoveListener(DespawnCurrent);
            _unit.OnInitialized.RemoveListener(Spawn);
        }
        private void Update()
        {
            if (_current)
                _current.transform.position = _cam.WorldToViewportPoint(transform.position);
        }
        private void DespawnCurrent(Unit unit)
        {
            Destroy(_current.gameObject);
        }
    }
}
