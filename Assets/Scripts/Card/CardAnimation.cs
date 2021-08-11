using DG.Tweening;
using System;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _rightAnswerEffect;
    [SerializeField]
    private SymbolAnimation _symbol;

    [Header("Bounce Animation")]

    [SerializeField]
    private AnimationCurve _bounceCurve;
    [SerializeField, Min(0)]
    private float _bounceDuration;

    private void Awake()
    {
        if (_symbol is null)
            throw new ArgumentNullException(nameof(_symbol));
        if (_rightAnswerEffect is null)
            throw new ArgumentNullException(nameof(_rightAnswerEffect));


    }

    public void AnimateShow(out float duration)
    {
        _rightAnswerEffect.Stop();
        duration = _bounceDuration;
        Vector3 tmpScale = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.DOScale(tmpScale, _bounceDuration).SetEase(_bounceCurve);
    }

    public void AnimateClick(bool win, out float duration)
    {
        if (win)
        {
            duration = _symbol.Bounce();
            _rightAnswerEffect.Play();
        }
        else
        {
            duration = _symbol.Shake();
        }
    }

    private void OnDestroy()
    {
        _symbol.DOKill();
    }

}
