using CMS.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CMS.Core.Repositories
{
    public class PageService : IPageService
    {
        private CmsContext _context;

        public PageService(CmsContext context)
        {
            _context = context;
        }

        public IEnumerable<Page> GetAllPages()
        {
            return _context.Pages;
        }

        public Page GetPageById(int id)
        {
            return _context.Pages.Include(p => p.PageGroup).SingleOrDefault(p => p.PageID == id);
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
                _context.Entry(GetPageById(page.PageID)).State = EntityState.Detached;
                Save();
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

        public IEnumerable<Page> GetTopPages(int take = 4)
        {
            return _context.Pages.OrderByDescending(p => p.Visit).Take(take);
        }

        public IEnumerable<Page> PageForSlider()
        {
            return _context.Pages.Where(p => p.ShowInSlider == true);
        }

        public IEnumerable<Page> LastPages()
        {
            return _context.Pages.OrderByDescending(p => p.CreateDate).Take(4);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
