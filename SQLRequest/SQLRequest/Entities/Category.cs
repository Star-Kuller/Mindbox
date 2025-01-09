using SQLRequest.Interfaces;

namespace SQLRequest.Entities;

public class Category : Entity, IName
{
    public string Name { get; set; } = "";
}