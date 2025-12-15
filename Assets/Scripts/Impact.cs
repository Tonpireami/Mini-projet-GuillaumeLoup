using UnityEngine;

public class Impact : MonoBehaviour
{
    public GameObject impactPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        // On vérifie si l'objet touché a le tag "Cible"
        if (collision.gameObject.CompareTag("Cible"))
        {
            // On spawn l'effet d'impact à l'endroit du contact
            Instantiate(impactPrefab, collision.contacts[0].point, Quaternion.identity);
            // On détruit toujours la balle après collision
            Destroy(gameObject);
        }
    }
}
