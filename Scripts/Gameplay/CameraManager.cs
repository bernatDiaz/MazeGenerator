using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject UICanvas;
    [SerializeField]
    private GameObject UIContainer;

    [SerializeField]
    private GameObject AudioUI;

    [SerializeField]
    private GameObject ball;
    private Camera cam;

    private SafeArea safeArea;

    private Vector3 mazeOrigin;
    private float mazeHeight;
    private float mazeWidth;
    private float distance = 10.0f;

    private float maxDistance;
    private float minDistance;
    private float deltaDistance;
    private const float MIN_DISTANCE = 20.0f;
    private const float DELTA_DISTANCE = 10.0f;

    private const float SCREEN_WIDTH_DPI = 573f;
    private const float SCREEN_HEIGHT_DPI = 232.5f;
    private const float TOP_UI_WIDTH = 30f;

    private bool init = false;

    public delegate void MaxDistance(bool enter);
    public event MaxDistance OnMaxDistance;

    public delegate void MinDistance(bool enter);
    public event MaxDistance OnMinDistance;

    private const int EXTRA_SIZE_WALL_MULTIPLIER = 6;
    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
            cam = gameObject.GetComponent<Camera>();
        if(safeArea == null)
            safeArea = new SafeArea();
    }

    // Update is called once per frame
    void Update()
    {
        if (init && ball != null)
        {
            Reallocate();
        }
    }
    private void Reallocate()
    {
        gameObject.transform.position = ball.transform.position + new Vector3(0, distance, 0);
        float frustumHeight = FrustumHeight(distance);
        float frustumWidth = FrustumWidth(frustumHeight);
        float x = XCoordinate(frustumWidth);
        float z = ZCoordinate(frustumHeight);
        gameObject.transform.position = new Vector3(x, distance, z);

        float ZCoordinate(float frustumHeight)
        {
            float halfHeight = mazeHeight / 2f;
            float halfFrustum = frustumHeight / 2f;
            //Debug.Log("Height landscape" + ScreenHeightLandscape().ToString());
            //Debug.Log("UI height" + UIWidth());
            float halfFrustumTopUI = FrustumUI(halfFrustum, ScreenHeightLandscape() / 2f, 
                UIWidth());
            float halfFrustumTopScreenUsed = HalfFrustumTopScreenUsed(halfFrustum);
            float halfFrustumTop = Mathf.Min(halfFrustumTopUI, halfFrustumTopScreenUsed);

            float halfFrustumBot = HalfFrustumBotScreenUsed(halfFrustum);
            if (halfHeight < halfFrustum)
            {
                return mazeOrigin.z + (halfFrustumBot - halfFrustumTop) / 2f; 
            }
            if (Mathf.Abs(mazeOrigin.z + halfHeight - ball.transform.position.z) < halfFrustumTop)
            {
                return mazeOrigin.z + halfHeight - halfFrustumTop;
            }
            if (Mathf.Abs(mazeOrigin.z - halfHeight - ball.transform.position.z) < halfFrustumBot)
            {
                return mazeOrigin.z - halfHeight + halfFrustumBot;
            }
            return ball.transform.position.z;

            float HalfFrustumTopScreenUsed(float halfFrustum)
            {
                return (safeArea.heightMax - 0.5f) / 0.5f * halfFrustum;
            }
            float HalfFrustumBotScreenUsed(float halfFrustum)
            {
                return (0.5f - safeArea.heightMin) / 0.5f * halfFrustum;
            }
        }
        float XCoordinate(float frustumWidth)
        {
            float halfWidth = mazeWidth / 2f;
            float halfFrustum = frustumWidth / 2f;

            float halfFrustumRight = HalfFrustumRightScreenUsed(halfFrustum);
            float halfFrustumLeft = HalfFrustumLeftScreenUsed(halfFrustum);

            if(halfWidth < halfFrustum)
            {
                return mazeOrigin.x + (halfFrustumLeft - halfFrustumRight) / 2f;
            }
            if (Mathf.Abs(mazeOrigin.x + halfWidth - ball.transform.position.x) < halfFrustumRight)
            {
                return mazeOrigin.x + halfWidth - halfFrustumRight;
            }
            if (Mathf.Abs(mazeOrigin.x -halfWidth - ball.transform.position.x) < halfFrustumLeft)
            {
                return mazeOrigin.x -halfWidth + halfFrustumLeft;
            }
            return ball.transform.position.x;

            float HalfFrustumRightScreenUsed(float halfFrustum)
            {
                return (safeArea.widthMax - 0.5f) / 0.5f * halfFrustum;
            }
            float HalfFrustumLeftScreenUsed(float halfFrustum)
            {
                return (0.5f - safeArea.widthMin) / 0.5f * halfFrustum;
            }
        }
    }
    private float ScreenHeightLandscape()
    {
        return Mathf.Min(Screen.height, Screen.width);
    }
    private float UIWidth()
    {
        return UIContainer.GetComponent<RectTransform>().sizeDelta.y * UICanvas.GetComponent<Canvas>().scaleFactor;
    }
    public void Zoom()
    {
        if (distance == maxDistance)
            TriggerMaxDistance(false);
        if(distance > minDistance)
        {
            AudioUI.GetComponent<AudioUI>().OnMaximize();
            distance = distance - deltaDistance;
            if(distance <= minDistance)
            {
                distance = minDistance;
                TriggerMinDistance(true);
            }
        }
    }
    public void UnZoom()
    {
        if (distance == minDistance)
            TriggerMinDistance(false);
        if (distance < maxDistance)
        {
            AudioUI.GetComponent<AudioUI>().OnMinimize();
            distance = distance + deltaDistance;
            if(distance >= maxDistance)
            {
                distance = maxDistance;
                TriggerMaxDistance(true);
            }
        }
    }
    private void TriggerMinDistance(bool enter)
    {
        if (OnMinDistance != null)
            OnMinDistance(enter);
    }
    private void TriggerMaxDistance(bool enter)
    {
        if (OnMaxDistance != null)
            OnMaxDistance(enter);
    }
    private void UpdateDistances()
    {
        if(cam == null)
            cam = gameObject.GetComponent<Camera>();
        if (safeArea == null)
            safeArea = new SafeArea();

        float maxDistanceHeight = MaxDistanceHeight();
        float maxDistanceWidth = MaxDistanceWidth();
        maxDistance = Mathf.Max(maxDistanceHeight, maxDistanceWidth);
        minDistance = Mathf.Min(MIN_DISTANCE, maxDistance / 2.0f);
        deltaDistance = Mathf.Min(DELTA_DISTANCE, maxDistance - minDistance / 2.0f);


        distance = maxDistance;
        TriggerMaxDistance(true);

        float MaxDistanceHeight()
        {
            float UIWidth = this.UIWidth();
            float percentScreenUsedSafeArea = this.ScreenHeightLandscape() * safeArea.Height();
            float percentScreenUsedUI = (float)(ScreenHeightLandscape() - UIWidth) / ScreenHeightLandscape();
            float maxDistanceHeight = mazeHeight * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            return maxDistanceHeight / Mathf.Min(percentScreenUsedUI, percentScreenUsedSafeArea);
        }
        float MaxDistanceWidth()
        {
            float percentScreenUsed = safeArea.Width();
            float maxDistanceWidth = mazeWidth * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            return maxDistanceWidth / cam.aspect / percentScreenUsed;
        }
    }
    public void InitCamera(bool init)
    {
        this.init = init;
    }
    public void SetBall(GameObject ball)
    {
        this.ball = ball;
    }
    public void SetMazeDimensions(float height, float width, float wallWidth, Vector3 origin)
    {
        mazeOrigin = origin;
        mazeHeight = Multiply(height);
        //mazeHeight += EXTRA_SIZE_WALL_MULTIPLIER * wallWidth;
        mazeWidth = Multiply(width);
        //mazeWidth += EXTRA_SIZE_WALL_MULTIPLIER * wallWidth;
        UpdateDistances();
    }
    public void SetMazeDimensions(float height, float width, float wallWidth)
    {
        mazeOrigin = new Vector3(0,0,0);
        mazeHeight = Multiply(height);
        //mazeHeight += EXTRA_SIZE_WALL_MULTIPLIER * wallWidth;
        mazeWidth = Multiply(width);
        //mazeWidth += EXTRA_SIZE_WALL_MULTIPLIER * wallWidth;
        UpdateDistances();
    }
    public void SetMazeDimensions(float height, float width)
    {
        mazeOrigin = new Vector3(0, 0, 0);
        float wallWidth = 4.0f;
        mazeHeight = Multiply(height);
        //mazeHeight += EXTRA_SIZE_WALL_MULTIPLIER * wallWidth;
        mazeWidth = Multiply(width);
        //mazeWidth += EXTRA_SIZE_WALL_MULTIPLIER * wallWidth;
        UpdateDistances();
    }
    private float Multiply(float dimension)
    {
        return dimension * 1.06f;
    }
    private float AddWallMaze(float dimension, float wallWidth)
    {
        return dimension + EXTRA_SIZE_WALL_MULTIPLIER * wallWidth;
    }
    private float FrustumHeight(float distance)
    {
        float frustumHeight = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        return frustumHeight;
    }
    private float FrustumWidth(float height)
    {
        return height * cam.aspect;
    }
    float FrustumUI(float frustum, float screenDim, float UIWidth)
    {
        float percentScreenUsed = (screenDim - UIWidth) / screenDim;
        return frustum * percentScreenUsed;
    }
}

public class SafeArea
{
    //Width - height on landscape mode 
    public float widthMin
    {
        get;
    }
    public float widthMax {
        get;
    }
    public float heightMin
    {
        get;
    }
    public float heightMax
    {
        get;
    }
    public SafeArea()
    {
        Rect rect = Screen.safeArea;
        Vector2 anchorMin = rect.position;
        Vector2 anchorMax = anchorMin + rect.size;
        anchorMin.x /= Screen.width;
        anchorMax.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.y /= Screen.height;
        if(Screen.height > Screen.width)
        {
            widthMin = anchorMin.y;
            widthMax = anchorMax.y;
            heightMin = anchorMin.x;
            heightMax = anchorMax.x;
        }
        else
        {
            widthMin = anchorMin.x;
            widthMax = anchorMax.x;
            heightMin = anchorMin.y;
            heightMax = anchorMax.y;
        }
    }
    public float Height()
    {
        return heightMax - heightMin;
    }
    public float Width()
    {
        return widthMax - widthMin;
    }
}