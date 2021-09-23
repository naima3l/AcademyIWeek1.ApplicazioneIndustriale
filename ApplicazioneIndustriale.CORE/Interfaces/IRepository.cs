using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicazioneIndustriale.CORE.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetItemsWithOutState();
        void Update(T item);
        void Add(T item);
    }
}
