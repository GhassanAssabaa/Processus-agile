using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class GameManager : MonoBehaviour
{
    [Range(1, 2)]
    [SerializeField]
    private int playerCount;

    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private GameObject winPanel;
    private InputManager inputManager;
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
        
        if (levelOneBlocks.Count == 0)
        {
            Debug.Log("Player One Won!");
            EndScreen(1);
        }
        
        if (playerCount == 2 && levelTwoBlocks.Count == 0)
        {
            Debug.Log("Player Two Won!");
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
        if ((Ball)o == levelOneBall)
        {
            Debug.Log("player one lost");
            EndScreen(2);
        }
        else if ((Ball)o == levelTwoBall)
        {
            Debug.Log("player two lost");
            EndScreen(1);
        }
    }

    private void EndScreen(int i)
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }

}
