using System.Text;
using UnityEngine;

public class AnswerRow : MonoBehaviour {

    public Renderer[] renderers = null;
    public TextMesh statusTextMesh;

    #region Private Fields

    private GameColor[] colors = null; 

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
        bool incorrect = false;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < GameManager.ANSWER_COUNT; i++)
        {
            if (colors[i] == solution.colors[i])
            {
                sb.Append(string.Format("P{0} correct, ", i + 1));
            }
            else
            {
                sb.Append(string.Format("P{0} incorrect, ", i + 1));
                incorrect = true;
            }
        }

        // Update display string
        this.statusTextMesh.text = sb.ToString();

        if (incorrect) return AnswerStatus.Incorrect;
        return AnswerStatus.Correct;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < GameManager.ANSWER_COUNT; i++)
        {
            if (renderers[i] != null) Destroy(renderers[i]);
        }
    }

    #endregion
}
