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

    public void Rabbit()
    {
        Instantiate(skill_table.skill[0].skill_effect, this.transform.position - new Vector3(0, -1, 0), Quaternion.identity);
        controller.rb.AddForce(new Vector3(0, 200, 0), ForceMode2D.Impulse);
    }

}
