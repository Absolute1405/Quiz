using System;
using System.Collections.Generic;

public class SymbolGenerator
{
    private CardBundleData _data;
    private List<IReadOnlySymbol> _busyAnswers;
    private List<IReadOnlySymbol> _busySymbols;
    private IReadOnlySymbol _answerSymbol;

    public SymbolGenerator(CardBundleData data)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(data));

        _data = data;

        _busyAnswers = new List<IReadOnlySymbol>();
        _busySymbols = new List<IReadOnlySymbol>();
        _answerSymbol = _data.GetRandomSymbol();
    }

    public void RefreshSymbolSet()
    {
        _busySymbols.Clear();
    }

    public IReadOnlySymbol GetSymbol()
    {
        int tries = 0;
        IReadOnlySymbol symbol = _data.GetRandomSymbol();

        while (_busySymbols.Contains(symbol) || symbol == _answerSymbol)
        {
            tries++;
            symbol = _data.GetRandomSymbol();

            if (tries == _data.Symbols.Count)
                throw new InvalidOperationException("All answers is busy");
        }

        _busySymbols.Add(symbol);
        return symbol;
    }

    public IReadOnlySymbol GetAnswer()
    {
        int tries = 0;

        while (_busyAnswers.Contains(_answerSymbol))
        {
            tries++;
            _answerSymbol = _data.GetRandomSymbol();

            if (tries == _data.Symbols.Count)
                throw new InvalidOperationException("All answers is busy");
        }

        _busyAnswers.Add(_answerSymbol);
        return _answerSymbol;
    }
}
