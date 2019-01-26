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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
