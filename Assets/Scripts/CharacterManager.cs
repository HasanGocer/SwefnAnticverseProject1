using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoSingleton<CharacterManager>
{
    Coroutine healthCorotine;
    [SerializeField] GameObject character;
    [SerializeField] int characterReHealthCountdownContdown;
    [SerializeField] int health;
    [SerializeField] AnimController animController;
    [SerializeField] CharacterFight characterFight;
    bool systemReset = false;

    public void HeathReHealth() { health = ItemData.Instance.field.characterHealth; }
    public void StartCharacterManager() { health = ItemData.Instance.field.characterHealth; }
    public CharacterFight CharacterFight() { return characterFight; }
    public int GetCharacterHealth() { return health; }
    public GameObject GetCharacter() { return character; }
    public AnimController GetAnimController() { return animController; }

    public void DownHealth(int tempHealth)
    {
        health -= tempHealth;
        ChechHealth();
        if (!systemReset)
            healthCorotine = StartCoroutine(HealthUp());
        else
        {
            StopCoroutine(healthCorotine);
            healthCorotine = StartCoroutine(HealthUp());
        }

    }

    private void ChechHealth()
    {
        if (health <= 0)
        {
            //oyun yeniden baþlat
        }
    }

    IEnumerator HealthUp()
    {
        systemReset = true;
        yield return new WaitForSeconds(characterReHealthCountdownContdown);
        HeathReHealth();
        systemReset = false;
    }
}
