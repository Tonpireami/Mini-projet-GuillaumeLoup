using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Explosion")]
    public GameObject explosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        // On vérifie si l'objet qui nous touche est un projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            SpawnExplosion();
            Destroy(gameObject);
        }
    }

    void SpawnExplosion()
    {
        if (explosionPrefab == null)
            return;

        // On instancie l'explosion à la position de la cible
        GameObject explosion = Instantiate(
            explosionPrefab,
            transform.position,
            Quaternion.identity
        );

        // On calcule la durée maximale des particules
        float maxDuration = 0f;
        foreach (var ps in explosion.GetComponentsInChildren<ParticleSystem>())
        {
            if (ps.main.duration > maxDuration)
                maxDuration = ps.main.duration;
        }

        // On détruit l'explosion après la durée totale
        Destroy(explosion, maxDuration + 0.5f);
    }
}
