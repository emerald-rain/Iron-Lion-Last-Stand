using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletTransform;
    public float timeBetweenFiring;

    private float timer;

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

        // Fire bullet
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && timer > timeBetweenFiring)
        {
            timer = 0;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}
