using UnityEngine;

public class AWPMechanic : MonoBehaviour
{
    [SerializeField] GameRules gameRules;
    [SerializeField] Transform Crosshair;
    [SerializeField] GameObject WeaponMode;

    void Update()
    {
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(Crosshair.localPosition);
        transform.LookAt(currentPos);
    }

    
    public void SoundOnAWP(AudioClip audioClip)
    {
        TriggerDestroyter.SpawnSound(audioClip);
    }
    public void OnCrossHairOnningAWP(AudioClip audioClip)
    {
        TriggerDestroyter.SpawnSound(audioClip);
        gameRules.SetFire(true);
    }
    public void OnFireAWP()
    {
       gameRules.SetFire(true);
    }
    public void OnReloadAWP()
    {
        gameRules.ReloadAWP();
    }
    public void OffAWP()
    {
        WeaponMode.SetActive(false);
    }

    
}
