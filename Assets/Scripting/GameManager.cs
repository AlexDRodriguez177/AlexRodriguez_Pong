using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    //Score variables 
    [SerializeField] private List<int> playerScores;
    [SerializeField] private List<TextMeshProUGUI> playerScoreTexts;
    public static GameManager instance;
    /// <summary>
    /// Initalizes some logic to make sure only one GameManager exists in the scene.
    /// </summary>
    void Start()
    {
        // destroys any existing GameManager instance in the scene
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    /// <summary>
    /// Changes the players score
    /// Stores it was an integer and adds 1 each goal
    /// Stores the score integer into a TextMeshProUGUI object
    /// Converts the integer into a string to display it in the UI
    /// </summary>
    public static void IncrementScore(PlayerType playerType)
    {
        if (instance == null) return;
        
        if (instance.playerScoreTexts.Count <= (int) playerType) return;
        
        instance.playerScores[(int) playerType]++;
        TextMeshProUGUI scoreText = instance.playerScoreTexts[(int)playerType];
        scoreText.text = instance.playerScores[(int)playerType].ToString();
    }

}
