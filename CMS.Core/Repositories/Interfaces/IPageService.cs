using CMS.DataLayer;
using System;
using System.Collections.Generic;

namespace CMS.Core.Repositories
{
    public interface IPageService : IDisposable
    {
        IEnumerable<Page> GetAllPages();

        Page GetPageById(int id);

        bool InsertPage(Page page);

        bool UpdatePage(Page page);

        bool DeletePage(Page page);

        bool DeletePage(int id);

        void Save();
    }
}
