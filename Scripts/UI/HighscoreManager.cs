using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HighscoreManager : MonoBehaviour
{
    [SerializeField]
    private GameObject averageScore;
    private GameObject[] scorePositions;

    private const string PATH = "Container/";
    private const string PATH_SCORE_NUMBER = "Score";
    private const string PATH_TITLE = "Title";

    private const string UNDEFINED_TEXT = "NA";

    private Color blueDark;
    private Color blueMedium;
    private Color blueLight;

    private const int FONT_SIZE_HIGHLIGHTED = 40;

    private bool init = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!init)
            Init();
    }
    private void Init()
    {
        init = true;
        FindScorePositions();
        ColorUtility.TryParseHtmlString("#196D9E", out blueDark);
        ColorUtility.TryParseHtmlString("#3DABEB", out blueMedium);
        ColorUtility.TryParseHtmlString("#ACDDFA", out blueLight);
        InitColors();
        
        void InitColors()
        {
            ChangeColor(averageScore, blueDark, blueMedium);
            foreach (GameObject container in scorePositions)
                ChangeColor(container, blueDark, blueMedium);
        }
    }
    private void FindScorePositions()
    {
        scorePositions = new GameObject[CONST.NUM_TOP_SCORES];
        for(int i = 1; i <= CONST.NUM_TOP_SCORES; i++)
        {
            string path = PATH + i.ToString();
            scorePositions[i - 1] = gameObject.transform.Find(path).gameObject;
        }
    }
    public void SetScores(ScoreData scoreData)
    {
        if (!init)
            Init();
        for(int i = 0; i < CONST.NUM_TOP_SCORES; i++)
        {
            if(scoreData.topScores[i] == CONST.UNDEFINED)
            {
                GameObject text = scorePositions[i].transform.Find(PATH_SCORE_NUMBER).gameObject;
                text.GetComponent<TextMeshProUGUI>().text = UNDEFINED_TEXT;
            }
            else
            {
                GameObject text = scorePositions[i].transform.Find(PATH_SCORE_NUMBER).gameObject;
                text.GetComponent<TextMeshProUGUI>().text = Timer.FormatTime(scoreData.topScores[i]);
            }
        }

        float scoreSum = 0.0f;
        int numScores = 0;
        for (int i = 0; i < CONST.NUM_TOP_SCORES; i++)
        {
            if (scoreData.topScores[i] != CONST.UNDEFINED)
            {
                scoreSum += scoreData.topScores[i];
                numScores++;
            }
        }
        if (numScores > 0)
        {
            float average = scoreSum / numScores;
            GameObject text = averageScore.transform.Find(PATH_SCORE_NUMBER).gameObject;
            text.GetComponent<TextMeshProUGUI>().text = Timer.FormatTime(average);
        }
        else
        {
            GameObject text = averageScore.transform.Find(PATH_SCORE_NUMBER).gameObject;
            text.GetComponent<TextMeshProUGUI>().text = UNDEFINED_TEXT;
        }
    }
    public void HighlightScore(int index)
    {
        if (index < scorePositions.Length)
        {
            GameObject container = scorePositions[index];
            ChangeColor(container, blueLight);
            ChangeFontSize(container, FONT_SIZE_HIGHLIGHTED);
        }
    }
    private void ChangeColor(GameObject container, Color color)
    {
        ChangeColor(container, color, color);
    }
    private void ChangeColor(GameObject container, Color colorTitle, Color colorScore)
    {
        GameObject title = container.transform.Find(PATH_TITLE).gameObject;
        title.GetComponent<TextMeshProUGUI>().color = colorTitle;
        GameObject score = container.transform.Find(PATH_SCORE_NUMBER).gameObject;
        score.GetComponent<TextMeshProUGUI>().color = colorScore;
    }
    private void ChangeFontSize(GameObject container, int fontSize)
    {
        GameObject title = container.transform.Find(PATH_TITLE).gameObject;
        title.GetComponent<TextMeshProUGUI>().fontSize = fontSize;
        GameObject score = container.transform.Find(PATH_SCORE_NUMBER).gameObject;
        score.GetComponent<TextMeshProUGUI>().fontSize = fontSize;
    }
}
