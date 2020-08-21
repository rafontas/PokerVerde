using System;
using System.Collections.Generic;
using System.Text;

namespace Comum.Interfaces
{
    public interface IToJson
    {
        string ToJson();
    }
    public interface IFromJson<T>
    {
        T FromJson(string Json);
    }
}
