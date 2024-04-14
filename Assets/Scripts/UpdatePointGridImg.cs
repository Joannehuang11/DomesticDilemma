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
    public Sprite countdownImg5;
    public Sprite countdownImg4;
    public Sprite countdownImg3;
    public Sprite countdownImg2;
    public Sprite countdownImg1;

    //callback
    public GameObject pointGridPlayManager;
    Image pointGridImg;
    
    // Start is called before the first frame update
    void Start()
    {
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

    public void startCountDown()
    {
        StartCoroutine(updateCountDownImg());
    }
    
    private IEnumerator updateCountDownImg()
    {
        // Debug.Log("Start Countdown Img Update");
        
        pointGridImg.sprite = countdownImg5;
        yield return new WaitForSeconds(1f);

        pointGridImg.sprite = countdownImg4;
        yield return new WaitForSeconds(1f);

        pointGridImg.sprite = countdownImg3;
        yield return new WaitForSeconds(1f);

        pointGridImg.sprite = countdownImg2;
        yield return new WaitForSeconds(1f);

        pointGridImg.sprite = countdownImg1;
        yield return new WaitForSeconds(1f);

        pointGridPlayManager.GetComponent<PointGridPlayManager>().UpdateGridGameResult();
    }
}
