using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private Transform pfHealthBar;
    [SerializeField] private float maxWidth = 0.8f;
    [SerializeField] private float fixedHeight = 0.1f;
    [SerializeField] private Vector3 healthBarOffset = new Vector3(0f, 1f, 0f);
    [SerializeField] private int maxHealth = 100;

    public GameOverScreen GameOverScreen;

    public HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = new HealthSystem(maxHealth);
        CreateHealthBar();
    }

    private void CreateHealthBar()
    {
        Vector3 offset = healthBarOffset;
        Transform healthBarTransform = Instantiate(pfHealthBar, transform.position + offset, Quaternion.identity, transform);
        HealthBar healthBar = healthBarTransform.GetComponentInChildren<HealthBar>();
        healthBar.Setup(healthSystem, maxWidth, fixedHeight);
    }

    public void TakeDamage(int damage) 
    {
        healthSystem.Damage(damage);

        if (healthSystem.GetHealth() <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
