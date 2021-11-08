using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButton : MonoBehaviour
{
    [SerializeField]
    private GameObject helpPrefab;

    private GameObject help;

    [SerializeField]
    private GameObject quit;
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
        if(help == null)
        {
            help = Instantiate(helpPrefab);
            quit.GetComponent<Button>().onClick.AddListener(DestroyHelp);
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
