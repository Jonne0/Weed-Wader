public interface IDamagable
{
    float Health { get; set; }

    void TakeDamage(float amount);
}
