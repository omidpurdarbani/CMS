using CMS.DataLayer;
using System;
using System.Collections.Generic;

namespace CMS.Core.Repositories
{
    public interface IPageCommentService : IDisposable
    {
        IEnumerable<PageComment> GetAllComments();

        PageComment GetCommentById(int id);

        bool InsertComment(PageComment comment);

        bool UpdateComment(PageComment comment);

        bool DeleteComment(PageComment comment);

        bool DeleteComment(int id);

        void Save();
    }
}
