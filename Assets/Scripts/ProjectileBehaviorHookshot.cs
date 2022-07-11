using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviorHookshot : MonoBehaviour
{
    private bool hooked = false;
    private Vector3 direction;
    [SerializeField] private float speed;
    [SerializeField] private float dragSpeed;
    [SerializeField] private float maxDistance;
    private float distanceTravelled;

    private GameObject player;
    private GameObject hookedObject;
    [SerializeField] LineController hookshotRopePrefab;
    private LineController currentHookshotRope;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        /*TODO: If other characters than the player ever need to use hookshot
                or multiple players are present, another way of initializing
                projectile source(currently "GameObject player" variable) is 
                needed.*/
        this.player = GameObject.FindWithTag("Player");
        transform.parent = null;
        currentHookshotRope = Instantiate(hookshotRopePrefab);
        currentHookshotRope.setLineSource(this.transform);
        currentHookshotRope.setLineTarget(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void FixedUpdate()
    {
        moveSourceObject();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*TODO: In case another tag is ever needed for a hookable object(example:
                multiplayer with hookable players due to "Player" tag). use a
                different method of determining if an object is hookable or use
                a workaround to assign multiple tags, e.g. appending empty
                GameObjects as child with different tag*/
        if (other.tag == "Hookable")
        {
            hooked = true;
        }
    }

    private void move()
    {
        //Only move until target object is hit
        /*TODO: If hookable, non-stationary objects are introduced, this hook needs
                to move with the object + new method for moving the target object
                needs to be added*/
        if (!hooked)
        {
            transform.position += direction * Time.deltaTime;
            distanceTravelled += direction.magnitude;
            if (distanceTravelled > maxDistance)
            {
                destroyThis();
            }
        }
    }

    private void moveSourceObject()
    {
        //Only move source once this hook actually has another object to pull the
        //source object towards
        if (hooked)
        {
            direction = transform.position - player.transform.position;
            direction.z = 0;
            direction.Normalize();
            player.GetComponent<Rigidbody2D>().AddForce(direction * dragSpeed * Time.fixedDeltaTime);
        }
    }

    public void destroyThis()
    {
        player.GetComponent<PlayerController>().resetIsHookshotTravelling();
        Destroy(currentHookshotRope.gameObject);
        Destroy(gameObject);
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction * speed;
    }
}
