using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bullet;
    public Transform bulletTransform;

    [Header("Weapon Settings")]
    public float timeBetweenFiring;
    public SoundEffectsPlayer soundEffectsPlayer;

    [Header("Reaload System")]
    public float reloadTime;
    public int shotsBeforeReload;

    private float timer;
    private int shotsFired;

    private bool isReloading = false;

    void Update()
    {
        // Rotate gun towards mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float rotZ = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        // Flip weapon if mouse is to the left of the player
        bool isMouseToLeft = mousePos.x < transform.position.x;
        Vector3 scale = transform.localScale;
        scale.y = isMouseToLeft ? -1 : 1;
        transform.localScale = scale;

        // If reloading is in progress, stop executing further
        if (isReloading) { return; }

        // Fire bullet [LMB]
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && timer > timeBetweenFiring)
        {
            timer = 0;
            soundEffectsPlayer.PlayRandom();
            Vector3 spawnPosition = bulletTransform.position + bulletTransform.right;
            Instantiate(bullet, spawnPosition, bulletTransform.rotation);
            shotsFired++;

            // Check for reload
            if (shotsFired >= shotsBeforeReload)
            {
                StartCoroutine(Reload());
            }
        }

        // Reload now [R]
        if (Input.GetKeyDown(KeyCode.R) && shotsFired > 0 && !isReloading) {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        shotsFired = 0;
        isReloading = false;
    }
}
