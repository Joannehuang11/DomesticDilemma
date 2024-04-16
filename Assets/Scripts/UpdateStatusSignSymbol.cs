using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStatusSignSymbol : MonoBehaviour
{
    //status images
    public Sprite questionImg;
    public Sprite checkImg;
    public Sprite timeImg;
    
    public GameObject playerManager;
    Image StatusSignSymbolImg;
    
    // Start is called before the first frame update
    void Start()
    {
        StatusSignSymbolImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSymbol (playerStatus state)
    {
        switch (state)
        {
            case playerStatus.Selecting:
                StatusSignSymbolImg.sprite = questionImg;
                break;
            case playerStatus.Ready:
                StatusSignSymbolImg.sprite = checkImg;
                break;
            case playerStatus.Action:
                StatusSignSymbolImg.sprite = null;
                break;
            case playerStatus.Hold:
                StatusSignSymbolImg.sprite = questionImg;
                break;
            case playerStatus.Break:
                StatusSignSymbolImg.sprite = timeImg;
                break;
            default:
                Debug.Log("fail to set StatusSignSymbolImg");
                break;
        }
    }
}
