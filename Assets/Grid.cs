using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    bool win;
    public int id = 0;
    public float lastMovedTime;
    public List<Color> colors;
    public static List<Color> staticColors;
    public TMP_Dropdown drop;
    internal static string winNum;
    public TextMeshProUGUI gameStateText,currentText,highestText;
    int score;
    int highestScore;
    public AudioSource auSource;
    public AudioClip matchSound, loseSound, winSound, elseSound;
    public Button music;

    [ContextMenu("Delete")]
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
    private void Awake()
    {
        auSource =Camera.main.GetComponent<AudioSource>();
        staticColors = colors;
        inputSystem = new InputSystem();
    }
    private void Start()
    {
        if (PlayerPrefs.GetString("music")=="0")
        {
            auSource.Stop();
            music.transform.GetChild(0).gameObject.SetActive(false);
            music.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            music.transform.GetChild(0).gameObject.SetActive(true);
            music.transform.GetChild(1).gameObject.SetActive(false);
        }
        music.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetString("music") == "0")
            {
                auSource.Play();
                PlayerPrefs.SetString("music", "1");
                music.transform.GetChild(0).gameObject.SetActive(true);
                music.transform.GetChild(1).gameObject.SetActive(false);

            }
            else
            {
                auSource.Stop();
                PlayerPrefs.SetString("music", "0");
                music.transform.GetChild(0).gameObject.SetActive(false);
                music.transform.GetChild(1).gameObject.SetActive(true);

            }
        });
        
        currentText.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2,150);
        highestText.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2,150);
        drop.value = PlayerPrefs.GetInt("drop");
        winNum = drop.options[drop.value].text;

        drop.onValueChanged.AddListener(delegate { OnChange(); });
        StartNewGame();

    }

    private void OnChange()
    {
        //Debug.Log("Change");
        winNum = drop.options[drop.value].text;
        PlayerPrefs.SetInt("drop", drop.value);
        StartNewGame();
    }

    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.PlayerActionMap.Down.started += Down;
        inputSystem.PlayerActionMap.Up.started += Up;
        inputSystem.PlayerActionMap.Left.started += Left;
        inputSystem.PlayerActionMap.Right.started += Right;
        inputSystem.PlayerActionMap.TouchPosition.started += TouchPos;

    }
      private void TouchPos(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Time.time < lastMovedTime + 0.2f)
        {
            return;
        }
        lastMovedTime = Time.time;
        var dir = obj.ReadValue<Vector2>();
        bool hor = false, vert = false;

        if (dir.x > 0 || dir.x < 0)
        {

            hor = true;
        }
        if (dir.y > 0 || dir.y < 0)
        {
            vert = true;
        }
        if (vert && hor)
        {
            if (Mathf.Abs(dir.x)> Mathf.Abs(dir.y))
            {
                if (dir.x>0)
                {
                    Right();
                }
                else
                {
                    Left();
                }
            }
            else
            {
                if (dir.y > 0)
                {
                    Up();
                }
                else
                {
                    Down();
                }
            }
        }
        else if (vert)
        {
            if (dir.y > 0)
            {
                Up();
            }
            else
            {
                Down();
            }
        }
        else if (hor)
        {
            if (dir.x > 0)
            {
                Right();
            }
            else
            {
                Left();
            }
        }

    }


    [ContextMenu("win")]
    public void MakeWiningPosition()
    {
        Tile tile = tiles[0, 0];
        Tile tile1 = tiles[1, 0];
        var go = Instantiate(tilePrefab,parent);
        var go1 = Instantiate(tilePrefab,parent);
        tile.piece.id = id;
        id++;
        tile.piece.SetPosition(id, go, tile.worldPosition, int.Parse(winNum) / 2);
        
        tile1.piece.SetPosition(id, go1, tile1.worldPosition, int.Parse(winNum)/2);
        id++;
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
            if (tiles[(int)(tempIndex.x + direction.x), (int)(tempIndex.y + direction.y)].piece.value == 0)
            {



                Tile oldTile = tiles[(int)tempIndex.x, (int)tempIndex.y];
                tempIndex += direction;
                Tile newTile = tiles[(int)tempIndex.x, (int)tempIndex.y];
                newTile.piece.SetPosition(oldTile.piece.id, oldTile.piece.gameObject, newTile.worldPosition, oldTile.piece.value, true);
                oldTile.piece.gameObject = null;
                oldTile.piece.value = 0;

                if (delAction == null)
                {
                    delAction = CreateNewNumber;
                }

            }
            else if (tiles[(int)tempIndex.x, (int)tempIndex.y].piece.value == tiles[(int)(tempIndex.x + direction.x), (int)(tempIndex.y + direction.y)].piece.value)
            {
                if (tiles[(int)(tempIndex.x + direction.x), (int)(tempIndex.y + direction.y)].piece.isChanged || tiles[(int)tempIndex.x, (int)tempIndex.y].piece.isChanged)
                {
                    return tempIndex;
                }
                Tile oldTile = tiles[(int)tempIndex.x, (int)tempIndex.y];
                tiles[(int)tempIndex.x, (int)tempIndex.y].piece.gameObject.name = "ss";
                DestroyImmediate(tiles[(int)tempIndex.x, (int)tempIndex.y].piece.gameObject);
                tiles[(int)tempIndex.x, (int)tempIndex.y].piece.gameObject = null;
                tiles[(int)tempIndex.x, (int)tempIndex.y].piece.value = 0;

                tempIndex += direction;
                Tile newTile = tiles[(int)tempIndex.x, (int)tempIndex.y];
                bool win = newTile.piece.SetPosition(/*tiles[(int)tempIndex.x, (int)tempIndex.y].worldPosition*/);
                score += newTile.piece.value;
                auSource.PlayOneShot(matchSound);

                if (win)
                {
                    Debug.Log("You won ");
                    win = true;
                    canPlay = false;
                    auSource.PlayOneShot(winSound);

                    StartCoroutine(DelayStartGame("Win"));
                    return tempIndex;
                }
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

    void Left()
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
        oldAndNewPos.Clear();

        if (delAction != null)
        {
            delAction();
            delAction = null;
        }
        else
        {
            canPlay = true;
        }
    }
    private void Left(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        Left();
        

    }

    private void Up(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Up();
        
    }

    private void Up()
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
        oldAndNewPos.Clear();

        if (delAction != null)
        {
            delAction();
            delAction = null;
        }
        else
        {
            canPlay = true;
        }
    }

    private void Right(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Right();
       

    }

    private void Right()
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
        oldAndNewPos.Clear();

        if (delAction != null)
        {
            delAction();
            delAction = null;
        }
        else
        {
            canPlay = true;
        }
    }

    private void Down(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Down();
       

    }

    private void Down()
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
        oldAndNewPos.Clear();
        if (win)
        {

        }
        if (delAction != null)
        {
            delAction();
            delAction = null;
        }
        else
        {
            canPlay = true;
        }
    }
    public void SetScore()
    {
        currentText.text = $"Score : {score}";
        if (highestScore<score)
        {
            PlayerPrefs.SetInt($"{drop.value}", score);
            highestScore = score;
            highestText.text = $"Highest Score : {score}";
        }
        
    }
       void StartNewGame()
    {
        
        
        score = 0;
        highestScore = PlayerPrefs.GetInt($"{drop.value}");
        highestText.text = $"Highest Score : {highestScore}";
        SetScore();
        gameStateText.gameObject.SetActive(false);
        win = false;

        canPlay = true;
        id = 0;
        if (drop.value == 0)
        {
            sizeOfGrid = 4;



        }
        else if (drop.value<=2)
        {

            sizeOfGrid = 5;
        }
        else
        {

            sizeOfGrid = 6;
        }
        float size = Screen.safeArea.width / (sizeOfGrid+0.5f);
        size -= 50;
        tilePrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        if (tiles != null)
        {
            foreach (var item in tiles)
            {
                if (item.gameObject != null)
                {
                    Destroy(item.gameObject);
                }
                if (item.piece.gameObject != null)
                {
                    Destroy(item.piece.gameObject);

                }
            }
        }
        tiles = new Tile[sizeOfGrid, sizeOfGrid];

        width = tilePrefab.GetComponent<RectTransform>().sizeDelta.x;
        Vector2 startx = Vector2.left * width * (sizeOfGrid - 1) / 2 + Vector2.down * (sizeOfGrid - 1) * width / 2;
        for (int i = 0; i < tiles.GetLength(0); i++)
        {



            for (int j = 0; j < tiles.GetLength(1); j++)
            {

                GameObject go = Instantiate(tilePrefab, parent);
                go.GetComponent<RectTransform>().anchoredPosition = startx + i * width * Vector2.right + j * width * Vector2.up;


                tiles[i, j] = new Tile();
                tiles[i, j].gameObject = go;
                tiles[i, j].gridPosition = new Vector2(i, j);
                tiles[i, j].gameObject.name = new Vector2(i, j).ToString();
                tiles[i, j].worldPosition = go.transform.position;

            }


        }
        CreateNewNumber();
    }
    public void CreateNewNumber()
    {
        
        delAction = null;
        Tile tile = tiles[Random.Range(0, sizeOfGrid), Random.Range(0, sizeOfGrid)];
        if (tile.piece.gameObject != null)
        {
            CreateNewNumber();
            return;
        }
        StartCoroutine(CreateNewNumberD(tile));

    }
    IEnumerator CreateNewNumberD(Tile tile)
    {
        yield return new WaitForSeconds(0.05f);

        auSource.PlayOneShot(elseSound);

        var go = Instantiate(tilePrefab,parent);
        go.GetComponent<Image>().color = Color.blue;
        tile.piece.id = id;
        tile.piece.SetPosition(id, go, tile.worldPosition, 2);
        SetScore();


        id++;
        foreach (var item in tiles)
        {
            item.piece.isChanged = false;
        }
        if (!IsGameOver())
        {

            canPlay = true;
        }
        else
        {

            GameOver();
        }
    }
    void GameOver()
    {
        print("game over");
        auSource.PlayOneShot(loseSound);
        canPlay = false;
        StartCoroutine(DelayStartGame("Lost"));
    }
    
    IEnumerator DelayStartGame(string gameState)
    {
        gameStateText.gameObject.SetActive(true);
        gameStateText.text = $"You {gameState}";
        gameStateText.transform.localScale = new Vector3(1, 1, 1);
        iTween.ScaleTo(gameStateText.gameObject, new Vector3(2, 2, 2), 1);
        yield return new WaitForSeconds(2);
        
        StartNewGame();
    }
    bool IsGameOver()
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tiles[i, j].piece.value == 0)
                {
                    i = 99;
                    return false;
                }
                else if (i + 1 < sizeOfGrid && tiles[i, j].piece.value == tiles[i + 1, j].piece.value)

                {
                    i = 99;

                    return false;
                }
                else if (i - 1 > 0 && tiles[i, j].piece.value == tiles[i - 1, j].piece.value)
                {
                    i = 99;

                    return false;
                }
                else if (j + 1 < sizeOfGrid && tiles[i, j].piece.value == tiles[i , j+1].piece.value)
                {
                    i = 99;

                    return false;
                }
                else if (j - 1 > 0 && tiles[i, j].piece.value == tiles[i , j-1].piece.value)
                {
                    i = 99;

                    return false;
                }

            }
        }
        return true;
    }
}
[System.Serializable]
public class Tile
{
    public GameObject gameObject;
    public Vector2 gridPosition;
    public Vector2 worldPosition;
    public Piece piece;
    public Tile()
    {
        piece = new();
    }
}
public class Piece
{

