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
        GameObject child;
        child = Instantiate(skill_table.skill[1].skill_effect,this.transform.position, Quaternion.identity);
        child.gameObject.GetComponent<BiteClass>().KamuSkill(controller.right, this.gameObject);
        //デクリメント処理
        this.gameObject.SetActive(false);
    }

    public void UltraSounds() 
    {
        GameObject ultrasoounds=Instantiate(skill_table.skill[2].skill_effect,this.transform.position,Quaternion.identity);
        ultrasoounds.transform.SetParent(this.transform);
        Destroy(ultrasoounds,2f);
    }

    public void SmallSlime() 
    {
        Instantiate(skill_table.skill[3].skill_effect,this.transform.position,Quaternion.identity);
        controller.SmallSlime();
    }

}
