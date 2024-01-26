using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;
    [SerializeField] Transform battleGround;
    public Pokemon pokemon { get; set; }

    public GameObject model;

    Animator anim;


    public void Setup()
    {
        pokemon = new Pokemon(_base, level);
        if (isPlayerUnit)
            //transform.rotation = Quaternion.Euler(new Vector3(0f, 30f, 0f));
            model = Instantiate(pokemon.Base.Model, battleGround.position, Quaternion.Euler(new Vector3(0f, 30f, 0f)), transform);

        else
            //transform.rotation = Quaternion.Euler(new Vector3(0f, 220f, 0f));
            model = Instantiate(pokemon.Base.Model, battleGround.position, Quaternion.Euler(new Vector3(0f, 220f, 0f)), transform);



        anim = model.GetComponent<Animator>();

        //PlayEnterAnimation();

    }

    public void  PlayEnterAnimation()
    {
        anim.SetTrigger("Enter");
    }

    

    public void PlayAttackAnimation()
    {
        anim.SetTrigger("Attack");
    }

    public void PlayHitAnimation()
    {
        anim.SetTrigger("Hit");
    }

    public void PlayFaintAnimation()
    {
        anim.SetBool("isFaint", true);
    }
    
}
