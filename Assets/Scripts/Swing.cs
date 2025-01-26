using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] private Transform _batTrans;
    [SerializeField] private float _force;
    private void OnTriggerStay(Collider other)
    {
        Bubble bubble = other.gameObject.GetComponentInParent<Bubble>();
        if (bubble != null)
        {
            Vector3 finalForce = -(_batTrans.forward) * _force;
            bubble.Move(finalForce);
        }
    }

}
