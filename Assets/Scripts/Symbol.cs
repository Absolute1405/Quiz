using System;
using UnityEngine;

[Serializable]
public class Symbol : IReadOnlySymbol
{
    public string key;
    public Sprite sprite;

    public string Key => key;
    public Sprite Sprite => sprite;
}
