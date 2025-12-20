using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject HealthText;

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float moveSpeed;

    [SerializeField] private int health;
    public int MaxHealth;
    [SerializeField] private int HealthDecayInterval;
    [SerializeField] private int HealthDecayDamage;
    
    [SerializeField] private ParticleSystem particle;
    private ParticleSystem particleInstance;
    
    private LostCondition lostCondition;
    private ColorChanger colorChanger;
    
    private IEnumerator _healthDecay;

    private WaitForSeconds healthDelayYield;

    [SerializeField] private MainAnalyser analyser;

    private void Awake()
    {
        MaxHealth = health;
        particleInstance = Instantiate(particle, transform.position - new Vector3(0f, 0f, 1f), Quaternion.identity);
        
        lostCondition = GetComponent<LostCondition>();
        colorChanger = GetComponent<ColorChanger>();
        
        _healthDecay = HealthDecayRoutine();
        
        //analyser = GetComponent<MainAnalyser>();
        
        healthDelayYield = new WaitForSeconds(HealthDecayInterval);
    }

    private void Start()
    {
        analyser.InitializeAnalytics("mama", "papa");
        analyser.TrackGameStart();
        
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

    public void HealthDown(int downAmount, string reasoner)
    {
        health -= downAmount;
        HealthUpdate();
        
        //analyser.InitializeAnalytics("mama", "papa");
        analyser.TrackEvent("player_damage", new Dictionary<string, string> { { "reason", reasoner } });
        
        if (health <= 0) {lostCondition.PlayerLost();}
    }

    public void HealthUp(int upAmount)
    {
        health += upAmount;
        if (health > MaxHealth) {health = MaxHealth;}
        HealthUpdate();
    }
    
    public void HealthSet(int setAmount)
    {
        health = setAmount;
        HealthUpdate();
    }

    private void HealthUpdate()
    {
        HealthText.GetComponent<TextMeshPro>().text = health.ToString();
        colorChanger.ChangeWithParameter(health);
    }
    
    private IEnumerator HealthDecayRoutine()
    {
        while (true)
        {
            yield return healthDelayYield;
        
            HealthDown(HealthDecayDamage, "withering");
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        analyser.FlushAnalytics();
    }
}
