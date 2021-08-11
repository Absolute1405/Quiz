using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New difficulty settings", menuName = "Difficulty settings", order = 1)]
public class DifficultySettings : ScriptableObject
{
    [SerializeField, Min(1)]
    private int _amountOfLevels;

    [SerializeField, Min(1)]
    private int _cellsInRow;

    [SerializeField, Min(1)]
    private int _rows;

    [SerializeField]
    private List<CardBundleData> _cardBundles;

    public int AmountOfLevels => _amountOfLevels;
    public int CellsInRow => _cellsInRow;
    public int Rows => _rows;
    public IReadOnlyList<CardBundleData> CardBundles => _cardBundles;
}
