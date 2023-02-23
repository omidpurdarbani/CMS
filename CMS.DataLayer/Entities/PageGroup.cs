using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.DataLayer
{
    public class PageGroup
    {

        public PageGroup()
        {

        }

        [Key]
        public int GroupID { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string GroupTitle { get; set; }

        #region Relations

        public virtual List<Page> Pages { get; set; }

        #endregion

    }
}
