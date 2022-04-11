using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Buk.PhysicsLogic.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Buk.PhysicsLogic.Implementation
{

  public class PhysicsGun : MonoBehaviour, IGun
  {
    public int bulletKillDelay = 2; //This implies a delay of 2 seconds.
 
    public GameObject bulletType;
    public InputAction trigger;
    public bool inputEnabled;
    public AudioSource audioSource;
    public AudioClip audioClip;
    // How fast the bullet is launched
    public float muzzleVelocity = 10.0f;
    // Seconds before you can shoot a new bullet.
    public float coolDown = .25f;
    // At what time was the last bullet shot.
    private float lastShotTime = 0.0f;

    private void TriggerPressed(InputAction.CallbackContext _)
    {
      if (inputEnabled)
        Shoot();
    }

    public void Awake()
    {

      if (bulletType == null)
      {
        throw new Exception("You must add a bullet type!");
      }

      if (bulletType.GetComponent<Rigidbody>() == null)
      {
        throw new Exception("Your bullet must have a Rigidbody to use it with this gun!");
      }

      if (trigger != null)
      {
        trigger.performed += TriggerPressed;
        trigger.Enable();
      }
    }

    public void Shoot()
    {
      if (Time.fixedTime - lastShotTime < coolDown)
      {
        // If the time since the last shot is less than the cooldown, we can't shoot yet. Do nothing.
        return;
      }
      audioSource.PlayOneShot(audioClip);
      // Create a new copy of bulletType using the gun's position and rotation.
      GameObject bulletRef = Instantiate(bulletType, transform.position, transform.rotation) as GameObject;
        // Get the Rigidbody of that bullet, so that we can apply physics to it.
      bulletRef.GetComponent<Rigidbody>()
        // Apply velocity to the bullet's body, relative to its current position and rotation
        .AddRelativeForce(0f, muzzleVelocity, 0f, ForceMode.VelocityChange);
      
      DestroyItForMe(bulletRef);
    }

    private async void DestroyItForMe(GameObject ObjRef)
    {
      await Task.Delay(bulletKillDelay*1000);
      Destroy(ObjRef);
    }

    public void OnDestroy()
    {
      if (trigger != null)
      {
        trigger.performed -= TriggerPressed;
      }
    }
  }
}
