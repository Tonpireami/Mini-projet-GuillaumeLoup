using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public GameSequence gameSequence;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.name.Contains("WeaponZone"))
                gameSequence.OnEnterWeaponZone();
            else if (gameObject.name.Contains("ObjectifZone"))
                gameSequence.OnEnterObjectifZone();
        }
    }
}