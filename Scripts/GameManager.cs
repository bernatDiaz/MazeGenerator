using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
/*
width + 2 * length = 10
length = x * width
width + 2 * x * width = 10
width = 10 / (1 + 2x)

width * (n - 1) + length * n = size


n = 1
size = width + 2 length
n = 2
size = 2 width + 3 length
 */
public class GameManager : MonoBehaviour
{
    #region UIVariables
    [SerializeField]
    private GameObject UIMain;
    [SerializeField]
    private GameObject UICreate;
    [SerializeField]
    private GameObject UIScore;
    [SerializeField]
    private GameObject UIGame;
    [SerializeField]
    private GameObject UIGoal;
    [SerializeField]
    private GameObject UIInstructions;
    [SerializeField]
    private GameObject UIScoreLocal;
    [SerializeField]
    private GameObject UIScoreOnline;

    [SerializeField]
    private GameObject AudioUI;

    [SerializeField]
    private GameObject highscorePrefab;

    [SerializeField]
    private GameObject warningPrefab;

    private GameObject quitWarning;
    private GameObject exitWarning;


    private Timer timer;
    private HelpUIManager help;
    #endregion

    [SerializeField]
    private GameObject mainCamera;

    [SerializeField]
    private GameObject AdsManager;

    #region maze pieces
    [SerializeField]
    private GameObject ballPrefab;
    private GameObject ball;

    [SerializeField]
    private GameObject goalPrefab;
    [SerializeField]
    private GameObject spotLight;
    private List<GameObject> spotLights;
    private GameObject goal;

    [SerializeField]
    private GameObject mazePrefab;
    private GameObject maze;

    [SerializeField]
    private GameObject wall;
    #endregion

    private MazeGenerator mazeGenerator;

    #region maze variables
    const float BASE_MAZE_HEIGHT_WORLD = 10.0f;
    const float BASE_MAZE_WIDTH_WORLD = 10.0f;
    float mazeHeightWorld;
    float mazeWidthWorld;
    int mazeHeight;
    int mazeWidth;

    float WALL_LENGTH_WORLD;
    float WALL_WIDTH_WORLD;
    const float LENGHT_WIDTH_RELATION = 2.0f;

    const string GOAL_CONTINUE_PATH = "Continue";
    DIFICULTIES dificulty;
    CONTROL control;
    #endregion
    bool first = true;

    public delegate void ChangeScreen(SCREEN screen);
    public event ChangeScreen OnChangeScreen;

