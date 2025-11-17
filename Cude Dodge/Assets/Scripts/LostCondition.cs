using System;
using UnityEngine;

public class LostCondition : MonoBehaviour
{
    private PlayerManager playerManager;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    public void PlayerLost()
    {
        AdvertiseManager advertiseManager = playerManager.Admin.GetComponent<AdvertiseManager>();
        
        advertiseManager.AdvertiseStart();
    }

    public void PlayerRevive()
    {
        Player player = GetComponent<Player>();

        player.HealthSet(player.MaxHealth);
    }
}
