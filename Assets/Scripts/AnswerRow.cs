using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerRow : MonoBehaviour {

    // single prefab
    // 4 materials
    // 4 colors

    public Renderer[] renderers = null;

    #region Private Fields

    //private GameObject answerRowPrefab = null;    
    private GameColor[] colors = null;
    private string statusString = string.Empty;

    #endregion

    #region Public Properties

    public string StatusString
    {
        private set { this.statusString = value; }
        get { return ""; }
    }

    #endregion

    #region Methods

    private void Awake()
    {
        // instantiate prefab from game manager object reference
        //renderers = new Renderer[GameManager.TRY_COUNT];
        colors = new GameColor[GameManager.TRY_COUNT];

        // Init materials to black
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = Color.black;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
    public void SetByPosition(int index, GameColor color)
    {
        if (index < GameManager.TRY_COUNT)
        {
            colors[index] = color;
            //materials[index] = GetMaterialForColor(color);
        }
    }

    public void InitializeAsSolution(GameColor color1, GameColor color2, GameColor color3, GameColor color4)
    {

    }

    public Material GetMaterialForColor(GameColor color)
    {
        return null;
        //switch (color)
        //{
        //    case GameColor.Blue: return item1Material;
        //    case GameColor.Green: return item2Material;
        //    case GameColor.Red: return item3Material;
        //    case GameColor.Yellow: return item4Material;
        //    default: return null;
        //}
    }

    public bool IsValid()
    {
        return false;
    }


    public AnswerStatus CheckAnswer(AnswerRow solution)
    {
        // todo - set status string
        return AnswerStatus.Incorrect;
    }

    #endregion
}
