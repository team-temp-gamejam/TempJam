using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private int player;
    private float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //for player 1
        if (player == 1)
        {
            if (Input.GetButtonDown("p1Up"))
            {
                UpMove();
            }
            if (Input.GetButtonDown("p1Right"))
            {
                RightMove();
            }
            if (Input.GetButtonDown("p1Left"))
            {
                LeftMove();
            }
            if (Input.GetButtonDown("p1Down"))
            {
                DownMove();
            }
            if (Input.GetButtonDown("p1Rotate"))
            {
                Rotate();
            }
            if (Input.GetButtonDown("p1Action"))
            {
                Action();
            }
        }

        //player 2
        if (player == 2)
        {
            if (Input.GetButtonDown("p2Up"))
            {
                UpMove();
            }
            if (Input.GetButtonDown("p2Right"))
            {
                RightMove();
            }
            if (Input.GetButtonDown("p2Left"))
            {
                LeftMove();
            }
            if (Input.GetButtonDown("p2Down"))
            {
                DownMove();
            }
            if (Input.GetButtonDown("p2Rotate"))
            {
                Rotate();
            }
            if (Input.GetButtonDown("p2Action"))
            {
                Action();
            }
        }

        //player 3
        if (player == 3)
        {
            if (Input.GetButtonDown("p3Up"))
            {
                UpMove();
            }
            if (Input.GetButtonDown("p3Right"))
            {
                RightMove();
            }
            if (Input.GetButtonDown("p3Left"))
            {
                LeftMove();
            }
            if (Input.GetButtonDown("p3Down"))
            {
                DownMove();
            }
            if (Input.GetButtonDown("p3Rotate"))
            {
                Rotate();
            }
            if (Input.GetButtonDown("p3Action"))
            {
                Action();
            }
        }

        //player 4
        if (player == 4)
        {
            if (Input.GetButtonDown("p4Up"))
            {
                UpMove();
            }
            if (Input.GetButtonDown("p4Right"))
            {
                RightMove();
            }
            if (Input.GetButtonDown("p4Left"))
            {
                LeftMove();
            }
            if (Input.GetButtonDown("p4Down"))
            {
                DownMove();
            }
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

    //move up
    public void UpMove()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    //move down
    public void DownMove()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    //move left
    public void LeftMove()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    //move right
    public void RightMove()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
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
