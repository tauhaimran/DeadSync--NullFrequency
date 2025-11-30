using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Collectable : MonoBehaviour
{
    public enum Type {Health, Ammo, Coin}
    public Type type;
    public int value;

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Player>(out var p)) return;
        
       // switch(type)
       // {
       //     case Type.Health: p.Heal(value); break;
       //     case Type.Ammo: p.AddAmmo(value); break;
       //     case Type.Coin: SaveGame.Coins += value; break;
       // }
      //  Destroy(gameObject);
    }
}
