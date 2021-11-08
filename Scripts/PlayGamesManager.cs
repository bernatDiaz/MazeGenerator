using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGamesManager : MonoBehaviour
{
    private const string ID_ERROR = "ERROR";
    // Start is called before the first frame update
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
    }

    private void SignIn()
    {
        Social.localUser.Authenticate(success => { });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private static string GetTouchID(DIFICULTIES dificulty)
    {
        switch (dificulty)
        {
            case DIFICULTIES.VERY_EASY:
                return GPGSIds.leaderboard_touch_very_easy;
            case DIFICULTIES.EASY:
                return GPGSIds.leaderboard_touch_easy;
            case DIFICULTIES.MEDIUM:
                return GPGSIds.leaderboard_touch_medium;
            case DIFICULTIES.HARD:
                return GPGSIds.leaderboard_touch_hard;
            case DIFICULTIES.VERY_HARD:
                return GPGSIds.leaderboard_touch_very_hard;
        }
        return ID_ERROR;
    }
    private static string GetGyroID(DIFICULTIES dificulty)
    {
        switch (dificulty)
        {
            case DIFICULTIES.VERY_EASY:
                return GPGSIds.leaderboard_rotate_very_easy;
            case DIFICULTIES.EASY:
                return GPGSIds.leaderboard_rotate_easy;
            case DIFICULTIES.MEDIUM:
                return GPGSIds.leaderboard_rotate_medium;
            case DIFICULTIES.HARD:
                return GPGSIds.leaderboard_rotate_hard;
            case DIFICULTIES.VERY_HARD:
                return GPGSIds.leaderboard_rotate_very_hard;
        }
        return ID_ERROR;
    }
    private static string GetAnyID(DIFICULTIES dificulty)
    {
        switch (dificulty)
        {
            case DIFICULTIES.VERY_EASY:
                return GPGSIds.leaderboard_any_very_easy;
            case DIFICULTIES.EASY:
                return GPGSIds.leaderboard_any_easy;
            case DIFICULTIES.MEDIUM:
                return GPGSIds.leaderboard_any_medium;
            case DIFICULTIES.HARD:
                return GPGSIds.leaderboard_any_hard;
            case DIFICULTIES.VERY_HARD:
                return GPGSIds.leaderboard_any_very_hard;
        }
        return ID_ERROR;
    }
    private static string GetLeaderBoardID(DIFICULTIES dificulty, CONTROL control)
    {
        switch (control)
        {
            case CONTROL.TOUCH:
                return GetTouchID(dificulty); 
            case CONTROL.GYRO:
                return GetGyroID(dificulty);
        }
        return ID_ERROR;
    }
    private static string GetLeaderBoardID(DIFICULTIES dificulty, LEADERBOARDS_CONTROL control)
    {
        switch (control)
        {
            case LEADERBOARDS_CONTROL.TOUCH:
                return GetTouchID(dificulty);
            case LEADERBOARDS_CONTROL.GYRO:
                return GetGyroID(dificulty);
            case LEADERBOARDS_CONTROL.ANY:
                return GetAnyID(dificulty);
        }
        return ID_ERROR;
    }
    public static void AddScoreToLeaderboard(DIFICULTIES dificulty, CONTROL control, float score)
    {
        long ms = Mathf.RoundToInt(score * 1000);
        string id = GetLeaderBoardID(dificulty, control);
        if(id != ID_ERROR)
            AddScoreToLeaderboard(id, ms);
        string idAny = GetAnyID(dificulty);
        AddScoreToLeaderboard(idAny, ms);
    }
    private static void AddScoreToLeaderboard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }
    public static void ShowLeaderboard(DIFICULTIES dificulty, LEADERBOARDS_CONTROL control)
    {
        string id = GetLeaderBoardID(dificulty, control);
        if (id != ID_ERROR)
            ShowLeaderboard(id);
    }
    public static void ShowLeaderboard(DIFICULTIES dificulty, CONTROL control)
    {
        string id = GetLeaderBoardID(dificulty, control);
        if(id != ID_ERROR)
            ShowLeaderboard(id);
    }
    private static void ShowLeaderboard(string leaderboardId)
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardId);
    }
    public static void ShowAllLeaderboards()
    {
        Social.ShowLeaderboardUI();
    }
}
