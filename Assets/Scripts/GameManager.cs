using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(InputManager), typeof(GameState))]
public class GameManager : MonoBehaviour
{
    [Range(1, 2)]
    //[SerializeField]
    public static int playerCount;
    public static User currentUser;

    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI winPanelText;
    private InputManager inputManager;
    private GameState gameState;
    private List<BreakableBlock> levelOneBlocks;
    private List<BreakableBlock> levelTwoBlocks;
    private Vector3 levelPositionPlayerOne = new Vector3(0, 0, 0);
    private Vector3 levelPositionPlayerTwo = new Vector3(10, 0, 0);
    private GameObject levelOne;
    private GameObject levelTwo;
    private Camera cameraOne;
    private Camera cameraTwo;
    private Ball levelOneBall;
    private Ball levelTwoBall;

    private void Start()
    {
        inputManager = gameObject.GetComponent<InputManager>();
        gameState = gameObject.GetComponent<GameState>();
        
        levelOne = Instantiate(levelPrefab, levelPositionPlayerOne, Quaternion.identity);
        inputManager.OnPlayerOnePressLeft += levelOne.GetComponentInChildren<Paddle>().OnPressKeyLeft;
        inputManager.OnPlayerOnePressRight += levelOne.GetComponentInChildren<Paddle>().OnPressKeyRight;
        inputManager.OnPressStart += levelOne.GetComponentInChildren<Ball>().OnStartKeyPressed;


        if (playerCount == 2)
        {
            levelTwo = Instantiate(levelPrefab, levelPositionPlayerTwo, Quaternion.identity);

            inputManager.OnPlayerTwoPressLeft += levelTwo.GetComponentInChildren<Paddle>().OnPressKeyLeft;
            inputManager.OnPlayerTwoPressRight += levelTwo.GetComponentInChildren<Paddle>().OnPressKeyRight;
            inputManager.OnPressStart += levelTwo.GetComponentInChildren<Ball>().OnStartKeyPressed;

            cameraOne = levelOne.GetComponentInChildren<Camera>();
            cameraTwo = levelTwo.GetComponentInChildren<Camera>();

            cameraOne.rect = new Rect(0, 0, 0.5f, 1);
            cameraTwo.rect = new Rect(0.5f, 0, 0.5f, 1);

            cameraTwo.GetComponent<AudioListener>().enabled = false;
        }

        GetBlocks();
        GetBalls();
    }

    public void CountBlocks()
    {
        //Debug.Log("Recounting Blocks");
        GetBlocks();

        if (playerCount == 1)
        {
            if (levelOneBlocks.Count == 0)
                EndScreen(currentUser, true);
        }
        else if (playerCount == 2)
        {
            if (levelOneBlocks.Count == 0)
                EndScreen(1);
            else if (levelTwoBlocks.Count == 0)
                EndScreen(2);
        }
    }

    private void GetBlocks()
    {
        levelOneBlocks = levelOne.GetComponentsInChildren<BreakableBlock>().ToList<BreakableBlock>();

        if (playerCount == 2)
        {
            levelTwoBlocks = levelTwo.GetComponentsInChildren<BreakableBlock>().ToList<BreakableBlock>();
        }
    }

    private void GetBalls() 
    {
        levelOneBall = levelOne.GetComponentInChildren<Ball>();
        levelOneBall.OnHitBottomCollider += OnPlayerHitBottomCollider;

        if (playerCount == 2)
        {
            levelTwoBall = levelTwo.GetComponentInChildren<Ball>();
            levelTwoBall.OnHitBottomCollider += OnPlayerHitBottomCollider;
        }
    }

    public void OnPlayerHitBottomCollider(object o, EventArgs e)
    {
        if (playerCount == 1)
        {
            EndScreen(currentUser, false);
        }
        else if (playerCount == 2)
        {
            if ((Ball)o == levelOneBall)
                EndScreen(2);
            else if ((Ball)o == levelTwoBall)
                EndScreen(1);
        }
       
    }

    private void EndScreen(int winner)
    {
        gameState.Pause();
        if (winner == 1)
            winPanelText.SetText("Player one won!");
        else if (winner == 2)
            winPanelText.SetText("Player two won!");
    }

    private void EndScreen(User player, bool won)
    {
        gameState.Pause();
        if (player == null)
        {
            if (won)
                winPanelText.SetText("You won!");
            else
                winPanelText.SetText("You lost!");
        }
        else
        {
            if (won)
                winPanelText.SetText(player.userName + " won!");
            else
                winPanelText.SetText(player.userName + " lost!");
        }
        winPanel.SetActive(true);
    }


}
