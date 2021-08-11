using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextFade : MonoBehaviour, IFader
{
    [SerializeField]
    private float _fadeDuration;

    private TextMeshProUGUI _textField;
    private Color _startColorTransparent;

    private void Awake()
    {
        _textField = GetComponent<TextMeshProUGUI>();
        Color _startColorTransparent = _textField.color;
        _startColorTransparent.a = 0;

        SetTransparent();
    }

    public void FadeIn()
    {
        _textField.DOFade(0, _fadeDuration);
    }

    public void FadeOut()
    {
        _textField.DOFade(1, _fadeDuration);
    }

    public void SetTransparent()
    {
        _textField.color = _startColorTransparent;
    }
}
