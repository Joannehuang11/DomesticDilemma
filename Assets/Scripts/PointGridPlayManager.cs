using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public enum pointGridPlayingState
{
    None,
    Selecting,
    Result
}

public enum pointGridPlayResult
{
    None,
    P1Win,
    P2Win,
    P1P2Draw,
    P1P2GiveUp
}

public class PointGridPlayManager : MonoBehaviour
{
    public pointGridPlayingState currentGridPlayingState = pointGridPlayingState.None;
    public pointGridPlayResult currentGridPlayResult = pointGridPlayResult.None;


    // imgs and obj for GamePlayBg
    public Sprite activeGamePlayBgImg;
    public GameObject activeGamePlayBg;


    // Start is called before the first frame update
    void Start()
    {
        UpdateGridPlayingState(pointGridPlayingState.None);
        
        //GamePlayBg
        Image image = activeGamePlayBg.GetComponent<Image>(); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    // Update the current state of the game    
    public void UpdateGridPlayingState (pointGridPlayingState newState)
    {
        currentGridPlayingState = newState;

        
        // Handle the new state
        switch (currentGridPlayingState)
        {
            case pointGridPlayingState.None:
                Debug.Log("The point grid game is not being played.");
                break;
            case pointGridPlayingState.Selecting:
                Debug.Log("The point grid game is in the selecting stage.");
                break;
            case pointGridPlayingState.Result:
                Debug.Log("The point grid game has a result.");
                break;
        }
    }

    void updateGridGamePlayBg(pointGridPlayingState state)
    {
        Image image = activeGamePlayBg.GetComponent<Image>(); 
        if (state == pointGridPlayingState.None)
        {
            image.sprite = activeGamePlayBgImg;
        }
        else
        {
            image.sprite = null;
        }
    }
}