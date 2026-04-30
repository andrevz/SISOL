namespace SISOL.Domain.ValueObjects;

public record class IdentityDocument
{
    public string Type { get; private set; }
    public string Number { get; private set; }

    private IdentityDocument(string type, string number)
    {
        Type = type;
        Number = number;
    }
}
