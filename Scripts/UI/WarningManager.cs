using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningManager : MonoBehaviour
{
    private GameObject audioUI;

    [SerializeField]
    private GameObject message;

    public GameObject yes;
    public GameObject no;

    private const string AUDIO_UI_PATH = "/AudioUI";
    // Start is called before the first frame update
    void Start()
    {
        audioUI = GameObject.Find(AUDIO_UI_PATH);
        yes.GetComponent<Button>().onClick.AddListener(audioUI.GetComponent<AudioUI>().OnClick);
        no.GetComponent<Button>().onClick.AddListener(audioUI.GetComponent<AudioUI>().OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMessage(string message)
    {
        this.message.GetComponent<TextMeshProUGUI>().text = message;
    }
}
