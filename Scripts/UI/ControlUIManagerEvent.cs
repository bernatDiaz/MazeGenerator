using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlUIManagerEvent : ControlUIManager
{
    public delegate void Click();
    public event Click OnClick;
    protected new void Awake()
    {
        base.Awake();
    }
    protected new void Start()
    {
        touch.GetComponent<Button>().onClick.AddListener(onClickTouch);
        gyro.GetComponent<Button>().onClick.AddListener(onClickGyro);
    }
    public new void onClickTouch()
    {
        base.onClickTouch();
        TriggerClick();
    }
    public new void onClickGyro()
    {
        base.onClickGyro();
        TriggerClick();
    }
    private void TriggerClick()
    {
        if (OnClick != null)
            OnClick();
    }
}
