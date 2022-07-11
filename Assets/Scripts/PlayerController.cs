using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement2D playerMovement;
    [SerializeField] private ProjectileBehaviorHookshot hookshotPrefab;
    private ProjectileBehaviorHookshot currentHookshot;

    private float horizontalInputDirection;
    
    private bool isHookshotTravelling = false;
    private bool wantsToJump = false;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        basicMovementInput();
        hookshotInput();
    }

    private void FixedUpdate()
    {
        //Pass on inputs to player movement
        playerMovement.move(horizontalInputDirection, wantsToJump);
        wantsToJump = false;
    }

    //Only reference in Update() to avoid eating inputs
    private void basicMovementInput()
    {
        horizontalInputDirection = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump"))
            wantsToJump = true;
    }

    //Only reference in Update() to avoid eating inputs
    private void hookshotInput()
    {
        //Fire hookshot projectile
        if (Input.GetButtonDown("Fire2") && !isHookshotTravelling)
        {
            isHookshotTravelling = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 hookDirection = mousePos - transform.position;
            hookDirection.z = 0;
            hookDirection.Normalize();
            currentHookshot = Instantiate(hookshotPrefab);
            currentHookshot.transform.position = transform.position;
            currentHookshot.setDirection(hookDirection);
        }
        //Lets player stop hookshot by themself
        else if (Input.GetButtonUp("Fire2") && isHookshotTravelling)
        {
            currentHookshot.destroyThis();
        }
    }

    /*For access to private variable isHookshotTravelling when projectile wants to
    destroy itself*/
    public void resetIsHookshotTravelling()
    {
        isHookshotTravelling = false;
    }
}
