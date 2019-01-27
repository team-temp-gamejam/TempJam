using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public Generator mapGenerator;
    public GhostMovement ghostMovement;

    private List<List<Room>> map;

    // Start is called before the first frame update
    void Start()
    {
        mapGenerator.Generate();
        map = mapGenerator.map;
        ghostMovement.SetMap(map);
        // ghostMovement.SetDestination(ghostMovement.destination);
        /*ghostMovement.isChasing = true;
        ghostMovement.target = new Vector2(-2, -2);*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.A))
        {
            ghostMovement.isChasing = !ghostMovement.isChasing;
        }*/
    }


}
