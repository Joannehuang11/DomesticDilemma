using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGridGameplayBg : MonoBehaviour
{
    public Sprite activeBgImg;
    public Sprite inactiveBgImg;

    Image GamePlayBg;
    
    // Start is called before the first frame update
    void Start()
    {
        GamePlayBg = GetComponent<Image>();
    }

    public void UpdateBg(pointGridPlayingState state)
    {
        switch (state)
        {
            case pointGridPlayingState.None:
                GamePlayBg.sprite = inactiveBgImg;
                break;
            case pointGridPlayingState.Selecting:
                GamePlayBg.sprite = activeBgImg;
                break;
            case pointGridPlayingState.ResultAction:
                GamePlayBg.sprite = inactiveBgImg;
                break;
            case pointGridPlayingState.Break:
                GamePlayBg.sprite = activeBgImg;
                break;
        }
    }
}