    private void TriggerChangeScreen(SCREEN screen)
    {
        if (OnChangeScreen != null)
            OnChangeScreen(screen);
    }
    // Start is called before the first frame update
    void Start()
    {
        mazeGenerator = new MazeGenerator();
        timer = UIGame.GetComponentInChildren<Timer>();
        help = UIGame.GetComponentInChildren<HelpUIManager>();
        spotLights = new List<GameObject>();
        OnChangeScreen += AdsManager.GetComponent<AdsManager>().OnChangeScreen;
        MainScreen();
    }
    private void Clear()
    {
        if(maze != null)
        {
            Destroy(maze);
        }
        if(ball != null)
        {
            Destroy(ball);
        }
        if(goal != null)
        {
            Destroy(goal);
        }
        mainCamera.GetComponent<CameraManager>().InitCamera(false);
        timer.ClearTimer();
        foreach(GameObject spotLight in spotLights)
        {
            Destroy(spotLight);
        }
    }
    #region change screen
    public void MainScreen()
    {
        Show(UIMain);
        Hide(UICreate);
        Hide(UIScore);
        Hide(UIGame);
        Hide(UIGoal);
        Hide(UIInstructions);
        Hide(UIScoreLocal);
        Hide(UIScoreOnline);
        TriggerChangeScreen(SCREEN.MAIN);
    }
    public void PlayScreen()
    {
        Show(UICreate);
        Hide(UIMain);
        //Hide(UIScore);
        //Hide(UIGame);
        //Hide(UIGoal);
        //Hide(UIInstructions);
        //Hide(UIScoreLocal);
        //Hide(UIScoreOnline);
        TriggerChangeScreen(SCREEN.PLAY);
    }
    public void ScoreScreen()
    {
        Show(UIScore);
        Hide(UIMain);
        //Hide(UICreate);
        //Hide(UIGame);
        //Hide(UIGoal);
        //Hide(UIInstructions);
        Hide(UIScoreLocal);
        Hide(UIScoreOnline);
        TriggerChangeScreen(SCREEN.SCORE);
    }
    public void InstructionsScreen()
    {
        Show(UIInstructions);
        //Hide(UICreate);
        Hide(UIMain);
        //Hide(UIScore);
        //Hide(UIGame);
        //Hide(UIGoal);
        //Hide(UIScoreLocal);
        //Hide(UIScoreOnline);
        TriggerChangeScreen(SCREEN.INSTRUCTIONS);
    }
    public void GameScreen()
    {
        Hide(UICreate);
        //Hide(UIMain);
        //Hide(UIScore);
        Show(UIGame);
        //Hide(UIGoal);
        //Hide(UIInstructions);
        //Hide(UIScoreLocal);
        //Hide(UIScoreOnline);
        TriggerChangeScreen(SCREEN.GAME);
    }
    public void LocalScoreScreen()
    {
        //Hide(UICreate);
        //Hide(UIMain);
        Hide(UIScore);
        //Hide(UIGame);
        //Hide(UIGoal);
        //Hide(UIInstructions);
        Show(UIScoreLocal);
        //Hide(UIScoreOnline);
        TriggerChangeScreen(SCREEN.LOCAL_SCORE);
    }
    public void OnlineScoreScreen()
    {
        //Hide(UICreate);
        //Hide(UIMain);
        Hide(UIScore);
        //Hide(UIGame);
        //Hide(UIGoal);
        //Hide(UIInstructions);
        //Hide(UIScoreLocal);
        Show(UIScoreOnline);
        TriggerChangeScreen(SCREEN.ONLINE_SCORE);
    }
    #endregion
    // Update is called once per frame
    void Update()
    {

    }
    private void SaveScreenshot()
    {
        if (Application.isEditor)
        {
            string file = "16_9t";
            Debug.Log("taking screenshot");
            string path = Directory.GetCurrentDirectory() + "/Assets/Screenshots/" + file + ".png";
            ScreenCapture.CaptureScreenshot(path);
        }
    }
    public void GenerateMaze(DIFICULTIES dificulty, CONTROL control)
    {
        this.dificulty = dificulty;
        this.control = control;
        MazeGenerator.MazeDimensions(dificulty, out mazeHeight, out mazeWidth);
        GenerateMaze();
        InstantiateBallAndGoal();
        InitCamera();
        timer.StartTimer();
        GameScreen();
    }
    #region maze generation
    private void GenerateMaze()
    {
        int height = mazeHeight;
        int width = mazeWidth;
        GenerateWalls();
        InstantiateMaze(height, width);
        if (first)
        {
            mazeGenerator.Init(height, width);
            mazeGenerator.Generate();
            GenerateWallsHorizontal(height, width);
            GenerateWallsVertical(height, width);
            GenerateColumns(height, width);
        }


        void GenerateWalls()
        {
            WALL_WIDTH_WORLD = 10.0f / (2 * LENGHT_WIDTH_RELATION + 1);
            WALL_LENGTH_WORLD = LENGHT_WIDTH_RELATION * WALL_WIDTH_WORLD;
        }

        void InstantiateMaze(int height, int width)
        {
            mazeHeightWorld = WALL_WIDTH_WORLD * (height - 1) + WALL_LENGTH_WORLD * height;
            mazeWidthWorld = WALL_WIDTH_WORLD * (width - 1) + WALL_LENGTH_WORLD * width;
            maze = Instantiate(mazePrefab);
            maze.transform.localScale = new Vector3(mazeWidthWorld / BASE_MAZE_WIDTH_WORLD,
                maze.transform.localScale.y, mazeHeightWorld / BASE_MAZE_HEIGHT_WORLD);
        }

        void GenerateWallsHorizontal(int height, int width)
        {
            for (int i = 0; i < height - 1; i++)
                for (int j = 0; j < width; j++)
                    if (!mazeGenerator.GetBottomWall(i, j))
                        InstantiateWall(i, j);

            void InstantiateWall(int row, int col)
            {
                float z = -(mazeHeightWorld / 2.0f) + row * (WALL_LENGTH_WORLD + WALL_WIDTH_WORLD) + (WALL_WIDTH_WORLD / 2.0f) + WALL_LENGTH_WORLD;
                float x = -(mazeWidthWorld / 2.0f) + col * (WALL_LENGTH_WORLD + WALL_WIDTH_WORLD) + (WALL_LENGTH_WORLD / 2.0f);
                Vector3 position = new Vector3(x, 0.0f, z);
                GameObject wallClone = Instantiate(wall, position, Quaternion.identity);
                wallClone.transform.localScale = new Vector3(WALL_LENGTH_WORLD, wallClone.transform.localScale.y, WALL_WIDTH_WORLD);
                wallClone.transform.parent = maze.transform;
            }
        }
        void GenerateWallsVertical(int height, int width)
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width - 1; j++)
                    if (!mazeGenerator.GetRightWall(i, j))
                        InstantiateWall(i, j);

