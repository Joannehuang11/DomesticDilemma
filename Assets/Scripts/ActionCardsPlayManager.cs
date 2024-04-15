using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum actionCardsPlayingState
{
    None,
    Waiting,
    P0Selected,
    P1Selected,
}

public class ActionCardsPlayManager : MonoBehaviour
{
    public GameObject pointGridPlayManager;
    public GameObject player0;
    public GameObject player1;

    public actionCardsPlayingState actionCardsPlayingState;

    public List<GameObject> actionCards;
    public int selectedCoinCost;

    //UI
    public GameObject GamePlayUI;

    
    // Start is called before the first frame update
    void Start()
    {
        setActionCardsPlayingState(actionCardsPlayingState.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActionCardsPlayingState(actionCardsPlayingState state)
    {
        GamePlayUI.GetComponent<UpdateActionCardsPlayBg>().UpdateBg(state);

        switch (state)
        {
            case actionCardsPlayingState.None:
                Debug.Log("actionCardsPlayingState is None");
                break;
            case actionCardsPlayingState.Waiting:
                Debug.Log("actionCardsPlayingState is Waiting");
                break;
            case actionCardsPlayingState.P0Selected:
                Debug.Log("actionCardsPlayingState is P0Selected");
                break;
            case actionCardsPlayingState.P1Selected:
                Debug.Log("actionCardsPlayingState is P1Selected");
                break;
        }
    }

    public actionCardsPlayingState getActionCardsPlayingState()
    {
        return actionCardsPlayingState;
    }

    public GameObject getActionCard(int cardNo)
    {
        return actionCards[cardNo];
    }
}
