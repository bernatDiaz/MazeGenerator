using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;

public class AdsManager : MonoBehaviour
{
    private Banner bannerBottomLeft;
    private Banner bannerBottomRight;

    private List<SCREEN> bottomLeftScreens;
    private List<SCREEN> bottomRightScreens;
    private void InitLists()
    {
        bottomLeftScreens = new List<SCREEN>();
        bottomLeftScreens.Add(SCREEN.MAIN);
        bottomLeftScreens.Add(SCREEN.SCORE);
        bottomRightScreens = new List<SCREEN>();
        bottomRightScreens.Add(SCREEN.MAIN);
        bottomRightScreens.Add(SCREEN.SCORE);
    }
    void Start()
    {
        if (bottomLeftScreens == null || bottomRightScreens == null)
            InitLists();
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        if(bannerBottomLeft == null)
            LoadBannerBottomLeft();
        if(bannerBottomRight == null)
            LoadBannerBottomRight();
    }
    public void OnChangeScreen(SCREEN screen)
    {
        if (bottomLeftScreens == null || bottomRightScreens == null)
            InitLists();
        if (BottomLeftScreens(screen))
        {
            if (bannerBottomLeft == null)
                LoadBannerBottomLeft();
            else
            {
                bannerBottomLeft.Show();
            }
        }
        else
        {
            if (bannerBottomLeft != null)
            {
                bannerBottomLeft.Hide();
            }
        }
        if (BottomRightScreens(screen))
        {
            if (bannerBottomRight == null)
                LoadBannerBottomRight();
            else
            {
                bannerBottomRight.Show();
            }
        }
        else
        {
            if (bannerBottomRight != null)
                bannerBottomRight.Hide();
        }        
    }
    bool BottomLeftScreens(SCREEN screen)
    {
        return bottomLeftScreens.Contains(screen);
    }
    bool BottomRightScreens(SCREEN screen)
    {
        return bottomRightScreens.Contains(screen);
    }
    private void LoadBannerBottomLeft()
    {
        if (bannerBottomLeft != null)
            bannerBottomLeft.Destroy();
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6033388627780203/7672934018";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif
        bannerBottomLeft = new Banner(adUnitId, AdPosition.BottomLeft);
        //bannerViewBottomLeft = new BannerView(adUnitId, AdSize.Banner, AdPosition.BottomLeft);
        AdRequest request = new AdRequest.Builder().Build();
        bannerBottomLeft.LoadAd(request);
        //bannerViewBottomLeft.LoadAd(request);
    }
    private void LoadBannerBottomRight()
    {
        if (bannerBottomRight != null)
            bannerBottomRight.Destroy();
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6033388627780203/3158973933";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif
        bannerBottomRight = new Banner(adUnitId, AdPosition.BottomRight);
        //bannerViewBottomRight = new BannerView(adUnitId, AdSize.Banner, AdPosition.BottomRight);
        AdRequest request = new AdRequest.Builder().Build();
        bannerBottomRight.LoadAd(request);
        //bannerViewBottomRight.LoadAd(request);
    }
}
public class Banner
{
    public BannerView bannerView;
    public bool visible;
    private AdPosition position;
    public Banner(string id, AdPosition position)
    {
        bannerView = new BannerView(id, AdSize.Banner, position);
        this.position = position;
        visible = true;
    }
    public void LoadAd(AdRequest request)
    {
        bannerView.LoadAd(request);
    }
    public void Destroy()
    {
        bannerView.Destroy();
    }
    public void Show()
    {
        if (!visible)
        {
            bannerView.Show();
            bannerView.SetPosition(position);
            visible = true;
        }
    }
    public void Hide()
    {
        if (visible)
        {
            bannerView.Hide();
            visible = false;
        }
    }
}
/*public class AdsManager : MonoBehaviour
{
    string gameId = "4210199";
    string bannerId = "Banner_Android";
    bool testMode = false;


    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }
    public void OnChangeScreen(SCREEN screen)
    {
        if(screen == SCREEN.MAIN)
        {
            StartCoroutine(RequestBanner());
        }
        else
        {
            Advertisement.Banner.Hide();
        }
    }
    IEnumerator RequestBanner()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_LEFT);
        Advertisement.Banner.Show(bannerId);
    }
}*/
