using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalUIManager : MonoBehaviour
{
    public GameObject continueButton;
    [SerializeField]
    private GameObject score;

    public void SetScore(float score)
    {
        string scoreFormatted = Timer.FormatTime(score);
        this.score.GetComponentInChildren<TextMeshProUGUI>().text = scoreFormatted;
    }
    public void SetScore(string score)
    {
        this.score.GetComponentInChildren<TextMeshProUGUI>().text = score;
    }
}
