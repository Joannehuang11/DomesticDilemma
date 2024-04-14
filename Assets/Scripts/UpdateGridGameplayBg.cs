using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGridGameplayBg : MonoBehaviour
{
    public Sprite activeGamePlayBgImg;
    public Sprite inactiveGamePlayBgImg;

    Image GamePlayBg;
    
    // Start is called before the first frame update
    void Start()
    {
        GamePlayBg = GetComponent<Image>();
    }

    public void UpdateBg(bool isActive)
    {
        if (isActive)
        {
            GamePlayBg.sprite = activeGamePlayBgImg;
            Debug.Log("Update the GamePlayBg to active.");
        }
        else
        {
            GamePlayBg.sprite = inactiveGamePlayBgImg;
            Debug.Log("Update the GamePlayBg to inactive.");
        }
    }
}
