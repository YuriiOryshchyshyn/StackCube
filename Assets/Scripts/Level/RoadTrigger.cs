using UnityEngine;
using UnityEngine.Events;

public class RoadTrigger : MonoBehaviour
{
    public event UnityAction RoadInTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road"))
        {
            RoadInTrigger?.Invoke();
        }
    }
}
