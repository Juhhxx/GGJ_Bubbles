using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField] private PlayerInput _player1;
    [SerializeField] private PlayerInput _player2;

    [SerializeField] private GameObject _spikes;
    [SerializeField] private GameObject _bounce;

    [SerializeField] public int point1;
    [SerializeField] public int point2;
    [SerializeField] private int _roundAmount;
    [SerializeField] private Camera _cam;

    [SerializeField] private GameObject _winObject;
    [SerializeField] private GameObject _scoreObject;
    [SerializeField] private GameObject _pauseObject;


    private void Start()
    {
        _scoreObject.GetComponentInChildren<TMP_Text>().text = $"0 - 0";
    }

    private void Update()
    {
        if (_winObject.activeSelf) return;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Activate pause");
            _pauseObject.SetActive( ! _pauseObject.activeSelf);

            Time.timeScale = _pauseObject.activeSelf ? 0f : 1f;
            Time.fixedDeltaTime = _pauseObject.activeSelf ? 0.02f * Time.timeScale : 0.02f;

            _player1.enabled = ! _pauseObject.activeSelf;
            _player2.enabled = ! _pauseObject.activeSelf;
        }
    
        point1 = _player1.Points;
        point2 = _player2.Points;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeScene(string scene)
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        SceneManager.LoadScene(scene);
    }

    public void AddPoint(int player)
    {
        Debug.Log("adding to player " + player);

        if (player == 1)
            _player1.Points++;
        else if (player == 2)
            _player2.Points++;
        
        if (_player2.Points + _player1.Points == 1)
            _bounce.SetActive(true);

        if (_player2.Points + _player1.Points == 2)
            _spikes.SetActive(true);


        _scoreObject.GetComponentInChildren<TMP_Text>().text = $"{_player1.Points} - {_player2.Points}";

        CheckForWin();
        DoReset();
    }

    private void CheckForWin()
    {

        if (_player1.Points >= _roundAmount)
            Win(_player1);
        if (_player2.Points >= _roundAmount)
            Win(_player2);
    }

    private void Win(PlayerInput winner)
    {
        Debug.Log("Start Winning");
        StartCoroutine(StartWin(winner));
    }
    private IEnumerator StartWin(PlayerInput winner)
    {
        _player1.enabled = false;
        _player2.enabled = false;

        _winObject.SetActive(true);

        // _winObject.GetComponentInChildren<TMP_Text>().text = $"Player {winner.PlayerNumber} wins!";

        Debug.Log("dis: " + Vector3.Distance(_cam.transform.position, winner.transform.position + new Vector3(0f, 80f, 1f)));

        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        while ( Vector3.Distance(_cam.transform.position, winner.transform.position + new Vector3(0f, 80f, 0f)) > 0.1f )
        {
            Debug.Log("dis: " + Vector3.Distance(_cam.transform.position, winner.transform.position + new Vector3(0f, 80f, 1f)));

            _cam.transform.position = Vector3.Lerp(
                _cam.transform.position,
                winner.transform.position + new Vector3(0f, 80f, 1f),
                0.05f);

            yield return null;
        }
    }
    private void DoReset()
    {
        StartCoroutine(Reset());
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1f);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        _player1.ResetPlayer();
        _player2.ResetPlayer();
    }
}
