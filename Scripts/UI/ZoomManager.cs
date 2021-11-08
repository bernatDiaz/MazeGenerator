using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomManager : MonoBehaviour
{
    [SerializeField]
    private Sprite zoomInImage;
    [SerializeField]
    private Sprite zoomOutImage;
    [SerializeField]
    private Sprite zoomInEqualImage;
    [SerializeField]
    private Sprite zoomOutEqualImage;

    [SerializeField]
    private GameObject zoomIn;
    [SerializeField]
    private GameObject zoomOut;

    private CameraManager cameraManager;
    // Start is called before the first frame update
    void Start()
    {
        cameraManager = Camera.main.gameObject.GetComponent<CameraManager>();
        cameraManager.OnMaxDistance += MaxDistance;
        cameraManager.OnMinDistance += MinDistance;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void MaxDistance(bool enter)
    {
        if (enter)
        {
            zoomOut.GetComponent<Image>().sprite = zoomOutEqualImage;
            zoomOut.GetComponent<Button>().enabled = false;
        }
        else
        {
            zoomOut.GetComponent<Image>().sprite = zoomOutImage;
            zoomOut.GetComponent<Button>().enabled = true;
        }
    }
    void MinDistance(bool enter)
    {
        if (enter)
        {
            zoomIn.GetComponent<Image>().sprite = zoomInEqualImage;
            zoomIn.GetComponent<Button>().enabled = false;
        }
        else
        {
            zoomIn.GetComponent<Image>().sprite = zoomInImage;
            zoomIn.GetComponent<Button>().enabled = true;
        }
    }
}
