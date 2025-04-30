using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> player1Troops, player2Troops;    // le truppe del primo e del secondo player
    private byte playerID;            // serve a capire di chi sarà il prossimo turno (viene passato alla funzione SetUpNextPlayerAction)

    private byte MoveActions { get; set; }
    private byte AttackActions { get; set; }
    public enum GameState { CoinFlip, Placement, Draw, PlayerAction, Victory }
    private GameState CurrentState { get; set; }

    public GameObject prefabWarrior;

    public GameObject prefabArcher;

    public GameObject prefabWizard;

    public Troop troop;

    private PlayerMove activePlayerMove;

    //funzioni di Unity
    void Start()
    {
        CurrentState = GameState.CoinFlip;
        playerID = 2;
    }

    private void Update()
    {
        StateUpdate();
    }


    //funzioni custom
    public void ChangeState(GameState newState)
    {
        ExitState();
        CurrentState = newState;
        EnterState();
    }

    private void ExitState()
    {

    }

    private void EnterState()
    {
        switch (CurrentState) 
        {
            case GameState.PlayerAction:
                SetUpNextPlayerAction();
                break;
        }
    }

    private void StateUpdate()
    {
        switch (CurrentState)
        {
            case GameState.CoinFlip:
                break;

            case GameState.Placement:
                Debug.Log("ciao Diego");
                break;

            case GameState.Draw:
                Debug.Log("Rob, scelgo te!");
                break;

            case GameState.PlayerAction:
                if (CheckTurnPass())
                {
                    if (playerID == 1)          // determina a chi bisogna passare il turno
                        playerID++;
                    else
                        playerID--;

                    ChangeState(GameState.Draw);
                }
                TroopSelectionRaycast();
                break;

            case GameState.Victory:
                Debug.Log("Bella per Filo");
                break;
        }
    }

    private bool CheckTurnPass()                        // è nell'update perciò potrebbe consumare di più di se fosse fatto con eventi
    {
        if (MoveActions == 0 && AttackActions == 0)     
        {
            Debug.Log("Cambio turno");
            return true;
        }
        return false;
    }

    private void SetUpNextPlayerAction()
    {
        MoveActions = 3;
        AttackActions = 3;
        switch (playerID)
        {
            case 1:
                Debug.Log("yipee");
                HandleTroopActivation(player2Troops, player1Troops);
                break;

            case 2:
                HandleTroopActivation(player1Troops, player2Troops);
                break;
        }
    }

    private void HandleTroopActivation(List<GameObject> deactivatedTroops, List<GameObject> activatedTroops)
    {
        foreach (GameObject troop in deactivatedTroops)
        {
            PlayerMove troopMove = troop.GetComponent<PlayerMove>();
            troopMove.isInTurn = false;
        }

        foreach (GameObject troop in activatedTroops)
        {
            PlayerMove troopMove = troop.GetComponent<PlayerMove>();
            troopMove.isInTurn = true;
        }
    }

    GameObject hitCharacter;
    private void TroopSelectionRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        LayerMask characterMask = LayerMask.GetMask("Character");

        // Controlla il click e vede se colpisce un Character
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 1000f, characterMask))
        {
            Debug.Log("Personaggio colpito");
            // qua possiamo inserire il display delle statistiche del personaggio selezionato

            hitCharacter = hit.transform.gameObject;
            PlayerMove charactersel = hitCharacter.GetComponent<PlayerMove>();

            if (charactersel.isInTurn)      // se il personaggio appartiene ai personaggi del giocatore corrente
            {
                Debug.Log("Personaggio correttamente selezionato");
                charactersel.isSelected = true;
            }
        }
        if (hitCharacter != null)    // per deselezionare il personaggio
        {
            PlayerMove charactersel = hitCharacter.GetComponent<PlayerMove>();
            charactersel.isSelected = false;
            hitCharacter = null;
        }
    }

    public void DeployTroop(int IdTroop)
    {
        switch (IdTroop)
        {
            case 0:
                DeployWarrior();
                break;
            case 1:
                DeployArcher();
                break;
            case 2:
                DeployWizard();
                break;
            default:
                break;
        }
    }

    public void DeployWarrior()
    {
        GameObject newPlayer = Instantiate(prefabWarrior, Vector3.zero, Quaternion.identity);
        activePlayerMove = newPlayer.GetComponent<PlayerMove>();

        //player1Troops.Add(newPlayer);                                  solo per test, da cambiare successivamente

        foreach (GridNode node in FindObjectsOfType<GridNode>())
        {
            node.SetPlayer(activePlayerMove);
        }
    }

    public void DeployArcher()
    {
        GameObject newPlayer = Instantiate(prefabArcher, Vector3.zero, Quaternion.identity);
        activePlayerMove = newPlayer.GetComponent<PlayerMove>();

        //player1Troops.Add(newPlayer);

        foreach (GridNode node in FindObjectsOfType<GridNode>())
        {
            node.SetPlayer(activePlayerMove);
        }
    }

    public void DeployWizard()
    {
        GameObject newPlayer = Instantiate(prefabWizard, Vector3.zero, Quaternion.identity);
        activePlayerMove = newPlayer.GetComponent<PlayerMove>();

        //player1Troops.Add(newPlayer);

        foreach (GridNode node in FindObjectsOfType<GridNode>())
        {
            node.SetPlayer(activePlayerMove);
        }
    }
}
