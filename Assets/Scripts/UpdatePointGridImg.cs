using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePointGridImg : MonoBehaviour
{
    //status images
    public Sprite defaultImg;
    public Sprite p1WinImg;
    public Sprite p2WinImg;
    public Sprite drawImg;
    public Sprite giveUpImg;
    
    //countdown images
    public Sprite countdownImg5;
    public Sprite countdownImg4;
    public Sprite countdownImg3;
    public Sprite countdownImg2;
    public Sprite countdownImg1;

    private Image pointGridImg;
    
    // Start is called before the first frame update
    void Start()
    {
        pointGridImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePointGridImgStatus(pointGridPlayResult result)
    {
        switch (result)
        {
            case pointGridPlayResult.None:
                pointGridImg.sprite = defaultImg;
                break;
            case pointGridPlayResult.P1Win:
                pointGridImg.sprite = p1WinImg;
                break;
            case pointGridPlayResult.P2Win:
                pointGridImg.sprite = p2WinImg;
                break;
            case pointGridPlayResult.P1P2Draw:
                pointGridImg.sprite = drawImg;
                break;
            case pointGridPlayResult.P1P2GiveUp:
                pointGridImg.sprite = giveUpImg;
                break;
            default:
                Debug.Log("fail update pointGridImgStatus");
                break;
        }
    }

    public void startCountDown()
    {
        StartCoroutine(updateCountDownImg());
    }
    
    private IEnumerator updateCountDownImg()
    {
        Debug.Log("Start Countdown Img Update");
        
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
    }
}
