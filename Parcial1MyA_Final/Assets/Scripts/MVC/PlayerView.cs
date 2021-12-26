using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public Image cooldownBar;
    public float shootCooldown;
    Coroutine _shootCDCor;
    private AudioSource audioSource;

    public AudioClip soundMove;
    public AudioClip soundDeath;
    public AudioClip soundFire;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StarFireCooldown()
    {
        _shootCDCor = StartCoroutine(ShootCooldown());
    }

    public void StopFireCooldown()
    {
        if (_shootCDCor != null)
        {
            StopCoroutine(_shootCDCor);
        }
        CompletedFireCooldown();
    }

    void CompletedFireCooldown()
    {
        cooldownBar.color = Color.green;
        cooldownBar.fillAmount = 1;
    }

    IEnumerator ShootCooldown()
    {
        float ticks = 0;

        cooldownBar.color = Color.red;
        cooldownBar.fillAmount = 0;

        while (ticks < shootCooldown)
        {
            ticks += Time.deltaTime;
            cooldownBar.fillAmount = ticks;
            yield return null;
        }

        CompletedFireCooldown();
    }

    public void FireSound()
    {
        if (soundFire is null)
            return;

        audioSource.clip = soundFire;
        audioSource.Play();
    }

    public void MoveSound()
    {
        if (soundMove is null)
            return;

        if (!audioSource.isPlaying)
        {
            audioSource.clip = soundMove;

            audioSource.PlayOneShot(soundMove);
        }
    }

    public void DeathSound()
    {
        if (soundMove is null)
            return;

        audioSource.clip = soundDeath;
        audioSource.Play();
    }
}
