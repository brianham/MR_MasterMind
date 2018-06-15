using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LuisManager : MonoBehaviour
{
    public static LuisManager instance;

    // MasterMindLUIS 
    string luisEndpoint = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/11c79c29-8289-45ff-9c97-797655aaed8b?subscription-key=1ec44d85ed7a4152b3f4e4a5c3c17bfa&verbose=true&timezoneOffset=0&q=";

    // BrianLanguageUnderstandingService
    //string luisEndpoint = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/e55cc38f-5070-450a-8d2d-8ae4239a6935?subscription-key=e87015cabeeb45dca44eb179d00eb275&verbose=true&timezoneOffset=-480&q=";

    private void Awake()
    {
        Debug.Log("LuisManager.Awake");

        // allows this class instance to behave like a singleton
        instance = this;
    }

    /// <summary>
    /// Call LUIS to submit a dictation result.
    /// </summary>
    public IEnumerator SubmitRequestToLuis(string dictationResult)
    {
        string queryString;

        queryString = string.Concat(Uri.EscapeDataString(dictationResult));

        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(luisEndpoint + queryString))
        {
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return unityWebRequest.SendWebRequest();

            long responseCode = unityWebRequest.responseCode;

            AnalysedQuery analysedQuery = JsonUtility.FromJson<AnalysedQuery>(unityWebRequest.downloadHandler.text);

            //analyse the elements of the response 
            AnalyseResponseElements(analysedQuery);
        }

        MicrophoneManager.instance.StartCapturingAudio();
    }

    public static Stream GenerateStreamFromString(string receivedString)
    {
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(receivedString);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    private void AnalyseResponseElements(AnalysedQuery aQuery)
    {
        if (aQuery == null || aQuery.topScoringIntent == null || aQuery.topScoringIntent.intent == null)
        {
            Debug.Log("LuisManager.AnalyseResponseElements no intent");
            return;
        }

        string topIntent = aQuery.topScoringIntent.intent;
        Debug.Log("LuisManager.AnalyseResponseElements intent:" + topIntent);

        // Create a dictionary of entities associated with their type
        Dictionary<string, string> entityDic = new Dictionary<string, string>();

        foreach (EntityData ed in aQuery.entities)
        {
            if (!entityDic.ContainsKey(ed.type))
            {
                entityDic.Add(ed.type, ed.entity);
            }
        }

        // Depending on the topmost recognised intent, read the entities name
        switch (aQuery.topScoringIntent.intent)
        {
            case "ChangeObjectColorIntent":
                {
                    string target = null;
                    string color = null;

                    foreach (var pair in entityDic)
                    {
                        switch (pair.Key)
                        {
                            case "PositionOne":
                            case "PositionTwo":
                            case "PositionThree":
                            case "PositionFour":
                                {
                                    target = pair.Key;
                                    break;
                                }
                            case "color":
                                {
                                    color = pair.Value;
                                    break;
                                }
                            case "target":
                                {
                                    //todo
                                    break;
                                }

                        }
                    }

                    Debug.Log(string.Format("LuisManager.AnalyseResponseElements target:{0}, color:{1}", target, color));

                    if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(color))
                    {
                        GameManager.instance.ChangeTargetColor(target, color);
                    }

                    break;
                }
            case "ChangeObjectColorShortcutIntent":
                {
                    Debug.Log(string.Format("LuisManager.AnalyseResponseElements.ChangeObjectColorShortcutIntent ", null));
                    GameManager.instance.ChangeTargetColorShortcut("", "", "", "");
                    break;
                }
            case "GameStartIntent":
                {
                    GameManager.instance.StartNewGame();
                    break;
                }
            case "SubmitAnswerIntent":
                {
                    GameManager.instance.SubmitAnswer();
                    break;
                }
        }
    }
}

[System.Serializable] //this class represents the LUIS response
public class AnalysedQuery
{
    public TopScoringIntentData topScoringIntent;
    public EntityData[] entities;
    public string query;
}

// This class contains the Intent LUIS determines 
// to be the most likely
[System.Serializable]
public class TopScoringIntentData
{
    public string intent;
    public float score;
}

// This class contains data for an Entity
[System.Serializable]
public class EntityData
{
    public string entity;
    public string type;
    public int startIndex;
    public int endIndex;
    public float score;
}


//private void AnalyseResponseElements(AnalysedQuery aQuery)
//{
//    string topIntent = aQuery.topScoringIntent.intent;

//    // Create a dictionary of entities associated with their type
//    Dictionary<string, string> entityDic = new Dictionary<string, string>();

//    foreach (EntityData ed in aQuery.entities)
//    {
//        entityDic.Add(ed.type, ed.entity);
//    }

//    // Depending on the topmost recognised intent, read the entities name
//    switch (aQuery.topScoringIntent.intent)
//    {
//        case "ChangeObjectColor":
//            string targetForColor = null;
//            string color = null;

//            foreach (var pair in entityDic)
//            {
//                if (pair.Key == "target")
//                {
//                    targetForColor = pair.Value;
//                }
//                else if (pair.Key == "color")
//                {
//                    color = pair.Value;
//                }
//            }

//            Behaviours.instance.ChangeTargetColor(targetForColor, color);
//            break;

//        case "ChangeObjectSize":
//            string targetForSize = null;
//            foreach (var pair in entityDic)
//            {
//                if (pair.Key == "target")
//                {
//                    targetForSize = pair.Value;
//                }
//            }

//            if (entityDic.ContainsKey("upsize") == true)
//            {
//                Behaviours.instance.UpSizeTarget(targetForSize);
//            }
//            else if (entityDic.ContainsKey("downsize") == true)
//            {
//                Behaviours.instance.DownSizeTarget(targetForSize);
//            }
//            break;
//    }
//}

//if (pair.Key == "target")
//{
//    targetForColor = pair.Value;
//}
//else if (pair.Key == "color")
//{
//    color = pair.Value;
//}