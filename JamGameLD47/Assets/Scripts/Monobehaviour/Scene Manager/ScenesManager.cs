using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenesManager : MonoBehaviour
{
    [Header("Название следующего уровня для загрузки")]
    [SerializeField] private string nextLevelName;

    [Header("Название заставки при старте")]
    [SerializeField] private GameObject splashScreenStart;

    [Header("Название заставки при концовке")]
    [SerializeField] private GameObject splashScreenEnd;

    [Header("Место спавна персонажа")]
    [SerializeField] private Transform respawn;

    [Header("Время, через которое начнется движение камеры")]
    [SerializeField] private float startTime = 1f;

    public static Action<bool> MoveCamera;

    private int sceneIndex = 0;

    private float waitInterval = 2f;

    private void Awake()
    {

        Instantiate(Resources.Load("Player", typeof(GameObject)), respawn.position, Quaternion.identity);

        StartCoroutine(StartLevel());

        splashScreenStart.SetActive(true);

        PlayerController.PlayerDeath += RestartLevel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            splashScreenEnd.SetActive(true);
            StartCoroutine(LoadLevel());
            
        }
    }

    IEnumerator LoadLevel ()
    {
        yield return new WaitForSeconds(waitInterval);
        if (nextLevelName != null)
        {
            splashScreenEnd.SetActive(true);
            SceneManager.LoadScene(nextLevelName);
        }
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(10f);
        if (MoveCamera != null) MoveCamera(true);
        
    }

    private void OnDisable()
    {

        PlayerController.PlayerDeath -= RestartLevel;
    }
    private void RestartLevel()
    {
        StartCoroutine(ReloadLevel());
        splashScreenEnd.SetActive(true);
    }
    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(waitInterval);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
