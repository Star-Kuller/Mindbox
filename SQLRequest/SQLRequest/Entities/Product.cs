using SQLRequest.Interfaces;

namespace SQLRequest.Entities;

public class Product : Entity, IName
{
    public string Name { get; set; } = "";
}