using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlLeaderboardsUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject AudioUI;

    public GameObject touch;
    public GameObject gyro;
    public GameObject any;

    private LEADERBOARDS_CONTROL control;

    private Color redMedium;
    private Color greenStrong;

    private Color notActive;
    private Color active;
    // Start is called before the first frame update
    void Awake()
    {
        control = LEADERBOARDS_CONTROL.ANY;
        ColorUtility.TryParseHtmlString("#FE3D31", out redMedium);
        ColorUtility.TryParseHtmlString("#00B34A", out greenStrong);

        active = greenStrong;
        notActive = redMedium;

        touch.GetComponent<Image>().color = notActive;
        gyro.GetComponent<Image>().color = notActive;
        any.GetComponent<Image>().color = active;
    }
    private void Start()
    {
        touch.GetComponent<Button>().onClick.AddListener(onClickTouch);
        gyro.GetComponent<Button>().onClick.AddListener(onClickGyro);
        any.GetComponent<Button>().onClick.AddListener(onClickAny);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClickTouch()
    {
        if (control != LEADERBOARDS_CONTROL.TOUCH)
        {
            control = LEADERBOARDS_CONTROL.TOUCH;
            touch.GetComponent<Image>().color = active;
            gyro.GetComponent<Image>().color = notActive;
            any.GetComponent<Image>().color = notActive;
            AudioUI.GetComponent<AudioUI>().OnClick();
        }
    }
    public void onClickGyro()
    {
        if (control != LEADERBOARDS_CONTROL.GYRO)
        {
            control = LEADERBOARDS_CONTROL.GYRO;
            touch.GetComponent<Image>().color = notActive;
            gyro.GetComponent<Image>().color = active;
            any.GetComponent<Image>().color = notActive;
            AudioUI.GetComponent<AudioUI>().OnClick();
        }
    }
    public void onClickAny()
    {
        if (control != LEADERBOARDS_CONTROL.ANY)
        {
            control = LEADERBOARDS_CONTROL.ANY;
            touch.GetComponent<Image>().color = notActive;
            gyro.GetComponent<Image>().color = notActive;
            any.GetComponent<Image>().color = active;
            AudioUI.GetComponent<AudioUI>().OnClick();
        }
    }
    public LEADERBOARDS_CONTROL GetControl()
    {
        return control;
    }
}
