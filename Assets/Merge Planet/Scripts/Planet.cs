using UnityEngine;
using System;
using UnityEngine.Events;

public class Planet : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [Header(" Datas ")]
    [SerializeField] private PlanetType type;
    private bool hasCollided;
    private bool canBeMerged;

    [Header(" Actions ")]
    public static UnityAction<Planet, Planet> onCollisionWithPlanet;
    public static UnityAction<Planet> onCollisionWithRainbow;
    public static UnityAction onCollisionWithBomb;

    void Start()
    {
        //Invoke("AllowMerge", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AllowMerge()
    {
        canBeMerged = true;
    }

    public void EnablePhysics()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    public void MoveTo(Vector2 targetPosition)
    {
        transform.position = targetPosition;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        ManageCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ManageCollision(collision);
    }

    private void ManageCollision(Collision2D collision)
    {
        hasCollided = true;

        //if (!canBeMerged)
        //    return;

        if (collision.collider.TryGetComponent(out Planet planent))
        {

            //if (!planent.CanBeMerged())
            //    return;
            if (planent.GetPlanetType() != type)
                return;

            onCollisionWithPlanet?.Invoke(this, planent);
        }
    }

    public void Merge()
    {
        Destroy(gameObject);
    }

    public  PlanetType GetPlanetType() 
    { 
        return type; 
    }

    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }

    public bool HasCollided()
    {
        return hasCollided;
    }

    public bool CanBeMerged()
    {
        return canBeMerged;
    }
}
