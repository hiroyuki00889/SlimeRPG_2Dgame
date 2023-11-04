using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterDisplay : MonoBehaviour
{
    public EnemyTagCounter enemyTagCounter;
    public Text counterTextBunny;
    public Text counterTextDog;
    public Text counterTextBat;
    public Button Bunny;
    public Button Dog;
    public Button Bat;



    private void Update()
    {
        // カウンターの情報を取得
        Dictionary<string, int> counters = enemyTagCounter.GetAllCounters();

        // カウンターの値をUIテキストに表示
        foreach (var kvp in counters)
        {
            if (kvp.Key == "Bunny")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // 画像を非表示
                    Bunny.interactable = false;
                    counterTextBunny.gameObject.SetActive(false);  // テキストを非表示
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // 画像を表示
                    Bunny.interactable = true;
                    counterTextBunny.gameObject.SetActive(true);   // テキストを表示

                    // テキストに数値を表示
                    counterTextBunny.text = kvp.Value.ToString();
                }
                // counterTextA.text = $"EnemyTagA: {kvp.Value}";
            }
            else if (kvp.Key == "Dog")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // 画像を非表示
                    Dog.interactable = false;
                    counterTextDog.gameObject.SetActive(false);  // テキストを非表示
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // 画像を表示
                    Dog.interactable = true;
                    counterTextDog.gameObject.SetActive(true);   // テキストを表示

                    // テキストに数値を表示
                    counterTextDog.text = kvp.Value.ToString();
                }
                // counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            else if (kvp.Key == "Bat")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // 画像を非表示
                    Bat.interactable = false;
                    counterTextBat.gameObject.SetActive(false);  // テキストを非表示
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // 画像を表示
                    Bat.interactable = true;
                    counterTextBat.gameObject.SetActive(true);   // テキストを表示

                    // テキストに数値を表示
                    counterTextBat.text = kvp.Value.ToString();
                }
                // counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            // 他のタグに対する表示も追加できます
        }
    }
}





