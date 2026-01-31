using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gametime;
    public float maxgametime = 2 * 10f;
    public PoolManager pool;
    public Player player;

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        gametime += Time.deltaTime;

        if (gametime > maxgametime) {
            gametime = maxgametime;
        }
    }
}
