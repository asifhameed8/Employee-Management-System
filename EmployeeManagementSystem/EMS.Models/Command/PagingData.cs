using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models.Command
{
    public class PagingData
    {
        public int CurrentPage { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagingEnabled { get; set; }


        public PagingData(int? pageid, int? PageSize)
        {
            IsPagingEnabled = true;
            Take = PageSize == null ? 20 : (int)PageSize;
            CurrentPage = pageid == null ? 0 : (int)pageid;
            Skip = (Take * (CurrentPage));
        }
    }
}
