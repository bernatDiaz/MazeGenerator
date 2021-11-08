using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject touchHelp;
    [SerializeField]
    private GameObject rotateHelp;
    [SerializeField]
    private GameObject clickHelp;

    private GameObject help;

    [SerializeField]
    private GameObject quit;

    [HideInInspector]
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Help()
    {
        if (help == null)
        {
            if (ball.GetComponent<MovementTouch>())
            {
                help = Instantiate(touchHelp);
                quit.GetComponent<Button>().onClick.AddListener(DestroyHelp);
            }
            else if (ball.GetComponent<MovementGyro>())
            {
                help = Instantiate(rotateHelp);
                quit.GetComponent<Button>().onClick.AddListener(DestroyHelp);
            }
            else
            {
                help = Instantiate(clickHelp);
                quit.GetComponent<Button>().onClick.AddListener(DestroyHelp);
            }
        }
        else
        {
            DestroyHelp();
        }
    }
    private void DestroyHelp()
    {
        Destroy(help);
    }
}
