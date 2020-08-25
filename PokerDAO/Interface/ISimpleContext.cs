using System;
using System.Collections.Generic;
using System.Text;

namespace PokerDAO.Interface
{
    public interface ISimpleContext<T>
    {
        string ToInsertQuery(T item);

        string ToInsertQuery(IList<T> itens);
    }
}
