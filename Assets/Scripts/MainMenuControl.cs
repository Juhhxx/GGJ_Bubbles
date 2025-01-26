using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonsActivate;
    [SerializeField] private GameObject[] _buttonsDeactivate;
    [SerializeField] private Animator[] _buttonsAnimators;
    [SerializeField] private GameObject _impact;
    private int _button;

    private void Start()
    {
        _impact.SetActive(false);

        _button = 0;
        UpdateButtons();

    }
    private void Update()
    {
        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
        {
            _buttonsAnimators[_button].Play("Click");
        }


        if (Input.GetAxis("MoveX1") > 0.9f || Input.GetAxis("MoveX2") > 0.9f)
        {
            _button --;
            UpdateButtons();
        }
        else if (Input.GetAxis("MoveX1") < 0.9f || Input.GetAxis("MoveX2") < 0.9f)
        {
            _button ++;
            UpdateButtons();
        }
    }
    private void UpdateButtons()
    {
        _button = Mathf.Clamp(_button, 0, _buttonsActivate.Length-1);

        for ( int i = 0; i < _buttonsActivate.Length ; i++ )
        {
            if (i == _button)
            {
                _buttonsActivate[i].SetActive(true);
                _buttonsDeactivate[i].SetActive(false);
            }
            else
            {
                _buttonsActivate[i].SetActive(false);
                _buttonsDeactivate[i].SetActive(true);
            }
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
