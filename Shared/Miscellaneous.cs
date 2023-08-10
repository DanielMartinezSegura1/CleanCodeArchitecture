using System.Text;

namespace Shared;

public static class Miscellaneous
{
    public static string CodeGenerator(int length)
    {
        var sb = new StringBuilder();

        var charsAccepted = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        for (var i = 0; i <= length - 1; i++) sb.Append(charsAccepted[Random.Shared.Next(0, charsAccepted.Length)]);

        return sb.ToString();
    }
}