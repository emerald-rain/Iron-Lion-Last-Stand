using System;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    // private Animator aimAnimator;

    private void Awake() {
        aimTransform = transform.Find("Aim");
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        // aimAnimator = aimTransform.GetComponent<Animator>();
    }

    private void Update() {
        HandleAiming();
        HandleShooting();
    }

    private Vector3 GetMouseWorldPosition() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z; // Set the z-coordinate to match the camera's position
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void HandleAiming() {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void HandleShooting() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            Debug.Log("Shooting");
            // aimAnimator.SetTriggering("Shoot");

            OnShoot?.Invoke(this, new OnShootEventArgs{
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mouseWorldPosition,
            });
        }
    }
}
