using UnityEngine;

// Useless shit
public class AdvertiseManager : MonoBehaviour
{
    [SerializeField] private Player player;
    
    public void AdvertiseStart()
    {
        Debug.Log("Advertise Started");
        // Something happened with add
        AdvertiseEnd(1);
    }

    public void AdvertiseEnd(int result)
    {
        switch (result)
        {
            case 1:
                Debug.Log("Advertise ended Fully, reward given");
                player.GetComponent<LostCondition>().PlayerRevive();
                break;
            case 2:
                Debug.Log("Advertise ended not fully, reward skipped");
                break;
        }
    }
}
