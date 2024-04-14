using UnityEngine;

public enum pointGridPlayingState
{
    None,
    Selecting,
    ResultAction
}

public enum pointGridPlayResult
{
    None,
    P0Win,
    P1Win,
    P0P1Draw,
    P0P1GiveUp
}

public class PointGridPlayManager : MonoBehaviour
{
    //basic game data
    public pointGridPlayingState currentGridPlayingState = pointGridPlayingState.None;
    public pointGridPlayResult currentGridPlayResult = pointGridPlayResult.None;
    public int currentRound;
    public int maxRound = 20;

    // trigger updates to the UI
    public GameObject GamePlayUI;
    public GameObject PointGridUI;

    // players data
    public GameObject player0;
    public GameObject player1;


    // Start is called before the first frame update
    void Start()
    {
        currentRound = 0;
        SetGridPlayingState(pointGridPlayingState.None);
        SetGridPlayResult(pointGridPlayResult.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPointGridGame()
    {
        if (currentRound < maxRound)
        {
            currentRound++;
            Debug.Log("Start Round" + currentRound);
            
            SetGridPlayingState(pointGridPlayingState.Selecting);
            PointGridUI.GetComponent<UpdatePointGridImg>().startCountDown();
        }
        else
        {
            Debug.Log("Game Over");
        } 
    }

    //check game result
    public void UpdateGridGameResult()
    {
        playerCall playercall0 = player0.GetComponent<PlayerManager>().GetPlayerCall();
        playerCall playercall1 = player1.GetComponent<PlayerManager>().GetPlayerCall();

        // if not select
        if (playercall0 == playerCall.None)
        {
            player0.GetComponent<PlayerManager>().SetPlayerCall(playerCall.NotCollab); 
            // Debug.Log("Player 1 not select, set playercall to NotCollab");
        }
        if (playercall1 == playerCall.None)
        {
            player1.GetComponent<PlayerManager>().SetPlayerCall(playerCall.NotCollab); 
            // Debug.Log("Player 2 not select, set playercall to NotCollab");
        }

        //get result
        if (player0.GetComponent<PlayerManager>().GetPlayerCall() == playerCall.Collab && player1.GetComponent<PlayerManager>().GetPlayerCall() == playerCall.Collab)
        {
            SetGridPlayResult(pointGridPlayResult.P0P1Draw);
        }
        else if (player0.GetComponent<PlayerManager>().GetPlayerCall() == playerCall.Collab && player1.GetComponent<PlayerManager>().GetPlayerCall() == playerCall.NotCollab)
        {
            SetGridPlayResult(pointGridPlayResult.P0Win);
        }
        else if (player0.GetComponent<PlayerManager>().GetPlayerCall() == playerCall.NotCollab && player1.GetComponent<PlayerManager>().GetPlayerCall() == playerCall.Collab)
        {
            SetGridPlayResult(pointGridPlayResult.P1Win);
        }
        else if (player0.GetComponent<PlayerManager>().GetPlayerCall() == playerCall.NotCollab && player1.GetComponent<PlayerManager>().GetPlayerCall() == playerCall.NotCollab)
        {
            SetGridPlayResult(pointGridPlayResult.P0P1GiveUp);
        }
        else
        {
            Debug.Log("fail to update grid game result");
        }

        Debug.Log("Update game: Player 0 call:" + player0.GetComponent<PlayerManager>().GetPlayerCall() + " Player 1 call:" + player1.GetComponent<PlayerManager>().GetPlayerCall() + " Result:" + currentGridPlayResult);

        //update player coin

        //update game playing state
        SetGridPlayingState(pointGridPlayingState.ResultAction);

        //update game UI
        PointGridUI.GetComponent<UpdatePointGridImg>().SetPointGridImg(currentGridPlayResult);
            // update status sign
            // update status text
        // update game playing state -> result action
    }

    // Update the current state of the game    
    public void SetGridPlayingState (pointGridPlayingState newState)
    {
        currentGridPlayingState = newState;
        
        // Handle the new state
        switch (currentGridPlayingState)
        {
            case pointGridPlayingState.None:
                GamePlayUI.GetComponent<UpdateGridGameplayBg>().UpdateBg(false);
                Debug.Log("pointGridPlayingState is None.");
                break;
            case pointGridPlayingState.Selecting:
                GamePlayUI.GetComponent<UpdateGridGameplayBg>().UpdateBg(true);
                Debug.Log("pointGridPlayingState is Selecting.");
                break;
            case pointGridPlayingState.ResultAction:
                GamePlayUI.GetComponent<UpdateGridGameplayBg>().UpdateBg(false);
                Debug.Log("pointGridPlayingState is Result Action.");
                break;
        }
    }

    // Update the current result of the game
    public void SetGridPlayResult (pointGridPlayResult newResult)
    {
        currentGridPlayResult = newResult;

        // Handle the new result
        switch (currentGridPlayResult)
        {
            case pointGridPlayResult.None:
                Debug.Log("pointGridPlayResult is None.");
                break;
            case pointGridPlayResult.P0Win:
                Debug.Log("pointGridPlayResult is P1Win.");
                break;
            case pointGridPlayResult.P1Win:
                Debug.Log("pointGridPlayResult is P2Win.");
                break;
            case pointGridPlayResult.P0P1Draw:
                Debug.Log("pointGridPlayResult is P1P2Draw.");
                break;
            case pointGridPlayResult.P0P1GiveUp:
                Debug.Log("pointGridPlayResult is P1P2GiveUp.");
                break;
        }
    }
}