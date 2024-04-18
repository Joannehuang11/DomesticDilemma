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
    public GameObject actionCardsPlayManagerObj;
    ActionCardsPlayManager actionCardsPlayManager;
    private GameObject audioManagerObj;
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
        audioManagerObj = GameObject.Find("AudioManager");
        audioManager = audioManagerObj.GetComponent<AudioManager>();
        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
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
        int selectedActionCardNo = actionCardsPlayManager.selectedActionCardNo;

        //update UI
        GamePlayUI.GetComponent<UpdateTerritoryPlayBg>().UpdateBg(currentTerritoryPlayingState);

        switch (currentTerritoryPlayingState)
        {
            case territoryPlayingState.None:
                setLandCardsButtonEnabled(false);
                // Debug.Log("territoryPlayingState is None");
                break;
            case territoryPlayingState.Waiting:
                if (selectedActionCardNo == 0)
                {
                    setLineCardButtonEnabled(true);
                }
                else
                {
                    setLandCardsButtonEnabled(true);
                }
                // Debug.Log("territoryPlayingState is Waiting");
                break;
            case territoryPlayingState.P0Placed:
                setLandCardsButtonEnabled(false);
                setLineCardButtonEnabled(false);
                // Debug.Log("territoryPlayingState is P0Placed");
                break;
            case territoryPlayingState.P1Placed:
                setLandCardsButtonEnabled(false);
                setLineCardButtonEnabled(false);
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

    public void setLineCardButtonEnabled(bool isEnable)
    {
        foreach (GameObject card in landCards)
        {
            foreach (GameObject line in card.GetComponent<LandUnit>().lineObjs)
            {
                line.GetComponent<LineUnitManager>().setButtonEnabled(isEnable);
            }
            
        }
    }
}
