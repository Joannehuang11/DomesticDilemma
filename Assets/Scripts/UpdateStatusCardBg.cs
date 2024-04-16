using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStatusCardBg : MonoBehaviour
{
    public Sprite ActiveStatusCardBg;
    public Sprite InactiveStatusCardBg;
    public Sprite P0StatusCardBg;
    public Sprite P1StatusCardBg;

    Image StatusCardBg;

    
    // Start is called before the first frame update
    void Start()
    {
        StatusCardBg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBg(playerStatus playerStatus, int playerNo)
    {
        switch (playerStatus)
        {
            case playerStatus.Selecting:
                StatusCardBg.sprite = InactiveStatusCardBg;
                break;
            case playerStatus.Ready:
                StatusCardBg.sprite = ActiveStatusCardBg;
                break;
            case playerStatus.Action:
                if (playerNo == 0)
                {
                    StatusCardBg.sprite = P0StatusCardBg;
                }
                else
                {
                    StatusCardBg.sprite = P1StatusCardBg;
                }
                break;
            case playerStatus.Hold:
                StatusCardBg.sprite = InactiveStatusCardBg;
                break;
            case playerStatus.Break:
                StatusCardBg.sprite = InactiveStatusCardBg;
                break;
        }
    }
}
