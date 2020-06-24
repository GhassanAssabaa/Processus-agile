using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

namespace Assests.Scripts
{

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
        [SerializeField] private bool playerOneAuto = false;
        [SerializeField] private bool playerTwoAuto = false;
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
        private Paddle levelOnePaddle;
        private Paddle levelTwoPaddle;

        private void Start()
        {
            gameState = gameObject.GetComponent<GameState>();

            levelOne = Instantiate(levelPrefab, levelPositionPlayerOne, Quaternion.identity);

            if (playerCount == 2)
            {
                levelTwo = Instantiate(levelPrefab, levelPositionPlayerTwo, Quaternion.identity);
            }

            SetCameras();
            GetBlocks();
            GetBalls();
            GetPaddles();
            SetInput();

            if (playerOneAuto)
                SetAutoPaddle(levelOnePaddle, levelOneBall);
            if (playerCount == 2 && playerTwoAuto)
                SetAutoPaddle(levelTwoPaddle, levelTwoBall);
        }

        private void SetInput()
        {
            inputManager = gameObject.GetComponent<InputManager>();

            inputManager.OnPlayerOnePressLeft += levelOnePaddle.OnPressKeyLeft;
            inputManager.OnPlayerOnePressRight += levelOnePaddle.OnPressKeyRight;
            inputManager.OnPressStart += levelOne.GetComponentInChildren<Ball>().OnStartKeyPressed;

            if (playerCount == 2)
            {
                inputManager.OnPlayerTwoPressLeft += levelTwo.GetComponentInChildren<Paddle>().OnPressKeyLeft;
                inputManager.OnPlayerTwoPressRight += levelTwo.GetComponentInChildren<Paddle>().OnPressKeyRight;
                inputManager.OnPressStart += levelTwo.GetComponentInChildren<Ball>().OnStartKeyPressed;
            }
        }

        private void SetCameras()
        {
            cameraOne = levelOne.GetComponentInChildren<Camera>();
            cameraOne.rect = new Rect(0.25f, 0, 0.5f, 1);

            if (playerCount == 1)
            {
                cameraOne.rect = new Rect(0, 0, 1, 1);
            }

            if (playerCount == 2)
            {
                cameraTwo = levelTwo.GetComponentInChildren<Camera>();
                cameraOne.rect = new Rect(0, 0, 0.5f, 1);
                cameraTwo.rect = new Rect(0.5f, 0, 0.5f, 1);
                cameraTwo.GetComponent<AudioListener>().enabled = false;
            }
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

        private void GetPaddles()
        {
            levelOnePaddle = levelOne.GetComponentInChildren<Paddle>();
            levelOnePaddle.GetComponent<AutoPaddle>().ball = levelOneBall;

            if (playerCount == 2)
            {
                levelTwoPaddle = levelTwo.GetComponentInChildren<Paddle>();
                levelTwoPaddle.GetComponent<AutoPaddle>().ball = levelTwoBall;
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
            winPanel.SetActive(true);
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

        private void SetAutoPaddle(Paddle paddle, Ball ball)
        {
            AutoPaddle autoPaddle = paddle.gameObject.GetComponent<AutoPaddle>();
            autoPaddle.ball = ball;
            autoPaddle.moveLeft += paddle.OnPressKeyLeft;
            autoPaddle.moveRight += paddle.OnPressKeyRight;
        }
    }
}
