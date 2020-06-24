using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assests.Scripts
{

    public class InputManager : MonoBehaviour
    {
        public event EventHandler OnPlayerOnePressLeft;
        public event EventHandler OnPlayerOnePressRight;
        public event EventHandler OnPlayerTwoPressLeft;
        public event EventHandler OnPlayerTwoPressRight;
        public event EventHandler OnPressStart;

        [SerializeField] private KeyCode playerOneLeftKey = KeyCode.LeftArrow;
        [SerializeField] private KeyCode playerOneRightKey = KeyCode.RightArrow;
        [SerializeField] private KeyCode playerTwoLeftKey = KeyCode.A;
        [SerializeField] private KeyCode playerTwoRightKey = KeyCode.D;
        [SerializeField] private KeyCode startKey = KeyCode.Space;

        private void Update()
        {
            // Player start input
            if (Input.GetKey(startKey))
            {
                OnPressStart?.Invoke(this, EventArgs.Empty);
            }

            // Player one input
            if (Input.GetKey(playerOneLeftKey))
            {
                OnPlayerOnePressLeft?.Invoke(this, EventArgs.Empty);
            }
            else if (Input.GetKey(playerOneRightKey))
            {
                OnPlayerOnePressRight?.Invoke(this, EventArgs.Empty);
            }

            // Player two input
            if (Input.GetKey(playerTwoLeftKey))
            {
                OnPlayerTwoPressLeft?.Invoke(this, EventArgs.Empty);
            }
            else if (Input.GetKey(playerTwoRightKey))
            {
                OnPlayerTwoPressRight?.Invoke(this, EventArgs.Empty);
            }
        }

    }

}
