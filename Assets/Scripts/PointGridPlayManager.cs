using UnityEngine;

public enum pointGridPlayingState
{
    None,
    Selecting,
    ResultAction,
    Break,
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
    public int coinWinDraw;
    public int coinWinGiveUp;
    public int pointWinPlayer;

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
    // public GameObject territoryPlayManagerObj;
    // TerritoryPlayManager territoryPlayManager;
    // private GameObject progressManagerObj;
    // ProgressManager progressManager;


    // Start is called before the first frame update
    void Start()
    {
        player0Manager = player0Obj.GetComponent<PlayerManager>();
        player1Manager = player1Obj.GetComponent<PlayerManager>();
        actionCardsPlayManager = actionCardsPlayManagerObj.GetComponent<ActionCardsPlayManager>();
        // territoryPlayManager = territoryPlayManagerObj.GetComponent<TerritoryPlayManager>();
        
        // progressManagerObj = GameObject.Find("ProgressManager");
        // progressManager = progressManagerObj.GetComponent<ProgressManager>();

        SetGridPlayingState(pointGridPlayingState.None);
        // SetGridPlayResult(pointGridPlayResult.None);
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
        //reset players call and status
        player0Manager.SetPlayerCall(playerCall.None);
        player0Manager.SetPlayerStatus(playerStatus.Selecting, 0, true, false);
        player1Manager.SetPlayerCall(playerCall.None);
        player1Manager.SetPlayerStatus(playerStatus.Selecting, 0, true, false);

        //update UI countdown
        PointGridUI.GetComponent<UpdatePointGridImg>().startCountDownSelecting();
    }

    public void BreakPointGridGame(int min)
    {
        player0Manager.SetPlayerStatus(playerStatus.Break, 0, true, false);
        player1Manager.SetPlayerStatus(playerStatus.Break, 0, true, false);

        //update UI
        PointGridUI.GetComponent<UpdatePointGridImg>().startCountDownBreak(min);
    }

    public void SkipBreakPointGridGame()
    {
        //reset players call and status
        player0Manager.SetPlayerCall(playerCall.None);
        player0Manager.SetPlayerStatus(playerStatus.Selecting, 0, true, false);
        player1Manager.SetPlayerCall(playerCall.None);
        player1Manager.SetPlayerStatus(playerStatus.Selecting, 0, true, false);

        //update UI countdown
        PointGridUI.GetComponent<UpdatePointGridImg>().stopCountDownBreak();
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
            player0Manager.SetPlayerStatus(playerStatus.Action, coinWinDraw, false, true);
            player1Manager.SetPlayerStatus(playerStatus.Hold, coinWinDraw, false, true);
            
            //update games
            SetGridPlayingState(pointGridPlayingState.ResultAction);
            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting, true);
            // territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);        
        }
        else if (player0Manager.GetPlayerCall() == playerCall.NotCollab && player1Manager.GetPlayerCall() == playerCall.Collab)
                {
            SetGridPlayResult(pointGridPlayResult.P0Win);
            player0Manager.SetPlayerStatus(playerStatus.Action, pointWinPlayer, false, true);
            player1Manager.SetPlayerStatus(playerStatus.Hold, 0, false, true);

            //update games
            SetGridPlayingState(pointGridPlayingState.ResultAction);
            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting, true);        
            // territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);        
        }
        else if (player0Manager.GetPlayerCall() == playerCall.Collab && player1Manager.GetPlayerCall() == playerCall.NotCollab)
        {
            SetGridPlayResult(pointGridPlayResult.P1Win);
            player0Manager.SetPlayerStatus(playerStatus.Action, 0, false, true);
            player1Manager.SetPlayerStatus(playerStatus.Hold, pointWinPlayer, false, true);

            //update games
            SetGridPlayingState(pointGridPlayingState.ResultAction);
            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting, true);        
            // territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);            
        }
        else if (player0Manager.GetPlayerCall() == playerCall.NotCollab && player1Manager.GetPlayerCall() == playerCall.NotCollab)
        {
            SetGridPlayResult(pointGridPlayResult.P0P1GiveUp);
            player0Manager.SetPlayerStatus(playerStatus.Action, coinWinGiveUp, false, true);
            player1Manager.SetPlayerStatus(playerStatus.Hold, coinWinGiveUp, false, true);
        
            //update games
            SetGridPlayingState(pointGridPlayingState.ResultAction);
            actionCardsPlayManager.setActionCardsPlayingState(actionCardsPlayingState.Waiting, true);
            // territoryPlayManager.setTerritoryPlayingState(territoryPlayingState.None);        
        }
        else
        {
            Debug.Log("fail to update grid game result");
        }
        // Debug.Log("Update game: Player 0 call:" + player0.GetComponent<PlayerManager>().GetPlayerCall() + " Player 1 call:" + player1.GetComponent<PlayerManager>().GetPlayerCall() + " Result:" + currentGridPlayResult);

        //update game UI
        SetPointGridImg(currentGridPlayResult);

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
                // Debug.Log("pointGridPlayingState is None.");
                break;
            case pointGridPlayingState.Selecting:
                // Debug.Log("pointGridPlayingState is Selecting.");
                break;
            case pointGridPlayingState.ResultAction:
                // Debug.Log("pointGridPlayingState is Result Action.");
                break;
            case pointGridPlayingState.Break:
                // Debug.Log("pointGridPlayingState is Break.");
                break;
        }
    }

    // Update the current result of the game
    public void SetGridPlayResult (pointGridPlayResult newResult)
    {
        currentGridPlayResult = newResult;
        SetPointGridImg(currentGridPlayResult);

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

    public void SetPointGridImg(pointGridPlayResult result)
    {
        PointGridUI.GetComponent<UpdatePointGridImg>().SetPointGridImg(result);
    }
}