using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;

    [SerializeField]
    private GameObject control;
    [SerializeField]
    private GameObject dificulty;

    [SerializeField]
    private TextMeshProUGUI noGyroText;

    [SerializeField]
    private GameObject start;

    private Color greenWeak;
    // Start is called before the first frame update
    void Start()
    {
        ColorUtility.TryParseHtmlString("#30FF86", out greenWeak);

        start.GetComponent<Image>().color = greenWeak;


        start.GetComponent<Button>().onClick.AddListener(onClickStart);

        if (SystemInfo.supportsGyroscope)
        {
            control.GetComponent<ControlUIManager>().gyro.GetComponent<Button>().enabled = true;
            noGyroText.gameObject.SetActive(false);
        }
        else
        {
            control.GetComponent<ControlUIManager>().gyro.GetComponent<Button>().enabled = false;
            noGyroText.gameObject.SetActive(true);
            Debug.Log("No gyro");
        }
    }
    public void onClickStart()
    {
        DIFICULTIES dificulty = this.dificulty.GetComponent<DificultyUIManager>().GetDificulty();
        CONTROL control = this.control.GetComponent<ControlUIManager>().GetControl();
        gameManager.GetComponent<GameManager>().
            GenerateMaze(dificulty, control);
    }
}
