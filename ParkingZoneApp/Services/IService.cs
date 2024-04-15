namespace ParkingZoneApp.Services
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int? Id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
