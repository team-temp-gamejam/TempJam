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
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private AudioSource footStep, lockerShake, pickUp, wink;

    [SerializeField]
    private Camera playerCam;

    public bool haveLock;
    public bool compassCollected;
    public bool canLeaveCupboard;
    private bool stepping;
    private bool inCupboard;

    public GameObject soundVisual;

    public int orientation = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        stepping = false;
        compassCollected = false;
        canLeaveCupboard = true;
    }

    //need to set currentRoom once at spawning player
    public void SetCurrentRoom(Vector2 position)
    {
        currentRoom = position;
    }

    public Vector2 GetCurrentRoom()
    {
        return currentRoom;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, orientation);
        GetMoveInput();
        Move();
        if(inCupboard&& Input.GetButtonDown("p" + player + "Action"))
        {
            inCupboard = false;
            GetComponent<SpriteRenderer>().enabled = true;
        }
        if (stepping)
        {
            StartCoroutine("footStepSound");
        }
        else StopAllCoroutines();
        if (Input.GetButtonDown("p" + player + "Rotate"))
        {
            Rotate();
        }
        stepping = false;
        if (inCupboard)
        {
            for (int i = 1; i < 5; i++)
            {
                playerCam.cullingMask &= ~(1 << LayerMask.NameToLayer("p" + i + "Light"));
            }
            GetComponent<SpriteRenderer>().enabled = false;
            playerCam.cullingMask |= 1 << LayerMask.NameToLayer("p" + player + "Hide");
        }
        /*
        else
        {
            StopCoroutine("footStepSound");
        }
        */
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

        anim.SetBool("isWalk", false);
        direction = Vector2.zero;
        if (!inCupboard)
        {
            if (Input.GetButton("p" + player + "Up"))
            {
                direction += Vector2.up;
                stepping = true;
                anim.SetBool("isWalk", true);
                anim.SetBool("FaceUp", true);

                anim.SetBool("FaceDown", false);
                anim.SetBool("FaceLeft", false);
                anim.SetBool("FaceRight", false);
            }
            if (Input.GetButton("p" + player + "Right"))
            {
                direction += Vector2.right;
                stepping = true;
                anim.SetBool("isWalk", true);
                anim.SetBool("FaceRight", true);

                anim.SetBool("FaceUp", false);
                anim.SetBool("FaceDown", false);
                anim.SetBool("FaceLeft", false);
            }
            if (Input.GetButton("p" + player + "Left"))
            {
                direction += Vector2.left;
                stepping = true;
                anim.SetBool("isWalk", true);
                anim.SetBool("FaceLeft", true);

                anim.SetBool("FaceUp", false);
                anim.SetBool("FaceDown", false);
                anim.SetBool("FaceRight", false);
            }
            if (Input.GetButton("p" + player + "Down"))
            {
                direction += Vector2.down;
                stepping = true;
                anim.SetBool("isWalk", true);
                anim.SetBool("FaceDown", true);

                anim.SetBool("FaceUp", false);
                anim.SetBool("FaceLeft", false);
                anim.SetBool("FaceRight", false);
            }
        }
    }

    //rotate camera
    public void Rotate()
    {
        //orientation = (orientation + 90) % 360;
    }

    IEnumerator footStepSound()
    {
        while (!footStep.isPlaying)
        {
            footStep.Play();
        }
        yield return 0;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetButtonDown("p" + player + "Action"))
        {
            if (col.gameObject.tag == "Interactable")
            {
                if (col.parent.gameObject.tag == "Door") {
                    if (!haveLock) return;
                    
                }
                col.gameObject.GetComponent<InteractItem>().Interact();
            }

            

        }
    }

    public void SetPlayer(int player)
    {
        this.player = player;
    }

    public void hiding()
    {
        Debug.Log("Hiding");
        inCupboard = true;
        
        
    }

    public void Captured()
    {
        Debug.Log("captured!!");
    }

    public void Die()
    {
        Debug.Log("captured!!");
    }

}
