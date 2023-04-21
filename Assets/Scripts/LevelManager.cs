using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum LevelState
{
    Playing,
    Won,
    Lost,
}
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Player player;
    [SerializeField]private LevelState state;

    [SerializeField] private GameObject lossUi;
    [SerializeField] private GameObject winUi;

    public LevelState levelState
    {
        get
        {
            return state;
        }
        set
        {
            if(state == LevelState.Playing)
            {
                state = value;
                HandleStateChange(value);
            }
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        lossUi.SetActive(false);
        winUi.SetActive(false);
    }
    private void HandleStateChange(LevelState state)
    {
        if(state == LevelState.Lost)
        {
            lossUi.SetActive(true);
        }else if(state == LevelState.Won)
        {
            winUi.SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(state == LevelState.Lost)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }else if(state == LevelState.Won)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
