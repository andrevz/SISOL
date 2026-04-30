namespace SISOL.Domain.ValueObjects;

public record class JobTitle
{
    public string Title { get; private set; }
    public string SeniorityLevel { get; private set; }

    private JobTitle(string title, string seniorityLevel)
    {
        Title = title;
        SeniorityLevel = seniorityLevel;
    }
}
