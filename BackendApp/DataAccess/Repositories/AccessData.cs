using System;
using DataAccessInterface.Repositories;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public abstract class AccessData<T> : IAccessData<T> where T : class
    {
        protected IRepository<T> repository;
        public abstract void Validate(T element);
        public abstract void Update(T elementToUpdate, T element);
        public T Add(T element)
        {
            Validate(element);
            if (ExistElement(element))
            {
                throw new ArgumentException("This element already exist");
            }
            return repository.AddInContext(element);
        }

        public bool ExistElement(T element)
        {
            return repository.ExistInRepository(element);
        }
        public bool ExistElement(int id)
        {
            return repository.ExistInRepository(id);
        }

        public void Delete(T element)
        {
            if (!ExistElement(element))
            {
                throw new ArgumentException();
            }
            repository.Delete(element);
        }

        public void Delete(int id)
        {
            if (!ExistElement(id))
            {
                throw new ArgumentException("There's not element with id " + id);
            }
            repository.Delete(id);
        }

        public T Find(int id)
        {
            var elementFind = repository.FindInRepository(id);
            if (elementFind == null)
            {
                throw new ArgumentException("No element with id " + id);
            }
            return elementFind;
        }

        public void Update(int id, T element)
        {
            T elementFind = repository.FindInRepository(id);
            if (elementFind != null)
            {
                repository.UpdateInContext(elementFind);
            }
            else
            {
                throw new ArgumentException("Can't find element with id "+ id + " to update");
            }
        }

        public List<T> GetElements()
        {
            return repository.GetElementsInContext();
        }

    }
}