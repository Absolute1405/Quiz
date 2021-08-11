using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BlackoutFade : MonoBehaviour, IFader
{
    [SerializeField]
    private float _fadeDuration;

    private Image image;
    private Color _startColorTransparent;

    private void Awake()
    {
        image = GetComponent<Image>();

        Color _startColorTransparent = image.color;
        _startColorTransparent.a = 0;

        SetTransparent();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        image.DOFade(0, _fadeDuration);
        Invoke(nameof(Deactivate), _fadeDuration);
    }

    public void FadeOut()
    {
        image.DOFade(1, _fadeDuration);
    }

    public void SetTransparent()
    {
        image.color = _startColorTransparent;
    }


}
