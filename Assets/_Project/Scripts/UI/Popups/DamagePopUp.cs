using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Selivura.UI
{
    public class DamagePopUp : MonoBehaviour
    {
        [SerializeField] TMP_Text _textMeshPro;
        [SerializeField] private float _lifetime = 1f;
        [SerializeField] private float _moveUpValue = 1;
        [SerializeField] Ease _ease = Ease.Linear;
        public void Setup(int damageAmount)
        {
            _textMeshPro.text = (-damageAmount).ToString();
            transform.DOMoveY(transform.position.y + _moveUpValue, _lifetime);
            transform.DOScale(0, _lifetime).SetEase(_ease);
            Destroy(gameObject, _lifetime);
        }
    }
}
