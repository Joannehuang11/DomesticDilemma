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

    // public int currentRound;
    // public int maxRound = 20;

    //UI
    public GameObject GamePlayUI;
    public GameObject PointGridUI;

    // interface
    public GameObject player0Obj;
    PlayerManager player0Manager;
    public GameObject player1Obj;
    PlayerManager player1Manager;
    public GameObject actionCardsPlayManagerObj;
    ActionCardsPlayManager actionCardsPlayManager;
    public GameObject territoryPlayManagerObj;
    TerritoryPlayManager territoryPlayManager;
    public GameObject progressManagerObj;
    ProgressManager progressManager;


    // Start is called before the first frame update
    void Start()
    {
        SetGridPlayingState(pointGridPlayingState.None);
        SetGridPlayResult(pointGridPlayResult.None);

        player0Manager = player0Obj.GetComponent<PlayerManager>();
        player1Manager = player1Obj.GetComponent<PlayerManager>();
        
        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        progressManager = progressManagerObj.GetComponent<ProgressManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //player make calls
        if (currentGridPlayingState == pointGridPlayingState.Selecting)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Debug.Log("Q key was pressed.");
                player0Manager.SetPlayerCall(playerCall.Collab);
            } 
            else if (Input.GetKeyDown(KeyCode.W))
            {
                // Debug.Log("W key was pressed.");
                player0Manager.SetPlayerCall(playerCall.NotCollab);
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                // Debug.Log("O key was pressed.");
                player1Manager.SetPlayerCall(playerCall.Collab);
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                // Debug.Log("P key was pressed.");
                player1Manager.SetPlayerCall(playerCall.NotCollab);
            }   
        }
    }

    public void StartPointGridGame()
    {
        int currentRound = progressManager.GetCurrentRound();
        int maxRound = progressManager.GetMaxRound();
        
        if (currentRound < maxRound)
        {
            currentRound++;
            Debug.Log("Start Round from pointGrid: " + currentRound);
            progressManager.SetCurrentRound(currentRound);
            
            //reset player call and status
            player0Manager.SetPlayerCall(playerCall.None);
            player0Manager.SetPlayerStatus(playerStatus.Selecting, 0, true, false);
            player1Manager.SetPlayerCall(playerCall.None);
            player1Manager.SetPlayerStatus(playerStatus.Selecting, 0, true, false);

            //update games
            SetGridPlayingState(pointGridPlayingState.Selecting);
            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.None);
            territoryPlayManager.SetTerritoryPlayingState(territoryPlayingState.None);

            //start game
            PointGridUI.GetComponent<UpdatePointGridImg>().startCountDown();
        }
        else
        {
            Debug.Log("Game Over");
        } 
    }

    //update game result
    public void UpdateGridGameResult()
    {
        playerCall playercall0 = player0Manager.GetPlayerCall();
        playerCall playercall1 = player1Manager.GetPlayerCall();

        // if not select
        if (playercall0 == playerCall.None)
        {
            player0Manager.SetPlayerCall(playerCall.NotCollab); 
            // Debug.Log("Player 1 not select, set playercall to NotCollab");
        }
        if (playercall1 == playerCall.None)
        {
            player1Manager.SetPlayerCall(playerCall.NotCollab); 
            // Debug.Log("Player 2 not select, set playercall to NotCollab");
        }

        //update result
        if (player0Manager.GetPlayerCall() == playerCall.Collab && player1Manager.GetPlayerCall() == playerCall.Collab)
        {
            SetGridPlayResult(pointGridPlayResult.P0P1Draw);
            player0Manager.SetPlayerStatus(playerStatus.Action, 3, false, true);
            player1Manager.SetPlayerStatus(playerStatus.Hold, 3, false, true);
            
            //update games
            SetGridPlayingState(pointGridPlayingState.ResultAction);
            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting);
            territoryPlayManager.SetTerritoryPlayingState(territoryPlayingState.None);        
        }
        else if (player0Manager.GetPlayerCall() == playerCall.NotCollab && player1Manager.GetPlayerCall() == playerCall.Collab)
                {
            SetGridPlayResult(pointGridPlayResult.P0Win);
            player0Manager.SetPlayerStatus(playerStatus.Action, 5, false, true);
            player1Manager.SetPlayerStatus(playerStatus.Hold, 0, false, true);

            //update games
            SetGridPlayingState(pointGridPlayingState.ResultAction);
            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting);        
            territoryPlayManager.SetTerritoryPlayingState(territoryPlayingState.None);        
        }
        else if (player0Manager.GetPlayerCall() == playerCall.Collab && player1Manager.GetPlayerCall() == playerCall.NotCollab)
        {
            SetGridPlayResult(pointGridPlayResult.P1Win);
            player0Manager.SetPlayerStatus(playerStatus.Action, 0, false, true);
            player1Manager.SetPlayerStatus(playerStatus.Hold, 5, false, true);

            //update games
            SetGridPlayingState(pointGridPlayingState.ResultAction);
            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting);        
            territoryPlayManager.SetTerritoryPlayingState(territoryPlayingState.None);            
        }
        else if (player0Manager.GetPlayerCall() == playerCall.NotCollab && player1Manager.GetPlayerCall() == playerCall.NotCollab)
        {
            SetGridPlayResult(pointGridPlayResult.P0P1GiveUp);
            player0Manager.SetPlayerStatus(playerStatus.Action, 1, false, true);
            player1Manager.SetPlayerStatus(playerStatus.Hold, 1, false, true);
        
            //update games
            SetGridPlayingState(pointGridPlayingState.ResultAction);
            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting);
            territoryPlayManager.SetTerritoryPlayingState(territoryPlayingState.None);        
        }
        else
        {
            Debug.Log("fail to update grid game result");
        }
        // Debug.Log("Update game: Player 0 call:" + player0.GetComponent<PlayerManager>().GetPlayerCall() + " Player 1 call:" + player1.GetComponent<PlayerManager>().GetPlayerCall() + " Result:" + currentGridPlayResult);


        //update game UI
        PointGridUI.GetComponent<UpdatePointGridImg>().SetPointGridImg(currentGridPlayResult);

    }

    // Update the current state of the game    
    public void SetGridPlayingState (pointGridPlayingState newState)
    {
        currentGridPlayingState = newState;

        // Update the section background
        GamePlayUI.GetComponent<UpdateGridGameplayBg>().UpdateBg(currentGridPlayingState);
        
        // Handle the new state
        switch (currentGridPlayingState)
        {
            case pointGridPlayingState.None:
                Debug.Log("pointGridPlayingState is None.");
                break;
            case pointGridPlayingState.Selecting:
                Debug.Log("pointGridPlayingState is Selecting.");
                break;
            case pointGridPlayingState.ResultAction:
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
                // Debug.Log("pointGridPlayResult is None.");
                break;
            case pointGridPlayResult.P0Win:
                // Debug.Log("pointGridPlayResult is P1Win.");
                break;
            case pointGridPlayResult.P1Win:
                // Debug.Log("pointGridPlayResult is P2Win.");
                break;
            case pointGridPlayResult.P0P1Draw:
                // Debug.Log("pointGridPlayResult is P1P2Draw.");
                break;
            case pointGridPlayResult.P0P1GiveUp:
                // Debug.Log("pointGridPlayResult is P1P2GiveUp.");
                break;
        }
    }
}