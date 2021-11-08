using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineScoreUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject control;
    [SerializeField]
    private GameObject dificulty;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Show()
    {
        LEADERBOARDS_CONTROL control = this.control.GetComponent<ControlLeaderboardsUIManager>().GetControl();
        DIFICULTIES dificulty = this.dificulty.GetComponent<DificultyUIManager>().GetDificulty();
        PlayGamesManager.ShowLeaderboard(dificulty, control);
    }
    public void ShowAll()
    {
        PlayGamesManager.ShowAllLeaderboards();
    }
}
