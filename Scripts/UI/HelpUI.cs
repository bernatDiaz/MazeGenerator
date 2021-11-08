using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUI : MonoBehaviour
{
    private float lastTime;
    private int numClicks = 0;

    private const float TIME_ELAPSED = 1f;
    private const int CLICKS_TO_CLOSE = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastTime < TIME_ELAPSED)
            {
                numClicks++;
                if (numClicks >= CLICKS_TO_CLOSE)
                {
                    OnClose();
                }
                else
                {
                    lastTime = Time.time;
                }
            }
            else
            {
                lastTime = Time.time;
                numClicks = 1;
            }
        }
    }

    public void OnClose()
    {
        Destroy(gameObject);
    }
}
