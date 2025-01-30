using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Swing : MonoBehaviour
{
    [SerializeField] private Transform _batTrans;
    [SerializeField] public float _force;
    [SerializeField] private int _impactTime;
    [SerializeField] private GameObject _invert;
    private YieldInstruction _wfs;
    private Bubble _bubble;
    private PlayerInput _player;
    private Coroutine _impact;
    private Shaker _shaker;

    private void Start()
    {
        _shaker = FindFirstObjectByType<Shaker>();
        _wfs = new WaitForEndOfFrame();
    }
    private void OnTriggerEnter(Collider other)
    {
        // if (!other.isTrigger)
        //     return;

        _bubble = other.gameObject.GetComponentInParent<Bubble>();
        // _player = other.gameObject.GetComponent<PlayerInput>();

        // Debug.Log("triggered start");

        if (_bubble != null)
        {
            // Debug.Log("triggered");

            // Debug.Log(_impact == null);

            if (_impact == null)
            {
                Debug.Log("Starting");
                _impact = StartCoroutine(Impact(_impactTime));
            }
            
            // Debug.Log(_impact == null);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        _bubble = other.gameObject.GetComponentInParent<Bubble>();
        // _player = other.gameObject.GetComponent<PlayerInput>();

        // Debug.Log("staying start");

        if (_bubble != null)
        {
            // Debug.Log("staying");
            ApplyForce();
        }
    }

    private IEnumerator Impact(int waitFrames)
    {
        Debug.Log("IMPACT");

        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;


        Debug.Log("impact frame ");

        // _invert.SetActive(true);

        yield return new WaitForSecondsRealtime(0.05f);

        _invert.SetActive(false);

        yield return new WaitForSecondsRealtime(0.15f);

        // Restore time scale and fixed delta time
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        
        _shaker.Shake(0.2f, 30f);

        Debug.Log("IMPACT END");

        _impact = null; // Reset the coroutine reference
    }
    private void ApplyForce()
    {
            Vector3 finalForce = -(_batTrans.forward) * _force;
            _bubble?.Move(finalForce);
            // _player?.Move(finalForce);
    }

}
