using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Shared.CommonType
{
    public class ImageStatus
    {
        public bool IsModified { get; set; }
        public IEnumerable<int> ImageIdModifiled { get; set; }
        public ModifyType ModifyType { get; set; }
    }
}
