using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject control;
    [SerializeField]
    GameObject dificulty;

    [SerializeField]
    GameObject highscore;
    // Start is called before the first frame update
    void Start()
    {
        control.GetComponent<ControlUIManagerEvent>().OnClick += UpdateScore;

        dificulty.GetComponent<DificultyUIManagerEvent>().OnClick += UpdateScore;
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        CONTROL control = this.control.GetComponent<ControlUIManager>().GetControl();
        DIFICULTIES dificulty = this.dificulty.GetComponent<DificultyUIManager>().GetDificulty();
        ScoreData scoreData = SaveSystem.LoadScore(control, dificulty);
        highscore.GetComponent<HighscoreManager>().SetScores(scoreData);
    }
}
