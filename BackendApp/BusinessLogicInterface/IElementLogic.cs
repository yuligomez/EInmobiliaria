using System.Collections.Generic;
using Domain.Entities;

namespace BusinessLogicInterface
{
    public interface IElementLogic
    {
        IEnumerable<Element> GetAll();
        Element GetById(int id);
        Element Add(Element element);
        Element Update(int id, Element element);
        void Delete(int id);
        bool Exist(Element element);
    }
}