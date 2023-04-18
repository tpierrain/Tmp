using System.Collections.Generic;

namespace TrainTrain.Dal
{
    public interface IRepositoryEntity<T>
    {
        T Get(string id);
        List<T> GetAll();
        void Save(T entity);
        void SaveAll(T[] entities);
        void Remove(string id);
        void RemoveAll();
    }
}