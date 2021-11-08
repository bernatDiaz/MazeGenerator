using UnityEngine;

public static class DebugSystem
{
    public static void Print(ScoreData scoreData)
    {
        for(int j = 0; j < CONST.NUM_TOP_SCORES; j++)
        {
                Debug.Log(j + 1 + ": " + scoreData.topScores[j]);
        }
    }
    public static void Between(string name, float value, float max, float min)
    {
        if(value > max)
        {
            Debug.Log(name + " > " + max);
        }
        else if(value < min)
        {
            Debug.Log(name + " < " + min);
        }
    }
}
public static class DebugSystem<T>
{
    public static void Print(T[] array)
    {
        for(int i = 0; i < array.Length; ++i)
        {
            Debug.Log(array[i]);
        }
    }
}
