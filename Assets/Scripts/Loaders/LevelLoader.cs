using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour, ILevelLoader
{
    [SerializeField]
    private Card _cardPrefab;

    [SerializeField]
    private AnswerTextField _answerTextField;

    public UnityEvent ReadyToStart;
    public UnityEvent LevelsDone;

    private CellGrid _grid;
    private List<Card> _cards;

    private int _currentLevel;
    private int _amount;

    private SymbolGenerator[] _symbolGenerators;
    public string CurrentAnswer { get; private set; }

    private void CreateGenerators(IReadOnlyList<CardBundleData> cardBundles)
    {
        _symbolGenerators = new SymbolGenerator[cardBundles.Count];

        for (int i = 0; i < cardBundles.Count; i++)
        {
            _symbolGenerators[i] = new SymbolGenerator(cardBundles[i]);
        }
    }
    private IReadOnlySymbol[] GetRandomSymbols(int count)
    {
        if (count < 1)
            throw new ArgumentOutOfRangeException(nameof(count));

        int randGenIndex = UnityEngine.Random.Range(0, _symbolGenerators.Length);
        SymbolGenerator symbolGen = _symbolGenerators[randGenIndex];
        symbolGen.RefreshSymbolSet();
        IReadOnlySymbol answer = symbolGen.GetAnswer();

        IReadOnlySymbol[] symbols = new IReadOnlySymbol[count];
        int randAnswerIndex = UnityEngine.Random.Range(0, count);

        for (int i = 0; i < count; i++)
        {
            if (i == randAnswerIndex)
            {
                symbols[i] = answer;
                CurrentAnswer = symbols[i].Key;
                _answerTextField.RefreshAnswer(CurrentAnswer);
            } 
            else
            {
                symbols[i] = symbolGen.GetSymbol();
            }
        }

        return symbols;
    }

    private void SetCardSymbols()
    {
        IReadOnlySymbol[] symbols = GetRandomSymbols(_cards.Count);

        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].SetSymbol(symbols[i]);
        }
    }

    private IEnumerator CreateCards(int cardsInRow, int rows)
    {
        _cards = new List<Card>();

        for (int i = 0; i < cardsInRow * rows; i++)
        {
            Card card = Instantiate(_cardPrefab, transform);
            _cards.Add(card);
            card.gameObject.SetActive(false);
        }

        SetCardSymbols();

        for (int i = 0; i < cardsInRow * rows; i++)
        {
            Card card = _cards[i];
            GridPosition cardPos = new GridPosition(i % cardsInRow, i / cardsInRow);
            _grid.PlaceCell(card.transform, cardPos);

            card.gameObject.SetActive(true);
            card.GetComponent<CardAnimation>().AnimateShow(out float duration);

            for (float j = 0; j < duration; j += Time.deltaTime)
            {
                yield return null;
            }
        }

        ReadyToStart?.Invoke();
    }

    public void Init(DifficultySettings difficulty)
    {
        if (difficulty is null)
            throw new ArgumentNullException(nameof(difficulty));
        if (_cardPrefab is null)
            throw new ArgumentNullException(nameof(_cardPrefab));
        if (_answerTextField is null)
            throw new ArgumentNullException(nameof(_answerTextField));

        _cards?.ForEach(x => Destroy(x.gameObject));
        _amount = difficulty.AmountOfLevels;
        _currentLevel = 1;

        Vector2 cellSize = _cardPrefab.GetComponent<SpriteRenderer>().bounds.size;
        _grid = new CellGrid(difficulty.CellsInRow, difficulty.Rows, cellSize);

        CreateGenerators(difficulty.CardBundles);
        StartCoroutine(CreateCards(difficulty.CellsInRow, difficulty.Rows));
    }

    public IEnumerator LoadNew(float delay)
    {
        if (delay < 0)
            throw new ArgumentOutOfRangeException(nameof(delay));

        for (float i = 0; i < delay; i += Time.deltaTime)
        {
            yield return null;
        }

        if (_currentLevel == _amount)
        {
            LevelsDone?.Invoke();
            yield break;
        }

        SetCardSymbols();

        _currentLevel++;

        ReadyToStart?.Invoke();
    }
}
