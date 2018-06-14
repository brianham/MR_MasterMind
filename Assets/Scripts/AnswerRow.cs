﻿using System.Collections;
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

    #region Constructor

    public AnswerRow(GameColor color1, GameColor color2, GameColor color3, GameColor color4)
    {
        colors = new GameColor[GameManager.ANSWER_COUNT];
        colors[0] = color1;
        colors[1] = color2;
        colors[2] = color3;
        colors[3] = color4;
    }

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
        colors = new GameColor[GameManager.ANSWER_COUNT];

        // Init materials to black
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = Color.black;
            colors[i] = GameColor.NotSet;
        }
    }

    public void SetByPosition(int index, GameColor color)
    {
        if (index < GameManager.ANSWER_COUNT) colors[index] = color;
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
        if (colors == null) return false;
        for (int i = 0; i < GameManager.ANSWER_COUNT; i++)
        {
            if (colors[i] == GameColor.NotSet) return false;
        }

        return true;
    }
    
    public AnswerStatus CheckAnswer(AnswerRow solution)
    {
        // todo - set status string
        return AnswerStatus.Incorrect;
    }

    #endregion
}
