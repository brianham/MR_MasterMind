using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public const int TRY_COUNT = 5;
    public const int ANSWER_COUNT = 4;
    public const float rowDepth = 0.15f;

    private AnswerRow solution = new AnswerRow(GameColor.Blue, GameColor.Yellow, GameColor.Blue, GameColor.Yellow);

    private int currentAnswerIndex = -1;
    //private int firstItemTopLeft = Point

    private Vector3 initialTopLeft = new Vector3(0f, 0.0f, 1.0f);
    private Vector3 currentTopLeft = new Vector3(0f, 0.0f, 1.0f);

    private GameObject[] answers = null;

    public static GameManager instance;


    public GameObject answerRowPrefab = null;


    private void Awake()
    {
        Debug.Log("GameManager.Awake");

        // allows this class instance to behave like a singleton
        instance = this;        
    }

    public void StartNewGame()
    {



        //Debug.Log("GameManager.StartNewGame");

        //// Instantiate static prefab for test
        //Vector3 initialPosition = new Vector3(0f, 0.0f, 1f);
        //Quaternion initialRotation = Quaternion.Euler(0, -90, 0);

        //currentAnswerRow = GameObject.Instantiate(answerRowPrefab, initialPosition, initialRotation);
        //rowController = currentAnswerRow.GetComponent<RowController>();

        //item1Material = rowController.item1.material;
        //item2Material = rowController.item2.material;
        //item3Material = rowController.item3.material;
        //item4Material = rowController.item4.material;

        ////rowController.item1.enabled = false;
        ////rowController.item2.enabled = false;
        ////rowController.item3.enabled = false;
        ////rowController.item4.enabled = false;

        //item1Material.color = Color.black;
        //item2Material.color = Color.black;
        //item3Material.color = Color.black;
        //item4Material.color = Color.black;
    }

    public void StartNewGame2()
    {
        answers = new GameObject[TRY_COUNT];
        currentAnswerIndex = 0;
        answers[currentAnswerIndex] = GetNewAnswerRow(initialTopLeft);
    }

    public GameObject GetNewAnswerRow(Vector3 topLeft)
    {
        // Instantiate static prefab for test
        Quaternion initialRotation = Quaternion.Euler(0, -90, 0);        
        GameObject newAnswerRow = GameObject.Instantiate(answerRowPrefab, topLeft, initialRotation);
        currentTopLeft = new Vector3(topLeft.x, topLeft.y, topLeft.z + rowDepth);
        return newAnswerRow;
    }

    public void SubmitAnswer()
    {
        Debug.Log("GameManager.SubmitAnswer");

        try
        {
            // Get current prefab gameobject
            GameObject answerRowPrefab = answers[currentAnswerIndex];

            // Get prefab script (reference to renderer and material
            AnswerRow answerRow = answerRowPrefab.GetComponent<AnswerRow>();

            // Get answer text
            //answerRowPrefab.GetComponent<>();

            if (answerRow.IsValid())
            {
                Debug.Log("GameManager.SubmitAnswer Valid Answer");
                var answerStatus = answerRow.CheckAnswer(solution);


                if (answerStatus == AnswerStatus.Correct)
                {
                    // Win case
                } else
                {
                    // create next row
                    answers[++currentAnswerIndex] = GetNewAnswerRow(currentTopLeft);
                }
            } else
            {
                Debug.Log("GameManager.SubmitAnswer Invalid Answer");
            }
        }
        catch (Exception ex)
        {
            Debug.Log(string.Format("GameManager.SubmitAnswer Error: {0}", ex.Message));
        }
    }

    public void ResetGame()
    {

    }

    public void ChangeTargetColor(string targetName, string colorName)
    {
        Debug.Log(string.Format("GameManager.ChangeTargetColor target: {0}, color: {1}", targetName, colorName));

        var targetMaterial = FindTarget(targetName);
        if (targetMaterial == null)
        {
            Debug.Log("GameManager.ChangeTargetColor target is null");
        }
        else
        {
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

    public void ChangeTargetColor2(string targetName, string colorName)
    {
        if (currentAnswerIndex < 0) return;

        Debug.Log(string.Format("GameManager.ChangeTargetColor2 target: {0}, color: {1}", targetName, colorName));

        // Get current prefab gameobject
        GameObject answerRowPrefab = answers[currentAnswerIndex];

        // Get prefab script (reference to renderer and material
        AnswerRow answerRow = answerRowPrefab.GetComponent<AnswerRow>();

        // Get targetColor enum
        GameColor targetColor = GetGameColorForString(colorName);

        // Get index for target
        int index = GetIndexForTargetName(targetName);

        // Set answer row color
        answerRow.SetByPosition(index, targetColor);

        // Get target material
        var targetMaterial = FindTarget2(targetName);

        if (targetMaterial == null)
        {
            Debug.Log("GameManager.ChangeTargetColor2 target is null");
        } else
        {
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

    public void ChangeTargetColorShortcut(string color1, string color2, string color3, string color4)
    {
        if (currentAnswerIndex <= 0) return;
    }

    private Material FindTarget(string name)
    {
        //Debug.Log(string.Format("GameManager.FindTarget name: {0}", name));

        //switch (name)
        //{
        //    case "PositionOne": return item1Material;
        //    case "PositionTwo": return item2Material;
        //    case "PositionThree": return item3Material;
        //    case "PositionFour": return item4Material;
        //    default: return null;
        //}
        return null;
    }

    private Material FindTarget2(string name)
    {
        Debug.Log(string.Format("GameManager.FindTarget name: {0}", name));

        // Get current prefab gameobject
        GameObject answerRowPrefab = answers[currentAnswerIndex];

        // Get prefab script (reference to renderer and material
        AnswerRow answerRow = answerRowPrefab.GetComponent<AnswerRow>();
        
        switch (name)
        {
            case "PositionOne": return answerRow.renderers[0].material;
            case "PositionTwo": return answerRow.renderers[1].material;
            case "PositionThree": return answerRow.renderers[2].material;
            case "PositionFour": return answerRow.renderers[3].material;
            default: return null;
        }
    }

    private GameColor GetGameColorForString(string color)
    {
        switch (color)
        {
            case "red": return GameColor.Red;
            case "blue": return GameColor.Blue;
            case "green": return GameColor.Green;
            case "yellow": return GameColor.Yellow;
            default: return GameColor.NotSet;
        }
    }

    private int GetIndexForTargetName(string targetName)
    {
        switch (targetName)
        {
            case "PositionOne": return 0;
            case "PositionTwo": return 1;
            case "PositionThree": return 2;
            case "PositionFour": return 3;
            default: return -1;
        }
    }
}

public enum GameColor
{
    NotSet,
    Blue,
    Red,
    Green,
    Yellow
}

public enum AnswerStatus
{
    NotSet,
    Incorrect,
    Correct
}

public static class TestClass
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {

    }
}


//private void OnDestroy()
//{
//    //Destroy(item1Material);
//    //Destroy(item2Material);
//    //Destroy(item3Material);
//    //Destroy(item4Material);
//}


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





//private GameObject currentAnswerRow = null;
//private RowController rowController = null;

//private Material item1Material = null;
//private Material item2Material = null;
//private Material item3Material = null;
//private Material item4Material = null;