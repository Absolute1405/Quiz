using System;

public struct GridPosition
{
    public readonly int Col;
    public readonly int Row;

    public GridPosition(int col, int row)
    {
        if (col < 0)
            throw new ArgumentOutOfRangeException(nameof(col));
        if (row < 0)
            throw new ArgumentOutOfRangeException(nameof(row));

        Col = col;
        Row = row;
    }
}
