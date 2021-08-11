using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CardAnimation))]
public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _symbolRenderer;

    public IReadOnlySymbol Symbol { get; private set; }

    public void SetSymbol(IReadOnlySymbol symbol)
    {
        if (symbol is null)
            throw new ArgumentNullException(nameof(symbol));

        Symbol = symbol;
        _symbolRenderer.sprite = symbol.Sprite;
    }
}
