using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private int player;
    private float speed = 2;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveInput();
        Move();
        if (player == 1)
        {
            if (Input.GetButtonDown("p1Rotate"))
            {
                Rotate();
            }
            if (Input.GetButtonDown("p1Action"))
            {
                Action();
            }
        }
        if (player == 2)
        {
            if (Input.GetButtonDown("p2Rotate"))
            {
                Rotate();
            }
            if (Input.GetButtonDown("p2Action"))
            {
                Action();
            }
        }
        if (player == 3)
        {
            if (Input.GetButtonDown("p3Rotate"))
            {
                Rotate();
            }
            if (Input.GetButtonDown("p3Action"))
            {
                Action();
            }
        }
        if (player == 4) { 
            if (Input.GetButtonDown("p4Rotate"))
            {
                 Rotate();
            }
            if (Input.GetButtonDown("p4Action"))
            {
                Action();
            }
        }
    }

    //move
    public void Move()
    {
        if (direction.sqrMagnitude > 1)
        {
            direction = direction.normalized;
        }
        transform.Translate(direction * speed * Time.deltaTime);
    }

    //input for moving
    private void GetMoveInput()
    {
        direction = Vector2.zero;
        //for player 1
        if (player == 1)
        {
            if (Input.GetButton("p1Up"))
            {
                direction += Vector2.up;
            }
            if (Input.GetButton("p1Right"))
            {
                direction += Vector2.right;
            }
            if (Input.GetButton("p1Left"))
            {
                direction += Vector2.left;
            }
            if (Input.GetButton("p1Down"))
            {
                direction += Vector2.down;
            }
        }

        //player 2
        if (player == 2)
        {
            if (Input.GetButton("p2Up"))
            {
                direction += Vector2.up;
            }
            if (Input.GetButton("p2Right"))
            {
                direction += Vector2.right;
            }
            if (Input.GetButton("p2Left"))
            {
                direction += Vector2.left;
            }
            if (Input.GetButton("p2Down"))
            {
                direction += Vector2.down;
            }
          
        }

        //player 3
        if (player == 3)
        {
            if (Input.GetButton("p3Up"))
            {
                direction += Vector2.up;
            }
            if (Input.GetButton("p3Right"))
            {
                direction += Vector2.right;
            }
            if (Input.GetButton("p3Left"))
            {
                direction += Vector2.left;
            }
            if (Input.GetButton("p3Down"))
            {
                direction += Vector2.down;
            }
        }

        //player 4
        if (player == 4)
        {
            if (Input.GetButton("p4Up"))
            {
                direction += Vector2.up;
            }
            if (Input.GetButton("p4Right"))
            {
                direction += Vector2.right;
            }
            if (Input.GetButton("p4Left"))
            {
                direction += Vector2.left;
            }
            if (Input.GetButton("p4Down"))
            {
                direction += Vector2.down;
            }
           
        }
    }
    
    //rotate camera
    public void Rotate()
    {

    }

    //interaction
    public void Action()
    {

    }
}
