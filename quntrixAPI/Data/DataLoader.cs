namespace quntrixAPI.Data
{
    public abstract class DataLoader
    {
        public abstract Task LoadDataAsync(Stream fileStream, AppDbContext db);
    }
}
