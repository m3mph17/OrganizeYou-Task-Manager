using OrganizeYou.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeYou.BLL.Interfaces
{
    public interface IStatusService
    {
        StatusDTO GetStatus(int? id);
        StatusDTO GetStatus(string name);
        IEnumerable<StatusDTO> GetStatuses();
        void Dispose();
    }
}
