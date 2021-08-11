using UnityEngine;
using DG.Tweening;

public class SymbolAnimation : MonoBehaviour
{
    [Header("Bounce Animation")]

    [SerializeField, Min(1f)]
    private float _maxScale;
    [SerializeField]
    private AnimationCurve _bounceCurve;
    [SerializeField, Min(0)]
    private float _bounceDuration;

    [Header("Shake Animation")]

    [SerializeField]
    private float _shakeMaxAngle;
    [SerializeField, Min(0)]
    private int _shakeVibrato;
    [SerializeField, Min(0)]
    private float _shakeDuration;

    public float Bounce()
    {
        transform.DOScale(transform.localScale * _maxScale, _bounceDuration).SetEase(_bounceCurve);
        return _bounceDuration;
    }

    public float Shake()
    {
        transform.DOShakeRotation(_shakeDuration, Vector3.forward * _shakeMaxAngle, _shakeVibrato, _shakeMaxAngle * 2);
        return _shakeDuration;
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
