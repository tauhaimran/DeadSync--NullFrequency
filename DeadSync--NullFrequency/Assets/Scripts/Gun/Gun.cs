public abstract class Gun : MonoBehaviour
{
    public float damage;
    public float fireRate;
    public AudioSource sound;
    public ParticleSystem muzzleFx;

    public virtual void Shoot()
    {
        muzzleFx?.Play();
        sound?.PlayOneShot(sound.clip);
    }
}

public class Pistol : Gun
{
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Pistol Fired");
    }
}
public class Shotgun : Gun
{
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Shotgun pellets fired");
    }
}
public class Sniper : Gun
{
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Sniper Boom!");
    }
}
