using CMS.DataLayer;
using System;
using System.Collections.Generic;

namespace CMS.Core.Repositories
{
    public interface IPageGroupService : IDisposable
    {

        IEnumerable<PageGroup> GetAllGroups();

        PageGroup GetGroupById(int id);

        bool InsertGroup(PageGroup group);

        bool UpdateGroup(PageGroup group);

        bool DeleteGroup(PageGroup group);

        bool DeleteGroup(int id);

        void Save();

    }
}
