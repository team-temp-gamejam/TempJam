using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    private MapManager mapManager;

    public float speed;
    public float attackRange;
    private Vector2Int destination;
    private List<int> direction = new List<int>();
    private float currentScore;
    private int mode; // 0 = find, 1 = chase

    private NavMeshAgent agent;
    private Transform nav;

    private List<List<float>> score;
    private List<List<Room>> map;
    private Vector2Int position;
    private GameObject room;
    private Vector2[] doorPositions = new Vector2[4];

    // Start is called before the first frame update
    void Start()
    {
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        GameObject target = GetClosestTargetInRoom();
        if (target == null)
        {
            mode = 0;
        }
        else
        {
            mode = 1;
        }

        if (mode == 0)
        {
            Find();
        }
        else
        {
            Chase(target);
            Attack(target);
        }
    }

    void Attack(GameObject target)
    {
        Vector3 distance = target.transform.position - transform.position;
        distance.z = 0;
        Debug.Log(distance.magnitude);
        if (distance.magnitude < attackRange)
        {
            target.GetComponent<PlayerControl>().Captured();
        }
    }

    void Find()
    {
        if (direction.Count > 0)
        {
            agent.SetDestination(RoomToNav(doorPositions[direction[0]]));
            // Debug.Log(RoomToNav(doorPositions[direction[0]]));
            transform.position = NavToRoom(agent.transform.position);
        }
        else
        {
            SetDestination(findBestDestination());
        }
    }

    void Chase(GameObject target)
    {
        agent.SetDestination(RoomToNav(target.transform.position));
        // Debug.Log(RoomToNav(doorPositions[direction[0]]));
        transform.position = NavToRoom(agent.transform.position);
    }

    GameObject GetClosestTargetInRoom()
    {
        GameObject result = null;
        float distance = 100;
        foreach (GameObject target in mapManager.players)
        {
            Vector2 targetPositionFloat = target.GetComponent<PlayerControl>().GetCurrentRoom();
            Vector2Int targetPosition = new Vector2Int((int)targetPositionFloat.x, (int)targetPositionFloat.y);
            if (targetPosition != position || target.GetComponent<PlayerControl>().inCupboard)
            {
                continue;
            }
            if ((target.transform.position - transform.position).magnitude < distance)
            {
                distance = (target.transform.position - transform.position).magnitude;
                result = target;
            }
        }
        return result;
    }

    public void SetMap(List<List<Room>> map, Vector2Int position)
    {
        this.position = position;
        this.map = map;
        room = map[position.x][position.y].interior;

        score = new List<List<float>>();
        for (int i = 0; i < map.Count; i++)
        {
            score.Add(new List<float>());
            for (int j = 0; j < map[0].Count; j++)
            {
                score[i].Add(1);
            }
        }

        GetDoors();
        SetNavAgent();
    }

    public void SetNavAgent()
    {
        if (nav != null)
        {
            Destroy(nav.gameObject);
        }
        nav = Instantiate(room.GetComponent<Navigator>().navigator).transform;
        NavMeshSurface navMeshSurface = nav.GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
        agent = nav.GetComponentInChildren<NavMeshAgent>();
        agent.speed = speed;
        agent.Warp(RoomToNav(transform.position));
    }

    private Vector3 RoomToNav(Vector3 pos)
    {
        // Vector3 vector = Quaternion.Euler(0, 0, -90 * (map[position.x][position.y].type % 4)) * (pos - room.transform.position);
        Vector3 vector = Quaternion.Euler(0, 0, -room.transform.rotation.eulerAngles.z) * (pos - room.transform.position);
        Vector3 navRoot = nav.transform.position;
        return new Vector3(vector.x + navRoot.x, 0, vector.y + navRoot.z);
    }

    private Vector3 NavToRoom(Vector3 pos)
    {
        // Vector3 vector = Quaternion.Euler(0, -90 * (map[position.x][position.y].type % 4), 0) * (pos - nav.transform.position);
        Vector3 vector = Quaternion.Euler(0, -room.transform.rotation.eulerAngles.z, 0) * (pos - nav.transform.position);
        Vector3 roomRoot = room.transform.position;
        return new Vector3(vector.x + roomRoot.x, vector.z + roomRoot.y, roomRoot.z);
    }

    public void CrossRoom(Vector3 warpPosition)
    {
        Vector2Int displacement = new Vector2Int();
        switch (direction[0])
        {
            case 0:
                displacement = new Vector2Int(0, 1);
                break;
            case 1:
                displacement = new Vector2Int(1, 0);
                break;
            case 2:
                displacement = new Vector2Int(0, -1);
                break;
            case 3:
                displacement = new Vector2Int(-1, 0);
                break;
            default:
                break;
        }
        direction.RemoveAt(0);
        transform.position = warpPosition;
        // transform.position += new Vector3(displacement.x * 4, displacement.y * 4);

        position += displacement;
        room = map[position.x][position.y].interior;
        updateScore();
        GetDoors();
        SetNavAgent();
    }

    void GetDoors()
    {
        DoorScript[] doors = room.GetComponentsInChildren<DoorScript>();
        Room roomMap = map[position.x][position.y];

        Vector2 doorPos = doors[0].transform.position;
        if (roomMap.upActive)
        {
            foreach (DoorScript door in doors)
            {
                if (door.transform.position.y > doorPos.y)
                {
                    doorPos = door.transform.position;
                }
            }
            doorPositions[0] = doorPos;
        }
        if (roomMap.rightActive)
        {
            foreach (DoorScript door in doors)
            {
                if (door.transform.position.x > doorPos.x)
                {
                    doorPos = door.transform.position;
                }
            }
            doorPositions[1] = doorPos;
        }
        if (roomMap.downActive)
        {
            foreach (DoorScript door in doors)
            {
                if (door.transform.position.y < doorPos.y)
                {
                    doorPos = door.transform.position;
                }
            }
            doorPositions[2] = doorPos;
        }
        if (roomMap.leftActive)
        {
            foreach (DoorScript door in doors)
            {
                if (door.transform.position.x < doorPos.x)
                {
                    doorPos = door.transform.position;
                }
            }
            doorPositions[3] = doorPos;
        }
    }

    private void updateScore()
    {
        for (int i = 0; i < score.Count; i++)
        {
            for (int j = 0; j < score[0].Count; j++)
            {
                if (position.x == i && position.y == j)
                {
                    score[i][j] = 0;
                }
                else
                {
                    score[i][j] = (score[i][j] + 2) / 3;
                }
            }
        }
    }

    private Vector2Int findBestDestination()
    {
        List<Vector2Int> pool = new List<Vector2Int>();
        float highScore = 0;
        for (int i = 0; i < score.Count; i++)
        {
            for (int j = 0; j < score[0].Count; j++)
            {
                if (highScore > score[i][j])
                {
                    continue;
                }
                if (highScore < score[i][j])
                {
                    pool.Clear();
                    highScore = score[i][j];
                }
                pool.Add(new Vector2Int(i, j));
            }
        }
        return pool[Random.Range(0, pool.Count)];
    }

    public void SetDestination(Vector2Int destination)
    {
        this.destination = destination;
        direction = ShortestPath(map[position.x][position.y], map[destination.x][destination.y]);
    }

    List<int> ShortestPath(Room source, Room destination)
    {
        List<Room> traversed = new List<Room>();
        List<List<Room>> path = new List<List<Room>>();
        List<Room> resultPath = new List<Room>();
        traversed.Add(source);
        List<Room> firstPath = new List<Room>();
        firstPath.Add(source);
        path.Add(firstPath);

        List<Room> currentPath;
        Room currentRoom;
        Room currentUp;
        Room currentRight;
        Room currentDown;
        Room currentLeft;
        while (true)
        {
            currentPath = path[0];
            path.RemoveAt(0);
            currentRoom = currentPath[currentPath.Count - 1];
            currentPath.Add(new Room());
            if (currentRoom.upActive && !traversed.Contains(currentRoom.up))
            {
                currentUp = currentRoom.up;
                currentPath[currentPath.Count - 1] = currentUp;
                if (currentUp == destination)
                {
                    resultPath = new List<Room>(currentPath);
                    break;
                }
                path.Add(new List<Room>(currentPath));
            }
            if (currentRoom.rightActive && !traversed.Contains(currentRoom.right))
            {
                currentRight = currentRoom.right;
                currentPath[currentPath.Count - 1] = currentRight;
                if (currentRight == destination)
                {
                    resultPath = new List<Room>(currentPath);
                    break;
                }
                path.Add(new List<Room>(currentPath));
            }
            if (currentRoom.downActive && !traversed.Contains(currentRoom.down))
            {
                currentDown = currentRoom.down;
                currentPath[currentPath.Count - 1] = currentDown;
                if (currentDown == destination)
                {
                    resultPath = new List<Room>(currentPath);
                    break;
                }
                path.Add(new List<Room>(currentPath));
            }
            if (currentRoom.leftActive && !traversed.Contains(currentRoom.left))
            {
                currentLeft = currentRoom.left;
                currentPath[currentPath.Count - 1] = currentLeft;
                if (currentLeft == destination)
                {
                    resultPath = new List<Room>(currentPath);
                    break;
                }
                path.Add(new List<Room>(currentPath));
            }
        }

        List<int> result = new List<int>();
        Room current;
        Room previous;
        for (int i = 1; i < resultPath.Count; i++)
        {
            current = resultPath[i];
            previous = resultPath[i - 1];
            if (current.up == previous)
            {
                result.Add(2);
            }
            else if (current.right == previous)
            {
                result.Add(3);
            }
            else if (current.down == previous)
            {
                result.Add(0);
            }
            else if (current.left == previous)
            {
                result.Add(1);
            }
        }

        return result;
    }
}