    public GameObject gameObject;    public int value;
    internal bool isChanged;
    public int id;
    public void SetPosition(int id,GameObject gameObject, Vector2 worldPosition, int value,bool animate=false)
    {
        
        if (gameObject == null)
        {
            Debug.Log(null) ;
        }
        this.gameObject = gameObject;
               if (animate)
        {

            iTween.MoveTo(gameObject, worldPosition, 0.4f);
          
        }
        else
        {
            gameObject.transform.position = worldPosition;
        }


        this.value = value;
        gameObject.name = $"id = {id}, value = {value}";
        if (value == 0)
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";

        }
        else
        {

            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();
            SetColor();        }
    }
    public bool SetPosition()
    {


        value += value;
        
        gameObject.name = $"id = {id}, value = {value}";
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();

        SetColor();
        if (value.ToString() == Grid.winNum)
        {
            return true;
        }
        return false;
    }
    void SetColor()
    {
        switch (value)
        {
            case 2:
                gameObject.GetComponent<Image>().color = Grid.staticColors[0];
                break;
            case 4:
                gameObject.GetComponent<Image>().color = Grid.staticColors[1];
                break;
            case 8:
                gameObject.GetComponent<Image>().color = Grid.staticColors[2];
                break;
            case 16:
                gameObject.GetComponent<Image>().color = Grid.staticColors[3];
                break;
            case 32:
                gameObject.GetComponent<Image>().color = Grid.staticColors[4];
                break;
            case 64:
                gameObject.GetComponent<Image>().color = Grid.staticColors[5];
                break;
            case 128:
                gameObject.GetComponent<Image>().color = Grid.staticColors[6];
                break;
            case 256:
                gameObject.GetComponent<Image>().color = Grid.staticColors[7];
                break;
            case 512:
                gameObject.GetComponent<Image>().color = Grid.staticColors[8];
                break;
            case 1024:
                gameObject.GetComponent<Image>().color = Grid.staticColors[9];
                break;
            case 2048:
                gameObject.GetComponent<Image>().color = Grid.staticColors[10];
                break;
            case 4096:
                gameObject.GetComponent<Image>().color = Grid.staticColors[11];
                break;

            case 8192:
                gameObject.GetComponent<Image>().color = Grid.staticColors[12];
                break;
            case 16384:
                gameObject.GetComponent<Image>().color = Grid.staticColors[13];
                break; ;
            default:
                break;
        }
    }

}

