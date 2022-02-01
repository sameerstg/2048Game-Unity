using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public static BoardGenerator _instance;
    public GameObject tilePrefab;
    Vector3 initialPosition = new Vector3(-3,3,0);

    public Cell[] y;
    private void Awake()
    {
        _instance = this;
        for (int i = 0; i < 5; i++)
        {
            y = new Cell[i];
        }
        
    }
    private void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var obj = Instantiate(tilePrefab, initialPosition, Quaternion.identity);
                
                if (y[i].x[j] == null)
                {
                    y[i].x[j] = obj;
                    obj.transform.parent = gameObject.transform;
                }

                initialPosition.x += 1;
            }
            initialPosition.x -= 4;
            initialPosition.y -= 1;
        }
        
    }
    
}
[System.Serializable]
public class Cell
{
    public GameObject[] x = new GameObject[4];

}