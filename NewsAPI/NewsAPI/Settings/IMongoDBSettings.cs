namespace NewsAPI.Settings
{
    public interface IMongoDBSettings
    {
        string AnnouncementsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

}
