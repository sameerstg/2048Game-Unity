using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //      Creating Instance
    public static TileManager _instance;
    //      Array Of Class of y-axis Tiles Gameobjects
    public PCell[] py;
    //      List of All Tiles
    public List<GameObject> tiles;
    //      List of Sprites
    public List<Material> tilesMaterials;
    //      Simple GameObject Cube for tiles
    public GameObject tile;
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        //      Initializing the array for tiles in Class
        for (int i = 0; i < 5; i++)
        {
            py = new PCell[i];
        }
        StartCoroutine(CreateATile());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move("left");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move("right");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Move("up");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move("down");
        }
    }
    public Vector2 FindTile(GameObject tile)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (py[i].px[j] != null && py[i].px[j] == tile)
                {
                    return new Vector2(i, j);
                }
            }
        }
        return new Vector2(6,6);
    }
    public void Move(string direction)
    {
        
        if (direction == "left")
        {
            //      Finding All Available Piece
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //      If Piece Got find
                    if (py[i].px[j] != null)
                    {
                        //      Temporary y axis so the loop x-axis wont get affected
                        var jj = j;
                        //      transform tile left until theres nothing 
                        while (jj - 1>=0 &&py[i].px[jj - 1] == null)
                        {
                            GameObject obj = py[i].px[jj].gameObject;
                            py[i].px[jj] = null;
                            py[i].px[jj - 1] = obj;
                            obj.transform.position = BoardGenerator._instance.y[i].x[jj - 1].transform.position;
                            SetZAxis(obj);
                            jj -= 1;
                        }
                        //      if theres another tile on left
                        if (jj - 1 >= 0) {
                            if ( py[i].px[jj - 1] == null)
                            {
                                continue;
                                
                            }
                            else
                            {
                                CheckAndJoinTile(py[i].px[jj], i, jj, py[i].px[jj - 1], i, jj - 1);
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        if (direction == "right")
        {
            //      Finding All Available Piece
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //      If Piece Got find
                    if (py[i].px[j] != null)
                    {
                        //      Temporary y axis so the loop x-axis wont get affected
                        var jj = j;
                        //      transform tile right until theres nothing 
                        while (jj + 1 <= 3 && py[i].px[jj + 1] == null)
                        {
                            GameObject obj = py[i].px[jj].gameObject;
                            py[i].px[jj] = null;
                            py[i].px[jj + 1] = obj;
                            obj.transform.position = BoardGenerator._instance.y[i].x[jj + 1].transform.position;
                            SetZAxis(obj);
                            jj += 1;
                        }
                        //      if theres another tile on right
                        if (jj + 1 <= 3)
                        {
                            if (py[i].px[jj + 1] == null)
                            {
                                continue;

                            }
                            else
                            {
                                CheckAndJoinTile(py[i].px[jj], i, jj, py[i].px[jj + 1], i, jj + 1);
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        if (direction == "down")
        {
            //      Finding All Available Piece
            for (int i = 3; i > -1; i--)
            {
                for (int j = 3; j >-1; j--)
                {
                    //      If Piece Got find
                    if (py[i].px[j] != null)
                    {
                        //      Temporary y axis so the loop y-axis wont get affected
                        var ii = i;
                        //      transform tile down until theres nothing 
                        while (ii + 1 <= 3 && py[ii+1].px[j] == null)
                        {
                            GameObject obj = py[ii].px[j].gameObject;
                            py[ii].px[j] = null;
                            py[ii+1].px[j] = obj;
                            obj.transform.position = BoardGenerator._instance.y[ii+1].x[j].transform.position;
                            SetZAxis(obj);
                            ii += 1;
                        }
                        //      if theres another tile on down
                        if (ii + 1 <= 3)
                        {
                            if (py[ii+1].px[j] == null)
                            {
                                continue;

                            }
                            else
                            {
                                CheckAndJoinTile(py[ii].px[j], ii, j, py[ii+1].px[j], i+1, j);
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        if (direction == "up")
        {
            //      Finding All Available Piece
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //      If Piece Got find
                    if (py[i].px[j] != null)
                    {
                        //      Temporary y axis so the loop y-axis wont get affected
                        var ii = i;
                        //      transform tile right until theres nothing 
                        while (ii - 1 >= 0 && py[ii-1].px[j] == null)
                        {
                            GameObject obj = py[ii].px[j].gameObject;
                            py[ii].px[j] = null;
                            py[ii-1].px[j] = obj;
                            obj.transform.position = BoardGenerator._instance.y[ii-1].x[j].transform.position;
                            SetZAxis(obj);
                            ii -= 1;
                        }
                        //      if theres another tile on right
                        if (ii - 1 >= 0)
                        {
                            if (py[ii-1].px[j] == null)
                            {
                                continue;

                            }
                            else
                            {
                                CheckAndJoinTile(py[ii].px[j], ii, j, py[ii-1].px[j], i-1,j);
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        StartCoroutine(CreateATile());
    }
 
   
    void CheckAndJoinTile(GameObject tile1,int tile1y,int tile1x, GameObject tile2,int tile2y,int tile2x)
    {
        if (tile1.tag == tile2.tag)
        {
            Destroy(tile1);
            py[tile1y].px[tile1x] = null;
            switch (tile2.tag)
            {
                case "2":
                    tile2.GetComponent<MeshRenderer>().material = tilesMaterials[1];
                    tile2.tag = "4";
                    break;
                case "4":
                    tile2.GetComponent<MeshRenderer>().material = tilesMaterials[2];
                    tile2.tag = "8";
                    break;
                case "8":
                    tile2.GetComponent<MeshRenderer>().material = tilesMaterials[3];
                    tile2.tag = "16";
                    break;
                case "16":
                    tile2.GetComponent<MeshRenderer>().material = tilesMaterials[4];
                    tile2.tag = "32";
                    break;
                case "32":
                    tile2.GetComponent<MeshRenderer>().material = tilesMaterials[5];
                    tile2.tag = "64";
                    break;
                case "64":
                    tile2.GetComponent<MeshRenderer>().material = tilesMaterials[6];
                    tile2.tag = "128";
                    break;
                case "128":
                    tile2.GetComponent<MeshRenderer>().material = tilesMaterials[7];
                    tile2.tag = "256";
                    break;
                case "256":
                    tile2.GetComponent<MeshRenderer>().material = tilesMaterials[8];
                    tile2.tag = "512";
                    break;
                case "512":
                    tile2.GetComponent<MeshRenderer>().material = tilesMaterials[9];
                    tile2.tag = "1024";
                    break;
                case "1024":
                    tile2.GetComponent<MeshRenderer>().material = tilesMaterials[10];
                    tile2.tag = "2048";
                    break;

                default:
                    break;
            }
        }
        
    }
    public void SetZAxis(GameObject tile)
    {
        var x = tile.transform.position;
        x.z -= 1;
        tile.transform.position = x;
    }
    public IEnumerator CreateATile()
    {
        yield return new WaitForSeconds(0.1f);
        var obj = Instantiate(tile,transform);
        obj.GetComponent<MeshRenderer>().material = tilesMaterials[0];
        int y;
        int x;
        
        do
        {
           y = Random.Range(0, 4);
           x = Random.Range(0, 4);
        } while (py[y].px[x] != null);
        
        obj.transform.position = BoardGenerator._instance.y[y].x[x].transform.position;
        py[y].px[x] = obj;
        tiles.Add(obj);
        SetZAxis(obj);
        obj.tag = "2";
    }
}
[System.Serializable]
public class PCell
{
    public GameObject[] px = new GameObject[4];
}
