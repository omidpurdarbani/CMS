using CMS.DataLayer;
using System;
using System.Collections.Generic;

namespace CMS.Core.Repositories
{
    public interface IPageService : IDisposable
    {
        IEnumerable<Page> GetAllPages(bool paging = false, int skip = 1, int take = 10);

        Page GetPageById(int id);

        bool InsertPage(Page page);

        bool UpdatePage(Page page);

        bool DeletePage(Page page);

        bool DeletePage(int id);

        void Save();

        IEnumerable<Page> GetTopPages(int take = 4);
        IEnumerable<Page> PageForSlider();
        IEnumerable<Page> LastPages();
    }
}
