using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DificultyUIManagerEvent : DificultyUIManager
{
    public delegate void Click();
    public event Click OnClick;
    protected new void Awake()
    {
        base.Awake();
    }
    protected new void Start()
    {
        veryEasy.GetComponent<Button>().onClick.AddListener(onClickVeryEasy);
        easy.GetComponent<Button>().onClick.AddListener(onClickEasy);
        medium.GetComponent<Button>().onClick.AddListener(onClickMedium);
        hard.GetComponent<Button>().onClick.AddListener(onClickHard);
        veryHard.GetComponent<Button>().onClick.AddListener(onClickVeryHard);
    }
    public new void onClickVeryEasy()
    {
        base.onClickVeryEasy();
        TriggerClick();
    }
    public new void onClickEasy()
    {
        base.onClickEasy();
        TriggerClick();
    }
    public new void onClickMedium()
    {
        base.onClickMedium();
        TriggerClick();
    }
    public new void onClickHard()
    {
        base.onClickHard();
        TriggerClick();
    }
    public new void onClickVeryHard()
    {
        base.onClickVeryHard();
        TriggerClick();
    }
    private void TriggerClick()
    {
        if (OnClick != null)
            OnClick();
    }
}
