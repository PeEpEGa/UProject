using TODOListUniProject.Contracts.Database;

namespace TODOListUniProject.Contracts.Http;

public class ObjectiveResponse
{
    public string Message { get; set; }
}

public class ObjectivesResponse
{
    public List<Objective> Objectives { get; set; }
}