using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private int player;
    [SerializeField]
    private float speed = 2;

    private Vector2 direction;
    [SerializeField]
    private Vector2 currentRoom;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //need to set currentRoom once at spawning player
    public void SetCurrentRoom(Vector2 position) {
        currentRoom = position;
    }

    public Vector2 GetCurrentRoom() {
        return currentRoom;
    }

    // Update is called once per frame
    void Update()
    {
        
        GetMoveInput();
        Move();
        if (Input.GetButtonDown("p"+player+"Rotate"))
        {
            Rotate();
        }
        if (Input.GetButtonDown("p"+player+"Action"))
        {
            Action();
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
        
        anim.SetBool("isWalk" , false);
        direction = Vector2.zero;

         if (Input.GetButton("p"+player+"Up"))
        {
            direction += Vector2.up;
            anim.SetBool("isWalk" , true);
            anim.SetBool("FaceUp" , true);
            
            anim.SetBool("FaceDown" , false);
            anim.SetBool("FaceLeft" , false);
            anim.SetBool("FaceRight" , false);
        }
        if (Input.GetButton("p"+player+"Right"))
        {
            direction += Vector2.right;
            anim.SetBool("isWalk" , true);
            anim.SetBool("FaceRight" , true);
            
            anim.SetBool("FaceUp" , false);
            anim.SetBool("FaceDown" , false);
            anim.SetBool("FaceLeft" , false);
        }
        if (Input.GetButton("p"+player+"Left"))
        {
            direction += Vector2.left;
            anim.SetBool("isWalk" , true);
            anim.SetBool("FaceLeft" , true);
            
            anim.SetBool("FaceUp" , false);
            anim.SetBool("FaceDown" , false);
            anim.SetBool("FaceRight" , false);
        }
        if (Input.GetButton("p"+player+"Down"))
        {
            direction += Vector2.down;
            anim.SetBool("isWalk" , true);
            anim.SetBool("FaceDown" , true);
            
            anim.SetBool("FaceUp" , false);
            anim.SetBool("FaceLeft" , false);
            anim.SetBool("FaceRight" , false);
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


    private void OnTriggerStay2D(Collider2D col) {
        if (Input.GetButtonDown("p"+player+"Action")) {
            if (col.gameObject.tag == "Interactable")     
                col.gameObject.GetComponent<InteractItem>().Interact();
        }
    }
}
