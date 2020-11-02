using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerDAO.Interface
{
    public interface ISimpleContext<T> : IQueryable<T>
    {
        void insertQuery(T item);

        void insertQuery(IList<T> itens);

        T GetById();

    }
}
