using System.Collections;
using System.Collections.Generic;
// using UnityEngine;
using static ColorCategory;

public class GameManager/* : MonoBehaviour*/
{
  // Q: こいつらグローバルでいいのか？
  private List<TraditionalColor> userPredict = new List<TraditionalColor>();
  private JudgeColor judge;
  private List<TraditionalColor> answer = new List<TraditionalColor>();
  private List<TraditionalColor> choiceColor = new List<TraditionalColor>();

  // Start is called before the first frame update
  public void Start()
  {
    // Console.WriteLine("start");
    //色のカテゴリを前のシーンから受け取る
    EColorCategory e = EColorCategory.red;
    var category = new ColorCategory(e);

    //カテゴリ内から色を6色ランダムに選択する
    choiceColor = category.RandomChoice(6);

    // Console.WriteLine("choice color");
    // foreach (var item in choiceColor)
    // {
    //   Console.Write($"{item.GetColorName()} ");
    // }
    // Console.WriteLine();
    //その中から答えを生成する
    answer = makeAnswer(choiceColor, 4);
    // Console.WriteLine("answer");
    // foreach (var item in answer)
    // {
    //   Console.Write($"{item.GetColorName()} ");
    // }
    // Console.WriteLine();
    // Q: makeAnserを直接入れてもいいのか？
    judge = new JudgeColor(answer);
  }

  // Update is called once per frame
  public void Update()
  {
    Console.WriteLine("色の名前が表示されるから，それに対応するカラーコードを当ててくれ");
    while (true)
    {
      for (int i = 0; i < answer.Count; i++)
      {
        if (i == answer.Count - 1)
        {
          Console.WriteLine($"{answer[i].GetColorName()}");
          break;
        }

        Console.Write($"{answer[i].GetColorName()} ");
      }
      for (int i = 0; i < choiceColor.Count; i++)
      {
        if (i == choiceColor.Count - 1)
        {
          Console.WriteLine($"{i}: {choiceColor[i].GetColorCode()}");
          break;
        }

        Console.Write($"{i}: {choiceColor[i].GetColorCode()} ");
      }

      //ユーザーが答えを当てる
      string predictString = Console.ReadLine();
      string[] predictNumber = predictString.Split(" ");

      // もしユーザーが予想の確定をしていないなら何もしない
      while (predictNumber.Length != answer.Count)
      {
        Console.WriteLine("予想する名前の個数分番号を空白で区切って入力してほしい");
        predictString = Console.ReadLine();
        predictNumber = predictString.Split(" ");
      }

      // continue? return?

      // ユーザーの入力の受け取り
      // TODO: userPredict = getUserPredict()的なやつ
      userPredict = new List<TraditionalColor>();
      for (int i = 0; i < predictNumber.Length; i++)
      {
        userPredict.Add(choiceColor[int.Parse(predictNumber[i])]);
      }

      // 正誤判定を行う
      // Judge.checkHitandBlow(入力);
      // 結果を返す
      List<int> result = judge.checkHitAndBlow(userPredict);

      // UIで表示
      Console.WriteLine($"HIT: {result[0]}, BLOW: {result[1]}");
      // 合っていたら終了
      if (result[0] == answer.Count)
      {
        break;
      }
      // Finish();
    }

  }

  private List<TraditionalColor> makeAnswer(List<TraditionalColor> choiceColor, int answerLength)
  {
    List<TraditionalColor> cColor = listDeepCopy(choiceColor);
    var rand = new System.Random();
    for (int i = cColor.Count - 1; i > 0; i--)
    {
      //乱数生成を使ってランダムに取り出す値を決める 
      //   int r = UnityEngine.Random.Range(0, i + 1);
      int r = rand.Next(0, i + 1);
      //取り出した値と箱の外の先頭の値を交換する
      var tmp = cColor[i];
      cColor[i] = cColor[r];
      cColor[r] = tmp;
    }

    return cColor.GetRange(0, answerLength);
  }

  private List<TraditionalColor> listDeepCopy(List<TraditionalColor> list)
  {
    List<TraditionalColor> newlist = new List<TraditionalColor>();
    for (int i = 0; i < list.Count; i++)
    {
      newlist.Add(list[i]);
    }

    Console.WriteLine("new list");
    foreach (var item in newlist)
    {
      Console.Write($"{item.GetColorName()} ");
    }
    Console.WriteLine();

    return newlist;
  }

}
