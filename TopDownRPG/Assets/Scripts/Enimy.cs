using UnityEditor.SceneManagement;
using UnityEngine;

public class Enimy : MonoBehaviour
{
    public int health = 5;
    private PlayerController _player;
    private Animator _anim;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _anim = GetComponent<Animator>();
    }

    public int Health
    {
        set
        {
            health = value;
            if(health <= 0)
                _anim.SetTrigger("slimeDead");
        }
        get
        {
            return health;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("sword"))
        {
            Health -= _player.damage;
            _anim.SetTrigger("slimeDamage");
        }
    }

    void RemoveObject()
    {
        Destroy(gameObject);
    }
}
