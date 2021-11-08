using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject AudioUI;

    public GameObject touch;
    public GameObject gyro;

    private CONTROL control;

    private Color redStrong;
    private Color redWeak;
    private Color redMedium;
    private Color greenStrong;
    private Color greenWeak;

    private Color notActive;
    private Color active;
    protected void Awake()
    {
        control = CONTROL.TOUCH;

        ColorUtility.TryParseHtmlString("#B31C11", out redStrong);
        ColorUtility.TryParseHtmlString("#FF564A", out redWeak);
        ColorUtility.TryParseHtmlString("#FE3D31", out redMedium);
        ColorUtility.TryParseHtmlString("#00B34A", out greenStrong);
        ColorUtility.TryParseHtmlString("#30FF86", out greenWeak);

        active = greenStrong;
        notActive = redMedium;

        touch.GetComponent<Image>().color = active;
        gyro.GetComponent<Image>().color = notActive;
    }
    // Start is called before the first frame update
    protected void Start()
    {
        touch.GetComponent<Button>().onClick.AddListener(onClickTouch);
        gyro.GetComponent<Button>().onClick.AddListener(onClickGyro);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickTouch()
    {
        if (control != CONTROL.TOUCH)
        {
            control = CONTROL.TOUCH;
            touch.GetComponent<Image>().color = active;
            gyro.GetComponent<Image>().color = notActive;
            AudioUI.GetComponent<AudioUI>().OnClick();
        }
    }
    public void onClickGyro()
    {
        if (control != CONTROL.GYRO)
        {
            control = CONTROL.GYRO;
            touch.GetComponent<Image>().color = notActive;
            gyro.GetComponent<Image>().color = active;
            AudioUI.GetComponent<AudioUI>().OnClick();
        }
    }
    public CONTROL GetControl()
    {
        return control;
    }
}
