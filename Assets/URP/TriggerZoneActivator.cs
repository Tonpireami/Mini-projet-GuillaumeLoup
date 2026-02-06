using UnityEngine;

public class ZoneActivator : MonoBehaviour
{
    public MonoBehaviour playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.enabled = false;
        }
    }
}
