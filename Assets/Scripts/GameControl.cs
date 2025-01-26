using System.Collections;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private int[] _points;
    [SerializeField] private GameObject[] _players;
    [SerializeField] private int _roundAmount;
    [SerializeField] private Camera _cam;

    [SerializeField] private GameObject _winObject;

    private void Start()
    {
        _points = new int[_players.Length];

        for(int i = 0; i < _points.Length; i++)
        {
            _points[i] = 0;
        }
    }

    public void AddPoint(int playerNum)
    {
        if (playerNum-1 > _points.Length) return;

        _points[playerNum-1]++;

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
            }
            currentPoints += _points[i];
        }

        if (currentPoints >_roundAmount)
            Win(currentWinner);
    }

    private void Win(int winner)
    {
        StartCoroutine(StartWin(winner));

    }
    private IEnumerator StartWin(int winner)
    {
        _winObject.SetActive(true);
        
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
