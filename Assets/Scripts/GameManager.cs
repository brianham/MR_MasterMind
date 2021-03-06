﻿using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public const int TRY_COUNT = 3;
    public const int ANSWER_COUNT = 4;
    public const float ROW_DEPTH = 0.15f;

    public GameObject answerRowPrefab = null;
    public GameObject trophy = null;
    public GameObject mooseHead = null;
    public static GameManager instance;

    private AnswerRow solution = new AnswerRow(GameColor.Blue, GameColor.Yellow, GameColor.Blue, GameColor.Yellow);
    private int currentAnswerIndex = -1;
    private Vector3 initialTopLeft = new Vector3(0f, .5f, 1.5f);
    private Vector3 currentTopLeft = new Vector3(0f, 0.0f, 1.0f);
    private GameObject[] answers = null;    

    private void Awake()
    {
        Debug.Log("GameManager.Awake");

        // allows this class instance to behave like a singleton
        instance = this;        
    }

    public void StartNewGame()
    {
        ShowHideWinMessage(false);
        ShowHideLoseMessage(false);
        answers = new GameObject[TRY_COUNT];
        currentAnswerIndex = 0;
        answers[currentAnswerIndex] = GetNewAnswerRow(initialTopLeft);
    }

    public GameObject GetNewAnswerRow(Vector3 topLeft)
    {
        Quaternion initialRotation = Quaternion.Euler(0, -90, 0);        
        GameObject newAnswerRow = GameObject.Instantiate(answerRowPrefab, topLeft, initialRotation);
        currentTopLeft = new Vector3(topLeft.x, topLeft.y, topLeft.z + ROW_DEPTH);
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

            if (answerRow.IsValid())
            {
                Debug.Log("GameManager.SubmitAnswer Valid Answer");
                var answerStatus = answerRow.CheckAnswer(solution);

                if (answerStatus == AnswerStatus.Correct)
                {
                    // Win case
                    ShowHideWinMessage(true);

                    // Clear answers
                    ClearAnswers();
                }
                else
                {
                    if (currentAnswerIndex + 1 >= GameManager.TRY_COUNT)
                    {
                        // You lose, exceeded try count
                        ShowHideLoseMessage(true);

                        // Clear answers
                        ClearAnswers();
                    }
                    else
                    {
                        // Create next row
                        GameObject newAnswerRowPrefab = GetNewAnswerRow(currentTopLeft);

                        // Get script
                        AnswerRow newAnswerRow = newAnswerRowPrefab.GetComponent<AnswerRow>();

                        // Init new row with correct answers
                        newAnswerRow.InitCorrectAnswers(answerRow, solution);

                        // Add to answers collection
                        answers[++currentAnswerIndex] = newAnswerRowPrefab;
                    }
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
        ClearAnswers();
        StartNewGame();
    }

    public void ChangeTargetColor(string targetName, string colorName)
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
        var targetMaterial = FindTarget(targetName);

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

    private void ClearAnswers()
    {
        // todo - testing, need to refactor
        GameObject o1 = answers[0];
        if (o1 != null) Destroy(o1);

        GameObject o2 = answers[1];
        if (o2 != null) Destroy(o2);

        GameObject o3 = answers[2];
        if (o3 != null) Destroy(o3);

        GameObject o4 = answers[3];
        if (o4 != null) Destroy(o4);

        GameObject o5 = answers[4];
        if (o5 != null) Destroy(o5);
    }

    private void ShowHideWinMessage(bool show)
    {
        var trophyRenderer = trophy.GetComponent<Renderer>();
        trophyRenderer.enabled = show ? true : false;
    }

    private void ShowHideLoseMessage(bool show)
    {
        var moose = mooseHead.GetComponent<Renderer>();
        moose.enabled = show ? true : false;
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

#region cleanup
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


//public void ChangeTargetColor(string targetName, string colorName)
//{
//    Debug.Log(string.Format("GameManager.ChangeTargetColor target: {0}, color: {1}", targetName, colorName));

//    var targetMaterial = FindTarget(targetName);
//    if (targetMaterial == null)
//    {
//        Debug.Log("GameManager.ChangeTargetColor target is null");
//    }
//    else
//    {
//        switch (colorName)
//        {
//            case "red":
//                {
//                    targetMaterial.color = Color.red;
//                    break;
//                }
//            case "blue":
//                {
//                    targetMaterial.color = Color.blue;
//                    break;
//                }
//            case "green":
//                {
//                    targetMaterial.color = Color.green;
//                    break;
//                }
//            case "yellow":
//                {
//                    targetMaterial.color = Color.yellow;
//                    break;
//                }
//            default:
//                {
//                    break;
//                }
//        }
//    }
//}

//private Material FindTarget(string name)
//{
//    //Debug.Log(string.Format("GameManager.FindTarget name: {0}", name));

//    //switch (name)
//    //{
//    //    case "PositionOne": return item1Material;
//    //    case "PositionTwo": return item2Material;
//    //    case "PositionThree": return item3Material;
//    //    case "PositionFour": return item4Material;
//    //    default: return null;
//    //}
//    return null;
//}
#endregion