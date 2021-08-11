using System;
using UnityEngine;

public class CellGrid 
{
    private int _cellsInRow;
    private int _rows;
    private Vector2 _cellSize;
    private Vector2 _zeroPoint;

    public CellGrid(int cellsInRow, int rows, Vector2 cellSize)
    {
        if (cellsInRow < 1 || rows < 1)
            throw new InvalidOperationException("Trying to create empty grid");
        if (cellSize.x <= 0 || cellSize.y <= 0)
            throw new ArgumentOutOfRangeException(nameof(cellSize));

        _cellsInRow = cellsInRow;
        _rows = rows;
        _cellSize = cellSize;

        // top left corner of a grid
        _zeroPoint = new Vector2(-cellsInRow, rows) * cellSize / 2; 
    }

    public void PlaceCell(Transform cell, GridPosition posOnGrid)
    {
        if (posOnGrid.Col > _cellsInRow || posOnGrid.Row > _rows)
            throw new ArgumentOutOfRangeException(nameof(posOnGrid));
        if (cell is null)
            throw new ArgumentNullException(nameof(cell));

        float x = _zeroPoint.x + posOnGrid.Col * _cellSize.x + _cellSize.x / 2;
        float y = _zeroPoint.y - posOnGrid.Row * _cellSize.y - _cellSize.x / 2;

        cell.position = new Vector2(x, y);
    }
}
