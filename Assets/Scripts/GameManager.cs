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
        Debug.Log("GameManager.Awake");

        // allows this class instance to behave like a singleton
        instance = this;

        // Instantiate static prefab for test
        Vector3 initialPosition = new Vector3(0f, 0.0f, 1f);
        Quaternion initialRotation = Quaternion.Euler(0, -90, 0);

        currentGuessRow = GameObject.Instantiate(guessRowPrefab, initialPosition, initialRotation);
        rowController = currentGuessRow.GetComponent<RowController>();
        

        item1Material = rowController.item1.material;
        item2Material = rowController.item2.material;
        item3Material = rowController.item3.material;
        item4Material = rowController.item4.material;

        //rowController.item1.enabled = false;
        //rowController.item2.enabled = false;
        //rowController.item3.enabled = false;
        //rowController.item4.enabled = false;

        item1Material.color = Color.black;
        item2Material.color = Color.black;
        item3Material.color = Color.black;
        item4Material.color = Color.black;


    }

    private void OnDestroy()
    {
        Destroy(item1Material);
        Destroy(item2Material);
        Destroy(item3Material);
        Destroy(item4Material);
    }

    public void ChangeTargetColor(string targetName, string colorName)
    {
        //Debug.Log($"GameManager.ChangeTargetColor target: {targetName}, color: {colorName}");
        Debug.Log(string.Format("GameManager.ChangeTargetColor target: {0}, color: {1}", targetName, colorName));

        var targetMaterial = FindTarget(targetName);
        if (targetMaterial == null)
        {
            Debug.Log("GameManager.ChangeTargetColor target is null");
        }
        else
        {
            Debug.Log("Changing color " + colorName + " to target: " + targetMaterial.name);

            switch (colorName)
            {
                case "red":
                    {
                        targetMaterial.color = Color.red;
                        break;
                    }
                case "blue":
                    {
                        targetMaterial.color = Color.blue;
                        break;
                    }
                case "green":
                    {
                        targetMaterial.color = Color.green;
                        break;
                    }
                case "yellow":
                    {
                        targetMaterial.color = Color.yellow;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }

    private Material FindTarget(string name)
    {
        //Debug.Log($"GameManager.FindTarget name: {name}");
        Debug.Log(string.Format("GameManager.FindTarget name: {0}", name));

        switch (name)
        {
            case "first item": return item1Material;
            case "second item": return item2Material;
            case "third item": return item3Material;
            case "fourth item": return item4Material;
            default: return null; 
        }
    }
    /// <summary>
    /// Determines which obejct reference is the target GameObject by providing its name
    /// </summary>
    //private GameObject FindTarget(string name)
    //{
    //    GameObject targetAsGO = null;

    //    switch (name)
    //    {
    //        case "sphere":
    //            targetAsGO = sphere;
    //            break;

    //        case "cylinder":
    //            targetAsGO = cylinder;
    //            break;

    //        case "cube":
    //            targetAsGO = cube;
    //            break;

    //        case "this": // as an example of target words that the user may use when looking at an object
    //        case "it":  // as this is the default, these are not actually needed in this example
    //        case "that":
    //        default: // if the target name is none of those above, check if the user is looking at something
    //            if (gazedTarget != null)
    //            {
    //                targetAsGO = gazedTarget;
    //            }
    //            break;
    //    }
    //    return targetAsGO;
    //}
}


public static class TestClass
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {

    }
}