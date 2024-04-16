using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePointGridImg : MonoBehaviour
{
    //status images
    public Sprite defaultImg;
    public Sprite p0WinImg;
    public Sprite p1WinImg;
    public Sprite drawImg;
    public Sprite giveUpImg;
    
    //countdown images
    public List<Sprite> countdownImgs;

    //interface
    public GameObject pointGridPlayManagerObj;
    PointGridPlayManager pointGridPlayManager;
    // public GameObject territoryPlayManagerObj;
    // TerritoryPlayManager territoryPlayManager;
    public GameObject progressManagerObj;
    ProgressManager progressManager;
    
    Image pointGridImg;
    
    // Start is called before the first frame update
    void Start()
    {
        pointGridPlayManager = pointGridPlayManagerObj.GetComponent<PointGridPlayManager>();
        // territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        progressManager = progressManagerObj.GetComponent<ProgressManager>();
        
        pointGridImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPointGridImg(pointGridPlayResult result)
    {
        
        switch (result)
        {
            case pointGridPlayResult.None:
                pointGridImg.sprite = defaultImg;
                break;
            case pointGridPlayResult.P0Win:
                pointGridImg.sprite = p0WinImg;
                break;
            case pointGridPlayResult.P1Win:
                pointGridImg.sprite = p1WinImg;
                break;
            case pointGridPlayResult.P0P1Draw:
                pointGridImg.sprite = drawImg;
                break;
            case pointGridPlayResult.P0P1GiveUp:
                pointGridImg.sprite = giveUpImg;
                break;
            default:
                Debug.Log("fail to set pointGridImgStatus");
                break;
        }
    }

    public void startCountDownSelecting()
    {
        StartCoroutine(updateCountDownImgSelecting());
    }
    
    private IEnumerator updateCountDownImgSelecting()
    {
        // Debug.Log("Start Countdown Img Update");
        for (int i = 4; i >= 0; i--)
        {
            pointGridImg.sprite = countdownImgs[i];
            yield return new WaitForSeconds(1f);
        }
        pointGridPlayManager.UpdateGridGameResult();
    }

    public void startCountDownBreak(int min)
    {
        StartCoroutine(updateCountDownImgBreak(min));
    }

    private IEnumerator updateCountDownImgBreak(int min)
    {
        // Debug.Log("Start Countdown Img Update");
        for (int i = min-1; i >= 0; i--)
        {
            pointGridImg.sprite = countdownImgs[i];
            yield return new WaitForSeconds(60f);
        }

        progressManager.startGame();
    }
}
