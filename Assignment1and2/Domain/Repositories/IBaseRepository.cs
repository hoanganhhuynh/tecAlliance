using System;
using System.Collections.Immutable;
using Domain.Entities;

namespace Domain.Repositories
{
	public interface IBaseRepository<T> where T : class
    {
        List<EmployeeEntity> GetAll();
        T? GetById(Guid id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}