namespace Manasoup.Interfaces
{
    public interface IDamageable
    {
        public int Health { get; set; }

        public void TakeDamage(int damage);

        public void Die();

        public void Heal();

        public void Heal(int health);
    }
}