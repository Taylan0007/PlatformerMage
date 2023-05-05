using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Extensions;
using Unity.VisualScripting;
using Object = System.Object;

public class Spell : MonoBehaviour
{
    public float FireBallSpeed = 8f;

    public GameObject FireBallSpell;
    public PlayerMovement PlayerCharacter;

    private void OnEnable()
    {
        Signals.Instance.OnSkillUse += CastSpell;
    }

    public void CastSpell(string spell)
    {

        switch (spell)
        {
            case "FireBall":
                {
                    GameObject FireBall = Instantiate(FireBallSpell, PlayerCharacter.transform);
                    Rigidbody2D FireBallRB = FireBall.GetComponent<Rigidbody2D>();


                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 shootDirection = mousePosition - (Vector2)PlayerCharacter.transform.position;
                    shootDirection.Normalize();
                    FireBallRB.velocity = shootDirection * FireBallSpeed;

                    break;
                }

        }
    }

}
