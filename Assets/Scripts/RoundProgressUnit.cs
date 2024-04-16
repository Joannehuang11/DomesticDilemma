using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundProgressUnit : MonoBehaviour
{
    public int unitNo;
    public bool isDone;

    public Sprite doneImg;
    public Sprite NotDoneImg;

    Image unitImg;
    
    // Start is called before the first frame update
    void Start()
    {
        unitImg = GetComponent<Image>();

        if(isDone)
            unitImg.sprite = doneImg;
        else
            unitImg.sprite = NotDoneImg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUnit(int no, bool isDone)
    {
        this.unitNo = no;
        this.isDone = isDone;
    }
 
    public void updateImg(bool isDone)
    {
        if(isDone)
            unitImg.sprite = doneImg;
        else
            unitImg.sprite = NotDoneImg;
    }
}
