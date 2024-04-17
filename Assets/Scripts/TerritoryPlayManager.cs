using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum territoryPlayingState
{
    None,
    Waiting,
    P0Placed,
    P1Placed,
}

public class TerritoryPlayManager : MonoBehaviour
{
    public GameObject player0;
    public GameObject player1;


    public territoryPlayingState currentTerritoryPlayingState;

    public GameObject landCardsDatasObj;

    //UI
    public GameObject GamePlayUI;
    
    // Start is called before the first frame update
    void Start()
    {        
        SetTerritoryPlayingState(territoryPlayingState.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTerritoryPlayingState(territoryPlayingState newState)
    {
        currentTerritoryPlayingState = newState;

        //update UI
        GamePlayUI.GetComponent<UpdateTerritoryPlayBg>().UpdateBg(currentTerritoryPlayingState);

        switch (currentTerritoryPlayingState)
        {
            case territoryPlayingState.None:
                Debug.Log("territoryPlayingState is None");
                break;
            case territoryPlayingState.Waiting:
                Debug.Log("territoryPlayingState is Waiting");
                break;
            case territoryPlayingState.P0Placed:
                Debug.Log("territoryPlayingState is P0Selected");
                break;
            case territoryPlayingState.P1Placed:
                Debug.Log("territoryPlayingState is P1Selected");
                break;
        }
    }

    public territoryPlayingState GetTerritoryPlayingState()
    {
        return currentTerritoryPlayingState;
    }
}
