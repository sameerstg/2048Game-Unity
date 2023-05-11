using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    private InputSystem inputSystem;
    public Tile[,] tiles;
    public int sizeOfGrid;
    public GameObject tilePrefab;
    public Transform parent;
    float width;
    public delegate void DelAction();
    public DelAction delAction;
    Dictionary<Vector2, Vector2> oldAndNewPos = new();
    private bool canPlay;

    private void Awake()
    {
        inputSystem = new InputSystem();
        tiles = new Tile[sizeOfGrid, sizeOfGrid];
    }
    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.PlayerActionMap.Down.started += Down;
        inputSystem.PlayerActionMap.Up.started += Up;
        inputSystem.PlayerActionMap.Left.started += Left;
        inputSystem.PlayerActionMap.Right.started += Right;
    }
    public Vector2 KeepGoing(Vector2 index, Vector2 direction)
    {
        canPlay = false;
        Vector2 tempIndex = index;

        while (true)
        {
            Vector2 makeSureVector = tempIndex + direction;
            if (makeSureVector.x < 0 || makeSureVector.x > sizeOfGrid - 1 || makeSureVector.y < 0 || makeSureVector.y > sizeOfGrid - 1)
            {
                return tempIndex;
            }
            Debug.Log(tiles[(int)(tempIndex.x + direction.x), (int)(tempIndex.y + direction.y)].piece.value);
            if (tiles[(int)(tempIndex.x + direction.x), (int)(tempIndex.y + direction.y)].piece.value == 0)
            {



                Tile oldTile = tiles[(int)tempIndex.x, (int)tempIndex.y];
                tempIndex += direction;
                Tile newTile = tiles[(int)tempIndex.x, (int)tempIndex.y];
                newTile.piece.SetPosition(oldTile.piece.gameObject, newTile.worldPosition, oldTile.piece.value,true);
                oldTile.piece.gameObject = null;
                oldTile.piece.value = 0;

                if (delAction == null)
                {
                    delAction = CreateNewNumber;
                }

            }
            else if (tiles[(int)tempIndex.x, (int)tempIndex.y].piece.value == tiles[(int)(tempIndex.x + direction.x), (int)(tempIndex.y + direction.y)].piece.value)
            {
                if (tiles[(int)(tempIndex.x + direction.x), (int)(tempIndex.y + direction.y)].piece.isChanged)
                {
                    return tempIndex;
                }
                Tile oldTile = tiles[(int)tempIndex.x, (int)tempIndex.y];
                Destroy(tiles[(int)tempIndex.x, (int)tempIndex.y].piece.gameObject);
                tiles[(int)tempIndex.x, (int)tempIndex.y].piece.gameObject = null;
                tiles[(int)tempIndex.x, (int)tempIndex.y].piece.value = 0;

                tempIndex += direction;
                Tile newTile = tiles[(int)tempIndex.x, (int)tempIndex.y];
                newTile.piece.SetPosition(/*tiles[(int)tempIndex.x, (int)tempIndex.y].worldPosition*/);
                newTile.piece.isChanged = true;
                if (delAction == null)
                {
                    delAction = CreateNewNumber;
                }
            }
            else
            {

                return tempIndex;
            }
        }
    }

    private void Left(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {


        if (!canPlay)
        {
            return;
        }
        for (int i = 1; i < tiles.GetLength(0); i++)
        {



            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].piece.value != 0)
                {
                    var newPos = KeepGoing(new Vector2(i, j), Vector2.left);
                    if (newPos != new Vector2(i, j))
                    {
                        oldAndNewPos.Add(new Vector2(i, j), newPos);
                    }

                }

            }
        }
        Debug.Log(oldAndNewPos.Count);
        oldAndNewPos.Clear();

        if (delAction != null)
        {
            delAction();
            delAction = null;
        }

    }

    private void Up(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!canPlay)
        {
            return;
        }
        for (int i = tiles.GetLength(0) - 1; i >= 0; i--)
        {



            for (int j = tiles.GetLength(1) - 1; j >= 0; j--)
            {

                if (tiles[i, j].piece.value != 0)
                {
                    var newPos = KeepGoing(new Vector2(i, j), Vector2.up);
                    if (newPos != new Vector2(i, j))
                    {
                        oldAndNewPos.Add(new Vector2(i, j), newPos);
                    }
                }

            }
        }
        Debug.Log(oldAndNewPos.Count);
        oldAndNewPos.Clear();

        if (delAction != null)
        {
            delAction();
            delAction = null;
        }
    }

    private void Right(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!canPlay)
        {
            return;
        }
        for (int i = tiles.GetLength(0) - 1; i >= 0; i--)
        {



            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].piece.value != 0)
                {
                    var newPos = KeepGoing(new Vector2(i, j), Vector2.right);
                    if (newPos != new Vector2(i, j))
                    {
                        oldAndNewPos.Add(new Vector2(i, j), newPos);
                    }
                }

            }
        }
        Debug.Log(oldAndNewPos.Count);
        oldAndNewPos.Clear();

        if (delAction != null)
        {
            delAction();
            delAction = null;
        }

    }

    private void Down(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!canPlay)
        {
            return;
        }
        for (int i = tiles.GetLength(0) - 1; i >= 0; i--)
        {



            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].piece.value != 0)
                {
                    var newPos = KeepGoing(new Vector2(i, j), Vector2.down);
                    if (newPos != new Vector2(i, j))
                    {
                        oldAndNewPos.Add(new Vector2(i, j), newPos);
                    }
                }

            }
        }
        Debug.Log(oldAndNewPos.Count);
        oldAndNewPos.Clear();
        if (delAction != null)
        {
            delAction();
            delAction = null;
        }

    }

    private void Start()
    {
        width = tilePrefab.transform.localScale.x;
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
                Vector2 startx = Vector2.left*width/2+Vector2.down*width/2;
                
                

            for (int j = 0; j < tiles.GetLength(1); j++)
            {
               
                GameObject go = Instantiate(tilePrefab);
                go.transform.position = startx+j * width * Vector2.right+i * width * Vector2.up;
               

                tiles[i, j] = new Tile();
                tiles[i, j].gameObject = go;
                tiles[i, j].gridPosition = new Vector2(i, j);
                tiles[i, j].gameObject.name = new Vector2(i, j).ToString();
                tiles[i, j].worldPosition = startx + i * width * Vector2.right + j* width * Vector2.up;

            }


        }
        CreateNewNumber();
    }
    public void CreateNewNumber()
    {
        
        Tile tile = tiles[Random.Range(0, sizeOfGrid), Random.Range(0, sizeOfGrid)];
        if (tile.piece.gameObject != null)
        {
            CreateNewNumber();
        }
        StartCoroutine(CreateNewNumberD(tile));
        
    }
    IEnumerator CreateNewNumberD(Tile tile)
    {
        yield return new WaitForSeconds(.2f);
        var go = Instantiate(tilePrefab);
        go.GetComponent<SpriteRenderer>().color = Color.blue;
        tile.piece.SetPosition(go, tile.worldPosition, 2);
        foreach (var item in tiles)
        {
            item.piece.isChanged = false;
        }
        canPlay = true;
    }
}
[System.Serializable]
public class Tile
{
    public GameObject gameObject;
    public Vector2 gridPosition;
    public Vector2 worldPosition;
    //public int value;
    //public TextMeshProUGUI text;

    public Piece piece;
    public Tile()
    {
        piece = new();
    }
    /*public void SetValue(int value)
    {
        this.value = value;
        if (value ==0)
        {
            text.text = "";

        }
        else
        {
            text.text = value.ToString();

        }
    }*/

}
public class Piece
{

    public GameObject gameObject;
    public int value;
    internal bool isChanged;

    public void SetPosition(GameObject gameObject, Vector2 worldPosition, int value,bool animate=false)
    {
        if (gameObject == null)
        {
            Debug.Log(null) ;
        }
        this.gameObject = gameObject;
        //gameObject.transform.position = worldPosition;
        if (animate)
        {

        iTween.MoveTo(gameObject, worldPosition,0.3f);
        }
        else
        {
            gameObject.transform.position = worldPosition;
        }
        //gameObject.GetComponent<RectTransform>().anchoredPosition = worldPosition;

        

        this.value = value;
        if (value == 0)
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";

        }
        else
        {

            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();
        }
    }
    public void SetPosition()
    {


        value += value;

        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();
    }

}

