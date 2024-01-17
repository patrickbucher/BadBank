using System.Text.RegularExpressions;

namespace BadBank;

public class CloseTransaction : Transaction
{
    private static string PATTERN = "^close ([A-Za-z]+)$";

    public static Transaction Parse(string line)
    {
        var regex = new Regex(PATTERN);
        var match = regex.Match(line);
        if (!match.Success)
        {
            throw new ArgumentException($"{line} does not match {PATTERN}");
        }
        return new CloseTransaction(line, match.Groups[1].Value);
    }

    private string accountName;

    public CloseTransaction(string line, string accountName) : base(line)
    {
        this.accountName = accountName;
    }

    public override void Apply(IDictionary<string, double> accounts)
    {
        if (!accounts.ContainsKey(accountName))
        {
            throw new InvalidOperationException($"account {0} was never opened");
        }
        accounts.Remove(accountName);
    }
}