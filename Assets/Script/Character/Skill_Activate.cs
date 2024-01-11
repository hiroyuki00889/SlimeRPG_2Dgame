using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Activate : MonoBehaviour
{
    private PlayerController controller;
    private EnemyTagCounter enemyTagCounter;
    public Skill_Table skill_table;
    public float impulse;

    private void Start()
    {
        controller= GetComponent<PlayerController>();
        enemyTagCounter=GetComponent<EnemyTagCounter>();
    }

    public void Bunny()
    {
        // スキル発動前にデクリメント処理
        enemyTagCounter.DecrementCounter("Bunny");
        Instantiate(skill_table.skill[0].skill_effect, this.transform.position - new Vector3(0, -1, 0), Quaternion.identity);
        controller.rb.AddForce(new Vector3(0, 65, 0), ForceMode2D.Impulse);
    }

    public void Bite()
    {
        // スキル発動前にデクリメント処理
        enemyTagCounter.DecrementCounter("Dog");
        GameObject child;
        child = Instantiate(skill_table.skill[1].skill_effect,this.transform.position, Quaternion.identity);
        child.gameObject.GetComponent<BiteClass>().KamuSkill(controller.right, this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void UltraSounds() 
    {
        // スキル発動前にデクリメント処理
        enemyTagCounter.DecrementCounter("Bat");
        GameObject ultrasoounds=Instantiate(skill_table.skill[2].skill_effect,this.transform.position,Quaternion.identity);
        ultrasoounds.transform.SetParent(this.transform);
        Destroy(ultrasoounds,2f);
    }

    public void SmallSlime() 
    {
        Instantiate(skill_table.skill[3].skill_effect,this.transform.position,Quaternion.identity);
        controller.SmallSlime();
    }

    public void Fire()
    {
        // スキル発動前にデクリメント処理
        enemyTagCounter.DecrementCounter("Dino");
        GameObject fire = Instantiate(skill_table.skill[4].skill_effect, this.transform.position, Quaternion.identity);
        fire.transform.SetParent(this.transform);
        Destroy(fire, 2f);
    }

    public void PigImpact()
    {
        StartCoroutine(Impact());
    }


    private IEnumerator Impact()
    {
        //下に力かける処理
        yield return new WaitUntil(() => controller.rb.velocity.y == 0);
        float i = impulse;
        GameObject impact1 = Instantiate(skill_table.skill[5].skill_effect, new Vector2(this.transform.position.x - 1.5f, this.transform.position.y), Quaternion.identity);
        GameObject impact2 = Instantiate(skill_table.skill[5].skill_effect, new Vector2(this.transform.position.x + 1.5f, this.transform.position.y), Quaternion.identity);
        impact1.tag = "PlayerAttack";
        impact2.tag = "PlayerAttack";
        if (i > -1)
        {

        }
        else if (i > -3)
        {
            impact1.transform.localScale *=1.2f;
            impact2.transform.localScale *=1.2f;
        }
        else if (i > -6)
        {
            impact1.transform.localScale *= 1.4f;
            impact2.transform.localScale *= 1.4f;
        }
        else if (i > -9)
        {
            impact1.transform.localScale *= 1.6f;
            impact2.transform.localScale *= 1.6f;
        }
        else if (i > -12)
        {
            impact1.transform.localScale *= 1.8f;
            impact2.transform.localScale *= 1.8f;
        }
        else if (i > -16)
        {
            impact1.transform.localScale *= 2f;
            impact2.transform.localScale *= 2f;
        }
    }
}
