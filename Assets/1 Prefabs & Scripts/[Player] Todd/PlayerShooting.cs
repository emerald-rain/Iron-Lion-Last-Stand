using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bullet;
    public Transform bulletTransform;

    [Header("Weapon Settings")]
    public float timeBetweenFiring;
    public SoundEffectsPlayer soundEffectsPlayer;

    [Header("Reload System")]
    public float reloadTime;
    public int maxCharge;

    [Header("Reload System")]
    public Transform ammoDisplay;
    public GameObject ammoPrefab;
    public SoundEffectsPlayer soundEffectsPlayerReload;
    public TMP_Text hintText;

    private float timer;
    private int shotsFired;
    private bool isReloading = false;

    void Update() {
        HandleRotation(); // Rotating the player's weapon around
        HandleMouseFlip(); // Weapon flip when the character turns
        UpdateAmmoDisplay(); // Update the ammo indicator

        // If reloading is in progress, stop executing further
        if (isReloading) { return; }

        HandleShootingInput();
        HandleReloadInput();
    }

    void HandleRotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float rotZ = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void HandleMouseFlip()
    {
        bool isMouseToLeft = Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x;
        Vector3 scale = transform.localScale;
        scale.y = isMouseToLeft ? -1 : 1;
        transform.localScale = scale;
    }

    void HandleShootingInput()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && timer > timeBetweenFiring)
        {
            timer = 0;
            soundEffectsPlayer.PlayRandom();
            Vector3 spawnPosition = bulletTransform.position + bulletTransform.right;
            Instantiate(bullet, spawnPosition, bulletTransform.rotation);
            shotsFired++;

            if (shotsFired >= maxCharge)
            {
                StartCoroutine(Reload());
            }
        }
    }

    void HandleReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.R) && shotsFired > 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        RemoveHint();
        soundEffectsPlayerReload.PlayRandom();
        yield return new WaitForSeconds(reloadTime);
        shotsFired = 0;
        isReloading = false;
        UpdateAmmoDisplay();
    }

    public void RemoveHint()
    {
        hintText.gameObject.SetActive(false);
    }

    void UpdateAmmoDisplay()
    {
        // Clear existing ammo display objects
        foreach (Transform child in ammoDisplay)
        {
            Destroy(child.gameObject);
        }

        // Create new ammo display objects based on weapon charge
        for (int i = 0; i < maxCharge - shotsFired; i++)
        {
            Instantiate(ammoPrefab, ammoDisplay);
        }
    }
}
