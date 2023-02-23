using CMS.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace CMS.Core.Repositories
{
    public class PageCommentService : IPageCommentService
    {
        private CmsContext _context = new CmsContext();

        public IEnumerable<PageComment> GetAllComments()
        {
            return _context.PageComments;
        }

        public PageComment GetCommentById(int id)
        {
            return _context.PageComments.Find(id);
        }

        public bool InsertComment(PageComment comment)
        {
            try
            {
                _context.PageComments.Add(comment);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateComment(PageComment comment)
        {
            try
            {
                _context.Entry(comment).State = EntityState.Modified;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteComment(PageComment comment)
        {
            try
            {
                _context.Entry(comment).State = EntityState.Deleted;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteComment(int id)
        {
            try
            {
                var group = GetCommentById(id);
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
            _context.Dispose();
        }
    }
}
