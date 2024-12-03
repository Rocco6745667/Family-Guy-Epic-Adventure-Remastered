using UnityEngine;

public class BeerCan : MonoBehaviour
{
    public enum PowerUpType { StaminaRecharge, SpeedBoost, Invincibility }
    public PowerUpType powerUpEffect;
    public float powerUpDuration = 5f; // Duration for temporary effects

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                ApplyPowerUp(player);
                Destroy(gameObject); // Remove the beer can after collection
            }
        }
    }

    private void ApplyPowerUp(PlayerController player)
    {
        switch (powerUpEffect)
        {
            case PowerUpType.StaminaRecharge:
                player.RechargeStamina();
                break;

            case PowerUpType.SpeedBoost:
                player.ActivateSpeedBoost(powerUpDuration);
                break;

            case PowerUpType.Invincibility:
                player.ActivateInvincibility(powerUpDuration);
                break;
        }
    }
}
