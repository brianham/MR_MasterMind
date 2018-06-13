using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject guessRowPrefab = null;

    private GameObject currentGuessRow = null;
    private RowController rowController = null;

    private Material item1Material = null;
    private Material item2Material = null;
    private Material item3Material = null;
    private Material item4Material = null;

    private void Awake()
    {
        // allows this class instance to behave like a singleton
        instance = this;

        // Instantiate static prefab for test
        Vector3 initialPosition = new Vector3(0, 2, 12);
        Quaternion initialRotation = Quaternion.Euler(0, -90, 0);

        currentGuessRow = GameObject.Instantiate(guessRowPrefab, initialPosition, initialRotation);
        rowController = currentGuessRow.GetComponent<RowController>();

        

        item1Material = rowController.item1.material;
        item2Material = rowController.item2.material;
        item3Material = rowController.item3.material;
        item4Material = rowController.item4.material;

        rowController.item1.enabled = false;
        //rowController.item2.enabled = false;
        //rowController.item3.enabled = false;
        //rowController.item4.enabled = false;
        //item1Material.color = Color.black;
        //item2Material.color = Color.black;
        //item3Material.color = Color.black;
        //item4Material.color = Color.black;


    }

    private void OnDestroy()
    {
        Destroy(item1Material);
        Destroy(item2Material);
        Destroy(item3Material);
        Destroy(item4Material);
    }
}


public static class TestClass
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {

    }
}