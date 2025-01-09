namespace SQLRequest.Interfaces;

public interface IRepository
{
    void SetupDatabase(string databaseName); 
    void SetupSeedData();
}