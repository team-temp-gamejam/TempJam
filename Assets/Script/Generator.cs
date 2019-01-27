using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room
{
    public GameObject interior;
    public Room left;
    public Room right;
    public Room up;
    public Room down;
    public bool leftActive = false;
    public bool rightActive = false;
    public bool upActive = false;
    public bool downActive = false;
    public int type = -1;

    public int GetRoomType()
    {
        if (type == -1)
        {
            this.type = this.ToRoomType();
        }
        return this.type;
    }

    public int ToRoomType()
    {
        if (type >= 0)
        {
            return type;
        }
        if (upActive)
        {
            if (downActive)
            {
                if (leftActive)
                {
                    if (rightActive)
                    {
                        type = UnityEngine.Random.Range(16, 20);
                    }
                    else
                    {
                        type = 15;
                    }
                }
                else
                {
                    if (rightActive)
                    {
                        type = 13;
                    }
                    else
                    {
                        type = Random.Range(0, 2) * 2 + 9;
                    }
                }
            }
            else
            {
                if (leftActive)
                {
                    if (rightActive)
                    {
                        type = 12;
                    }
                    else
                    {
                        type = 4;
                    }
                }
                else
                {
                    if (rightActive)
                    {
                        type = 5;
                    }
                    else
                    {
                        type = 0;
                    }
                }
            }
        }
        else
        {
            if (downActive)
            {
                if (leftActive)
                {
                    if (rightActive)
                    {
                        type = 14;
                    }
                    else
                    {
                        type = 7;
                    }
                }
                else
                {
                    if (rightActive)
                    {
                        type = 6;
                    }
                    else
                    {
                        type = 2;
                    }
                }
            }
            else
            {
                if (leftActive)
                {
                    if (rightActive)
                    {
                        type = UnityEngine.Random.Range(0, 2) * 2 + 8;
                    }
                    else
                    {
                        type = 3;
                    }
                }
                else
                {
                    if (rightActive)
                    {
                        type = 1;
                    }
                    else
                    {
                        type = -1;
                    }
                }
            }
        }
        return type;
    }
}

public class Generator : MonoBehaviour
{
    public int dimension;
    /*public GameObject cube;
    public GameObject cylinder;*/
    //public List<List<GameObject>> cubes = new List<List<GameObject>>();
    public GameObject[] roomPrefabs;
    public List<List<Room>> map = new List<List<Room>>();
    private HashSet<Room> linked = new HashSet<Room>();
    private List<Room> linkable = new List<Room>();
    public float linkRate;
    public MapDrawer drawer;
    public GameObject ghost;

    public GameObject[] playerObject;
    public GameObject compass;
    public GameObject hourglass;

    public Camera[] cameras = new Camera[4];

    public List<List<int>> playerStart = new List<List<int>>();

