using CMS.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMS.Core.Repositories
{
    public class PageGroupService : IPageGroupService
    {
        private CmsContext _context;

        public PageGroupService(CmsContext context)
        {
            _context = context;
        }

        public IEnumerable<PageGroup> GetAllGroups()
        {
            return _context.PageGroups;
        }

        public PageGroup GetGroupById(int id)
        {
            return _context.PageGroups.Find(id);
        }

        public bool InsertGroup(PageGroup group)
        {
            try
            {
                _context.PageGroups.Add(group);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateGroup(PageGroup group)
        {
            try
            {
                _context.Entry(group).State = EntityState.Modified;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteGroup(PageGroup group)
        {
            try
            {
                _context.Entry(group).State = EntityState.Deleted;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteGroup(int id)
        {
            try
            {
                var group = GetGroupById(id);
                _context.Entry(group).State = EntityState.Deleted;

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
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
