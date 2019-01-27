using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class MapDrawer : MonoBehaviour
{
    public Object[] pieces = new Object[20];
    public GameObject[] map = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawMap(List<List<Room>> rooms)
    {
        for(int n = 0; n < 4; n++)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                for (int j = 0; j < rooms.Count; j++)
                {
                    GameObject mapPiece = Instantiate(pieces[rooms[i][j].GetRoomType()], new Vector3((i - ((rooms.Count - 1) / 2f)) * 30, (j - ((rooms.Count - 1) / 2f)) * 30, 0), Quaternion.identity) as GameObject;
                    mapPiece.transform.SetParent(map[n].transform, false);
                    //map[n].SetActive(true);
                }
            }
        }
    }
}
