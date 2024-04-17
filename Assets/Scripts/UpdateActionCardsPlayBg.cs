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

    public void UpdateBg(actionCardsPlayingState state, bool canMoveOn)
    {
        switch(state)
        {
            case actionCardsPlayingState.None:
                GamePlayBg.sprite = inactiveBgImg;
                break;
            case actionCardsPlayingState.P0Selected:
                GamePlayBg.sprite = canMoveOn ? inactiveBgImg : activeBgImg;
                break;
            case actionCardsPlayingState.P1Selected:
                GamePlayBg.sprite = canMoveOn ? inactiveBgImg : activeBgImg;
                break;
            case actionCardsPlayingState.Waiting:
                GamePlayBg.sprite = activeBgImg;
                break;
        }
    }
}
