using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject redItem;
    public GameObject greenItem;
    public GameObject blueItem;
    public GameObject yellowItem;
    
    private Bounds spawnArea;

    public GameObject[] spawnFields;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        
        InvokeRepeating("spawnItems", 5, Random.Range(5, 8));
        
    }

    public void spawnItems()
    {

        if (gameManager.GetPlayerCount() == 0)
        {
            return;
        }
        
        if (spawnFields.Length == 0)
        {
            return;
        }
        
        var spawnFieldIndex = Random.Range(0, spawnFields.Length);

        var spawnAreaObject = spawnFields[spawnFieldIndex];

        if (spawnAreaObject == null)
        {
            return;
        }

        spawnArea = spawnAreaObject.GetComponent<BoxCollider>().bounds;
        var randomItem = Random.Range(1, 5);
        
        
        switch (randomItem)
        {
            case 1:
                var newItem1 = Instantiate(redItem, randomSpawnPosition(), Quaternion.Euler(180, 0, 0), this.gameObject.transform);
                AddInventory(newItem1, Random.Range(1,3), 0, 0, 0);
                break;
            case 2:
                var newItem2 = Instantiate(greenItem, randomSpawnPosition(), Quaternion.Euler(180, 0, 0), this.gameObject.transform);
                AddInventory(newItem2, 0, 0, Random.Range(1,3), 0);
                break;
            case 3:
                var newItem3 = Instantiate(blueItem, randomSpawnPosition(), Quaternion.Euler(180, 0, 0), this.gameObject.transform);
                AddInventory(newItem3, 0, 0,0, Random.Range(1,3));
                break;
            case 4:
                var newItem4 = Instantiate(yellowItem, randomSpawnPosition(), Quaternion.Euler(180, 0, 0), this.gameObject.transform);
                AddInventory(newItem4, 0, Random.Range(1,3),0, 0);
                break;
        }
    }
    
    private void AddInventory(GameObject item, int red, int yellow, int green, int blue)
    {
        var pc = item.GetComponent<PickupController>();

        if (pc != null)
        {
            pc.TotalBlue += blue;
            pc.TotalGreen += green;
            pc.TotalRed += red;
            pc.TotalYellow += yellow;
        }
    }
    
    Vector3 randomSpawnPosition()
    {
        float x = Random.Range(spawnArea.min.x, spawnArea.max.x);
        float z = Random.Range(spawnArea.min.z, spawnArea.max.z);
        float y = 0.25f;

        return new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
