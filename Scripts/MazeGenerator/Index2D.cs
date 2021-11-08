using System;
using System.Collections.Generic;

public class Index2D : IEquatable<Index2D>, ICloneable
{
    public int row;
    public int col;
    public Index2D()
    {

    }
    public Index2D(int row, int col)
    {
        this.row = row;
        this.col = col;
    }
    public object Clone()
    {
        return MemberwiseClone();
    }
    public void Randomize(int maxRow, int maxCol)
    {
        row = UnityEngine.Random.Range(0, maxRow);
        col = UnityEngine.Random.Range(0, maxCol);
    }
    #region equals
    public static bool operator ==(Index2D obj1, Index2D obj2)
    {
        if (ReferenceEquals(obj1, obj2))
        {
            return true;
        }
        if (ReferenceEquals(obj1, null))
        {
            return false;
        }
        if (ReferenceEquals(obj2, null))
        {
            return false;
        }

        return obj1.Equals(obj2);
    }

    public static bool operator !=(Index2D obj1, Index2D obj2)
    {
        return !(obj1 == obj2);
    }

    public bool Equals(Index2D other)
    {
        if (ReferenceEquals(other, null))
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return row.Equals(other.row)
               && col.Equals(other.col);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Index2D);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = row.GetHashCode();
            hashCode = (hashCode * 397) ^ col.GetHashCode();
            return hashCode;
        }
    }
    #endregion
    public override string ToString()
    {
        return base.ToString() + ": [" + (row + 1).ToString() + "," + (col + 1).ToString() + "]";
    }
    #region add subtract
    public static Index2D operator +(Index2D index, Direction direction)
    {
        return new Index2D(index.row + direction.row, index.col + direction.col);
    }

    public static Index2D operator -(Index2D index, Direction direction)
    {
        return new Index2D(index.row - direction.row, index.col - direction.col);
    }
    #endregion
    public static void Add(Index2D position, List<Index2D> list)
    {
        list.Add((Index2D)position.Clone());
    }
}
