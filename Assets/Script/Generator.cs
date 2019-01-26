using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int ToRoomType()
    {
        if (upActive)
        {
            if (downActive)
            {
                if (leftActive)
                {
                    if (rightActive)
                    {
                        return Random.Range(16, 20);
                    }
                    else
                    {
                        return 15;
                    }
                }
                else
                {
                    if (rightActive)
                    {
                        return 13;
                    }
                    else
                    {
                        return Random.Range(0, 2) * 2 + 9;
                    }
                }
            }
            else
            {
                if (leftActive)
                {
                    if (rightActive)
                    {
                        return 12;
                    }
                    else
                    {
                        return 4;
                    }
                }
                else
                {
                    if (rightActive)
                    {
                        return 5;
                    }
                    else
                    {
                        return 0;
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
                        return 14;
                    }
                    else
                    {
                        return 7;
                    }
                }
                else
                {
                    if (rightActive)
                    {
                        return 6;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
            else
            {
                if (leftActive)
                {
                    if (rightActive)
                    {
                        return Random.Range(0, 2) * 2 + 8;
                    }
                    else
                    {
                        return 3;
                    }
                }
                else
                {
                    if (rightActive)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
    }
}

public class Generator : MonoBehaviour
{
    public int dimension;
    /*public GameObject cube;
    public GameObject cylinder;*/
    //public List<List<GameObject>> cubes = new List<List<GameObject>>();
    public GameObject interior;
    public List<List<Room>> map = new List<List<Room>>();
    private HashSet<Room> linked = new HashSet<Room>();
    private List<Room> linkable = new List<Room>();
    public float linkRate;

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
            for (int i = -GetDistance(); i < GetDistance(); i++)
            {
                for (int j = -GetDistance(); j < GetDistance(); j++)
                {
                    if (Mathf.Abs(i) + Mathf.Abs(j) <= GetDistance() && i + playerStart[player][0] < dimension && i + playerStart[player][0] >= 0 && j + playerStart[player][1] < dimension && j + playerStart[player][1] >= 0)
                    {
                        spawnable.Remove(map[i + playerStart[player][0]][j + playerStart[player][1]]);
                    }
                }
            }
        }

        /*for(int i = 0; i < 4; i++)
        {
            Instantiate(cube, new Vector3(playerStart[i][0] + 0.5f, playerStart[i][1] + 0.5f), Quaternion.identity);
        }*/

        GenerateObject(ref map);
    }

    private void GenerateObject(ref List<List<Room>> map)
    {
        GameObject mapObject = new GameObject("Map");
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                GameObject roomObject = Instantiate(interior, new Vector3(i * 11, j * 11, 0), Quaternion.identity, mapObject.transform);
                map[i][j].interior = roomObject;
            }
        }
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
                result[i].Add(map[i][j].ToRoomType());
            }
        }
        return result;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
