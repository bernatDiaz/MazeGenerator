public static class CONST
{
    public const int NUM_TOP_SCORES = 5;
    public const float UNDEFINED = -1;
    public const int NOT_FOUND = -1;
    public const int NOT_HIGHSCORE = -1;

    public static int NUM_DIFICULTIES = System.Enum.GetNames(typeof(DIFICULTIES)).Length;
}

public enum DIFICULTIES { VERY_EASY, EASY, MEDIUM, HARD, VERY_HARD};
public enum CONTROL { TOUCH, GYRO};

public enum TOUCH {BALL_START, BALL_LIFT, DIRECTION};

public enum SCREEN { MAIN, PLAY, GAME, SCORE, INSTRUCTIONS, GOAL, LOCAL_SCORE, ONLINE_SCORE}

public enum LEADERBOARDS_CONTROL { TOUCH, GYRO, ANY};