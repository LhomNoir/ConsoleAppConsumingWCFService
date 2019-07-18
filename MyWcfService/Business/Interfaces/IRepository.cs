﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWcfService.Business.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetByID(int id);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
