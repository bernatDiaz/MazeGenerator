using System;
using System.Collections.Generic;

public class Direction : ICloneable
{
    public int row
    {
        get;
    }
    public int col
    {
        get;
    }
    public Direction(int row, int col)
    {
        this.row = row;
        this.col = col;
    }
    public object Clone()
    {
        return MemberwiseClone();
    }
    public static Direction operator -(Direction direction1, Direction direction2)
    {
        return new Direction(direction1.row - direction2.row, direction1.col - direction2.col);
    }
    public bool Oposite(Direction direction)
    {
        return row + direction.row == 0 && col + direction.col == 0;
    }
    public override string ToString()
    {
        return base.ToString() + ": [" + row.ToString() + "," + col.ToString() + "]";
    }
    public static void Add(Direction direction, List<Direction> list)
    {
        list.Add((Direction)direction.Clone());
    }
}