            void InstantiateWall(int row, int col)
            {
                float z = -(mazeHeightWorld / 2.0f) + row * (WALL_LENGTH_WORLD + WALL_WIDTH_WORLD) + (WALL_LENGTH_WORLD / 2.0f);
                float x = -(mazeWidthWorld / 2.0f) + col * (WALL_LENGTH_WORLD + WALL_WIDTH_WORLD) + (WALL_WIDTH_WORLD / 2.0f) + WALL_LENGTH_WORLD;
                Vector3 position = new Vector3(x, 0.0f, z);
                GameObject wallClone = Instantiate(wall, position, Quaternion.identity);
                wallClone.transform.localScale = new Vector3(WALL_WIDTH_WORLD, wallClone.transform.localScale.y, WALL_LENGTH_WORLD);
                wallClone.transform.parent = maze.transform;
            }
        }
        void GenerateColumns(int height, int width)
        {
            for (int i = 0; i < height - 1; i++)
                for (int j = 0; j < width - 1; j++)
                    InstantiateColumn(i, j);

            void InstantiateColumn(int row, int col)
            {
                float z = -(mazeHeightWorld / 2.0f) + row * (WALL_LENGTH_WORLD + WALL_WIDTH_WORLD) + (WALL_WIDTH_WORLD / 2.0f) + WALL_LENGTH_WORLD;
                float x = -(mazeWidthWorld / 2.0f) + col * (WALL_LENGTH_WORLD + WALL_WIDTH_WORLD) + (WALL_WIDTH_WORLD / 2.0f) + WALL_LENGTH_WORLD;
                Vector3 position = new Vector3(x, 0.0f, z);
                GameObject wallClone = Instantiate(wall, position, Quaternion.identity);
                wallClone.transform.localScale = new Vector3(WALL_WIDTH_WORLD, wallClone.transform.localScale.y, WALL_WIDTH_WORLD);
                wallClone.transform.parent = maze.transform;
            }
        }
    }
    private void InstantiateBallAndGoal()
    {
        Vector3 startingPositionBall = StartingPositionBall();
        ball = Instantiate(ballPrefab, startingPositionBall, Quaternion.identity);
        AddControlScript(ball);
        SpotLight(ball);
        help.ball = ball;

        Vector3 positionGoal = PositionGoal();
        goal = Instantiate(goalPrefab, positionGoal, Quaternion.identity);
        goal.transform.localScale = new Vector3(WALL_LENGTH_WORLD, 3.0f, WALL_LENGTH_WORLD);
        goal.GetComponent<Goal>().OnGoal += Goal;
        SpotLight(goal);

        void SpotLight(GameObject parent)
        {
            GameObject spotLight = Instantiate(this.spotLight);
            spotLight.GetComponent<FollowXZ>().parent = parent;
            spotLights.Add(spotLight);
        }

        Vector3 StartingPositionBall()
        {
            Index2D startingPositionBallIndex = mazeGenerator.GetStartingPosition();
            float z = -mazeHeightWorld / 2.0f + WALL_LENGTH_WORLD / 2.0f +
                startingPositionBallIndex.row * (WALL_LENGTH_WORLD + WALL_WIDTH_WORLD);
            float x = -mazeWidthWorld / 2.0f + WALL_LENGTH_WORLD / 2.0f +
                startingPositionBallIndex.col * (WALL_LENGTH_WORLD + WALL_WIDTH_WORLD);
            float y = 1.0f;
            return new Vector3(x, y, z);
        }
        Vector3 PositionGoal()
        {
            Index2D positionGoalIndex = mazeGenerator.GetGoalPosition();
            float z = -mazeHeightWorld / 2.0f + WALL_LENGTH_WORLD / 2.0f +
                positionGoalIndex.row * (WALL_LENGTH_WORLD + WALL_WIDTH_WORLD);
            float x = -mazeWidthWorld / 2.0f + WALL_LENGTH_WORLD / 2.0f +
                positionGoalIndex.col * (WALL_LENGTH_WORLD + WALL_WIDTH_WORLD);
            float y = 1.0f;
            return new Vector3(x, y, z);
        }
        void AddControlScript(GameObject ball)
        {
            if (control == CONTROL.GYRO)
            {
                ball.AddComponent<MovementGyro>();
            }
            else if (control == CONTROL.TOUCH)
            {
                if (Input.touchSupported)
                {
                    ball.AddComponent<MovementTouch>();
                }
                else
                {
                    //ball.AddComponent<MovementTouch>();
                    ball.AddComponent<MovementClick>();
                }
            }
        }
    }
    private void InitCamera()
    {
        mainCamera.GetComponent<CameraManager>().SetBall(ball);
        mainCamera.GetComponent<CameraManager>().SetMazeDimensions(mazeHeightWorld, mazeWidthWorld, WALL_WIDTH_WORLD);
        mainCamera.GetComponent<CameraManager>().InitCamera(true);
    }
    #endregion
    private void Goal()
    {
        goal.GetComponent<Goal>().OnGoal -= Goal;
        timer.StopTimer();
        float time = timer.GetTime();
        string timeFormatted = Timer.FormatTime(time);

        ChangeUIVisibility();
        UIGoal.GetComponent<GoalUIManager>().SetScore(time);

        if (SaveSystem.SaveScore(time, dificulty, control, out int position))
        {
            UIGoal.GetComponent<GoalUIManager>().continueButton.GetComponent<Button>().onClick.AddListener(delegate{ContinueHighscore(position); }) ;
        }
        else
        {
            UIGoal.GetComponent<GoalUIManager>().continueButton.GetComponent<Button>().onClick.AddListener(ContinueNoHighscore);
        }
        PlayGamesManager.AddScoreToLeaderboard(dificulty, control, time);

        void ChangeUIVisibility()
        {
            Hide(UIGame);
            Show(UIGoal);
            TriggerChangeScreen(SCREEN.GOAL);
        }
        void ContinueHighscore(int position)
        {
            Hide(UIGoal);
            UIGoal.GetComponent<GoalUIManager>().continueButton.GetComponent<Button>().onClick.RemoveAllListeners();
            ShowHighscore(position);

            void ShowHighscore(int position)
            {
                GameObject highscore = Instantiate(highscorePrefab);
                ScoreData scoreData = SaveSystem.LoadScore(control, dificulty);
                highscore.GetComponent<HighscoreManager>().SetScores(scoreData);
                if (position != CONST.NOT_HIGHSCORE)
                    highscore.GetComponent<HighscoreManager>().HighlightScore(position);
                GameObject continueButton = highscore.transform.Find(GOAL_CONTINUE_PATH).gameObject;
                continueButton.GetComponent<Button>().onClick.AddListener(delegate { Continue(highscore); });
                continueButton.GetComponent<Button>().onClick.AddListener(AudioUI.GetComponent<AudioUI>().OnOk);

                void Continue(GameObject highscore)
                {
                    Destroy(highscore);
                    Restart();
                }
            }
        }
        void ContinueNoHighscore()
        {
            Hide(UIGoal);
            UIGoal.GetComponent<GoalUIManager>().continueButton.GetComponent<Button>().onClick.RemoveAllListeners();
            Restart();
        }
        void Restart()
        {
            PlayScreen();
            Clear();
        }
    }

    public void OnQuitMaze()
    {
        if (quitWarning == null)
        {
            string quitMazeMessage = "Are you sure you want to quit the maze?";
            quitWarning = Instantiate(warningPrefab);
            quitWarning.GetComponent<WarningManager>().SetMessage(quitMazeMessage);
            quitWarning.GetComponent<WarningManager>().yes.GetComponent<Button>().onClick.AddListener(OnClickYes);
            quitWarning.GetComponent<WarningManager>().no.GetComponent<Button>().onClick.AddListener(OnClickNo);
        }

        void OnClickYes()
        {
            Destroy(quitWarning);
            MainScreen();
            Clear();
        }

        void OnClickNo()
        {
            Destroy(quitWarning);
        }
    }
    public void OnExitGame()
    {
        if (exitWarning == null)
        {
            string exitGameMessage = "Are you sure you want to exit the game?";
            exitWarning = Instantiate(warningPrefab);
            exitWarning.GetComponent<WarningManager>().SetMessage(exitGameMessage);
            exitWarning.GetComponent<WarningManager>().yes.GetComponent<Button>().onClick.AddListener(OnClickYes);
            exitWarning.GetComponent<WarningManager>().no.GetComponent<Button>().onClick.AddListener(OnClickNo);
        }

        void OnClickYes()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        void OnClickNo()
        {
            Destroy(exitWarning);
        }
    }
    #region static functions
    public static void Hide(GameObject UI)
    {
        CanvasGroup canvasGroup = UI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public static void Show(GameObject UI)
    {
        CanvasGroup canvasGroup = UI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    #endregion
}