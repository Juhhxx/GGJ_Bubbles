using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private int[] _points;
    [SerializeField] private GameObject[] _players;
    [SerializeField] private int _roundAmount;
    [SerializeField] private Camera _cam;

    [SerializeField] private GameObject _winObject;
    [SerializeField] private GameObject _scoreObject;
    [SerializeField] private GameObject _pauseObject;


    private void Start()
    {
        _scoreObject.GetComponentInChildren<TMP_Text>().text = $"0 - 0";

        _points = new int[_players.Length];

        for(int i = 0; i < _points.Length; i++)
        {
            _points[i] = 0;
        }
    }

    private void Update()
    {
        if (_winObject.activeSelf) return;

        if (Input.GetKeyUp(KeyCode.Escape))
            _pauseObject.SetActive( ! _pauseObject.activeSelf);
    }

    public void Quit()
    {
        // Olá julia corrige isto pls
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void AddPoint(int playerNum)
    {
        if (playerNum-1 > _points.Length) return;

        _points[playerNum-1]++;

        _scoreObject.GetComponentInChildren<TMP_Text>().text = $"{_points[0]} - {_points[1]}";

        CheckForWin();
    }

    private void CheckForWin()
    {
        int currentWinner = 0;
        int currentPoints = 0;

        // Assume always odd rounds

        for(int i = 0; i < _points.Length; i++)
        {
            if (_points[i] > currentPoints)
            {
                currentWinner = i;
                currentPoints = _points[i];
            }
        }

        if (currentPoints >=_roundAmount/2)
            Win(currentWinner);
    }

    private void Win(int winner)
    {
        StartCoroutine(StartWin(winner));

    }
    private IEnumerator StartWin(int winner)
    {
        _winObject.SetActive(true);

        _winObject.GetComponentInChildren<TMP_Text>().text = $"Player {winner} wins!";

        while ( Vector3.Distance(_cam.transform.position, _players[winner-1].transform.position + new Vector3(0f, 0f, 1f)) < 0.01f )
        {
            _cam.transform.position = Vector3.Lerp(
                _cam.transform.position,
                _players[winner-1].transform.position + new Vector3(0f, 0f, 1f),
                0.65f);

            yield return null;
        }

        Time.timeScale = 0.01f;
    }
}
