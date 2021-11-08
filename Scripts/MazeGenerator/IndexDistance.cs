using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndexDistance
{
    public Index2D index
    {
        get;
    }
    public int distance
    {
        get;
    }
    public IndexDistance(Index2D index, int distance)
    {
        this.index = index;
        this.distance = distance;
    }
}
