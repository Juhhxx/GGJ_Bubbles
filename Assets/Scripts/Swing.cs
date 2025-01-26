using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Swing : MonoBehaviour
{
    [SerializeField] private Transform _batTrans;
    [SerializeField] private float _force;
    [SerializeField] private int _impactTime;
    private YieldInstruction _wfs;
    private Bubble _bubble;
    private PlayerInput _player;
    private Coroutine _impact;

    private void Start()
    {
        _wfs = new WaitForEndOfFrame();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
            return;

        _bubble = other.gameObject.GetComponentInParent<Bubble>();
        // _player = other.gameObject.GetComponent<PlayerInput>();

        if (_bubble != null)
        {
            Debug.Log(_impact == null);

            if (_impact != null)
            {
                Time.timeScale = 1f;
                StopCoroutine(_impact);
            }
            
            Debug.Log(_impact == null);
            
            _impact = StartCoroutine(Impact(_impactTime));
        }
    }
    private void OnTriggerStay(Collider other)
    {
        _bubble = other.gameObject.GetComponentInParent<Bubble>();
        // _player = other.gameObject.GetComponent<PlayerInput>();

        if (_bubble != null)
        {
            ApplyForce();
        }
    }

    private IEnumerator Impact(int waitFrames)
    {
        Debug.Log("IMPACT");

        Time.timeScale = 0.01f;

        for (int i = 0; i < waitFrames; i++)
            yield return _wfs;
        
        Time.timeScale = 1f;

        Debug.Log("IMPACT END");

        _impact = null;
    }
    private void ApplyForce()
    {
            Vector3 finalForce = -(_batTrans.forward) * _force;
            _bubble?.Move(finalForce);
            // _player?.Move(finalForce);
    }

}
