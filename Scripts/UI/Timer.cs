using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private bool start = false;
    private float timer = 0.0f;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timer += Time.deltaTime;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        text.text = FormatTime(timer);
    }

    public float GetTime()
    {
        return timer;
    }
    public void StartTimer()
    {
        start = true;
    }
    public void StopTimer()
    {
        start = false;
    }
    public void ClearTimer()
    {
        timer = 0f;
        start = false;
        UpdateText();
    }
    public static string FormatTime(float time)
    {
        return SecondsTwoDecimals(time);
    }
    private static string SecondsTwoDecimals(float time)
    {
        return time.ToString("F2");
    }
    private static string MinutesSeconds(float time)
    {
        int seconds = Mathf.FloorToInt(time);
        int minutes = seconds / 60;
        seconds = seconds - minutes * 60;
        string secondsString = seconds.ToString();
        secondsString = new string('0', 2 - secondsString.Length) + secondsString;
        return minutes.ToString() + ":" + secondsString;
    }
}
