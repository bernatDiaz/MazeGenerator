using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DificultyUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject AudioUI;

    private DIFICULTIES dificulty;

    public GameObject veryEasy;
    public GameObject easy;
    public GameObject medium;
    public GameObject hard;
    public GameObject veryHard;

    private Color redStrong;
    private Color redWeak;
    private Color redMedium;
    private Color greenStrong;
    private Color greenWeak;

    private Color notActive;
    private Color active;
    // Start is called before the first frame update
    protected void Awake()
    {
        dificulty = DIFICULTIES.EASY;

        ColorUtility.TryParseHtmlString("#B31C11", out redStrong);
        ColorUtility.TryParseHtmlString("#FF564A", out redWeak);
        ColorUtility.TryParseHtmlString("#FE3D31", out redMedium);
        ColorUtility.TryParseHtmlString("#00B34A", out greenStrong);
        ColorUtility.TryParseHtmlString("#30FF86", out greenWeak);

        notActive = redMedium;
        active = greenStrong;

        veryEasy.GetComponent<Image>().color = notActive;
        easy.GetComponent<Image>().color = active;
        medium.GetComponent<Image>().color = notActive;
        hard.GetComponent<Image>().color = notActive;
        veryHard.GetComponent<Image>().color = notActive;
    }
    protected void Start()
    {
        veryEasy.GetComponent<Button>().onClick.AddListener(onClickVeryEasy);
        easy.GetComponent<Button>().onClick.AddListener(onClickEasy);
        medium.GetComponent<Button>().onClick.AddListener(onClickMedium);
        hard.GetComponent<Button>().onClick.AddListener(onClickHard);
        veryHard.GetComponent<Button>().onClick.AddListener(onClickVeryHard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClickVeryEasy()
    {
        if (dificulty != DIFICULTIES.VERY_EASY)
        {
            dificulty = DIFICULTIES.VERY_EASY;
            veryEasy.GetComponent<Image>().color = active;
            easy.GetComponent<Image>().color = notActive;
            medium.GetComponent<Image>().color = notActive;
            hard.GetComponent<Image>().color = notActive;
            veryHard.GetComponent<Image>().color = notActive;
            AudioUI.GetComponent<AudioUI>().OnClick();
        }
    }
    public void onClickEasy()
    {
        if (dificulty != DIFICULTIES.EASY)
        {
            dificulty = DIFICULTIES.EASY;
            veryEasy.GetComponent<Image>().color = notActive;
            easy.GetComponent<Image>().color = active;
            medium.GetComponent<Image>().color = notActive;
            hard.GetComponent<Image>().color = notActive;
            veryHard.GetComponent<Image>().color = notActive;
            AudioUI.GetComponent<AudioUI>().OnClick();
        }
    }
    public void onClickMedium()
    {
        if (dificulty != DIFICULTIES.MEDIUM)
        {
            dificulty = DIFICULTIES.MEDIUM;
            veryEasy.GetComponent<Image>().color = notActive;
            easy.GetComponent<Image>().color = notActive;
            medium.GetComponent<Image>().color = active;
            hard.GetComponent<Image>().color = notActive;
            veryHard.GetComponent<Image>().color = notActive;
            AudioUI.GetComponent<AudioUI>().OnClick();
        }
    }
    public void onClickHard()
    {
        if (dificulty != DIFICULTIES.HARD)
        {
            veryEasy.GetComponent<Image>().color = notActive;
            dificulty = DIFICULTIES.HARD;
            easy.GetComponent<Image>().color = notActive;
            medium.GetComponent<Image>().color = notActive;
            hard.GetComponent<Image>().color = active;
            veryHard.GetComponent<Image>().color = notActive;
            AudioUI.GetComponent<AudioUI>().OnClick();
        }
    }
    public void onClickVeryHard()
    {
        if (dificulty != DIFICULTIES.VERY_HARD)
        {
            dificulty = DIFICULTIES.VERY_HARD;
            veryEasy.GetComponent<Image>().color = notActive;
            easy.GetComponent<Image>().color = notActive;
            medium.GetComponent<Image>().color = notActive;
            hard.GetComponent<Image>().color = notActive;
            veryHard.GetComponent<Image>().color = active;
            AudioUI.GetComponent<AudioUI>().OnClick();
        }
    }
    public DIFICULTIES GetDificulty()
    {
        return dificulty;
    }
}
