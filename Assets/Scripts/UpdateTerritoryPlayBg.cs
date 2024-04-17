using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTerritoryPlayBg : MonoBehaviour
{
    public Sprite activeBgImg;
    public Sprite inactiveBgImg;

    public Image GamePlayBg;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBg(territoryPlayingState state)
    {
        if (state == territoryPlayingState.None)
        {
            GamePlayBg.sprite = inactiveBgImg;
        }
        else
        {
            GamePlayBg.sprite = activeBgImg;
        }
    }
}
