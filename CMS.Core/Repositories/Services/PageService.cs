using CMS.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMS.Core.Repositories
{
    public class PageService : IPageService
    {
        private CmsContext _context = new CmsContext();

        public IEnumerable<Page> GetAllPages()
        {
            return _context.Pages;
        }

        public Page GetPageById(int id)
        {
            return _context.Pages.Find(id);
        }

        public bool InsertPage(Page page)
        {
            try
            {
                _context.Pages.Add(page);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePage(Page page)
        {
            try
            {
                _context.Entry(page).State = EntityState.Modified;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePage(Page page)
        {
            try
            {
                _context.Entry(page).State = EntityState.Deleted;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePage(int id)
        {
            try
            {
                var page = GetPageById(id);
                _context.Entry(page).State = EntityState.Deleted;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
