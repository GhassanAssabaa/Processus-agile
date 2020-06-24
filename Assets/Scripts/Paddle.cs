using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

namespace Assests.Scripts
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        private Rigidbody2D rb;


        private void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }

        public void OnPressKeyLeft(object o, EventArgs e)
        {
            Vector3 direction = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.position = direction;
            /*
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, direction.magnitude))
                rb.MovePosition(direction);
            else
                rb.MovePosition(hit.point);
            */
        }

        public void OnPressKeyRight(object o, EventArgs e)
        {
            Vector3 direction = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.position = direction;
            /*
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, direction.magnitude))
                rb.MovePosition(direction);
            else
                rb.MovePosition(hit.point);
            */
        }
    }
}