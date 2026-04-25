using System;
using System.Text;
using System.Text.RegularExpressions;
using xFunc.Maths;

namespace TRPGDice
{
  public static partial class TRPGDice
  {
    [GeneratedRegex(@"\d+d\d+")]
    private static partial Regex NdM();

    /// <summary>
    /// `"1d6 + 3"`のような表記に従って、ランダムな結果を返します
    /// </summary>
    /// <seealso cref="https://sys27.github.io/xFunc/articles/supported-functions-and-operations.html"/>
    /// <param name="equation">`"1d6 + 3"のように表記された文字列`</param>
    /// <returns>計算した結果</returns>
    public static double Roll(string equation)
    {
      // "1d100"のように表記された部分を先に処理する
      string rolled = NdM().Replace(equation, new MatchEvaluator(
        num => { return RollNdM(num.ToString()).ToString(); })
        );

      // この時点でただの定数ならば、xFuncの処理を行わずにreturnする

      if (int.TryParse(equation, out int num))
      {
        return num;
      }

      // xFuncを使うために必要な変数
      var processor = new Processor();

      // Rollされた式を計算して返す
      try
      {
        return processor.Solve(rolled).Number.Number;
      }
      catch (Exception) {
        throw new ArgumentException("""
          計算式の処理に失敗しました。計算式が正しく表記されているか確認してください。

          使用可能な式については、以下のサイトを参照してください。
          https://sys27.github.io/xFunc/articles/supported-functions-and-operations.html
          """);
      }
    }
    /// <summary>
    /// `"1d100"`のような形式に従ってランダムな結果を返します
    /// </summary>
    /// <param name="NdM">`"1d000"`のように表記された文字列</param>
    /// <returns>サイコロを振った結果</returns>
    public static int RollNdM(string NdM)
    {
      // "1d100"のように表された文字列をdの前後で分割し、
      // countとsurfaceにそれぞれ格納する
      string[] split = NdM.Split('d');

      int count = int.Parse(split[0]);
      int surface = int.Parse(split[1]);

      int sum = 0;

      // countの回数だけ 1 <= X <= surface の範囲で加算を繰り返す
      for (int i = 0; i < count; i++) {
        sum += rand.Next(1, 1 + surface);
      }

      return sum;
    }
   static readonly Random rand = new();
  }
}
