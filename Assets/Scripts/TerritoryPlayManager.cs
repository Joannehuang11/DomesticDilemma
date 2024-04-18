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
    // public GameObject player0;
    // public GameObject player1;
    public GameObject audioManagerObj;
    AudioManager audioManager;
    public GameObject LandCardsDatasObk;
    LandCardsDatas landCardsDatas;
    private List<GameObject> landCards;

    public territoryPlayingState currentTerritoryPlayingState;

    // public GameObject landCardsDatasObj;

    //UI
    public GameObject GamePlayUI;
    
    // Start is called before the first frame update
    void Start()
    {   
        audioManager = audioManagerObj.GetComponent<AudioManager>();
        landCardsDatas = LandCardsDatasObk.GetComponent<LandCardsDatas>();
        landCards = landCardsDatas.landCards;

        setTerritoryPlayingState(territoryPlayingState.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTerritoryPlayingState(territoryPlayingState newState)
    {
        currentTerritoryPlayingState = newState;

        //update UI
        GamePlayUI.GetComponent<UpdateTerritoryPlayBg>().UpdateBg(currentTerritoryPlayingState);

        switch (currentTerritoryPlayingState)
        {
            case territoryPlayingState.None:
                setLandCardsButtonEnabled(false);
                // Debug.Log("territoryPlayingState is None");
                break;
            case territoryPlayingState.Waiting:
                setLandCardsButtonEnabled(true);
                // Debug.Log("territoryPlayingState is Waiting");
                break;
            case territoryPlayingState.P0Placed:
                setLandCardsButtonEnabled(false);
                // Debug.Log("territoryPlayingState is P0Placed");
                break;
            case territoryPlayingState.P1Placed:
                setLandCardsButtonEnabled(false);
                // Debug.Log("territoryPlayingState is P1Placed");
                break;
        }
    }

    
    public territoryPlayingState GetTerritoryPlayingState()
    {
        return currentTerritoryPlayingState;
    }

    public void clickLandCardSoundPlay()
    {
        audioManager.playClickSound();
        // Debug.Log("clickLandCardSoundPlay");
    }

    public void setLandCardsButtonEnabled(bool isEnable)
    {
        foreach (GameObject card in landCards)
        {
            card.GetComponent<LandUnit>().setButtonEnabled(isEnable);
        }
    }
}
