using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateActionCardsPlayBg : MonoBehaviour
{
    public Sprite activeBgImg;
    public Sprite inactiveBgImg;

    Image GamePlayBg;
    
    // Start is called before the first frame update
    void Start()
    {
        GamePlayBg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBg(actionCardsPlayingState state)
    {
        if (state == actionCardsPlayingState.Waiting)
        {
            GamePlayBg.sprite = activeBgImg;
        }
        else
        {
            GamePlayBg.sprite = inactiveBgImg;
        }
    }
}
