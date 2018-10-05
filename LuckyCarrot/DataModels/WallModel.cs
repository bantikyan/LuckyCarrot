using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class WallModel
    {
        public List<PointTransferModel> PointTransfers { get; set; }
        public Dictionary<int, string> Users { get; set; }
        public Dictionary<int, string> Reasons { get; set; }

    }
}
