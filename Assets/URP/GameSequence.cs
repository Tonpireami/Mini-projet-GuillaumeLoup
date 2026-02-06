using UnityEngine;
using TMPro;
using System.Collections;

public class GameSequence : MonoBehaviour
{
    public TMP_Text uiText;

    public GameObject Ecran;

    public GameObject WeaponZone;
    public GameObject ObjectifZone;

    public AudioClip fireSound;
    public AudioSource audioSource;

    public float fadeDuration = 1f;
    public float delayAfterStart = 3f;

    private bool weaponZoneActivated = false;
    private bool objectifZoneActivated = false;

    void Start()
    {
        WeaponZone.SetActive(false);
        ObjectifZone.SetActive(false);

        StartCoroutine(SequenceCoroutine());
    }

    IEnumerator SequenceCoroutine()
    {
        yield return FadeText("Eh voilà, encore un des drones du gouvernement...\nIls ne nous lâchent plus...\nMais on en a marre de se laisser faire !");

        yield return new WaitForSeconds(delayAfterStart);

        yield return FadeText("J'ai caché un pistolet par ici...\nJe vais pas le louper !");
        WeaponZone.SetActive(true);
        weaponZoneActivated = true;
    }

    public void OnEnterWeaponZone()
    {
        if (!weaponZoneActivated) return;

        StartCoroutine(FadeText("Eh voilà !\nMais je devrais rapidement me rendre dans une zone \noù les ondes passent mal, \navant que le drone n'envoie une alerte !"));
        ObjectifZone.SetActive(true);
        objectifZoneActivated = true;

        WeaponZone.SetActive(false);
    }

    public void OnEnterObjectifZone()
    {
        if (!objectifZoneActivated) return;
        StartCoroutine(EndSequence());
        ObjectifZone.SetActive(false);
    }

    IEnumerator FadeText(string message)
    {
        Color c = uiText.color;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            uiText.color = c;
            yield return null;
        }
        c.a = 0f;
        uiText.color = c;

        uiText.text = message;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            uiText.color = c;
            yield return null;
        }
        c.a = 1f;
        uiText.color = c;
    }

    IEnumerator EndSequence()
    {
        Color c = uiText.color;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            uiText.color = c;
            yield return null;
        }
        uiText.color = new Color(c.r, c.g, c.b, 0f);

        Ecran.SetActive(true);

        yield return new WaitForSeconds(2f);

        if (audioSource != null && fireSound != null)
            audioSource.PlayOneShot(fireSound);

        yield return null;
    }
}
