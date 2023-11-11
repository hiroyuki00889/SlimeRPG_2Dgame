using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Activate : MonoBehaviour
{
    private PlayerController controller;
    private EnemyTagCounter enemyTagCounter;
    public Skill_Table skill_table;

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
        controller.rb.AddForce(new Vector3(0, 100, 0), ForceMode2D.Impulse);
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

}
