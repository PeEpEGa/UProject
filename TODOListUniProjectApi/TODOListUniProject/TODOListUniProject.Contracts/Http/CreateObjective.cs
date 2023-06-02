namespace TODOListUniProject.Contracts.Http;

public class CreateObjectiveRequest
{
    public string Title { get; init; }
}

public class CreateObjectiveResponse
{
    public int ObjectiveId { get; set; }
}