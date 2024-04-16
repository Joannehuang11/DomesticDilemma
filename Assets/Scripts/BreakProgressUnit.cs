using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreakProgressUnit : MonoBehaviour
{
    public int unitNo;
    public bool isDone;
    public int waitTime;

    public Sprite doneImg;
    public Sprite NotDoneImg;

    public Image unitImg;
    public TextMeshProUGUI waitTimeText;

    
    // Start is called before the first frame update
    void Start()
    {
        waitTimeText.text = waitTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUnit(int no, int waitTime, bool isDone)
    {
        this.unitNo = no;
        this.isDone = isDone;
        this.waitTime = waitTime;

        //update UI
        waitTimeText.text = waitTime.ToString();
    }
 
    public void updateImg(bool isDone)
    {
        if(isDone)
            unitImg.sprite = doneImg;
        else
            unitImg.sprite = NotDoneImg;
    }    
}
