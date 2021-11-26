using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int GameLevel = 1;
    public DungeonGenerator dg;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        dg.GenerateDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Souls: " + score;
    }

    public void StartNewLevel()
    {
        GameLevel += 1;
        dg.ResetDungeon();
        PlayerStats.Instance.gameObject.transform.position = Vector3.zero;
        dg.GenerateDungeon();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}