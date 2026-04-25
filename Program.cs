using static TRPGDice.TRPGDice;

namespace TRPGDice;

internal class Program
{
  public static void Main(string[] args)
  {
    if (args.Length == 0)
    {
      Console.WriteLine(HELP);
      return;
    }

    args[0] = args[0].ToLower();

    if (args[0] == "-h" | args[0] == "-v" | args[0] == "--help" | args[0] == "--version")
    {
      Console.WriteLine(HELP);
      return;
    }

    if (args.Length == 1)
    {
      Console.WriteLine(Roll(args[0]));
      return;
    }

    else
    {
      throw new ArgumentException("""
        コマンドの使い方が誤っています。

        正しい使い方を確認したい場合は、以下のコマンドをお試しください。

        dice -h
        """);
    }
  }


  const string HELP = """
      TRPGDice [Version 1.0.0]
      
      使い方: dice [計算式]

      例
      ----------------

      dice "2d6"          => 6面ダイスを2回振った結果を出力
      dice "1d100"        => 100面ダイスを1回振った結果を出力
      dice "1d6 + 3"      => 6面ダイスの結果に3を加えた値を出力
      dice "abs(1d5 - 2)" => 5面ダイスの結果から2を引き、その絶対値を取った値を出力

      diceの使い方については、以下を参照してください。
      https://github.com/Bismuth083/TRPGDice

      計算式に使用可能な要素については、以下を参照してください。
      https://sys27.github.io/xFunc/articles/supported-functions-and-operations.html
      """;
}

