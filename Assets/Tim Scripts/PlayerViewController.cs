using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewController : MonoBehaviour
{
    public SpriteRenderer baseSprite;
    public SpriteRenderer overlaySprite;
    public Animator overlayAnimator;
    public List<Sprite> spriteList;

    public SpriteRenderer thrusterSprite;
    public AudioSource thrusterAudioSource;
    public AudioClip thrusterAudioClip;

    public StatBar statbar;
    public Sprite statbarSprite_Base;
    public Sprite statbarSprite_WarpOnly;
    public Sprite statbarSprite_GunOnly;
    public Sprite statbarSprite_All;

    private PlayerShipController shipController;

    public bool playerIsDead = false;

	void Start ()
    {
        shipController = GetComponent<PlayerShipController>();
        overlayAnimator = overlaySprite.gameObject.GetComponent<Animator>();
        overlaySprite.enabled = false;

        UpdateSprite();
    }

    void Update()
    {
        if (playerIsDead)
            return;

        UpdateThrusters();
        UpdateSprite();
    }

    public void UpdateThrusters()
    {
        if (!(shipController.engine is NoEngine))
        {
            bool usingThrusters = shipController.usingThrusters;
            if (usingThrusters && !thrusterSprite.enabled)
                thrusterAudioSource.Play();
            else if (!usingThrusters && thrusterSprite.enabled)
            {
                thrusterAudioSource.Stop();
                AudioSource.PlayClipAtPoint(thrusterAudioClip, transform.position);
            }

            thrusterSprite.enabled = usingThrusters;
        }
    }

    public void OnPlayerDeath()
    {
        baseSprite.enabled = false;
        overlaySprite.sprite = baseSprite.sprite;
        overlaySprite.enabled = true;
        overlayAnimator.enabled = false;
        overlayAnimator.enabled = true;
        overlayAnimator.Play("player_death");
        thrusterSprite.enabled = false;
        playerIsDead = true;
    }

    public void UpdateSprite()
    {
        if (playerIsDead)
            return;

        if (shipController.hull <= 0)
        {
            OnPlayerDeath();
            return;
        }

        Sprite currentSprite = baseSprite.sprite;
        Sprite nextSprite;

        bool hasSensor = (shipController.GetComponent<Hyperdrive>() != null);
        bool hasGun = (shipController.GetComponent<Guns>() != null);
        if (hasSensor && !hasGun)
        {
            statbar.GetComponent<Image>().sprite = statbarSprite_WarpOnly;
        }
        else if (!hasSensor && hasGun)
        {
            statbar.GetComponent<Image>().sprite = statbarSprite_GunOnly;
        }
        else if (hasSensor && hasGun)
        {
            statbar.GetComponent<Image>().sprite = statbarSprite_All;
        }
        else
        {
            statbar.GetComponent<Image>().sprite = statbarSprite_Base;
        }

        bool hasShield = (shipController.GetComponent<ShieldGenerator>() != null);
        int engineLevel = 0;
        if (shipController.engine is DamagedEngine)
            engineLevel = 1;
        else if (shipController.engine is FullEngine)
            engineLevel = 2;
        bool damagedHull = (shipController.hull <= 2);

        if (damagedHull)
        {
            nextSprite = spriteList[0]; // Completely broken
            if (engineLevel >= 1) // Differ between engine level?
            {
                nextSprite = spriteList[4]; // Engine only
                if (hasSensor)
                {
                    nextSprite = spriteList[6]; // Engine and sensor
                }
            }
            else
            {
                if (hasSensor)
                {
                    nextSprite = spriteList[8]; // Sensor only
                }
            }
        }
        else
        {
            nextSprite = spriteList[5]; // Hull only
            if (engineLevel == 2)
            {
                nextSprite = spriteList[2]; // Engine and hull
                if (hasSensor)
                {
                    nextSprite = spriteList[1]; // Completely fixed*
                }
            }
            else
            {
                if (hasSensor)
                {
                    nextSprite = spriteList[7]; // Hull and sensor
                }
            }
        }

        if (nextSprite == currentSprite)
            return;

        // For now, just swapping sprites
        baseSprite.sprite = nextSprite;

        // Shader's busted, hoping to fix this still so I'm leaving it in
        /*
        if (nextSprite != null)
        {
            overlaySprite.sprite = nextSprite;
            overlaySprite.enabled = true;
            overlayAnimator.enabled = false;
            overlayAnimator.enabled = true;
            overlayAnimator.Play("player_overlay");
            StartCoroutine(FinishSpriteUpdate());
        }
        */
    }

    IEnumerator FinishSpriteUpdate()
    {
        yield return new WaitForSeconds(2.0f);
        baseSprite.sprite = overlaySprite.sprite;
        overlaySprite.enabled = false;
        overlayAnimator.StopPlayback();
    }

}