    public void Generate()
    {
        for (int i = 0; i < dimension; i++)
        {
            map.Add(new List<Room>());
            for (int j = 0; j < dimension; j++)
            {
                map[i].Add(new Room());
                if (j > 0)
                {
                    map[i][j].down = map[i][j - 1];
                    map[i][j - 1].up = map[i][j];
                }
                if (i > 0)
                {
                    map[i][j].left = map[i - 1][j];
                    map[i - 1][j].right = map[i][j];
                }
            }
        }

        Room first = map[Random.Range(0, dimension)][Random.Range(0, dimension)];
        linked.Add(first);
        if (first.left != null)
        {
            linkable.Add(first.left);
            first.leftActive = true;
            first.left.rightActive = true;
        }
        if (first.right != null)
        {
            linkable.Add(first.right);
            first.rightActive = true;
            first.right.leftActive = true;
        }
        if (first.up != null)
        {
            linkable.Add(first.up);
            first.upActive = true;
            first.up.downActive = true;
        }
        if (first.down != null)
        {
            linkable.Add(first.down);
            first.downActive = true;
            first.down.upActive = true;
        }

        while (linked.Count < CountRoom(dimension))
        {
            Room selected = linkable[Random.Range(0, linkable.Count)];
            linkable.Remove(selected);
            linked.Add(selected);
            if (selected.left != null && !linkable.Contains(selected.left) && !linked.Contains(selected.left))
            {
                linkable.Add(selected.left);
                selected.leftActive = true;
                selected.left.rightActive = true;
            }
            if (selected.right != null && !linkable.Contains(selected.right) && !linked.Contains(selected.right))
            {
                linkable.Add(selected.right);
                selected.rightActive = true;
                selected.right.leftActive = true;
            }
            if (selected.up != null && !linkable.Contains(selected.up) && !linked.Contains(selected.up))
            {
                linkable.Add(selected.up);
                selected.upActive = true;
                selected.up.downActive = true;
            }
            if (selected.down != null && !linkable.Contains(selected.down) && !linked.Contains(selected.down))
            {
                linkable.Add(selected.down);
                selected.downActive = true;
                selected.down.upActive = true;
            }
        }

        List<bool> addition = new List<bool>();
        for (int i = 0; i < (CountLink(dimension) - CountRoom(dimension) + 1) * linkRate; i++)
        {
            addition.Add(true);
        }
        for (int i = 0; i < (CountLink(dimension) - CountRoom(dimension) + 1) - ((CountLink(dimension) - CountRoom(dimension) + 1) * linkRate); i++)
        {
            addition.Add(false);
        }
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                if (!map[i][j].upActive && map[i][j].up != null)
                {
                    bool toAdd = addition[Random.Range(0, addition.Count)];
                    addition.Remove(toAdd);
                    if (toAdd)
                    {
                        linkable.Add(map[i][j].up);
                        map[i][j].upActive = true;
                        map[i][j].up.downActive = true;
                    }
                }
                if (!map[i][j].rightActive && map[i][j].right != null)
                {
                    bool toAdd = addition[Random.Range(0, addition.Count)];
                    addition.Remove(toAdd);
                    if (toAdd)
                    {
                        linkable.Add(map[i][j].right);
                        map[i][j].rightActive = true;
                        map[i][j].right.leftActive = true;
                    }
                }
            }
        }

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                map[i][j].GetRoomType();
            }
        }

        /*for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                Instantiate(cube, new Vector3(i, j), Quaternion.identity);
                if (map[i][j].upActive)
                {
                    Instantiate(cylinder, new Vector3(i, j + 0.5f), Quaternion.identity);
                }
                if (map[i][j].rightActive)
                {
                    Instantiate(cylinder, new Vector3(i + 0.5f, j), Quaternion.Euler(0,0,90));
                }
            }
        }*/

        /*List<List<int>> result = GetRoomTypes();
        for (int i = 0; i < dimension; i++)
        {
            string output = "";
            for (int j = 0; j < dimension; j++)
            {
                output += result[i][j] + " ";
            }
            Debug.Log(output);
        }*/

        List<Room> spawnable = new List<Room>();
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                spawnable.Add(map[i][j]);
            }
        }

        playerStart.Add(new List<int>());
        playerStart.Add(new List<int>());
        playerStart.Add(new List<int>());
        playerStart.Add(new List<int>());
        for (int player = 0; player < 4; player++)
        {
            Room selected = spawnable[Random.Range(0, spawnable.Count)];
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    if (map[i][j] == selected)
                    {
                        playerStart[player].Add(i);
                        playerStart[player].Add(j);
                    }
                }
            }
            for (int i = -GetDistance(); i <= GetDistance(); i++)
            {
                for (int j = -GetDistance(); j <= GetDistance(); j++)
                {
                    if (Mathf.Abs(i) + Mathf.Abs(j) <= GetDistance() && i + playerStart[player][0] < dimension && i + playerStart[player][0] >= 0 && j + playerStart[player][1] < dimension && j + playerStart[player][1] >= 0)
                    {
                        spawnable.Remove(map[i + playerStart[player][0]][j + playerStart[player][1]]);
                    }
                }
            }
        }

        Draw();

        /*for(int i = 0; i < 4; i++)
        {
            Instantiate(cube, new Vector3(playerStart[i][0] + 0.5f, playerStart[i][1] + 0.5f), Quaternion.identity);
        }*/

        GenerateObject(ref map);

        GeneratePlayer();
        GenerateGhost();
    }

    private void GenerateObject(ref List<List<Room>> map)
    {
        GetRoomTypes();
        GameObject mapObject = new GameObject("Map");
        List<float> startPosition = new List<float>();

        startPosition.Add(180.0f);
        startPosition.Add(180.0f);
        startPosition.Add(90.0f);
        startPosition.Add(180.0f);
        startPosition.Add(180.0f);

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                GameObject prefabedRoom;

                int roomTheme = Random.Range(0, 2);
                prefabedRoom = roomPrefabs[(int)Mathf.Floor(map[i][j].type / 4) * 3 + roomTheme];
                // - startPosition[map[i][j].type % 4] + 
                GameObject roomObject = Instantiate(prefabedRoom, new Vector3(i * 7.1f, j * 7.1f, 10), Quaternion.Euler(0, 0, startPosition[(int)Mathf.Floor(map[i][j].type / 4)] + (-90) * (map[i][j].type % 4)), mapObject.transform);
                roomObject.GetComponent<RoomScript>().setTilePosition(j, i);
                roomObject.GetComponent<RoomScript>().RoomType = map[i][j].type;
                map[i][j].interior = roomObject;
            }
        }
    }

    private void GeneratePlayer()
    {
        List<Vector2> location = this.GetPlayerSpawn();
        for (int i = 0; i < 4; i++)
        {
            GameObject player = Instantiate(playerObject[i], new Vector3(location[i].x * 7.1f, location[i].y * 7.1f, 5), Quaternion.identity);
            PlayerControl controller = player.GetComponent<PlayerControl>();
            controller.SetPlayer(i + 1);
            controller.SetCurrentRoom(location[i]);

            controller.orientation = Random.Range(0, 3) * 90;

            GameObject.Find("MapManager").GetComponent<MapManager>().players.Add(controller.gameObject);

            cameras[i].GetComponent<CameraFollowScript>().player = player;
        }
    }

    private void GenerateGhost()
    {
        Vector2Int spawnPosition = new Vector2Int();
        List<Vector2> players = GetPlayerSpawn();

        bool ok = false;
        while (!ok)
        {
            ok = true;
            spawnPosition = new Vector2Int(Random.Range(0, dimension), Random.Range(0, dimension));
            foreach (Vector2 player in players)
            {
                Vector2Int playerPosition = new Vector2Int((int)player.x, (int)player.y);
                if (playerPosition == spawnPosition)
                {
                    ok = false;
                    break;
                }
            }
        }

        GameObject ghostObject = Instantiate(ghost, new Vector3(spawnPosition.x * 7.1f, spawnPosition.y * 7.1f, 5), Quaternion.identity);
        GameObject.Find("MapManager").GetComponent<MapManager>().ghost = ghostObject;
        ghostObject.GetComponent<GhostMovement>().SetMap(map, spawnPosition);
    }

    private int CountLink(int dimension)
    {
        return 2 * dimension * (dimension - 1);
    }

    private int CountRoom(int dimension)
    {
        return dimension * dimension;
    }

    private int GetDistance()
    {
        if (dimension == 3)
            return 0;
        else
        {
            return (dimension - 1) / 2;
        }
    }

    public List<List<int>> GetRoomTypes()
    {
        List<List<int>> result = new List<List<int>>();
        for (int i = 0; i < dimension; i++)
        {
            result.Add(new List<int>());
            for (int j = 0; j < dimension; j++)
            {
                result[i].Add(map[i][j].GetRoomType());
            }
        }
        return result;
    }

    public List<Vector2> GetPlayerSpawn()
    {
        List<Vector2> result = new List<Vector2>();
        for (int i = 0; i < 4; i++)
        {
            result.Add(new Vector2(playerStart[i][0], playerStart[i][1]));
        }
        return result;
    }

    public GameObject GetRoom(int row, int column)
    {
        // return room[row][column];
        return map[column][row].interior;
    }

    public GameObject GetRoom(Vector2 index)
    {
        return map[(int)index.x][(int)index.y].interior;
    }

    public void Draw()
    {
        if (drawer != null)
        {
            drawer.DrawMap(map);
        }
    }

    void Start()
    {
        Generate();
        GameObject.Find("MapManager").GetComponent<MapManager>().ScanMap();
        GenerateItem();
    }

    private void GenerateItem()
    {
        List<Cupboard> cupboard = new List<Cupboard>();
        foreach (GameObject obj in GameObject.Find("MapManager").GetComponent<MapManager>().cupboards)
        {
            cupboard.Add(obj.GetComponent<Cupboard>());
        }
        while (cupboard.Count > 0)
        {
            Cupboard deciding = cupboard[Random.Range(0, cupboard.Count)];
            if (cupboard.Count % 10 == 0)
            {
                deciding.insideThing = Instantiate(compass, deciding.gameObject.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            }
            if (cupboard.Count % 10 == 9)
            {
                deciding.insideThing = Instantiate(hourglass, deciding.gameObject.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            }
            if (cupboard.Count % 10 != 0 && cupboard.Count % 10 != 9)
            {
                if (Random.Range(0, 3) == 0)
                {
                    deciding.isOpen = true;
                }
            }
            cupboard.Remove(deciding);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
