using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card bundle", menuName = "Card Bundle", order = 0)]
public class CardBundleData : ScriptableObject
{
    [SerializeField]
    private List<Symbol> _symbols;

    public IReadOnlyList<IReadOnlySymbol> Symbols => _symbols;

    public IReadOnlySymbol GetRandomSymbol()
    {
        int index = Random.Range(0, _symbols.Count);

        return _symbols[index];
    }
}
