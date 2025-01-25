using UnityEngine;

public class InputDebug : MonoBehaviour
{
    [SerializeField] private string _playerNumber;
    private string _verticalAxis;
    private string _horizontalAxis;
    private string _aimXAxis;
    private string _aimYAxis;
    private string _fireButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        _verticalAxis   = "MoveY" + _playerNumber;
        _horizontalAxis = "MoveX" + _playerNumber;
        _aimYAxis       = "AimY" + _playerNumber;
        _aimXAxis       = "AimX" + _playerNumber;
        _fireButton     = "Fire" + _playerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Player {_playerNumber}");
        Debug.Log($"Move\nHorizontal : {Input.GetAxis(_horizontalAxis)} Vertical : {Input.GetAxis(_verticalAxis)}");
        Debug.Log($"Aim\nHorizontal : {Input.GetAxis(_aimXAxis)} Vertical : {Input.GetAxis(_aimYAxis)}");
        Debug.Log($"Fire : {Input.GetButton(_fireButton)}");
    }
}
