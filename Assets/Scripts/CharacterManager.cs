using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoSingleton<CharacterManager>
{
    [SerializeField] GameObject character;
    [SerializeField] int health;
    [SerializeField] AnimController animController;
    [SerializeField] CharacterFight characterFight;

    public void StartCharacterManager() { health = ItemData.Instance.field.characterHealth; }
    public CharacterFight CharacterFight() { return characterFight; }
    public int GetCharacterHealth() { return health; }
    public void DownHealth(int tempHealth) { health -= tempHealth; }
    public GameObject GetCharacter() { return character; }
    public AnimController GetAnimController() { return animController; }

    private void ChechHealth()
    {
        if (health <= 0)
        {
            //oyun yeniden baþlat
        }
    }
}
