using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : NetworkBehaviour {

    public LayerMask movementMask;
    public Interactable focus;

    public Camera cam;
    PlayerMotor motor;

	// Use this for initialization
	void Start () {

        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }
        else
        {
            cam.enabled = true;
        }
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }
        else
        {
            cam.enabled = true;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

		if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Debug.Log("hit" + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);

                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                 Interactable interactable = hit.collider.GetComponent<Interactable> ();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform); 
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
}
