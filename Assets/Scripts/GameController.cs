using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameStates {
    Win,
    Loose,
}

public class GameController : MonoBehaviour
{
    public GameObject Hero;
    public float TurnDuration = 4.0f;
    public UIController Interface;
    public CameraController Camera;

    private List<GameObject> CharactersInGame;
    private List<GameObject> Positions;
    private int CurrentTurn = 0;
    private int MaxTurns;
    private float WaitSystem;
    private GameObject[] Enemies;
    private bool activeGame = true;
    private AudioSource audio;
    
    void Awake() {
        audio = GetComponent<AudioSource>();
        CharactersInGame = new List<GameObject>();
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        CharactersInGame.Add(Hero);
        for (int i = 0; i < Enemies.Length; i++) {
            AddEnemyToList(Enemies[i]);
        }
        MaxTurns = CharactersInGame.Count;
    }

    void Start()
    {
        AssignDelegates();
        // One turn for each character in game
        NextTurn(CurrentTurn);
    }

    private void AssignDelegates()
    {
        // Assign delegate function, one for each character
        for (int i = 0; i < MaxTurns; i++) {
            var controller = CharactersInGame[i].GetComponent<PlayerController>();
            controller.OnStopTime += StopWaiting;
            controller.OnKill += UpdateCharactersList;
            controller.OnLoose += LooseGame;
            controller.OnZoomOut += ZoomCamera;
        }
    } 

    // recursively move character turn from one to another
    private void NextTurn(int currentTurn)
    {
        if(activeGame)
        {
            // first we check if there is a winner
            CheckIfWinner();
            CurrentTurn = currentTurn;
            var currentCharacterTurn = CharactersInGame[currentTurn];
            var currentCharacterController = currentCharacterTurn.GetComponent<PlayerController>();
            // Give turn
            currentCharacterController.HasTurn = true;
            // Move camera to the current turn position
            Camera.MoveCameraTo(currentCharacterTurn.transform);
            // If turn is for Hero allow camera to follow him
            if(currentTurn == 0)
                Camera.followTarget = true;
            else 
                Camera.followTarget = false;

            StartCoroutine(FinishTurn(currentCharacterController));
        }
    }

    IEnumerator FinishTurn(PlayerController currentCharacterController)
    {
        WaitSystem = TurnDuration;
        while ( WaitSystem > 0 )
        {
            // wait real seconds;
            Interface.UpdateCounterBack(WaitSystem);
            WaitSystem -= Time.deltaTime;
            yield return null;
        }

        // Stop turn
        currentCharacterController.HasTurn = false;
        var next = CurrentTurn < MaxTurns - 1 ? CurrentTurn + 1 : 0;
        // TODO when there is a winner no next turn
        //Move to next turn
        NextTurn(next); 
    }

    private void CheckIfWinner(){
        // Only one character remain in game and is the Hero
        if(CharactersInGame.Count == 1 && CharactersInGame[0] == Hero)
            WinGame();
    }

    // Stop turns as the hero has been killed
    public void LooseGame()
    {
        var loseClip = Resources.Load<AudioClip>("Sounds/lose");
        audio.loop = false;
        audio.PlayOneShot(loseClip);
        FinishGame(GameStates.Loose);
    }

    private void WinGame()
    {
        var winClip = Resources.Load<AudioClip>("Sounds/win");
        audio.loop = false;
        audio.PlayOneShot(winClip);
        FinishGame(GameStates.Win);
    }
    
    private void FinishGame(GameStates state)
    {
        activeGame = false;
        if(state == GameStates.Win)
            Interface.ShowResult("You win");
        if(state == GameStates.Loose)
            Interface.ShowResult("You Loose");
    }

    // Remove character from the list when is dead
    // Update game turns
    public void UpdateCharactersList(GameObject character)
    {
        CharactersInGame.Remove(character);
        MaxTurns = CharactersInGame.Count;
    }

    public void StopWaiting()
    {
        WaitSystem = 0;
    }

    public void ZoomCamera()
    {
        Camera.ZoomOut();
    }

    // Instanciate enemy andd added to the list of characters in Game
    // Enemy appears on different appear points
    private void AddEnemyToList(GameObject EnemyInGame)
    {
        CharactersInGame.Add(EnemyInGame);
    }
}
