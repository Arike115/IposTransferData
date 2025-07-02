using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IposTransferData.Enum
 {    public class BusinessType
    { 
        public string Title { get; set; }
        public string Description { get; set; }
        public BusinessCategory BusinessCategory { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ThumbnailFileName { get; set; }
        public long ThumbnailFileSize { get; set; }
        public string ThumbnailOriginalFileName { get; set; }
        public string BannerUrl { get; set; }
        public string BannerFileName { get; set; }
        public long BannerFileSize { get; set; }
        public string BannerOriginalFileName { get; set; }
        public string Country { get; set; }
    }
    public enum BusinessCategory
    {
        //DEFAULT
        RETAIL
    }
}
