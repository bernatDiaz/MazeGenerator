[System.Serializable]
public class ScoreData
{
    public float[] topScores;

    public ScoreData()
    {
        topScores = new float[CONST.NUM_TOP_SCORES];
        for (int j = 0; j < CONST.NUM_TOP_SCORES; j++)
            topScores[j] = CONST.UNDEFINED;
    }
    public void AddScore(float score, out int position)
    {
        position = CONST.NOT_HIGHSCORE;
        for (int i = 0; i < CONST.NUM_TOP_SCORES; i++)
        {
            if (topScores[i] == CONST.UNDEFINED)
            {
                position = i;
                topScores[i] = score;
                break;
            }
            if (topScores[i] > score)
            {
                position = i;
                ReplaceScore(score, i);
                break;
            }
        }
        void ReplaceScore(float score, int position)
        {
            float aux = score;
            for (int i = position; i < CONST.NUM_TOP_SCORES; i++)
            {
                float temp = topScores[i];
                topScores[i] = aux;
                if (temp == CONST.UNDEFINED)
                    break;
                aux = temp;
            }
        }
    }
    public bool IsTopScore(float score)
    {
        if (topScores[topScores.Length - 1] == CONST.UNDEFINED)
            return true;
        else
            return topScores[topScores.Length - 1] > score;
    }
    public int IndexOf(float score, int dificulty)
    {
        for(int i = 0; i < CONST.NUM_TOP_SCORES; i++)
        {
            if (topScores[i] == score)
                return i;
        }
        return CONST.NOT_FOUND;
    }
    public int IndexOf(float score, DIFICULTIES dificulty)
    {
        return IndexOf(score, (int)dificulty);
    }
}

/* OLD
[System.Serializable]
public class ScoreData
{
    public float[,] topScores;

    public ScoreData()
    {
        topScores = new float[CONST.NUM_DIFICULTIES, CONST.NUM_TOP_SCORES];
        for (int i = 0; i < CONST.NUM_DIFICULTIES; i++)
            for (int j = 0; j < CONST.NUM_TOP_SCORES; j++)
                topScores[i, j] = CONST.UNDEFINED;
    }
    public void AddScore(float score, DIFICULTIES dificulty, out int position)
    {
        AddScore(score, (int)dificulty, out position);
    }
    public void AddScore(float score, int dificulty, out int position)
    {
        position = CONST.NOT_HIGHSCORE;
        for (int i = 0; i < CONST.NUM_TOP_SCORES; i++)
        {
            if (topScores[dificulty, i] == CONST.UNDEFINED)
            {
                position = i;
                topScores[dificulty, i] = score;
                break;
            }
            if (topScores[dificulty, i] > score)
            {
                position = i;
                ReplaceScore(score, i);
                break;
            }
        }
        void ReplaceScore(float score, int position)
        {
            float aux = score;
            for (int i = position; i < CONST.NUM_TOP_SCORES; i++)
            {
                float temp = topScores[dificulty, i];
                topScores[dificulty, i] = aux;
                if (temp == CONST.UNDEFINED)
                    break;
                aux = temp;
            }
        }
    }
    public bool IsTopScore(float score, DIFICULTIES dificulty)
    {
        return IsTopScore(score, (int)dificulty);
    }
    public bool IsTopScore(float score, int dificulty)
    {
        if (topScores[dificulty, topScores.GetLength(1) - 1] == CONST.UNDEFINED)
            return true;
        else
            return topScores[dificulty, topScores.GetLength(1) - 1] > score;
    }
    public int IndexOf(float score, int dificulty)
    {
        for(int i = 0; i < CONST.NUM_TOP_SCORES; i++)
        {
            if (topScores[dificulty, i] == score)
                return i;
        }
        return CONST.NOT_FOUND;
    }
    public int IndexOf(float score, DIFICULTIES dificulty)
    {
        return IndexOf(score, (int)dificulty);
    }
}
*/