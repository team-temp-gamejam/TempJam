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

    public Timer timer;

    public bool haveLock;
    public bool compassCollected;
    public bool canLeaveCupboard;
    private bool stepping;
    public bool inCupboard;

    public GameObject soundVisual;
    public GameObject lockSprite;
    public GameObject compassSprite;

    public GameObject interactingObject;
    private GameObject cupboardHiding;

    // public MapManager mapManager;

    public int orientation = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        stepping = false;
        compassCollected = false;
        canLeaveCupboard = true;
        timer = GameObject.Find("Timer").GetComponent<Timer>();
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

        if (inCupboard && Input.GetButtonDown("p" + player + "Action"))
        {
            inCupboard = false;
            GetComponent<SpriteRenderer>().enabled = true;
            for (int i = 1; i < 5; i++)
            {
                playerCam.cullingMask |= (1 << LayerMask.NameToLayer("p" + i + "Light"));
            }
            playerCam.cullingMask &= ~(1 << LayerMask.NameToLayer("p" + player + "Hide"));
            cupboardHiding.GetComponent<Cupboard>().SetOpen(true, this);
            transform.position = cupboardHiding.transform.GetChild(0).position;

            if (anim.GetBool("FaceUp")) {
                anim.SetBool("FaceUp", false);
                anim.SetBool("FaceDown", true);
            } else 
            if (anim.GetBool("FaceDown")) {
                anim.SetBool("FaceDown", false);
                anim.SetBool("FaceUp", true);
            } else 
            if (anim.GetBool("FaceRight")) {
                anim.SetBool("FaceRight", false);
                anim.SetBool("FaceLeft", true);
            } else 
            if (anim.GetBool("FaceLeft")) {
                anim.SetBool("FaceLeft", false);
                anim.SetBool("FaceRight", true);
            }

        }

        if (stepping)
        {
            PlayFootStepSound();
        } else {
            footStep.Stop();
        }

        if (Input.GetButtonDown("p" + player + "Rotate"))
        {
            Rotate();
        }

        stepping = false;
        
        if (Input.GetButtonDown("p" + player + "Action") && interactingObject != null)
        {
            if (interactingObject.gameObject.tag == "Interactable")
            {
                if (interactingObject.transform.parent.gameObject.tag == "Door") {
                    bool Doorlock = interactingObject.transform.parent.gameObject.GetComponent<DoorScript>().isLock;
                    
                    if (Doorlock) {
                        if (haveLock) return;
                        else haveLock = true;
                    } else {
                        if (!haveLock) return;
                        else haveLock = false;
                    }
                    
                }
                interactingObject.gameObject.GetComponent<InteractItem>().Interact();
                lockSprite.SetActive(haveLock);
                compassSprite.SetActive(compassCollected);
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

    // IEnumerator footStepSound()
    // {
    //     while (!footStep.isPlaying)
    //     {
    //         footStep.Play();
    //         EventRelay.Notify(transform.position, 1);
    //     }
    //     yield return 0;
    // }

    private void PlayFootStepSound() 
    {
        if (!footStep.isPlaying)
        {
            footStep.Play();
            EventRelay.Notify(transform.position, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        interactingObject = col.gameObject;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == interactingObject) {
            interactingObject = null;
        }
    }

    public void SetPlayer(int player)
    {
        this.player = player;
    }

    public void SetCam(Camera cam) {
        playerCam = cam;
    }

    public void hiding(GameObject cupboard)
    {
        Debug.Log("Hiding");
        inCupboard = true;
        for (int i = 1; i < 5; i++)
        {
            playerCam.cullingMask &= ~(1 << LayerMask.NameToLayer("p" + i + "Light"));
        }
        GetComponent<SpriteRenderer>().enabled = false;
        playerCam.cullingMask |= 1 << LayerMask.NameToLayer("p" + player + "Hide");
        cupboardHiding = cupboard;

    }

    public void Captured()
    {
        Debug.Log("captured!!");
        timer.timeLeft -= 100;
        /*
        bool capture = true;
        while (capture)
        {
            int i = Random.Range(0,mapManager.cupboards.Count-1);
            if (mapManager.cupboards[i].GetComponent<Cupboard>().Vacant())
            {
                mapManager.cupboards[i].GetComponent<Cupboard>().GetComponent<BoxCollider2D>().isTrigger = false;
                hiding();
                mapManager.cupboards[i].GetComponent<Cupboard>().isOpen = false;
                mapManager.cupboards[i].GetComponent<Cupboard>().insideThing = this.GetComponent<GameObject>();
                transform.position = mapManager.cupboards[i].GetComponent<Cupboard>().transform.position;
                SetCurrentRoom(new Vector2((transform.position.x+3.55f)/7.1f,(transform.position.y + 3.55f) / 7.1f));
                capture = false;
            }
            
        }
        */
    }

    public void Die()
    {
        Debug.Log("captured!!");
    }

}
