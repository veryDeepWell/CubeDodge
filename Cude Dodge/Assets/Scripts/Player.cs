using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject HealthText;

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float moveSpeed;

    [SerializeField] private int Health;
    public int MaxHealth;
    [SerializeField] private int HealthDecayInterval;
    [SerializeField] private int HealthDecayDamage;
    
    [SerializeField] private ParticleSystem particle;
    private ParticleSystem particleInstance;
    
    private LostCondition lostCondition;
    private ColorChanger colorChanger;
    
    private IEnumerator _healthDecay;

    private void Awake()
    {
        MaxHealth = Health;
        particleInstance = Instantiate(particle, transform.position - new Vector3(0f, 0f, 1f), Quaternion.identity);
        
        lostCondition = GetComponent<LostCondition>();
        colorChanger = GetComponent<ColorChanger>();
        
        _healthDecay = HealthDecayRoutine();
    }

    private void Start()
    {
        colorChanger.ParameterInit(0, MaxHealth);
        
        HealthUpdate();
        
        StartCoroutine(_healthDecay);
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = moveAction.action.ReadValue<Vector2>();
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        particleInstance.transform.position = transform.position;
    }

    public void HealthDown(int downAmount)
    {
        Health -= downAmount;
        HealthUpdate();

        if (Health <= 0) {lostCondition.PlayerLost();}
    }

    public void HealthUp(int upAmount)
    {
        Health += upAmount;
        if (Health > MaxHealth) {Health = MaxHealth;}
        HealthUpdate();
    }
    
    public void HealthSet(int setAmount)
    {
        Health = setAmount;
        HealthUpdate();
    }

    private void HealthUpdate()
    {
        HealthText.GetComponent<TextMeshPro>().text = Health.ToString();
        colorChanger.ChangeWithParameter(Health);
    }

    private IEnumerator HealthDecayRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(HealthDecayInterval);
        
            HealthDown(HealthDecayDamage);
        }
    }
}
